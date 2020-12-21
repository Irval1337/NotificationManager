using System.Drawing;
using System.Windows.Forms;

namespace NotificationManager
{
    public class Manager
    {
        public int MaxCount = 9;

        public Font font = new Font("Century Gothic", 12);

        public Colors Colors;

        public void Alert(string msg, NotificationType type)
        { 
            if (Properties.Notification.Default.nums < MaxCount)
            {
                NotificationForm frm = new NotificationForm();
                frm.showAlert(msg, type, this);
            }
        }

        public void Alert(string msg, NotificationType type, Color color, Image picture)
        {
            if (Properties.Notification.Default.nums < MaxCount)
            {
                NotificationForm frm = new NotificationForm();
                frm.showAlert(msg, type, color, picture, this);
            }
        }

        public void CloseAll()
        {
            string fname;
            for (int i = 0; i < MaxCount + 1; i++)
            {
                fname = "alert" + i.ToString();
                NotificationForm frm = (NotificationForm)Application.OpenForms[fname];

                if (frm != null)
                    frm.Close();
            }
        }
    }

    public enum NotificationType
    {
        Success,
        Warning,
        Error,
        Info,
        Custom
    }

    public class Colors
    {
        public Color Success = Color.FromArgb(255, 38, 171, 99);

        public Color Error = Color.FromArgb(255, 171, 37, 54);

        public Color Info = Color.RoyalBlue;

        public Color Warning = Color.DarkOrange;
    }
}
