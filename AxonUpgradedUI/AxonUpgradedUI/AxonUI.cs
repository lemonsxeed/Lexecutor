using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxonUpgradedUI
{
    public partial class AxonUI : Form
    {
        AxonDLL DLL = new AxonDLL();
        Point lastPoint;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void FormMoveable_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        
        public AxonUI()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized; 
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) 
            {
                TopMost = true;
            }
            else 
            {
                TopMost = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                Opacity = 0.50;
            }
            else
            {
                Opacity = 1;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to test the dll?\nCheck the developer console for results.","Test DLL.",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes) 
            {

            }
            else 
            {

            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void AxonUI_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DLL.LoadDllIntoRoblox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (AxonDLL.NamedPipeExist(AxonDLL.luapipename)) 
            {
                DLL.ExecFromLuaPipe(richTextBox1.Text, AxonDLL.luapipename);
            }
            else 
            {
                MessageBox.Show(AxonDLL.dllhackname +  " needs to be injected before execute?", "Please Inject!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName(AxonDLL.dllhackname))
            process.Kill();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName(AxonDLL.dllhackname).Length == 1)
                MessageBox.Show(AxonDLL.dllhackname + " is Currently Launched.", "Game Process: ON");
            else
                MessageBox.Show(AxonDLL.dllhackname + " is Not Launched.", "Game Process: OFF");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (File.Exists(AxonDLL.dllhackname))
                MessageBox.Show(AxonDLL.dllhackname + " Exists in the Process.", "DLL Exists: YES");
            else
                MessageBox.Show(AxonDLL.dllhackname + " Doesn't Exists in the Process.", "DLL Exists: NO");
        }
    }
}
