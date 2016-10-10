namespace Fountain.Forms
{
	partial class GradientDialog
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GradientDialog));
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.gradientTypeBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.label2 = new System.Windows.Forms.Label();
			this.minBox = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.maxBox = new System.Windows.Forms.NumericUpDown();
			this.gradientRuler = new Fountain.Controls.Ruler();
			this.gradientBox = new Fountain.Controls.GradientBox();
			((System.ComponentModel.ISupportInitialize)(this.minBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.maxBox)).BeginInit();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.okButton.Location = new System.Drawing.Point(372, 193);
			this.okButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(172, 27);
			this.okButton.TabIndex = 1;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.cancelButton.Location = new System.Drawing.Point(552, 193);
			this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(162, 27);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// gradientTypeBox
			// 
			this.gradientTypeBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.gradientTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.gradientTypeBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.gradientTypeBox.FormattingEnabled = true;
			this.gradientTypeBox.Location = new System.Drawing.Point(174, 162);
			this.gradientTypeBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.gradientTypeBox.Name = "gradientTypeBox";
			this.gradientTypeBox.Size = new System.Drawing.Size(190, 23);
			this.gradientTypeBox.TabIndex = 3;
			this.toolTip.SetToolTip(this.gradientTypeBox, "Determines how the colors of a gradient are mixed between nodes.");
			this.gradientTypeBox.SelectedIndexChanged += new System.EventHandler(this.gradientTypeBox_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 164);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(152, 17);
			this.label1.TabIndex = 4;
			this.label1.Text = "Interpolation Mode";
			// 
			// toolTip
			// 
			this.toolTip.UseAnimation = false;
			this.toolTip.UseFading = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(392, 166);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 17);
			this.label2.TabIndex = 6;
			this.label2.Text = "Min";
			// 
			// minBox
			// 
			this.minBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.minBox.DecimalPlaces = 6;
			this.minBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.minBox.Location = new System.Drawing.Point(430, 164);
			this.minBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.minBox.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
			this.minBox.Name = "minBox";
			this.minBox.Size = new System.Drawing.Size(120, 23);
			this.minBox.TabIndex = 7;
			this.minBox.ValueChanged += new System.EventHandler(this.minBox_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(556, 166);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 17);
			this.label3.TabIndex = 8;
			this.label3.Text = "Max";
			// 
			// maxBox
			// 
			this.maxBox.DecimalPlaces = 6;
			this.maxBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.maxBox.Location = new System.Drawing.Point(594, 164);
			this.maxBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.maxBox.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
			this.maxBox.Name = "maxBox";
			this.maxBox.Size = new System.Drawing.Size(120, 23);
			this.maxBox.TabIndex = 9;
			this.maxBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.maxBox.ValueChanged += new System.EventHandler(this.maxBox_ValueChanged);
			// 
			// gradientRuler
			// 
			this.gradientRuler.BackColor = System.Drawing.SystemColors.Control;
			this.gradientRuler.Horizontal = true;
			this.gradientRuler.Location = new System.Drawing.Point(14, 14);
			this.gradientRuler.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.gradientRuler.Name = "gradientRuler";
			this.gradientRuler.SegmentLineColor = System.Drawing.Color.Gray;
			this.gradientRuler.SegmentLineWidth = 1F;
			this.gradientRuler.Segments = 50;
			this.gradientRuler.Size = new System.Drawing.Size(700, 15);
			this.gradientRuler.TabIndex = 5;
			this.toolTip.SetToolTip(this.gradientRuler, "Guide with increments every 5%.\r\nHold Shift to snap when dragging a node.");
			this.gradientRuler.Vertical = false;
			// 
			// gradientBox
			// 
			this.gradientBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.gradientBox.Gradient = null;
			this.gradientBox.Location = new System.Drawing.Point(14, 30);
			this.gradientBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.gradientBox.Name = "gradientBox";
			this.gradientBox.Resolution = 512;
			this.gradientBox.Size = new System.Drawing.Size(700, 122);
			this.gradientBox.TabIndex = 0;
			this.toolTip.SetToolTip(this.gradientBox, "Left click to edit a node color.\r\nControl + Left Click to add a node.\r\nRight Clic" +
        "k to delete a node.\r\nDrag with the Middle Mouse Button to move a node.");
			this.gradientBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gradientBox_MouseClick);
			this.gradientBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gradientBox_MouseMove);
			// 
			// GradientDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(727, 234);
			this.ControlBox = false;
			this.Controls.Add(this.maxBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.minBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.gradientRuler);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.gradientTypeBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.gradientBox);
			this.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "GradientDialog";
			this.Text = "Gradient";
			((System.ComponentModel.ISupportInitialize)(this.minBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.maxBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Controls.GradientBox gradientBox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.ComboBox gradientTypeBox;
		private System.Windows.Forms.Label label1;
		private Controls.Ruler gradientRuler;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown minBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown maxBox;
	}
}