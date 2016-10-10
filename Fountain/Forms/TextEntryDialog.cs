using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fountain.Forms
{
	public partial class TextEntryDialog : Form
	{
		public string Entry
		{
			get
			{
				return entryField.Text;
			}
			private set
			{
				entryField.Text = value;
			}
		}

		public TextEntryDialog(string title, string defaultEntry = null)
		{
			CenterToParent();
			InitializeComponent();
			Text = title;
			Entry = defaultEntry;
			entryField.Select();
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}
		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
		private void entryField_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
				okButton_Click(sender, e);
		}
	}
}
