﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NotificationManager;

namespace NotificationExample
{
    public partial class Form1 : Form
    {
        Manager notify1 = new Manager();
        Manager notify2 = new Manager();
        Manager notify3 = new Manager();
        Manager notify4 = new Manager();

        public Form1()
        {
            InitializeComponent();
            notify1.MaxTextWidth = 250;
            notify1.EnableOffset = false;
            notify1.HasHighlighting = false;
            notify1.onFinish += Finish;
            notify1.onClose += Close;

            notify2.PositionType = NotificationPosition.Right;
            notify2.InvertAdding = true;

            notify3.PositionType = NotificationPosition.Middle;

            notify4.PositionType = NotificationPosition.Middle;
            notify4.TimerInterval = 20;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            notify1.Alert("SuccessSuccessSuccessSuccess", NotificationType.Success);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            notify1.CloseAll();
            notify2.CloseAll();
            notify3.CloseAll();
            notify4.CloseAll();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            notify2.Alert("Error", NotificationType.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            notify3.Alert("Warning", NotificationType.Warning);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            notify4.Alert("Info", NotificationType.Info);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            notify1.Alert("С наступающим!", NotificationType.Custom, Color.FromArgb(255, 21, 29, 33), Properties.Resources.logo);
            notify1.StopTimer(1000);
            notify1.Alert("Happy New Year!", NotificationType.Custom, Color.FromArgb(255, 21, 29, 33), Properties.Resources.logo);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            notify1.InvertAdding = notify2.InvertAdding = notify3.InvertAdding = notify4.InvertAdding = !notify4.InvertAdding;
        }

        private void Close(object sender, EventArgs e)
        {
            File.AppendAllLines("log.txt", new List<string>() { "Closed" });
        }

        private void Finish(object sender, EventArgs e)
        {
            File.AppendAllLines("log.txt", new List<string>() { "Finished" });
        }
    }
}
