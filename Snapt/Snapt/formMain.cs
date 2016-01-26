using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snapt
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
            int id = 0;
            RegisterHotKey(this.Handle, id, (int)KeyModifier.Alt, Keys.A.GetHashCode());
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0312)
            {
                show();
            }
        }

        void minimized()
        {
            lblCoords.Hide();
            this.TopMost = false;
            this.WindowState = FormWindowState.Minimized;
        }

        void show()
        {
            lblCoords.Show();
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;
        }

        void updateLabel()
        {
            lblCoords.Location = new Point(Cursor.Position.X + 5, Cursor.Position.Y + 5);
            lblCoords.Text = Cursor.Position.X.ToString() + ", " + Cursor.Position.Y.ToString();
        }

        private void formMain_MouseMove(object sender, MouseEventArgs e)
        {
            updateLabel();
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            this.TransparencyKey = Color.Turquoise;
            this.BackColor = Color.Turquoise;
            minimized();
        }

        private void lblCoords_MouseEnter(object sender, EventArgs e)
        {
            updateLabel();
        }

        private void niMain_MouseClick(object sender, MouseEventArgs e)
        {
            show();
        }

        private void formMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                minimized();
            }
        }

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, 0);
        }
    }
}
