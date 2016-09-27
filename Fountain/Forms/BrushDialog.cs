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
using System.Reflection;

using LlewellynScripting;
using LlewellynMedia;

using Fountain.Media;

namespace Fountain.Forms
{
	public partial class BrushDialog : Form
	{
		public string BrushName
		{
			get
			{
				return (string)brushNameBox.SelectedItem;
			}
			set
			{
				RefreshNameList();
				brushNameBox.SelectedItem = value;
				RefreshInfo();
			}
		}
		private HeightBrush brush;
		private CSScript script;

		public BrushDialog(Form owner)
		{
			Owner = owner;
			CenterToParent();
			InitializeComponent();

			RefreshInfo();
			RefreshNameList();

			Document.BrushSet += Document_BrushSet;
			Document.BrushRemoved += Document_BrushRemoved;
			Document.Loaded += Document_Loaded;
			Document.Cleared += Document_Cleared;
		}

		private void RefreshNameList()
		{
			brushNameBox.Items.Clear();
			foreach (string name in Document.BrushNames)
				brushNameBox.Items.Add(name);
		}
		private void RefreshInfo()
		{
			if (Document.ContainsBrush(BrushName))
			{
				//Fields
				brush = Document.GetBrush(BrushName);
				script = Document.GetBrushScript(BrushName);
				//Control Values
				Text = "Brush - " + BrushName;
				widthBox.Value = brush.Width;
				heightBox.Value = brush.Height;
				powerBox.Value = (decimal)brush.Power;
				precisionBox.Value = brush.Precision;
				scriptBox.Text = script.Source;
				//Enable Controls
				deleteButton.Enabled = true;
				widthBox.Enabled = true;
				heightBox.Enabled = true;
				powerBox.Enabled = true;
				precisionBox.Enabled = true;
				scriptBox.Enabled = true;
				compileButton.Enabled = true;
			}
			else
			{
				//Fields
				brush = null;
				script = null;
				//Control Values
				Text = "Brushes";
				widthBox.Value = 0;
				heightBox.Value = 0;
				powerBox.Value = 1;
				precisionBox.Value = 1;
				scriptBox.Text = null;
				//Disable Controls
				deleteButton.Enabled = false;
				widthBox.Enabled = false;
				heightBox.Enabled = false;
				powerBox.Enabled = false;
				precisionBox.Enabled = false;
				scriptBox.Enabled = false;
				compileButton.Enabled = false;
			}
		}

		private void Document_Cleared()
		{
			Close();
		}
		private void Document_Loaded(string path)
		{
			Close();
		}
		private void Document_BrushRemoved(string name, HeightBrush brush)
		{
			RefreshNameList();
			RefreshInfo();
		}
		private void Document_BrushSet(string name, HeightBrush brush)
		{
			BrushName = name;
		}

		private void widthBox_ValueChanged(object sender, EventArgs e)
		{
			if (brush != null)
				brush.Width = (int)widthBox.Value;
		}
		private void heightBox_ValueChanged(object sender, EventArgs e)
		{
			if (brush != null)
				brush.Height = (int)heightBox.Value;
		}
		private void powerBox_ValueChanged(object sender, EventArgs e)
		{
			if (brush != null)
				brush.Power = (float)powerBox.Value;
		}
		private void precisionBox_ValueChanged(object sender, EventArgs e)
		{
			if (brush != null)
				brush.Precision = (int)precisionBox.Value;
		}
		private void compileButton_Click(object sender, EventArgs e)
		{
			if (script != null)
			{
				script.Source = scriptBox.Text;
				HeightBrush.SampleFunction sample;
				HeightBrush.BlendFunction blend;
				string errors;
				switch (HeightBrush.CompileFunctions(script, out sample, out blend, out errors))
				{
					case HeightBrush.CompileResult.WrongSampleSignature:
						MessageBox.Show("The method signature for the \"Sample\" function should be:\n\nfloat Sample(int x, int y, float intensity, int left, int right, int top int bottom)", "Script Error");
						break;
					case HeightBrush.CompileResult.MissingSampleFunction:
						MessageBox.Show("The \"Sample\" function is missing from your script.");
						break;
					case HeightBrush.CompileResult.WrongBlendSignature:
						MessageBox.Show("The method signature for the \"Blend\" function should be:\n\nfloat Blend(float baseValue, float newValue)", "Script Error");
						break;
					case HeightBrush.CompileResult.MissingBlendFunction:
						MessageBox.Show("The \"Blend\" function is missing from your script.");
						break;
					case HeightBrush.CompileResult.SyntaxError:
						MessageBox.Show("There was a compilation error in your script:\r\n" + errors, "Syntax Error");
						break;
					case HeightBrush.CompileResult.Success:
						brush.Sample = sample;
						brush.Blend = blend;
						break;
				}
			}
		}
		private void scriptBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.A)
			{
				scriptBox.SelectionStart = 0;
				scriptBox.SelectionLength = scriptBox.Text.Length;

				e.SuppressKeyPress = true;
			}
		}
		private void scriptBox_TextChanged(object sender, EventArgs e)
		{
			if (script != null)
				script.Source = scriptBox.Text;
		}
		private void deleteButton_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to delete this brush?", "Delete Brush", MessageBoxButtons.OKCancel) == DialogResult.OK)
				Document.RemoveBrush(BrushName);
		}
		private void brushNameBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshInfo();
		}
		private void BrushDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				Hide();
			}
		}
	}
}
