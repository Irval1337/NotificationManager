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

        Manager manager;

        private enum Action
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
                    timer1.Interval = manager.WaitingTime;
                    action = Action.close;
                    break;
                case Action.start:
                    this.timer1.Interval = manager.TimerInterval;
                    this.Opacity += 0.1;
                    if (this.Opacity == 1.0)
                        action = Action.wait;
                    else
                    {
                        switch (manager.PositionType) 
                        {
                            case NotificationPosition.Right:
                                this.Left--;
                                break;
                            case NotificationPosition.Left:
                                this.Left++;
                                break;
                            case NotificationPosition.Middle:
                                this.Top += manager.InvertAdding ? 1 : -1;
                                break;
                        }
                    }
                    
                    break;
                case Action.close:
                    timer1.Interval = manager.TimerInterval;
                    this.Opacity -= 0.1;
                    switch (manager.PositionType)
                    {
                        case NotificationPosition.Right:
                            this.Left -= 3;
                            break;
                        case NotificationPosition.Left:
                            this.Left += 3;
                            break;
                        case NotificationPosition.Middle:
                            this.Top += manager.InvertAdding ? 3 : -3;
                            break;
                    }
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
            manager = notify;
            lblMsg.Font = notify.Font;
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
                    switch (manager.PositionType)
                    {
                        case NotificationPosition.Right:
                            this.x = Screen.PrimaryScreen.WorkingArea.Width - this.Width - 15;
                            break;
                        case NotificationPosition.Left:
                            this.x = 15;
                            break;
                        case NotificationPosition.Middle:
                            this.x = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
                            break;
                    }
                    this.y = !manager.InvertAdding ? Screen.PrimaryScreen.WorkingArea.Height - this.Height * i - 5 * i : this.Height * i + 5 * i;
                    this.Location = new Point(this.x, this.y);
                    Notification.Default.nums++;
                    break;
                }
            }
            this.x = Screen.PrimaryScreen.WorkingArea.Width - base.Width - 5;

            this.button1.Image = notify.Images.Cancel;

            switch (type)
            {
                case NotificationType.Success:
                    this.pictureBox1.Image = notify.Images.Success;
                    this.BackColor = notify.Colors.Success;
                    break;
                case NotificationType.Error:
                    this.pictureBox1.Image = notify.Images.Error;
                    this.BackColor = notify.Colors.Error;
                    break;
                case NotificationType.Info:
                    this.pictureBox1.Image = notify.Images.Info;
                    this.BackColor = notify.Colors.Info;
                    break;
                case NotificationType.Warning:
                    this.pictureBox1.Image = notify.Images.Warning;
                    this.BackColor = notify.Colors.Warning;
                    break;
            }


            this.lblMsg.Text = msg;
            this.Show();
            this.action = Action.start;
            this.timer1.Interval = notify.TimerInterval;
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
            timer1.Interval = manager.TimerInterval;
            this.action = Action.close;
            e.Cancel = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = manager.TimerInterval;
            action = Action.close;
        }
    }
}
