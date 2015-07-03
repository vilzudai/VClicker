using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Gma.System.MouseKeyHook;

namespace VClicker
{
    
    public partial class MainForm : Form
    {
        #region RECT for GetWindowRect User32.dll function call
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left, Top, Right, Bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public RECT(System.Drawing.Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom) { }

            public int X
            {
                get { return Left; }
                set { Right -= (Left - value); Left = value; }
            }

            public int Y
            {
                get { return Top; }
                set { Bottom -= (Top - value); Top = value; }
            }

            public int Height
            {
                get { return Bottom - Top; }
                set { Bottom = value + Top; }
            }

            public int Width
            {
                get { return Right - Left; }
                set { Right = value + Left; }
            }

            public System.Drawing.Point Location
            {
                get { return new System.Drawing.Point(Left, Top); }
                set { X = value.X; Y = value.Y; }
            }

            public System.Drawing.Size Size
            {
                get { return new System.Drawing.Size(Width, Height); }
                set { Width = value.Width; Height = value.Height; }
            }

            public static implicit operator System.Drawing.Rectangle(RECT r)
            {
                return new System.Drawing.Rectangle(r.Left, r.Top, r.Width, r.Height);
            }

            public static implicit operator RECT(System.Drawing.Rectangle r)
            {
                return new RECT(r);
            }

            public static bool operator ==(RECT r1, RECT r2)
            {
                return r1.Equals(r2);
            }

            public static bool operator !=(RECT r1, RECT r2)
            {
                return !r1.Equals(r2);
            }

            public bool Equals(RECT r)
            {
                return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
            }

            public override bool Equals(object obj)
            {
                if (obj is RECT)
                    return Equals((RECT)obj);
                else if (obj is System.Drawing.Rectangle)
                    return Equals(new RECT((System.Drawing.Rectangle)obj));
                return false;
            }

            public override int GetHashCode()
            {
                return ((System.Drawing.Rectangle)this).GetHashCode();
            }

            public override string ToString()
            {
                return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
            }
        }
        #endregion

        #region User32.dll functions definitions
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr A_0, int A_1, int A_2, int A_3);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(Point Point);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);
        #endregion

        static int WM_CLOSE = 0x10;
        static int WM_LBUTTONDOWN = 0x201;
        static int WM_LBUTTONUP = 0x202;

        private int m_mousePosX;
        private int m_mousePosY;
        private bool m_shouldGetWindowHandle;

        private IntPtr m_windowHandle;
        private IKeyboardMouseEvents m_GlobalHook;

        #region MouseKeyHook functions
        public void Subscribe()
        {
            // Note: for the application hook, use the Hook.AppEvents() instead
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress += GlobalHookKeyPress;
        }
        public void Unsubscribe()
        {
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress -= GlobalHookKeyPress;

            //It is recommened to dispose it, so let's dispose!
            m_GlobalHook.Dispose();
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            //Console.WriteLine("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.Button, e.Timestamp);

            if (!m_shouldGetWindowHandle && e.Button == MouseButtons.Middle)
            {
                //timerCheckBox1.Checked = !timerCheckBox1.Checked;
            }

            // Choose window process from mouse position
            if (m_shouldGetWindowHandle)
            {
                Point po = new Point(e.X, e.Y);
                m_windowHandle = WindowFromPoint(po);
                m_shouldGetWindowHandle = false;

                m_mousePosX = e.X;
                m_mousePosY = e.Y;

                if (m_windowHandle != null)
                {
                    string processName = GetText(m_windowHandle);

                    LabelForProcess.Text = String.Format("We have handle for process: " + processName);
                }
            }
            
            
        }
        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            //Console.WriteLine("KeyPress: \t{0}", e.KeyChar);
        }
        #endregion
        public static string GetText(IntPtr hWnd)
        {
            // Allocate correct string length first
            int length = GetWindowTextLength(hWnd);
            StringBuilder sb = new StringBuilder(length + 1);
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Subscribe();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_shouldGetWindowHandle = true;
        }

        public static void clickWindow(IntPtr hWnd, int x, int y)
        {
            RECT rct = new RECT();

            if (!GetWindowRect(hWnd, out rct))
            {
                return;
            }

            //Subtract window location from mouse hook's position, so CreateLParam creates correct position for click to happen, in relation to window location
            int newX = x - rct.Left;
            int newY = y - rct.Top;

            SendMessage(hWnd, WM_LBUTTONDOWN, 1, CreateLParam(newX, newY));
            SendMessage(hWnd, WM_LBUTTONUP, 0, CreateLParam(newX, newY));
        }

        private static int CreateLParam(int LoWord, int HiWord)
        {
            return (int)((HiWord << 16) | (LoWord & 0xffff));
        }

        public static void close(IntPtr hWnd)
        {
            SendMessage(hWnd, WM_CLOSE, 0, 0);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (timerCheckBox1.Checked)
            {
                clickerTimer1.Start();
            }

        }

        private void numericUpDownTimer_ValueChanged(object sender, EventArgs e)
        {
            if((int)numericUpDownTimer.Value > 0)
                clickerTimer1.Interval = (int)numericUpDownTimer.Value;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(timerCheckBox1.Checked)
            {
                clickWindow(m_windowHandle, m_mousePosX, m_mousePosY);
            }
        }
    }
}
