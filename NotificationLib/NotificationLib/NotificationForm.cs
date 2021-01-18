using NotificationManager.Properties;
using System;
using System.Collections.Generic;
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

        public Manager manager;

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
                        if (manager.PositionType == NotificationPosition.Right)
                            Notification.Default.right--;
                        if (manager.PositionType == NotificationPosition.Left)
                            Notification.Default.left--;
                        if (manager.PositionType == NotificationPosition.Middle)
                            Notification.Default.middle--;

                        foreach (var frm in Application.OpenForms)
                        {
                            if (frm.GetType() != this.GetType())
                                continue;
                            if (manager.EnableOffset && frm != null && ((NotificationForm)frm).manager.PositionType == manager.PositionType && !frm.Equals(this))
                                ((NotificationForm)frm).ChangePosition();
                        }

                        this.Close();
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
            int Count = 1;
            bool? isInverted = null;
            bool isDisposed = false;

            foreach (var frm in Application.OpenForms)
            {
                if (frm.GetType() != this.GetType())
                    continue;
                if (frm != null && ((NotificationForm)frm).manager.PositionType == manager.PositionType && !frm.Equals(this))
                {
                    if (isInverted == null)
                        isInverted = ((NotificationForm)frm).manager.InvertAdding;
                    else if (((NotificationForm)frm).manager.InvertAdding != isInverted || isInverted != manager.InvertAdding)
                    {
                        if (manager.PositionType == NotificationPosition.Right)
                            Notification.Default.right--;
                        if (manager.PositionType == NotificationPosition.Left)
                            Notification.Default.left--;
                        if (manager.PositionType == NotificationPosition.Middle)
                            Notification.Default.middle--;
                        isDisposed = true;
                        this.Close();
                    }
                    Count++;
                }
            }
            this.Name = "alert" + (Count + 1).ToString();
            this.lblMsg.Text = msg;
            int delta = lblMsg.Width - 225;
            if (lblMsg.Width > 225)
            {
                if (lblMsg.Width >= manager.MaxTextWidth)
                {
                    delta = manager.MaxTextWidth - 225;
                    lblMsg.AutoSize = false;
                    lblMsg.Width = manager.MaxTextWidth;
                }
                this.Width += delta;
                button1.Location = new Point(button1.Location.X + delta, button1.Location.Y);
            }

            switch (manager.PositionType)
            {
                case NotificationPosition.Right:
                    this.x = Screen.PrimaryScreen.WorkingArea.Width - this.Width - 10;
                    break;
                case NotificationPosition.Left:
                    this.x = 10;
                    break;
                case NotificationPosition.Middle:
                    this.x = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
                    break;
            }
            this.y = !manager.InvertAdding ? Screen.PrimaryScreen.WorkingArea.Height - this.Height * Count - 5 * Count : this.Height * Count + 5 * Count;
            this.Location = new Point(this.x, this.y);
            if (manager.PositionType == NotificationPosition.Right)
                Notification.Default.right++;
            if (manager.PositionType == NotificationPosition.Left)
                Notification.Default.left++;
            if (manager.PositionType == NotificationPosition.Middle)
                Notification.Default.middle++;

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

            if (!isDisposed)
            {
                this.Show();
                this.action = Action.start;
                this.timer1.Interval = notify.TimerInterval;
                this.timer1.Start();
            }
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
            if (base.Opacity == 1.0)
            {
                timer1.Interval = manager.TimerInterval;
                this.action = Action.close;
                e.Cancel = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = manager.TimerInterval;
            action = Action.close;
        }

        public void ChangePosition()
        {
            if (manager.InvertAdding ? this.Location.Y > this.Height + 5 : this.Location.Y < Screen.PrimaryScreen.WorkingArea.Height - this.Height - 5)
            {
                Timer timer = new Timer();
                timer.Tag = (manager.InvertAdding && this.Location.Y != this.Height + 5) 
                    || (!manager.InvertAdding && this.Location.Y != Screen.PrimaryScreen.WorkingArea.Height - this.Height - 5) ? this.Height - 1 : 0;
                timer.Interval = 1;
                timer.Tick += ((se, evu) =>
                {
                    if ((int)timer.Tag > 0)
                    {
                        this.Location = new Point(this.Location.X, this.Location.Y - (manager.InvertAdding ? 6 : -6));
                        timer.Tag = (int)timer.Tag - 6;
                    }
                    else
                        timer.Stop();
                });
                if ((int)timer.Tag != 0)
                    this.Location = new Point(this.Location.X, this.Location.Y - (manager.InvertAdding ? 6 : -6));
                timer.Start();
            }
        }
    }
}
