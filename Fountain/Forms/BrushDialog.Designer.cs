namespace Fountain.Forms
{
	partial class BrushDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrushDialog));
			this.scriptBox = new System.Windows.Forms.TextBox();
			this.compileButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.widthBox = new System.Windows.Forms.NumericUpDown();
			this.heightBox = new System.Windows.Forms.NumericUpDown();
			this.powerBox = new System.Windows.Forms.NumericUpDown();
			this.precisionBox = new System.Windows.Forms.NumericUpDown();
			this.panel1 = new System.Windows.Forms.Panel();
			this.deleteButton = new System.Windows.Forms.Button();
			this.brushNameBox = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.widthBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.heightBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.powerBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.precisionBox)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// scriptBox
			// 
			this.scriptBox.AcceptsReturn = true;
			this.scriptBox.AcceptsTab = true;
			this.scriptBox.AllowDrop = true;
			this.scriptBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.scriptBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scriptBox.Location = new System.Drawing.Point(0, 175);
			this.scriptBox.Multiline = true;
			this.scriptBox.Name = "scriptBox";
			this.scriptBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.scriptBox.Size = new System.Drawing.Size(519, 351);
			this.scriptBox.TabIndex = 0;
			this.scriptBox.WordWrap = false;
			this.scriptBox.TextChanged += new System.EventHandler(this.scriptBox_TextChanged);
			this.scriptBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.scriptBox_KeyDown);
			// 
			// compileButton
			// 
			this.compileButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.compileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.compileButton.Location = new System.Drawing.Point(0, 526);
			this.compileButton.Name = "compileButton";
			this.compileButton.Size = new System.Drawing.Size(519, 27);
			this.compileButton.TabIndex = 1;
			this.compileButton.Text = "Compile";
			this.compileButton.UseVisualStyleBackColor = true;
			this.compileButton.Click += new System.EventHandler(this.compileButton_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(11, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "Width";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(11, 39);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 17);
			this.label3.TabIndex = 4;
			this.label3.Text = "Height";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(11, 69);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 17);
			this.label4.TabIndex = 5;
			this.label4.Text = "Power";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(11, 99);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 17);
			this.label5.TabIndex = 6;
			this.label5.Text = "Precision";
			// 
			// widthBox
			// 
			this.widthBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.widthBox.Location = new System.Drawing.Point(122, 6);
			this.widthBox.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			this.widthBox.Name = "widthBox";
			this.widthBox.Size = new System.Drawing.Size(140, 23);
			this.widthBox.TabIndex = 7;
			this.widthBox.ValueChanged += new System.EventHandler(this.widthBox_ValueChanged);
			// 
			// heightBox
			// 
			this.heightBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.heightBox.Location = new System.Drawing.Point(122, 36);
			this.heightBox.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			this.heightBox.Name = "heightBox";
			this.heightBox.Size = new System.Drawing.Size(140, 23);
			this.heightBox.TabIndex = 9;
			this.heightBox.ValueChanged += new System.EventHandler(this.heightBox_ValueChanged);
			// 
			// powerBox
			// 
			this.powerBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.powerBox.DecimalPlaces = 6;
			this.powerBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
			this.powerBox.Location = new System.Drawing.Point(122, 66);
			this.powerBox.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
			this.powerBox.Name = "powerBox";
			this.powerBox.Size = new System.Drawing.Size(140, 23);
			this.powerBox.TabIndex = 10;
			this.powerBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.powerBox.ValueChanged += new System.EventHandler(this.powerBox_ValueChanged);
			// 
			// precisionBox
			// 
			this.precisionBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.precisionBox.Location = new System.Drawing.Point(122, 96);
			this.precisionBox.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			this.precisionBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.precisionBox.Name = "precisionBox";
			this.precisionBox.Size = new System.Drawing.Size(140, 23);
			this.precisionBox.TabIndex = 11;
			this.precisionBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.precisionBox.ValueChanged += new System.EventHandler(this.precisionBox_ValueChanged);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.precisionBox);
			this.panel1.Controls.Add(this.powerBox);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.heightBox);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.widthBox);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 50);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(519, 125);
			this.panel1.TabIndex = 12;
			// 
			// deleteButton
			// 
			this.deleteButton.BackColor = System.Drawing.SystemColors.ControlLight;
			this.deleteButton.Dock = System.Windows.Forms.DockStyle.Top;
			this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.deleteButton.Location = new System.Drawing.Point(0, 23);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(519, 27);
			this.deleteButton.TabIndex = 12;
			this.deleteButton.Text = "Delete";
			this.deleteButton.UseVisualStyleBackColor = false;
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// brushNameBox
			// 
			this.brushNameBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.brushNameBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.brushNameBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.brushNameBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.brushNameBox.FormattingEnabled = true;
			this.brushNameBox.Location = new System.Drawing.Point(0, 0);
			this.brushNameBox.Name = "brushNameBox";
			this.brushNameBox.Size = new System.Drawing.Size(519, 23);
			this.brushNameBox.TabIndex = 12;
			this.brushNameBox.SelectedIndexChanged += new System.EventHandler(this.brushNameBox_SelectedIndexChanged);
			// 
			// BrushDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(519, 553);
			this.Controls.Add(this.scriptBox);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.compileButton);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.brushNameBox);
			this.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "BrushDialog";
			this.Text = "Brush";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BrushDialog_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.widthBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.heightBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.powerBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.precisionBox)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox scriptBox;
		private System.Windows.Forms.Button compileButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown widthBox;
		private System.Windows.Forms.NumericUpDown heightBox;
		private System.Windows.Forms.NumericUpDown powerBox;
		private System.Windows.Forms.NumericUpDown precisionBox;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox brushNameBox;
		private System.Windows.Forms.Button deleteButton;
	}
}