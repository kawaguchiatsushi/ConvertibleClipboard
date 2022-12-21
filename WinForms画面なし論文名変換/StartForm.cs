using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace WinForms画面なし論文名変換
{
    
    public partial class StartForm : Form
    {
        private readonly SettingForm settingForm = new();
        NotifyIcon? notifyIcon;
        
        public static int maxCharCount = 50;

        /// <summary>
        /// Holds three modes for clipboard conversion.
        /// </summary>
        public enum Clipboardmode
        {
            LineBreakDeleteMode,
            FileMode,
            NomalMode,
        }
        /// <summary>
        /// Class for holding mode changes.
        /// </summary>
        public static Clipboardmode clipboardmode;

        string globaltxt = "";

        IDataObject data = Clipboard.GetDataObject();
        
        ///// <summary>
        ///// Places the given window in the system-maintained clipboard format listener list.
        ///// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AddClipboardFormatListener(IntPtr hwnd);


        ///// <summary>
        ///// Removes the given window from the system-maintained clipboard format listener list.
        ///// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool RemoveClipboardFormatListener(IntPtr hwnd);


        ///// <summary>
        ///// Sent when the contents of the clipboard have changed.
        ///// </summary>
        private const int WM_CLIPBOARDUPDATE = 0x031D;

        public StartForm()
        {
            this.ShowInTaskbar = false;
            this.SetComponents();

        }
        private void SetComponents()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new Icon(@"icon\クリップ.ico");
            notifyIcon.Visible = true;
            notifyIcon.Text = "マルチクリップモード";

            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem.Text = "&終了";
            toolStripMenuItem.Click += ToolStripMenuItem_Click;
            
            ToolStripMenuItem toolStripMenuItem_setting = new ToolStripMenuItem();
            toolStripMenuItem_setting.Text = "&設定変更";
            toolStripMenuItem_setting.Click += ToolStripMenuItem_setting_Click;

            contextMenuStrip.Items.Add(toolStripMenuItem);
            contextMenuStrip.Items.Add(toolStripMenuItem_setting);

            notifyIcon.ContextMenuStrip = contextMenuStrip;


            notifyIcon.Click += NotifyIcon_Click;
            AddClipboardFormatListener(Handle);

            settingForm.Visible = false;
            clipboardmode = Clipboardmode.FileMode;
        }

        private void ToolStripMenuItem_setting_Click(object? sender, EventArgs e) 
        {
            settingForm.Visible = true;
        }

        private void ToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            RemoveClipboardFormatListener(this.Handle);
            Application.Exit();
        }

        /// <summary>
        /// Display retained clipboard text with notifyIcon.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyIcon_Click(object? sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (notifyIcon!=null && me.Button == MouseButtons.Left && globaltxt.Length > 0) 
            {
                notifyIcon.BalloonTipTitle = "クリップボード";
                notifyIcon.BalloonTipText = globaltxt;
                notifyIcon.ShowBalloonTip(5000);
            }
            
        }

        /// <summary>
        /// main function
        /// </summary>
        /// <param name="clipboardmode"></param>
        private void ClipFunction(Clipboardmode clipboardmode)
        {
            if (clipboardmode == Clipboardmode.NomalMode) return;
            try
            {
                if (data.GetDataPresent(typeof(string)))
                {
                    if (globaltxt == null)return;
                    
                    globaltxt = (string)Clipboard.GetData(DataFormats.Text);

                    if (clipboardmode == Clipboardmode.LineBreakDeleteMode || clipboardmode == Clipboardmode.FileMode)
                    {
                        globaltxt = globaltxt.Replace(Environment.NewLine, " ");
                        globaltxt = globaltxt.Replace("\r", " ").Replace("\n", " ");

                    }
                    if (clipboardmode==Clipboardmode.FileMode) 
                    {
                        Regex reg = new Regex(
                        "[\\x00-\\x1f<>:\"/\\\\|?*]|^(CON|PRN|AUX|NUL|COM[0-9]|LPT[0-9]|CLOCK\\$)(\\.|$)|[\\. ]$"
                        , RegexOptions.IgnoreCase
                        );
                        globaltxt = reg.Replace(globaltxt, "_");
                        globaltxt = globaltxt.Replace(" ", "_").Replace("　", "_");
                        int len = globaltxt.Length;
                        if (len > maxCharCount)
                        {
                            globaltxt = globaltxt.Substring(0, maxCharCount);
                        }
                    }
                    
                    RemoveClipboardFormatListener(this.Handle);

                    if (notifyIcon!=null && globaltxt != "" && globaltxt != null)
                    {
                        Clipboard.Clear();
                        Clipboard.SetDataObject(globaltxt, true);
                        if (clipboardmode == Clipboardmode.FileMode)
                        {
                            notifyIcon.BalloonTipTitle = "file保存形式";
                        }
                        else if (clipboardmode == Clipboardmode.LineBreakDeleteMode)
                        {
                            notifyIcon.BalloonTipTitle = "クリップボード文字列の改行を削除";
                        }
                        notifyIcon.BalloonTipText = globaltxt;
                        notifyIcon.ShowBalloonTip(5000);
                    }
                    AddClipboardFormatListener(Handle);
                }
            }
            catch (ExternalException)
            {
                return;
            }
            catch (ArgumentException)
            {
                return;
            }

        }
        protected override void WndProc(ref Message m)
        {

            if (m.Msg == WM_CLIPBOARDUPDATE)
            {
                if (data != null)
                {
                    ClipFunction(clipboardmode);
                }
                m.Result = IntPtr.Zero;
            }
            else
                base.WndProc(ref m);
        }
    }
}