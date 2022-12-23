namespace WinForms画面なし論文名変換
{
    partial class SettingForm
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
            this.labelClose = new System.Windows.Forms.Label();
            this.maxcountlabel = new System.Windows.Forms.Label();
            this.radioButton_2 = new System.Windows.Forms.RadioButton();
            this.radioButton_1 = new System.Windows.Forms.RadioButton();
            this.NumericCharCount = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_nomal = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.NumericCharCount)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelClose
            // 
            this.labelClose.AutoSize = true;
            this.labelClose.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelClose.Location = new System.Drawing.Point(255, 9);
            this.labelClose.Name = "labelClose";
            this.labelClose.Size = new System.Drawing.Size(28, 28);
            this.labelClose.TabIndex = 0;
            this.labelClose.Text = "✕";
            this.labelClose.Click += new System.EventHandler(this.labelClose_Click);
            // 
            // maxcountlabel
            // 
            this.maxcountlabel.AutoSize = true;
            this.maxcountlabel.Font = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.maxcountlabel.Location = new System.Drawing.Point(12, 174);
            this.maxcountlabel.Name = "maxcountlabel";
            this.maxcountlabel.Size = new System.Drawing.Size(141, 20);
            this.maxcountlabel.TabIndex = 1;
            this.maxcountlabel.Text = "ファイル名最大文字数";
            // 
            // radioButton_2
            // 
            this.radioButton_2.AutoSize = true;
            this.radioButton_2.Checked = true;
            this.radioButton_2.Location = new System.Drawing.Point(7, 47);
            this.radioButton_2.Name = "radioButton_2";
            this.radioButton_2.Size = new System.Drawing.Size(107, 24);
            this.radioButton_2.TabIndex = 3;
            this.radioButton_2.TabStop = true;
            this.radioButton_2.Text = "file保存形式";
            this.radioButton_2.UseVisualStyleBackColor = true;
            this.radioButton_2.CheckedChanged += new System.EventHandler(this.radioButton_2_CheckedChanged);
            // 
            // radioButton_1
            // 
            this.radioButton_1.AutoSize = true;
            this.radioButton_1.Location = new System.Drawing.Point(7, 22);
            this.radioButton_1.Name = "radioButton_1";
            this.radioButton_1.Size = new System.Drawing.Size(231, 24);
            this.radioButton_1.TabIndex = 4;
            this.radioButton_1.Text = "クリップボード文字列の改行を削除";
            this.radioButton_1.UseVisualStyleBackColor = true;
            this.radioButton_1.CheckedChanged += new System.EventHandler(this.radioButton_1_CheckedChanged);
            // 
            // NumericCharCount
            // 
            this.NumericCharCount.Location = new System.Drawing.Point(12, 197);
            this.NumericCharCount.Name = "NumericCharCount";
            this.NumericCharCount.Size = new System.Drawing.Size(141, 23);
            this.NumericCharCount.TabIndex = 5;
            this.NumericCharCount.ValueChanged += new System.EventHandler(this.NumericCharCount_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_nomal);
            this.groupBox1.Controls.Add(this.radioButton_1);
            this.groupBox1.Controls.Add(this.radioButton_2);
            this.groupBox1.Font = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 118);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "設定変更";
            // 
            // radioButton_nomal
            // 
            this.radioButton_nomal.AutoSize = true;
            this.radioButton_nomal.Location = new System.Drawing.Point(6, 72);
            this.radioButton_nomal.Name = "radioButton_nomal";
            this.radioButton_nomal.Size = new System.Drawing.Size(69, 24);
            this.radioButton_nomal.TabIndex = 5;
            this.radioButton_nomal.Text = "nomal";
            this.radioButton_nomal.UseVisualStyleBackColor = true;
            this.radioButton_nomal.CheckedChanged += new System.EventHandler(this.radioButton_nomal_CheckedChanged);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 300);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.NumericCharCount);
            this.Controls.Add(this.maxcountlabel);
            this.Controls.Add(this.labelClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.NumericCharCount)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labelClose;
        private Label maxcountlabel;
        public RadioButton radioButton_2;
        public RadioButton radioButton_1;
        private NumericUpDown NumericCharCount;
        private GroupBox groupBox1;
        public RadioButton radioButton_nomal;
    }
}