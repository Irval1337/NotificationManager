using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace NotificationManager
{
    public class Manager
    {
        /// <summary>
		/// Задержка между изменением прозрачности уведомления.
		/// </summary>
        public int TimerInterval = 1;

        /// <summary>
		/// Время нахождения уведомления в статичном состоянии с прозрачностью 100%.
		/// </summary>
        public int WaitingTime = 5000;

        /// <summary>
		/// Максимальное количество уведомлений на экране.
		/// </summary>
        public int MaxCount = 9;

        /// <summary>
		/// Шрифт текста уведомлений.
		/// </summary>
        public Font Font = new Font("Century Gothic", 12);  

        /// <summary>
		/// Цвета стандартных типов уведомлений.
		/// </summary>
        public Colors Colors = new Colors();

        /// <summary>
		/// Изображения, используемые в стандартных типах уведомлений.
		/// </summary>
        public Images Images = new Images();

        /// <summary>
		/// Если true, то уведомления отображаются на экране сверху в низ.
		/// </summary>
        public bool InvertAdding = false;

        /// <summary>
		/// Место отображения уведомлений на экране.
		/// </summary>
        public NotificationPosition PositionType = NotificationPosition.Right;

        /// <summary>
		/// Событие искусственного закрытия уведомления.
		/// </summary>
        public System.EventHandler onClose = delegate { };

        /// <summary>
		/// Событие закрытия уведомления по завершении таймера.
		/// </summary>
        public System.EventHandler onFinish = delegate { };

        /// <summary>
		/// Смещение уведомлений после закрытия предыдущего.
		/// </summary>
        public bool EnableOffset = true;

        /// <summary>
		/// Максимальная длина текста уведомления в пикселях.
		/// </summary>
        public int MaxTextWidth = 225;

        /// <summary>
		/// Если true, то при наведении мыши на кнопку уведомления, она будет подсвечиваться.
		/// </summary>
        public bool HasHighlighting = true;

        /// <summary>
		/// Отобразить новое уведомление стандартного типа на экране.
		/// </summary>
        /// <param name="msg"> Основное отображаемое сообщение. </param>
		/// <param name="type"> Тип уведомления. </param>
		/// <returns> </returns>
        public void Alert(string msg, NotificationType type)
        {
            int max = 0;
            switch (PositionType)
            {
                case NotificationPosition.Right:
                    max = Properties.Notification.Default.right;
                    break;
                case NotificationPosition.Left:
                    max = Properties.Notification.Default.left;
                    break;
                case NotificationPosition.Middle:
                    max = Properties.Notification.Default.middle;
                    break;
            }
            if (max < MaxCount)
            {
                NotificationForm frm = new NotificationForm();
                frm.showAlert(msg, type, this);
            }
        }

        /// <summary>
		/// Отобразить новое уведомление кастомного типа на экране.
		/// </summary>
        /// <param name="msg"> Основное отображаемое сообщение. </param>
		/// <param name="type"> Тип уведомления. Обязательно равен Custom. </param>
        /// <param name="color"> Цвет фона уведомления. </param>
        /// <param name="picture"> Отображаемое слева изображение. </param>
		/// <returns> </returns>
        public void Alert(string msg, NotificationType type, Color color, Image picture)
        {
            if (Properties.Notification.Default.right < MaxCount)
            {
                NotificationForm frm = new NotificationForm();
                frm.showAlert(msg, type, color, picture, this);
            }
        }

        /// <summary>
		/// Закрыть все уведомления на экране.
		/// </summary>
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

        /// <summary>
		/// Остановить все события приложения на отведенное время.
		/// </summary>
        /// <param name="Milliseconds"> Время для паузы в миллисекундах. </param>
        /// returns> </returns>
        public void StopTimer(int Milliseconds)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < Milliseconds)
                Application.DoEvents();
        }
    }

    /// <summary>
    /// Тип отображаемого уведомления.
    /// </summary>
    public enum NotificationType
    {
        /// <summary>
		/// Успешно.
		/// </summary>
        Success,

        /// <summary>
		/// Предупреждение.
		/// </summary>
        Warning,

        /// <summary>
		/// Ошибка.
		/// </summary>
        Error,

        /// <summary>
		/// Информация
		/// </summary>
        Info,

        /// <summary>
		/// Настраеваемый тип. Использовать только во второй перегрузке метода Alert
		/// </summary>
        Custom
    }

    /// <summary>
    /// Цвета стандартных типов уведомлений.
    /// </summary>
    public class Colors
    {
        /// <summary>
		/// Цвет успешных уведомлений.
		/// </summary>
        public Color Success = Color.FromArgb(255, 38, 171, 99);

        /// <summary>
		/// Цвет уведомлений об ошибке.
		/// </summary>
        public Color Error = Color.FromArgb(255, 171, 37, 54);

        /// <summary>
		/// Цвет информационный уведомлений.
		/// </summary>
        public Color Info = Color.RoyalBlue;

        /// <summary>
		/// ОЦвет уведомлений с предупреждением.
		/// </summary>
        public Color Warning = Color.DarkOrange;
    }

    /// <summary>
    /// Изображения, используемые в стандартных типах уведомлений.
    /// </summary>
    public class Images
    {
        /// <summary>
		/// Изображение в успешных уведомлениях.
		/// </summary>
        public Image Success = Properties.Resources.success;

        /// <summary>
		/// Изображение в уведомлениях об ошибке.
		/// </summary>
        public Image Error = Properties.Resources.error;

        /// <summary>
		/// Изображение в информационных уведомлениях.
		/// </summary>
        public Image Info = Properties.Resources.info;

        /// <summary>
		/// Изображение в уведомлениях с предупреждением.
		/// </summary>
        public Image Warning = Properties.Resources.warning;

        /// <summary>
		/// Изображение кнопки закрытия всех типов уведомлений.
		/// </summary>
        public Image Cancel = Properties.Resources.cancel;
    }

    /// <summary>
    /// Место отображения уведомлений на экране.
    /// </summary>
    public enum NotificationPosition
    {
        /// <summary>
		/// С правой стороны экрана.
		/// </summary>
        Right,

        /// <summary>
		/// С левой стороны экрана.
		/// </summary>
        Left,

        /// <summary>
		/// По центру экрана.
		/// </summary>
        Middle
    }
}
