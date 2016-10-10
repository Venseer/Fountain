namespace Fountain.Forms
{
	partial class RenderDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RenderDialog));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.widthBox = new System.Windows.Forms.NumericUpDown();
			this.heightBox = new System.Windows.Forms.NumericUpDown();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.clampMaxBox = new System.Windows.Forms.NumericUpDown();
			this.clampMinBox = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.clampBox = new System.Windows.Forms.CheckBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.wrapYBox = new System.Windows.Forms.CheckBox();
			this.wrapXBox = new System.Windows.Forms.CheckBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.widthBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.heightBox)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.clampMaxBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.clampMinBox)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Width";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 17);
			this.label2.TabIndex = 1;
			this.label2.Text = "Height";
			// 
			// widthBox
			// 
			this.widthBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.widthBox.Location = new System.Drawing.Point(108, 17);
			this.widthBox.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
			this.widthBox.Minimum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.widthBox.Name = "widthBox";
			this.widthBox.Size = new System.Drawing.Size(120, 23);
			this.widthBox.TabIndex = 2;
			this.widthBox.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
			this.widthBox.ValueChanged += new System.EventHandler(this.widthBox_ValueChanged);
			// 
			// heightBox
			// 
			this.heightBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.heightBox.Location = new System.Drawing.Point(108, 46);
			this.heightBox.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
			this.heightBox.Minimum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.heightBox.Name = "heightBox";
			this.heightBox.Size = new System.Drawing.Size(120, 23);
			this.heightBox.TabIndex = 3;
			this.heightBox.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
			this.heightBox.ValueChanged += new System.EventHandler(this.heightBox_ValueChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.heightBox);
			this.groupBox1.Controls.Add(this.widthBox);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(234, 76);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Size";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.clampMaxBox);
			this.groupBox2.Controls.Add(this.clampMinBox);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.clampBox);
			this.groupBox2.Location = new System.Drawing.Point(12, 94);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(234, 106);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Height Data";
			// 
			// clampMaxBox
			// 
			this.clampMaxBox.DecimalPlaces = 4;
			this.clampMaxBox.Location = new System.Drawing.Point(114, 78);
			this.clampMaxBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.clampMaxBox.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
			this.clampMaxBox.Name = "clampMaxBox";
			this.clampMaxBox.Size = new System.Drawing.Size(114, 23);
			this.clampMaxBox.TabIndex = 4;
			this.clampMaxBox.ThousandsSeparator = true;
			this.clampMaxBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.clampMaxBox.ValueChanged += new System.EventHandler(this.clampMaxBox_ValueChanged);
			// 
			// clampMinBox
			// 
			this.clampMinBox.DecimalPlaces = 4;
			this.clampMinBox.Location = new System.Drawing.Point(114, 49);
			this.clampMinBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.clampMinBox.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
			this.clampMinBox.Name = "clampMinBox";
			this.clampMinBox.Size = new System.Drawing.Size(114, 23);
			this.clampMinBox.TabIndex = 3;
			this.clampMinBox.ThousandsSeparator = true;
			this.clampMinBox.ValueChanged += new System.EventHandler(this.clampMinBox_ValueChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 80);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 17);
			this.label4.TabIndex = 2;
			this.label4.Text = "Clamp Max";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 51);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 17);
			this.label3.TabIndex = 1;
			this.label3.Text = "Clamp Min";
			// 
			// clampBox
			// 
			this.clampBox.AutoSize = true;
			this.clampBox.Location = new System.Drawing.Point(6, 22);
			this.clampBox.Name = "clampBox";
			this.clampBox.Size = new System.Drawing.Size(123, 21);
			this.clampBox.TabIndex = 0;
			this.clampBox.Text = "Clamp Values";
			this.clampBox.UseVisualStyleBackColor = true;
			this.clampBox.CheckedChanged += new System.EventHandler(this.clampBox_CheckedChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.wrapYBox);
			this.groupBox3.Controls.Add(this.wrapXBox);
			this.groupBox3.Location = new System.Drawing.Point(12, 206);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(234, 76);
			this.groupBox3.TabIndex = 6;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Wrapping";
			// 
			// wrapYBox
			// 
			this.wrapYBox.AutoSize = true;
			this.wrapYBox.Location = new System.Drawing.Point(6, 49);
			this.wrapYBox.Name = "wrapYBox";
			this.wrapYBox.Size = new System.Drawing.Size(115, 21);
			this.wrapYBox.TabIndex = 1;
			this.wrapYBox.Text = "Wrap Y Axis";
			this.wrapYBox.UseVisualStyleBackColor = true;
			this.wrapYBox.CheckedChanged += new System.EventHandler(this.wrapYBox_CheckedChanged);
			// 
			// wrapXBox
			// 
			this.wrapXBox.AutoSize = true;
			this.wrapXBox.Location = new System.Drawing.Point(6, 22);
			this.wrapXBox.Name = "wrapXBox";
			this.wrapXBox.Size = new System.Drawing.Size(115, 21);
			this.wrapXBox.TabIndex = 0;
			this.wrapXBox.Text = "Wrap X Axis";
			this.wrapXBox.UseVisualStyleBackColor = true;
			this.wrapXBox.CheckedChanged += new System.EventHandler(this.wrapXBox_CheckedChanged);
			// 
			// okButton
			// 
			this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.okButton.Location = new System.Drawing.Point(12, 288);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(111, 26);
			this.okButton.TabIndex = 7;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cancelButton.Location = new System.Drawing.Point(129, 288);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(117, 26);
			this.cancelButton.TabIndex = 8;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// RenderDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(258, 323);
			this.ControlBox = false;
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "RenderDialog";
			this.Text = "Render";
			((System.ComponentModel.ISupportInitialize)(this.widthBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.heightBox)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.clampMaxBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.clampMinBox)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown widthBox;
		private System.Windows.Forms.NumericUpDown heightBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.NumericUpDown clampMaxBox;
		private System.Windows.Forms.NumericUpDown clampMinBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox clampBox;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox wrapYBox;
		private System.Windows.Forms.CheckBox wrapXBox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
	}
}