using GameOfLife.Controls;
using GraphPaint = GameOfLife.Controls.GraphPaint;

namespace GameOfLife
{
	sealed partial class MainForm
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
			this.btnStart = new System.Windows.Forms.Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
			this.label1 = new System.Windows.Forms.Label();
			this.txtPosX = new System.Windows.Forms.TextBox();
			this.txtPosY = new System.Windows.Forms.TextBox();
			this.btnLoad = new System.Windows.Forms.Button();
			this.trbScale = new System.Windows.Forms.TrackBar();
			this.label3 = new System.Windows.Forms.Label();
			this.btnStep = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.nudSpeed = new System.Windows.Forms.NumericUpDown();
			this.btnReset = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.txtAge = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.trbScale)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudSpeed)).BeginInit();
			this.SuspendLayout();
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(12, 12);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 0;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.vScrollBar1.LargeChange = 100;
			this.vScrollBar1.Location = new System.Drawing.Point(939, 44);
			this.vScrollBar1.Maximum = 1000;
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(17, 509);
			this.vScrollBar1.TabIndex = 1;
			this.vScrollBar1.Value = 450;
			this.vScrollBar1.ValueChanged += new System.EventHandler(this.VScrollBar1_ValueChanged);
			// 
			// hScrollBar1
			// 
			this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.hScrollBar1.LargeChange = 100;
			this.hScrollBar1.Location = new System.Drawing.Point(9, 553);
			this.hScrollBar1.Maximum = 1000;
			this.hScrollBar1.Name = "hScrollBar1";
			this.hScrollBar1.Size = new System.Drawing.Size(930, 17);
			this.hScrollBar1.TabIndex = 2;
			this.hScrollBar1.Value = 450;
			this.hScrollBar1.ValueChanged += new System.EventHandler(this.HScrollBar1_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(584, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Position";
			// 
			// txtPosX
			// 
			this.txtPosX.Location = new System.Drawing.Point(634, 14);
			this.txtPosX.Name = "txtPosX";
			this.txtPosX.ReadOnly = true;
			this.txtPosX.Size = new System.Drawing.Size(26, 20);
			this.txtPosX.TabIndex = 4;
			// 
			// txtPosY
			// 
			this.txtPosY.Location = new System.Drawing.Point(666, 14);
			this.txtPosY.Name = "txtPosY";
			this.txtPosY.ReadOnly = true;
			this.txtPosY.Size = new System.Drawing.Size(26, 20);
			this.txtPosY.TabIndex = 5;
			// 
			// btnLoad
			// 
			this.btnLoad.Location = new System.Drawing.Point(255, 12);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(75, 23);
			this.btnLoad.TabIndex = 0;
			this.btnLoad.Text = "Load";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
			// 
			// trbScale
			// 
			this.trbScale.AutoSize = false;
			this.trbScale.Location = new System.Drawing.Point(376, 11);
			this.trbScale.Minimum = 1;
			this.trbScale.Name = "trbScale";
			this.trbScale.Size = new System.Drawing.Size(104, 23);
			this.trbScale.TabIndex = 6;
			this.trbScale.Value = 10;
			this.trbScale.ValueChanged += new System.EventHandler(this.TrbScale_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(336, 17);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(34, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Scale";
			// 
			// btnStep
			// 
			this.btnStep.Location = new System.Drawing.Point(93, 12);
			this.btnStep.Name = "btnStep";
			this.btnStep.Size = new System.Drawing.Size(75, 23);
			this.btnStep.TabIndex = 0;
			this.btnStep.Text = "Step";
			this.btnStep.UseVisualStyleBackColor = true;
			this.btnStep.Click += new System.EventHandler(this.BtnStep_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(486, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Speed";
			// 
			// nudSpeed
			// 
			this.nudSpeed.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.nudSpeed.Location = new System.Drawing.Point(530, 15);
			this.nudSpeed.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.nudSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudSpeed.Name = "nudSpeed";
			this.nudSpeed.Size = new System.Drawing.Size(48, 20);
			this.nudSpeed.TabIndex = 7;
			this.nudSpeed.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.nudSpeed.ValueChanged += new System.EventHandler(this.NudSpeed_ValueChanged);
			// 
			// btnReset
			// 
			this.btnReset.Location = new System.Drawing.Point(174, 12);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(75, 23);
			this.btnReset.TabIndex = 0;
			this.btnReset.Text = "Reset";
			this.btnReset.UseVisualStyleBackColor = true;
			this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(698, 17);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(26, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Age";
			// 
			// txtAge
			// 
			this.txtAge.Location = new System.Drawing.Point(730, 14);
			this.txtAge.Name = "txtAge";
			this.txtAge.ReadOnly = true;
			this.txtAge.Size = new System.Drawing.Size(40, 20);
			this.txtAge.TabIndex = 4;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(965, 579);
			this.Controls.Add(this.nudSpeed);
			this.Controls.Add(this.trbScale);
			this.Controls.Add(this.txtPosY);
			this.Controls.Add(this.txtAge);
			this.Controls.Add(this.txtPosX);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.hScrollBar1);
			this.Controls.Add(this.vScrollBar1);
			this.Controls.Add(this.btnLoad);
			this.Controls.Add(this.btnReset);
			this.Controls.Add(this.btnStep);
			this.Controls.Add(this.btnStart);
			this.Name = "MainForm";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.trbScale)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudSpeed)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.VScrollBar vScrollBar1;
		private System.Windows.Forms.HScrollBar hScrollBar1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtPosX;
		private System.Windows.Forms.TextBox txtPosY;
		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.TrackBar trbScale;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnStep;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown nudSpeed;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtAge;
	}
}

