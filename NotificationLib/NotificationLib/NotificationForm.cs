using NotificationManager.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NotificationManager
{
    public partial class NotificationForm : Form
    {
        public NotificationForm()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
        }

        public enum Action
        {
            wait,
            start,
            close
        }

        private Action action;

        private int x, y;

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (this.action)
            {
                case Action.wait:
                    timer1.Interval = 5000;
                    action = Action.close;
                    break;
                case Action.start:
                    this.timer1.Interval = 1;
                    this.Opacity += 0.1;
                    if (this.x < this.Location.X)
                    {
                        this.Left--;
                    }
                    else
                    {
                        if (this.Opacity == 1.0)
                        {
                            action = Action.wait;
                        }
                    }
                    break;
                case Action.close:
                    timer1.Interval = 1;
                    this.Opacity -= 0.1;

                    this.Left -= 3;
                    if (base.Opacity == 0.0)
                    {
                        Notification.Default.nums--;
                        base.Dispose();
                    }
                    break;
            }
        }

        public void showAlert(string msg, NotificationType type, Manager notify)
        {
            lblMsg.Font = notify.font;
            this.Opacity = 0.0;
            this.StartPosition = FormStartPosition.Manual;
            string fname;

            for (int i = 1; i < notify.MaxCount + 1; i++)
            {
                fname = "alert" + i.ToString();
                NotificationForm frm = (NotificationForm)Application.OpenForms[fname];

                if (frm == null)
                {
                    this.Name = fname;
                    this.x = Screen.PrimaryScreen.WorkingArea.Width - this.Width + 15;
                    this.y = Screen.PrimaryScreen.WorkingArea.Height - this.Height * i - 5 * i;
                    this.Location = new Point(this.x, this.y);
                    Notification.Default.nums++;
                    break;
                }
            }
            this.x = Screen.PrimaryScreen.WorkingArea.Width - base.Width - 5;

            switch (type)
            {
                case NotificationType.Success:
                    this.pictureBox1.Image = Resources.success;
                    this.BackColor = Color.FromArgb(255, 38, 171, 99);
                    break;
                case NotificationType.Error:
                    this.pictureBox1.Image = Resources.error;
                    this.BackColor = Color.FromArgb(255, 171, 37, 54);
                    break;
                case NotificationType.Info:
                    this.pictureBox1.Image = Resources.info;
                    this.BackColor = Color.RoyalBlue;
                    break;
                case NotificationType.Warning:
                    this.pictureBox1.Image = Resources.warning;
                    this.BackColor = Color.DarkOrange;
                    break;
            }


            this.lblMsg.Text = msg;
            this.Show();
            this.action = Action.start;
            this.timer1.Interval = 1;
            this.timer1.Start();
        }

        public void showAlert(string msg, NotificationType type, Color color, Image picture, Manager notify)
        {
            this.pictureBox1.Image = picture;
            this.BackColor = color;
            showAlert(msg, type, notify);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var Params = base.CreateParams;
                Params.ExStyle |= 0x80;

                return Params;
            }
        }

        private void Notification_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Interval = 1;
            this.action = Action.close;
            e.Cancel = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1;
            action = Action.close;
        }
    }
}
