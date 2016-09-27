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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LlewellynMedia;
using LlewellynMath;

namespace Fountain.Controls
{
	[DesignerAttribute("System.Windows.Forms.Design.ParentControlDesigner, System.Design")]
	public partial class GradientBox : UserControl
	{
		private PhotonGradient gradient;
		public PhotonGradient Gradient
		{
			get
			{
				return gradient;
			}
			set
			{
				gradient = value;
				UpdateRender();
			}
		}
		private Bitmap render;
		public int Resolution
		{
			get
			{
				return render.Width;
			}
			set
			{
				render = new Bitmap(Numerics.Max(value, 100), 1);
				UpdateRender();
			}
		}
		private Pen linePen = new Pen(Color.FromArgb(255, 255, 255), 2);

		public GradientBox()
		{
			BorderStyle = BorderStyle.FixedSingle;
			SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
			Resolution = 512;
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
			if (Gradient != null)
			{
				pe.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
				pe.Graphics.DrawImage(render, new Rectangle(0, 0, Width, Height * 2));
				for (int i = 0; i < gradient.PhotonPositionCount; i++)
				{
					PhotonGradient.PhotonPosition pp = gradient[i];
					int u = Numerics.Floor((pp.Position - gradient.Start) * Width / gradient.Length);
					linePen.Color = new Photon(1f - pp.Photon.R, 1f - pp.Photon.G, 1f - pp.Photon.B);
					pe.Graphics.DrawLine(linePen, new Point(u, 0), new Point(u, Height));
				}
			}
		}

		public void UpdateRender()
		{
			if (gradient != null)
			{
				for (int u = 0; u < render.Width; u++)
				{
					float _u = (float)u / Resolution;
					render.SetPixel(u, 0, gradient[_u * gradient.Length + gradient.Start]);
				}
				Invalidate();
			}
		}
	}
}
