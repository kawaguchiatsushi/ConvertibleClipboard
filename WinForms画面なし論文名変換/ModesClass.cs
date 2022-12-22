using System.Runtime.CompilerServices;

namespace WinForms画面なし論文名変換
{
    public enum Clipboardmode
    {
        LineBreakDeleteMode,
        FileMode,
        NomalMode,
    }

    /// <summary>
    /// Retention of variables in settings. 
    /// </summary>
    public class ModesClass
    {
        private readonly string fileMode_Name = "file保存形式";
        private readonly string fileMode_Icon = @"icon\filenamemode.ico";

        private readonly string replacementMode_Name = "クリップボード文字列の改行を削除";
        private readonly string replacementMode_Icon = @"icon\replacementmode.ico";

        private readonly string nomalMode_Name = "nomal";
        private readonly string nomalMode_Icon = @"icon\normal.ico";

        public string ModeName;

        public string IconName;

        public int MaxCharCount;

        public ModesClass()
        {
            this.ModeName = this.fileMode_Name;
            this.IconName = this.fileMode_Icon;
            this.ClipBMode = Clipboardmode.FileMode;
            this.MaxCharCount = Properties.Settings.Default.maxCharCount;
        }


        private Clipboardmode clipboardmode;

        public Clipboardmode ClipBMode
        {
            get { return this.clipboardmode; }
            set
            {
                this.clipboardmode = value;
                if (this.clipboardmode == Clipboardmode.LineBreakDeleteMode)
                {
                    this.ModeName = this.replacementMode_Name;
                    this.IconName = this.replacementMode_Icon;
                }
                else if(this.clipboardmode == Clipboardmode.FileMode)
                {
                    this.ModeName = this.fileMode_Name;
                    this.IconName = this.fileMode_Icon;
                }
                else if(this.clipboardmode == Clipboardmode.NomalMode)
                {
                    this.ModeName = this.nomalMode_Name;
                    this.IconName = this.nomalMode_Icon;
                }
            }
        }

        

    }

}
