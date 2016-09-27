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
	public partial class GeneratorDialog : Form
	{
		public string GeneratorName
		{
			get
			{
				return (string)generatorNameBox.SelectedItem;
			}
			set
			{
				RefreshNameList();
				generatorNameBox.SelectedItem = value;
				RefreshInfo();
			}
		}
		private CSScript script;

		public GeneratorDialog(Form owner)
		{
			Owner = owner;
			CenterToParent();
			InitializeComponent();

			RefreshInfo();
			RefreshNameList();

			Document.GeneratorSet += Document_GeneratorSet;
			Document.GeneratorRemoved += Document_GeneratorRemoved;
			Document.Loaded += Document_Loaded;
			Document.Cleared += Document_Cleared;
		}

		private void RefreshNameList()
		{
			generatorNameBox.Items.Clear();
			foreach (string name in Document.GeneratorNames)
				generatorNameBox.Items.Add(name);
		}
		private void RefreshInfo()
		{
			if (Document.ContainsGenerator(GeneratorName))
			{
				//Fields
				script = Document.GetGeneratorScript(GeneratorName);
				//Control Values
				Text = "Generator - " + GeneratorName;
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
				Text = "Generators";
				scriptBox.Text = null;
				//Disable Controls
				deleteButton.Enabled = false;
				scriptBox.Enabled = false;
				compileButton.Enabled = false;
			}
		}

		private void Document_GeneratorSet(string name, HeightRender.Generator generator)
		{
			GeneratorName = name;
		}
		private void Document_GeneratorRemoved(string name, HeightRender.Generator generator)
		{
			RefreshNameList();
			RefreshInfo();
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
			HeightRender.Generator generator;
			string errors;
			switch (HeightRender.CompileGenerator(script, out generator, out errors))
			{
				case HeightRender.GeneratorCompileResult.WrongGenerateSignature:
					MessageBox.Show("The method signature for the \"Generate\" function should be:\n\nfloat Generate(int x, int y, HeightField heightField)", "Script Error");
					break;
				case HeightRender.GeneratorCompileResult.MissingGenerateFunction:
					MessageBox.Show("The \"Generate\" function is missing from your script.", "Script Error");
					break;
				case HeightRender.GeneratorCompileResult.SyntaxError:
					MessageBox.Show("There was a compilation error in your script:\r\n" + errors, "Script Error");
					break;
				case HeightRender.GeneratorCompileResult.Success:
					Document.SetGenerator(GeneratorName, generator, script);
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
			if (MessageBox.Show("Are you sure you want to delete this generator?", "Delete Generator", MessageBoxButtons.OKCancel) == DialogResult.OK)
				Document.RemoveGenerator(GeneratorName);
		}
		private void generatorNameBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			RefreshInfo();
		}
		private void GeneratorDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				Hide();
			}
		}
	}
}
