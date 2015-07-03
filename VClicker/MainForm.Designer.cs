namespace VClicker
{
    partial class MainForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.LabelForProcess = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownTimer = new System.Windows.Forms.NumericUpDown();
            this.timerCheckBox1 = new System.Windows.Forms.CheckBox();
            this.clickerTimer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimer)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(93, 183);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Choose Program";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LabelForProcess
            // 
            this.LabelForProcess.AccessibleName = "";
            this.LabelForProcess.AutoSize = true;
            this.LabelForProcess.Location = new System.Drawing.Point(12, 26);
            this.LabelForProcess.Name = "LabelForProcess";
            this.LabelForProcess.Size = new System.Drawing.Size(68, 13);
            this.LabelForProcess.TabIndex = 1;
            this.LabelForProcess.Text = "Got Process:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Set timer (ms)";
            // 
            // numericUpDownTimer
            // 
            this.numericUpDownTimer.Location = new System.Drawing.Point(102, 64);
            this.numericUpDownTimer.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numericUpDownTimer.Name = "numericUpDownTimer";
            this.numericUpDownTimer.Size = new System.Drawing.Size(66, 20);
            this.numericUpDownTimer.TabIndex = 3;
            this.numericUpDownTimer.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownTimer.ValueChanged += new System.EventHandler(this.numericUpDownTimer_ValueChanged);
            // 
            // timerCheckBox1
            // 
            this.timerCheckBox1.AutoSize = true;
            this.timerCheckBox1.Location = new System.Drawing.Point(197, 67);
            this.timerCheckBox1.Name = "timerCheckBox1";
            this.timerCheckBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.timerCheckBox1.Size = new System.Drawing.Size(63, 17);
            this.timerCheckBox1.TabIndex = 4;
            this.timerCheckBox1.Text = "Clicking";
            this.timerCheckBox1.UseVisualStyleBackColor = true;
            this.timerCheckBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // clickerTimer1
            // 
            this.clickerTimer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.timerCheckBox1);
            this.Controls.Add(this.numericUpDownTimer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LabelForProcess);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "VClicker";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label LabelForProcess;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownTimer;
        private System.Windows.Forms.CheckBox timerCheckBox1;
        private System.Windows.Forms.Timer clickerTimer1;
    }
}

