using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;


namespace ConvertibleClipboard
{

    public partial class StartForm : Form
    {
        readonly SettingForm settingForm = new();
        
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
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.SetComponents();

        }
        private void SetComponents()
        {
            
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

            ModesClass.notifyIcon.ContextMenuStrip = contextMenuStrip;


            ModesClass.notifyIcon.Click += NotifyIcon_Click;
            ModesClass.notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
            AddClipboardFormatListener(Handle);

 
            settingForm.Visible = false;

        }



        private void ToolStripMenuItem_setting_Click(object? sender, EventArgs e)
        {
            if(ModesClass.ClipBMode == Clipboardmode.LineBreakDeleteMode)
            {
                settingForm.radioButton_1.Checked = true;
            }
            else if(ModesClass.ClipBMode== Clipboardmode.FileMode) 
            {
                settingForm.radioButton_2.Checked = true;
            }
            else
            {
                settingForm.radioButton_nomal.Checked = true;
            }
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

        private void NotifyIcon_DoubleClick(object? sender,EventArgs e)
        {
            if (ModesClass.notifyIcon != null)
            {
                if (ModesClass.ClipBMode == Clipboardmode.FileMode)
                {
                    ModesClass.ClipBMode = Clipboardmode.LineBreakDeleteMode;
                    settingForm.radioButton_1.Checked = true;
                }
                else if(ModesClass.ClipBMode == Clipboardmode.LineBreakDeleteMode)
                {
                    ModesClass.ClipBMode = Clipboardmode.NomalMode;
                    settingForm.radioButton_nomal.Checked = true;
                }
                else
                {
                    ModesClass.ClipBMode = Clipboardmode.FileMode;
                    settingForm.radioButton_2.Checked = true;
                }
            }
        }

        /// <summary>
        /// Display retained clipboard text with notifBallon.
        /// </summary>
        private void NotifyBallonFnc()
        {
            if (ModesClass.notifyIcon != null)
            {
                ModesClass.notifyIcon.BalloonTipTitle = "MODE:" + ModesClass.ModeName;
                ModesClass.notifyIcon.BalloonTipText = globaltxt;
                ModesClass.notifyIcon.ShowBalloonTip(5000);
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
                    if (globaltxt == null)
                    {
                        throw new ArgumentNullException();
                    }


                    globaltxt = (string)Clipboard.GetData(DataFormats.Text);

                    if (ModesClass.ClipBMode == Clipboardmode.LineBreakDeleteMode || ModesClass.ClipBMode == Clipboardmode.FileMode)
                    {

                        globaltxt = charLang.MyReplacement(globaltxt);
                    }
                    if (ModesClass.ClipBMode == Clipboardmode.FileMode)
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
                    AddClipboardFormatListener(this.Handle);
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
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