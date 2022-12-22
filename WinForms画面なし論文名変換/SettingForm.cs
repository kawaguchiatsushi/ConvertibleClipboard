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
            StartForm.ModesClass.ClipBMode = Clipboardmode.LineBreakDeleteMode;
            ChangeMode();
        }

        private void radioButton_2_CheckedChanged(object sender, EventArgs e)
        {
            StartForm.ModesClass.ClipBMode = Clipboardmode.FileMode;
            ChangeMode();
        }

        private void radioButton_nomal_CheckedChanged(object sender, EventArgs e)
        {
            StartForm.ModesClass.ClipBMode = Clipboardmode.NomalMode;
            ChangeMode();
        }


        private static void ChangeMode()
        {
            if (StartForm.notifyIcon != null)
            {
                StartForm.notifyIcon.Icon = new Icon(StartForm.ModesClass.IconName);
                StartForm.notifyIcon.Text = StartForm.ModesClass.ModeName;
            }
        }
    }
}
