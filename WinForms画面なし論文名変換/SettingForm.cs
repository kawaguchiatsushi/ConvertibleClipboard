namespace WinForms画面なし論文名変換
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
            StartForm.ModesClass.MaxCharCount = Properties.Settings.Default.maxCharCount;
            NumericCharCount.Value = StartForm.ModesClass.MaxCharCount;
        }

        private void NumericCharCount_ValueChanged(object sender, EventArgs e)
        {
            StartForm.ModesClass.MaxCharCount = Convert.ToInt16(NumericCharCount.Value);
        }

        private void LabelClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void RadioButton_ReplacementofLineBlank_CheckedChanged(object sender, EventArgs e)
        {
            StartForm.ModesClass.ClipBMode = Clipboardmode.LineBreakDeleteMode;
        }

        private void RadioButton_Filemode_CheckedChanged(object sender, EventArgs e)
        {
            StartForm.ModesClass.ClipBMode = Clipboardmode.FileMode;
        }

        private void RadioButton_nomal_CheckedChanged(object sender, EventArgs e)
        {
            StartForm.ModesClass.ClipBMode = Clipboardmode.NomalMode;
        }


    }
}
