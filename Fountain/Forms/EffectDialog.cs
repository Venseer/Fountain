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
	public partial class EffectDialog : Form
	{
		public string EffectName
		{
			get
			{
				return (string)effectNameBox.SelectedItem;
			}
			set
			{
				RefreshNameList();
				effectNameBox.SelectedItem = value;
				RefreshInfo();
			}
		}
		private CSScript script;

		public EffectDialog(Form owner)
		{
			Owner = owner;
			CenterToParent();
			InitializeComponent();

			RefreshInfo();
			RefreshNameList();

			Document.EffectSet += Document_EffectSet;
			Document.EffectRemoved += Document_EffectRemoved;
			Document.Loaded += Document_Loaded;
			Document.Cleared += Document_Cleared;
		}

		private void RefreshNameList()
		{
			effectNameBox.Items.Clear();
			foreach (string name in Document.EffectNames)
				effectNameBox.Items.Add(name);
		}
		private void RefreshInfo()
		{
			if (Document.ContainsEffect(EffectName))
			{
				//Fields
				script = Document.GetEffectScript(EffectName);
				//Control Values
				Text = "Effect - " + EffectName;
				scriptBox.Text = script.Source;
				//Enable Controls
				deleteButton.Enabled = true;
				scriptBox.Enabled = true;
				compileButton.Enabled = true;
			}
			else
			{
				//Fields
				script = null;
				//Control Values
				Text = "Effects";
				scriptBox.Text = null;
				//Disable Controls
				deleteButton.Enabled = false;
				scriptBox.Enabled = false;
				compileButton.Enabled = false;
			}
		}

		private void Document_EffectRemoved(string name, Media.HeightRender.Effect effect)
		{
			RefreshNameList();
			RefreshInfo();
		}
		private void Document_EffectSet(string name, HeightRender.Effect effect)
		{
			EffectName = name;
		}
		private void Document_Loaded(string path)
		{
			Close();
		}
		private void Document_Cleared()
		{
			Close();
		}

		private void compileButton_Click(object sender, EventArgs e)
		{
			script.Source = scriptBox.Text;
			HeightRender.Effect effect;
			string errors;
			switch (HeightRender.CompileEffect(script, out effect, out errors))
			{
				case HeightRender.EffectCompileResult.WrongApplySignature:
					MessageBox.Show("The method signature for the \"Apply\" function should be:\n\nPhoton Apply(int x, int y, Photon color, HeightField heightField)", "Script Error");
					break;
				case HeightRender.EffectCompileResult.MissingApplyFunction:
					MessageBox.Show("The \"Apply\" function is missing from your script.", "Script Error");
					break;
				case HeightRender.EffectCompileResult.SyntaxError:
					MessageBox.Show("There was a compilation error in your script:\r\n" + errors, "Script Error");
					break;
				case HeightRender.EffectCompileResult.Success:
					Document.SetEffect(EffectName, effect, script);
					break;
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
			if (MessageBox.Show("Are you sure you want to delete this effect?", "Delete Effect", MessageBoxButtons.OKCancel) == DialogResult.OK)
				Document.RemoveEffect(EffectName);
		}
		private void EffectDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				Hide();
			}
		}
		private void effectNameBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshInfo();
		}
		private void addButton_Click(object sender, EventArgs e)
		{
			if (Document.ContainsEffect(EffectName))
				Document.SelectEffect(EffectName);
		}
	}
}
