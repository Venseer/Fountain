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
	public partial class RenderArea : UserControl
	{
		private Vector2 imageOffset;
		public Vector2 ImageOffset
		{
			get
			{
				return imageOffset;
			}
			set
			{
				imageOffset = value;
				Invalidate();
			}
		}
		private Vector2 imageScale;
		public Vector2 ImageScale
		{
			get
			{
				return imageScale;
			}
			set
			{
				imageScale = value;
				Invalidate();
			}
		}
		public Vector2 ImageSize
		{
			get
			{
				if (image != null) return new Vector2(imageScale.X * image.Width, imageScale.Y * image.Height);
				else return Vector2.Zero;
			}
			set
			{
				if (image != null) imageScale = new Vector2(value.X / image.Width, value.Y / image.Height);
			}
		}
		public RectangleF ImageBounds
		{
			get
			{
				Vector2 _size = ImageSize;
				Vector2 _offset = imageOffset * imageScale;
				return new RectangleF(Width / 2 - (float)_size.X / 2 + (float)_offset.X, Height / 2 - (float)_size.Y / 2 + (float)_offset.Y, (float)_size.X, (float)_size.Y);
			}
		}
		private Bitmap image;
		public Bitmap Image
		{
			get
			{
				return image;
			}
			set
			{
				image = value;

				if (image != null)
				{
					ImageOffset = Vector2.Zero;
					ImageScale = Vector2.One;
				}

				Invalidate();
			}
		}
		private SolidBrush canvasBrush = new SolidBrush(new Photon(0.2f, 0.2f, 0.2f, 0.5f));
		public Photon CanvasColor
		{
			get
			{
				return canvasBrush.Color;
			}
			set
			{
				canvasBrush.Color = value;
			}
		}
		private Pen overlayPen = new Pen(new Photon(0.8f, 0.8f, 0.8f, 1.0f), 1);
		public Photon OverlayColor
		{
			get
			{
				return overlayPen.Color;
			}
			set
			{
				overlayPen.Color = value;
			}
		}
		public float OverlayThickness
		{
			get
			{
				return overlayPen.Width;
			}
			set
			{
				overlayPen.Width = value;
			}
		}
		protected override bool DoubleBuffered
		{
			get
			{
				return base.DoubleBuffered;
			}
			set
			{
				base.DoubleBuffered = true;
			}
		}

		public RenderArea()
		{
			Image = null;
			SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.FillRectangle(canvasBrush, 0, 0, Width, Height);
			if (image != null)
			{
				#region Draw Image
				e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
				e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
				RectangleF imageBounds = ImageBounds;
				e.Graphics.DrawImage(image, imageBounds);
				#endregion
				#region Draw Ruling Lines
				e.Graphics.DrawLine(overlayPen, new PointF(0, imageBounds.Top), new PointF(Width, imageBounds.Top));
				e.Graphics.DrawLine(overlayPen, new PointF(0, imageBounds.Bottom), new PointF(Width, imageBounds.Bottom));
				e.Graphics.DrawLine(overlayPen, new PointF(imageBounds.Left, 0), new PointF(imageBounds.Left, Height));
				e.Graphics.DrawLine(overlayPen, new PointF(imageBounds.Right, 0), new PointF(imageBounds.Right, Height));
				#endregion
				#region Draw Crosshairs
				e.Graphics.DrawLine(overlayPen, new Point(Width / 2, Height / 2 - 20), new Point(Width / 2, Height / 2 + 20));
				e.Graphics.DrawLine(overlayPen, new Point(Width / 2 - 20, Height / 2), new Point(Width / 2 + 20, Height / 2));
				#endregion
			}
		}
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Invalidate();
		}

		public Vector2 ClientToImage(Vector2 clientPosition)
		{
			if (image != null)
			{
				RectangleF bounds = ImageBounds;
				Vector2 _pos = new Vector2(clientPosition.X - bounds.Left, clientPosition.Y - bounds.Top);
				_pos = new Vector2(_pos.X / imageScale.X, _pos.Y / imageScale.Y);
				return _pos;
			}
			else return clientPosition;
		}
		public Vector2 ImageToClient(Vector2 imagePosition)
		{
			if (image != null)
			{
				Vector2 _pos = imagePosition * imageScale;
				RectangleF bounds = ImageBounds;
				return new Vector2(_pos.X + bounds.Left, _pos.Y + bounds.Top);
			}
			else return imagePosition;
		}
	}
}
