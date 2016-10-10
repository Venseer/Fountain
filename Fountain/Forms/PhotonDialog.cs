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

using LlewellynMedia;
using LlewellynMath;

namespace Fountain.Forms
{
	public partial class PhotonDialog : Form
	{
		private bool skipBoxUpdate = false;
		public Photon Photon
		{
			get
			{
				return colorPanel.Photon;
			}
			set
			{
				colorPanel.Photon = value;
				redBar.Value = value.r;
				greenBar.Value = value.g;
				blueBar.Value = value.b;
				alphaBar.Value = value.a;
			}
		}

		public PhotonDialog(Form owner)
		{
			Owner = owner;
			CenterToParent();
			InitializeComponent();
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}
		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
		private void redBar_ValueChanged(object sender, EventArgs e)
		{
			if (ModifierKeys == Keys.Shift)
			{
				int red = Numerics.Round((float)redBar.Value / redBar.TickFrequency) * redBar.TickFrequency;
				if (red > 0) red -= 1;
				redBar.Value = red;
			}
			UpdateFromBars();
		}
		private void greenBar_ValueChanged(object sender, EventArgs e)
		{
			if (ModifierKeys == Keys.Shift)
			{
				int green = Numerics.Round((float)greenBar.Value / greenBar.TickFrequency) * greenBar.TickFrequency;
				if (green > 0) green -= 1;
				greenBar.Value = green;
			}
			UpdateFromBars();
		}
		private void blueBar_ValueChanged(object sender, EventArgs e)
		{
			if (ModifierKeys == Keys.Shift)
			{
				int blue = Numerics.Round((float)blueBar.Value / blueBar.TickFrequency) * blueBar.TickFrequency;
				if (blue > 0) blue -= 1;
				blueBar.Value = blue;
			}
			UpdateFromBars();
		}
		private void alphaBar_ValueChanged(object sender, EventArgs e)
		{
			if (ModifierKeys == Keys.Shift)
			{
				int alpha = Numerics.Round((float)alphaBar.Value / alphaBar.TickFrequency) * alphaBar.TickFrequency;
				if (alpha > 0) alpha -= 1;
				alphaBar.Value = alpha;
			}
			UpdateFromBars();
		}

		private void redBox_ValueChanged(object sender, EventArgs e)
		{
			skipBoxUpdate = true;
			redBar.Value = (int)redBox.Value;
		}
		private void greenBox_ValueChanged(object sender, EventArgs e)
		{
			skipBoxUpdate = true;
			greenBar.Value = (int)greenBox.Value;
		}
		private void blueBox_ValueChanged(object sender, EventArgs e)
		{
			skipBoxUpdate = true;
			blueBar.Value = (int)blueBox.Value;
		}
		private void alphaBox_ValueChanged(object sender, EventArgs e)
		{
			skipBoxUpdate = true;
			alphaBar.Value = (int)alphaBox.Value;
		}

		private void redBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return) e.SuppressKeyPress = true;
		}
		private void greenBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return) e.SuppressKeyPress = true;
		}
		private void blueBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return) e.SuppressKeyPress = true;
		}
		private void alphaBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return) e.SuppressKeyPress = true;
		}

		public void UpdateFromBars()
		{
			colorPanel.Photon = new Photon((byte)redBar.Value, (byte)greenBar.Value, (byte)blueBar.Value, (byte)alphaBar.Value);
			if (!skipBoxUpdate)
			{
				redBox.Value = redBar.Value;
				greenBox.Value = greenBar.Value;
				blueBox.Value = blueBar.Value;
				alphaBox.Value = alphaBar.Value;
			}
			else skipBoxUpdate = false;
		}
	}
}
