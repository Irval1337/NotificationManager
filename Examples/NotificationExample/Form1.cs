using System;
using System.Windows.Forms;
using NotificationManager;

namespace NotificationExample
{
    public partial class Form1 : Form
    {
        Manager notify;

        public Form1()
        {
            InitializeComponent();
            notify = new Manager();
            notify.MaxCount = 10;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            notify.Alert("Success", NotificationType.Success);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            notify.CloseAll();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            notify.Alert("Error", NotificationType.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            notify.Alert("Warning", NotificationType.Warning);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            notify.Alert("Info", NotificationType.Info);
        }
    }
}
