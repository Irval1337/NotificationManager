using System.Drawing;
using System.Windows.Forms;

namespace NotificationManager
{
    public class Notify
    {
        public int MaxCount = 9;

        public void Alert(string msg, NotificationForm.enmType type, Font font)
        {
            if (Properties.Notification.Default.nums < MaxCount)
            {
                NotificationForm frm = new NotificationForm();
                frm.showAlert(msg, type, font, MaxCount);
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
}
