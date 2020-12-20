using System;
using System.Drawing;
using System.Windows.Forms;
using NotificationManager;

namespace NotificationExample
{
    public partial class Form1 : Form
    {
        Notify notify;
        public Form1()
        {
            InitializeComponent();
            notify = new Notify();
            notify.MaxCount = 10;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            notify.Alert("Success", NotificationForm.enmType.Success, new Font("Century Gothic", 12));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            notify.CloseAll();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            notify.Alert("Error", NotificationForm.enmType.Error, new Font("Century Gothic", 12));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            notify.Alert("Warning", NotificationForm.enmType.Warning, new Font("Century Gothic", 12));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            notify.Alert("Info", NotificationForm.enmType.Info, new Font("Century Gothic", 12));
        }
    }
}
