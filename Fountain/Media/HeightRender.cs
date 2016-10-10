/* Fountain - Map painting/generating software for worldbuilders. Copyright (C) 2016 Evan Llewellyn Price
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;

using LlewellynScripting;
using LlewellynMedia;
using LlewellynMath;
using LlewellynMath.NoiseGenerators;

namespace Fountain.Media
{
	public class HeightRender
	{
		private HeightField heightField;
		public HeightField HeightField
		{
			get
			{
				return heightField;
			}
		}
		private Bitmap bitmap;
		public Bitmap Bitmap
		{
			get
			{
				return bitmap;
			}
		}
		public int Width
		{
			get
			{
				return bitmap.Width;
			}
		}
		public int Height
		{
			get
			{
				return bitmap.Height;
			}
		}
		public float AspectRatio
		{
			get
			{
				return (float)Width / Height;
			}
		}

		public HeightRender(int width, int height, bool clamp = false, float clampMin = 0, float clampMax = 1, bool wrapX = false, bool wrapY = false)
		{
			heightField = new HeightField(width, height, clamp, clampMin, clampMax, wrapX, wrapY);
			bitmap = new Bitmap(width, height);
		}

		public void Clear(float clearValue = 0)
		{
			for (int x = 0; x < heightField.Width; x++)
				for (int y = 0; y < heightField.Height; y++)
					heightField.TrySetHeight(x, y, clearValue);
		}
		public void UpdateAll(PhotonGradient gradient, IEnumerable<Effect> effects)
		{
			UpdateArea(0, 0, Width, Height, gradient, effects);
		}
		public void UpdateArea(int x, int y, int width, int height, PhotonGradient gradient, IEnumerable<Effect> effects)
		{
			if (gradient == null)
			{
				gradient = new PhotonGradient(PhotonInterpolationMode.Linear);
				gradient.Add(new Photon(0.0f, 0.0f, 0.0f), heightField.ClampMin);
				gradient.Add(new Photon(1.0f, 1.0f, 1.0f), heightField.ClampMax);
			}
			if (x >= 0 && width > 0 && x + width <= Width && y >= 0 && height > 0 && y + height <= Height)
			{
				BitmapData data = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
				try
				{
					unsafe
					{
						byte* start = (byte*)data.Scan0.ToPointer();
						for (int _x = x; _x < x + width; _x++)
						{
							for (int _y = y; _y < y + height; _y++)
							{
								Photon p = gradient[heightField[_x, _y]];

								if (effects != null)
									foreach (Effect e in effects)
										if (e != null)
											p = e(_x, _y, p, heightField);

								byte* pix = start + _y * Width * 4 + _x * 4;
								*pix = p.b;
								pix++;
								*pix = p.g;
								pix++;
								*pix = p.r;
								pix++;
								*pix = p.a;
							}
						}
					}
				}
				catch (Exception e)
				{
					throw new Exception(e.Message);
				}
				finally
				{
					bitmap.UnlockBits(data);
				}
			}
			else throw new Exception("The supplied region did not lie within the height render.");
		}

		public static EffectCompileResult CompileEffect(CSScript script, out Effect effect, out string errors)
		{
			if (script != null)
			{
				script.RequiredTypes.Clear();
				script.RequiredTypes.Add(typeof(Color));
				script.RequiredTypes.Add(typeof(HeightField));
				script.RequiredTypes.Add(typeof(Photon));
				script.RequiredTypes.Add(typeof(Numerics));
				script.RequiredTypes.Add(typeof(Math));
				script.RequiredTypes.Add(typeof(PerlinNoise));

				if (script.Compile(out errors))
				{
					MethodInfo applyInfo;
					if (script.TryGetMember<MethodInfo>("Apply", out applyInfo))
					{
						try
						{
							effect = (HeightRender.Effect)applyInfo.CreateDelegate(typeof(HeightRender.Effect), script.ScriptObject);
						}
						catch
						{
							effect = null;
							return EffectCompileResult.WrongApplySignature;
						}
					}
					else
					{
						effect = null;
						return EffectCompileResult.MissingApplyFunction;
					}
					return EffectCompileResult.Success;
				}
				else
				{
					effect = null;
					return EffectCompileResult.SyntaxError;
				}
			}
			else throw new Exception("The supplied script was null.");
		}
		public static GeneratorCompileResult CompileGenerator(CSScript script, out Generator generator, out string errors)
		{
			if (script != null)
			{
				script.RequiredTypes.Clear();
				script.RequiredTypes.Add(typeof(HeightField));
				script.RequiredTypes.Add(typeof(Numerics));
				script.RequiredTypes.Add(typeof(Math));
				script.RequiredTypes.Add(typeof(PerlinNoise));

				if (script.Compile(out errors))
				{
					MethodInfo applyInfo;
					if (script.TryGetMember<MethodInfo>("Generate", out applyInfo))
					{
						try
						{
							generator = (HeightRender.Generator)applyInfo.CreateDelegate(typeof(HeightRender.Generator), script.ScriptObject);
						}
						catch
						{
							generator = null;
							return GeneratorCompileResult.WrongGenerateSignature;
						}
					}
					else
					{
						generator = null;
						return GeneratorCompileResult.MissingGenerateFunction;
					}
					return GeneratorCompileResult.Success;
				}
				else
				{
					generator = null;
					return GeneratorCompileResult.SyntaxError;
				}
			}
			else throw new Exception("The supplied script was null.");
		}

		public enum GeneratorCompileResult { Success, SyntaxError, MissingGenerateFunction, WrongGenerateSignature }
		public enum EffectCompileResult { Success, SyntaxError, MissingApplyFunction, WrongApplySignature }

		public delegate Photon Effect(int x, int y, Photon color, HeightField heightField);
		public delegate double Generator(int x, int y, HeightField heightField);
	}
}
