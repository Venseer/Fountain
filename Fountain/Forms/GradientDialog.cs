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

namespace Fountain.Forms
{
	public partial class GradientDialog : Form
	{
		private string gradientName;
		private PhotonGradient copy;

		public GradientDialog(string gradientName, Form owner)
		{
			Owner = owner;
			if (gradientName != null && gradientName.Length > 0)
			{
				CenterToParent();
				InitializeComponent();
				gradientTypeBox.DataSource = Enum.GetValues(typeof(PhotonInterpolationMode));

				if (Document.ContainsGradient(this.gradientName = gradientName))
				{
					gradientBox.Gradient = Document.GetGradient(gradientName);
				}
				else
				{
					PhotonGradient pg = new PhotonGradient(PhotonInterpolationMode.Linear);
					pg.Add(new Photon(0.0f, 0.0f, 0.0f), 0.0f);
					pg.Add(new Photon(1.0f, 1.0f, 1.0f), 1.0f);
					Document.SetGradient(gradientName, gradientBox.Gradient = pg);
				}

				copy = gradientBox.Gradient.MakeCopy();

				gradientTypeBox.SelectedItem = gradientBox.Gradient.Mode;
				minBox.Value = (decimal)gradientBox.Gradient.Start;
				maxBox.Value = (decimal)(gradientBox.Gradient.Length - gradientBox.Gradient.Start);

				Document.Cleared += Document_Cleared;
				Document.Loaded += Document_Loaded;
				Document.GradientRemoved += Document_GradientRemoved;
			}
			else throw new Exception("The supplied name was empty or null.");
		}

		private void Document_GradientRemoved(string name, PhotonGradient gradient)
		{
			if (name == gradientName) Close();
		}
		private void Document_Loaded(string path)
		{
			Close();
		}
		private void Document_Cleared()
		{
			Close();
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}
		private void cancelButton_Click(object sender, EventArgs e)
		{
			Document.SetGradient(gradientName, copy);
			DialogResult = DialogResult.Cancel;
			Close();
		}
		private void gradientTypeBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (gradientBox.Gradient != null)
			{
				gradientBox.Gradient.Mode = (PhotonInterpolationMode)gradientTypeBox.SelectedItem;
				gradientBox.UpdateRender();
			}
		}
		private void gradientBox_MouseClick(object sender, MouseEventArgs e)
		{
			PhotonGradient gradient = gradientBox.Gradient;

			float u = (float)e.X / gradientBox.Width * gradient.Length + gradient.Start;
			int i = gradient.ClosestIndex(u);
			if (e.Button == MouseButtons.Left)
			{
				PhotonGradient.PhotonPosition pp = gradient[i];
				if (ModifierKeys == Keys.Control)
				{
					pp.Photon = gradient[u];
					pp.Position = u;
					gradient.Add(pp);
					gradientBox.UpdateRender();
				}
				else
				{
					PhotonDialog photonDialog = new PhotonDialog(this);
					photonDialog.Photon = pp.Photon;
					if (photonDialog.ShowDialog() == DialogResult.OK)
					{
						pp.Photon = photonDialog.Photon;
						gradient[i] = pp;
						gradientBox.UpdateRender();
					}
				}
			}
			else if (e.Button == MouseButtons.Right && i > 0 && i < gradient.PhotonPositionCount - 1)
			{
				gradient.Remove(i);
				gradientBox.UpdateRender();
			}
		}
		private void gradientBox_MouseMove(object sender, MouseEventArgs e)
		{
			PhotonGradient gradient = gradientBox.Gradient;

			float u = (float)e.X / gradientBox.Width * gradient.Length + gradient.Start;
			int i = gradient.ClosestIndex(u);
			if (e.Button == MouseButtons.Middle && i > 0 && i < gradient.PhotonPositionCount - 1)
			{
				PhotonGradient.PhotonPosition pp = gradient[i];
				if (ModifierKeys == Keys.Shift) pp.Position = (float)(int)(Math.Round(u * gradientRuler.Segments)) / gradientRuler.Segments;
				else pp.Position = u;
				gradient[i] = pp;
				gradientBox.UpdateRender();
			}
		}
		private void minBox_ValueChanged(object sender, EventArgs e)
		{
			gradientBox.Gradient.Normalize((float)minBox.Value, (float)maxBox.Value);
		}
		private void maxBox_ValueChanged(object sender, EventArgs e)
		{
			gradientBox.Gradient.Normalize((float)minBox.Value, (float)maxBox.Value);
		}
	}
}
