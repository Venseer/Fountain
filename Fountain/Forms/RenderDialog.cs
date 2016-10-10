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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Fountain.Media;

namespace Fountain.Forms
{
	public partial class RenderDialog : Form
	{
		private string renderName;
		private HeightRender render;
		public int RenderWidth
		{
			get
			{
				return (int)widthBox.Value;
			}
		}
		public int RenderHeight
		{
			get
			{
				return (int)heightBox.Value;
			}
		}
		public bool RenderClamp
		{
			get
			{
				return clampBox.Checked;
			}
		}
		public float RenderClampMin
		{
			get
			{
				return (float)clampMinBox.Value;
			}
		}
		public float RenderClampMax
		{
			get
			{
				return (float)clampMaxBox.Value;
			}
		}
		public bool RenderWrapX
		{
			get
			{
				return wrapXBox.Checked;
			}
		}
		public bool RenderWrapY
		{
			get
			{
				return wrapYBox.Checked;
			}
		}

		public RenderDialog(string renderName, Form owner)
		{
			Owner = owner;
			if (renderName != null && renderName.Length > 0)
			{
				CenterToParent();
				InitializeComponent();

				Text = "Render - " + renderName;
				if (Document.ContainsRender(this.renderName = renderName) && (render = Document.GetRender(renderName)) != null)
				{
					widthBox.Value = render.HeightField.Width;
					widthBox.Enabled = false;
					heightBox.Value = render.HeightField.Height;
					heightBox.Enabled = false;
					clampBox.Checked = render.HeightField.Clamp;
					clampMinBox.Value = (decimal)render.HeightField.ClampMin;
					clampMaxBox.Value = (decimal)render.HeightField.ClampMax;
					wrapXBox.Checked = render.HeightField.WrapX;
					wrapYBox.Checked = render.HeightField.WrapY;
				}
				else
				{
					widthBox.Enabled = true;
					heightBox.Enabled = true;
				}
			}
			else throw new Exception("The supplied name was empty or null.");
		}

		private void widthBox_ValueChanged(object sender, EventArgs e)
		{
			if (render != null) widthBox.Value = render.Width;
		}
		private void heightBox_ValueChanged(object sender, EventArgs e)
		{
			if (render != null) heightBox.Value = render.Height;
		}
		private void clampBox_CheckedChanged(object sender, EventArgs e)
		{
			if (render != null) render.HeightField.Clamp = RenderClamp;
		}
		private void clampMinBox_ValueChanged(object sender, EventArgs e)
		{
			if (render != null) render.HeightField.ClampMin = RenderClampMin;
		}
		private void clampMaxBox_ValueChanged(object sender, EventArgs e)
		{
			if (render != null) render.HeightField.ClampMax = RenderClampMax;
		}
		private void wrapXBox_CheckedChanged(object sender, EventArgs e)
		{
			if (render != null) render.HeightField.WrapX = RenderWrapX;
		}
		private void wrapYBox_CheckedChanged(object sender, EventArgs e)
		{
			if (render != null) render.HeightField.WrapY = RenderWrapY;
		}
		private void okButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}
		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
