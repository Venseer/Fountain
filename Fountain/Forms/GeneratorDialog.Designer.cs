namespace Fountain.Forms
{
	partial class GeneratorDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneratorDialog));
			this.scriptBox = new System.Windows.Forms.TextBox();
			this.compileButton = new System.Windows.Forms.Button();
			this.generatorNameBox = new System.Windows.Forms.ComboBox();
			this.deleteButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// scriptBox
			// 
			this.scriptBox.AcceptsReturn = true;
			this.scriptBox.AcceptsTab = true;
			this.scriptBox.AllowDrop = true;
			this.scriptBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.scriptBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scriptBox.Location = new System.Drawing.Point(0, 50);
			this.scriptBox.Multiline = true;
			this.scriptBox.Name = "scriptBox";
			this.scriptBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.scriptBox.Size = new System.Drawing.Size(484, 350);
			this.scriptBox.TabIndex = 0;
			this.scriptBox.WordWrap = false;
			this.scriptBox.TextChanged += new System.EventHandler(this.scriptBox_TextChanged);
			this.scriptBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.scriptBox_KeyDown);
			// 
			// compileButton
			// 
			this.compileButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.compileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.compileButton.Location = new System.Drawing.Point(0, 400);
			this.compileButton.Name = "compileButton";
			this.compileButton.Size = new System.Drawing.Size(484, 27);
			this.compileButton.TabIndex = 1;
			this.compileButton.Text = "Compile";
			this.compileButton.UseVisualStyleBackColor = true;
			this.compileButton.Click += new System.EventHandler(this.compileButton_Click);
			// 
			// generatorNameBox
			// 
			this.generatorNameBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.generatorNameBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.generatorNameBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.generatorNameBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.generatorNameBox.FormattingEnabled = true;
			this.generatorNameBox.Location = new System.Drawing.Point(0, 0);
			this.generatorNameBox.Name = "generatorNameBox";
			this.generatorNameBox.Size = new System.Drawing.Size(484, 23);
			this.generatorNameBox.TabIndex = 2;
			this.generatorNameBox.SelectedIndexChanged += new System.EventHandler(this.generatorNameBox_SelectedIndexChanged);
			// 
			// deleteButton
			// 
			this.deleteButton.Dock = System.Windows.Forms.DockStyle.Top;
			this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.deleteButton.Location = new System.Drawing.Point(0, 23);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(484, 27);
			this.deleteButton.TabIndex = 3;
			this.deleteButton.Text = "Delete";
			this.deleteButton.UseVisualStyleBackColor = true;
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// GeneratorDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 427);
			this.Controls.Add(this.scriptBox);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.generatorNameBox);
			this.Controls.Add(this.compileButton);
			this.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "GeneratorDialog";
			this.Text = "Generator";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GeneratorDialog_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox scriptBox;
		private System.Windows.Forms.Button compileButton;
		private System.Windows.Forms.ComboBox generatorNameBox;
		private System.Windows.Forms.Button deleteButton;
	}
}