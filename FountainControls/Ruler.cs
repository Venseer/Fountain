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
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fountain.Controls
{
	public partial class Ruler : UserControl
	{
		private int segments = 10;
		public int Segments
		{
			get
			{
				return segments;
			}
			set
			{
				segments = value;
				if (segments < 0) segments = 0;
			}
		}
		private Pen segmentPen = new Pen(Color.Black, 1);
		public Color SegmentLineColor
		{
			get
			{
				return segmentPen.Color;
			}
			set
			{
				segmentPen.Color = value;
			}
		}
		public float SegmentLineWidth
		{
			get
			{
				return segmentPen.Width;
			}
			set
			{
				if (value < 0) value = 0;
				segmentPen.Width = value;
			}
		}
		private bool alignment = true;
		public bool Horizontal
		{
			get
			{
				return alignment;
			}
			set
			{
				alignment = value;
			}
		}
		public bool Vertical
		{
			get
			{
				return !alignment;
			}
			set
			{
				alignment = !value;
			}
		}

		public Ruler()
		{
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			for (int i = 0; i < segments + 1; i++)
			{
				float u = (float)i / segments;

				int _i = 0;
				if (u > 0) _i = -1;

				if (Horizontal) e.Graphics.DrawLine(segmentPen, u * Width + _i, 0, u * Width + _i, Height);
				else e.Graphics.DrawLine(segmentPen, 0, u * Height + _i, Width, u * Height + _i);
			}
		}
	}
}
