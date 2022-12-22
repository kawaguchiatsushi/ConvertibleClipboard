using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WinForms画面なし論文名変換
{

    public partial class StartForm : Form
    {
        private readonly SettingForm settingForm = new();
        public static NotifyIcon? notifyIcon;
        readonly CharLangCheckClass charLang = new();
       

        /// <summary>
        /// Holds three modes for clipboard conversion.
        /// </summary>
        public static ModesClass ModesClass = new();
       

        string globaltxt = "";
        readonly IDataObject data = Clipboard.GetDataObject();
        
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
            notifyIcon = new NotifyIcon
            {
                Icon = new Icon(ModesClass.IconName),
                Visible = true,
                Text = ModesClass.ModeName
            };

            ContextMenuStrip contextMenuStrip = new();
            ToolStripMenuItem toolStripMenuItem_close = new()
            {
                Text = "&終了"
            };
            toolStripMenuItem_close.Click += ToolStripMenuItem_close_Click;

            ToolStripMenuItem toolStripMenuItem_setting = new()
            {
                Text = "&設定変更"
            };
            toolStripMenuItem_setting.Click += ToolStripMenuItem_setting_Click;

            contextMenuStrip.Items.Add(toolStripMenuItem_close);
            contextMenuStrip.Items.Add(toolStripMenuItem_setting);
            
            notifyIcon.ContextMenuStrip = contextMenuStrip;


            notifyIcon.Click += NotifyIcon_Click;
            AddClipboardFormatListener(Handle);

            settingForm.Visible = false;
            
        }

        private void ToolStripMenuItem_setting_Click(object? sender, EventArgs e) 
        {
            settingForm.Visible = true;
        }

        private void ToolStripMenuItem_close_Click(object? sender, EventArgs e)
        {
            RemoveClipboardFormatListener(this.Handle);
            Properties.Settings.Default.maxCharCount = ModesClass.MaxCharCount;
            Properties.Settings.Default.Save();
            Application.Exit();
        }

        /// <summary>
        /// if Clicked MouseButtonsLeft,
        ///      Display retained clipboard text with notifBallon.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyIcon_Click(object? sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Left && globaltxt.Length > 0)
            {
                NotifyBallonFnc();
            }
        }


        /// <summary>
        /// Display retained clipboard text with notifBallon.
        /// </summary>
        private void NotifyBallonFnc()
        {
            if (notifyIcon != null) 
            {
                notifyIcon.BalloonTipTitle = "MODE:" + ModesClass.ModeName;
                notifyIcon.BalloonTipText = globaltxt;
                notifyIcon.ShowBalloonTip(5000);
            }
            
        }

        /// <summary>
        /// main function
        /// </summary>
        private void ClipFunction()
        {
            if (ModesClass.ClipBMode == Clipboardmode.NomalMode) return;
            try
            {
                if (data.GetDataPresent(typeof(string)))
                {
                    if (globaltxt == null)return;
                    
                    globaltxt = (string)Clipboard.GetData(DataFormats.Text);

                    if (ModesClass.ClipBMode == Clipboardmode.LineBreakDeleteMode || ModesClass.ClipBMode == Clipboardmode.FileMode)
                    {
                        
                        globaltxt = charLang.MyReplacement(globaltxt);
                    }
                    if (ModesClass.ClipBMode==Clipboardmode.FileMode) 
                    {
                        globaltxt = CharLangCheckClass.FileModeReplacement(globaltxt, ModesClass.MaxCharCount);   
                    }
                    
                    RemoveClipboardFormatListener(this.Handle);

                    if (globaltxt != "" && globaltxt != null)
                    {
                        Clipboard.Clear();
                        Clipboard.SetDataObject(globaltxt, true);
                        NotifyBallonFnc();
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
                    ClipFunction();
                }
                m.Result = IntPtr.Zero;
            }
            else
                base.WndProc(ref m);
        }
    }
}