using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms画面なし論文名変換
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
            StartForm.maxCharCount = Properties.Settings.Default.maxCharCount;
            NumericCharCount.Value = StartForm.maxCharCount;
        }

        private void NumericCharCount_ValueChanged(object sender, EventArgs e)
        {
            StartForm.maxCharCount = Convert.ToInt16(NumericCharCount.Value);
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void radioButton_1_CheckedChanged(object sender, EventArgs e)
        {
            StartForm.clipboardmode = StartForm.Clipboardmode.LineBreakDeleteMode;
        }

        private void radioButton_2_CheckedChanged(object sender, EventArgs e)
        {
            StartForm.clipboardmode = StartForm.Clipboardmode.FileMode;
        }

        private void radioButton_nomal_CheckedChanged(object sender, EventArgs e)
        {
            StartForm.clipboardmode = StartForm.Clipboardmode.NomalMode;
        }
    }
}
