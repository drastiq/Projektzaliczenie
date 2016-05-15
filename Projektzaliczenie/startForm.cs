using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Projektzaliczenie
{
    public partial class LogowanieMain : Form
    {
        public LogowanieMain()
        {
            InitializeComponent();
         
        }
        private static string pass;
        private static string serv;
        private static string nazwuzytkownika;

        private Icon ico;
        private bool allowClose;
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!allowClose)
            {
                this.Hide();
                e.Cancel = true;
            } else
            {

                e.Cancel = false;
            }



            //base.OnFormClosing(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ico = notifyIcon1.Icon;
        }

        private void uToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Show();
            this.ShowInTaskbar = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.allowClose = true;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            serv = this.Text;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            ftpManager ftp = new ftpManager(serv, nazwuzytkownika, pass);
            mainForm main = new mainForm();
            main.Show();
            
        }

        private void user_TextChanged(object sender, EventArgs e)
        {
            nazwuzytkownika = this.Text;
        }

        private void password_TextChanged(object sender, EventArgs e)
        {
            pass= this.Text;
        }
    }
}
