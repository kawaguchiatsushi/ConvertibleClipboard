using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace ConvertibleClipboard
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

        private readonly string toolItem_close_Text = "&終了";
        private readonly string toolItem_setting_Text = "&設定変更";

        public string ModeName;

        public string IconName;

        public int MaxCharCount;

        public NotifyIcon notifyIcon;

        public ContextMenuStrip contextMenuStrip;

        public ToolStripMenuItem toolStripMenuItem_close;

        public ToolStripMenuItem toolStripMenuItem_setting;

        public ModesClass()
        {
            this.ModeName = this.fileMode_Name;
            this.IconName = this.fileMode_Icon;
            this.ClipBMode = Clipboardmode.FileMode;
            this.MaxCharCount = Properties.Settings.Default.maxCharCount;
            this.notifyIcon = new NotifyIcon
            {
                Icon = new Icon(this.IconName),
                Visible = true,
                Text = this.ModeName
            };

            this.contextMenuStrip = new();
            this.toolStripMenuItem_close = new()
            {
                Text = this.toolItem_close_Text
            };
            

            this.toolStripMenuItem_setting = new()
            {
                Text = this.toolItem_setting_Text
            };
            

            this.contextMenuStrip.Items.Add(this.toolStripMenuItem_close);
            this.contextMenuStrip.Items.Add(this.toolStripMenuItem_setting);
            this.notifyIcon.ContextMenuStrip = contextMenuStrip;

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
                        ChangeMode();
                }
                else if(this.clipboardmode == Clipboardmode.FileMode)
                {
                 
                        this.ModeName = this.fileMode_Name;
                        this.IconName = this.fileMode_Icon;
                        ChangeMode();
                    
                }
                else if(this.clipboardmode == Clipboardmode.NomalMode)
                {
                        this.ModeName = this.nomalMode_Name;
                        this.IconName = this.nomalMode_Icon;
                        ChangeMode();                    
                }
            }
        }

        /// <summary>
        /// Change Mode with NotifyIcon
        /// </summary>
        public void ChangeMode()
        {
            if (this.notifyIcon!=null)
            {
                this.notifyIcon.Text = this.ModeName;
                this.notifyIcon.Icon.Dispose();
                this.notifyIcon.Icon = new Icon(this.IconName);
                
            }
           
        }

        /// <summary>
        /// Display retained clipboard text with notifBallon.
        /// </summary>
        public void NotifyBallonFnc(string txt)
        {
            if (this.notifyIcon != null)
            {
                this.notifyIcon.BalloonTipTitle = "MODE:" + this.ModeName;
                this.notifyIcon.BalloonTipText = txt;
                this.notifyIcon.ShowBalloonTip(5000);
            }

        }




    }

}
