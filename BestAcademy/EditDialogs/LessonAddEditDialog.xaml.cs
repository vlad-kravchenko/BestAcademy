using System.Collections.Generic;
using System.Windows;

namespace BestAcademy.EditDialogs
{
    public partial class LessonAddEditDialog : Window
    {
        List<string> days = new List<string> { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };

        public LessonAddEditDialog()
        {
            InitializeComponent();
            foreach (var day in days) DayCombo.Items.Add(day);
            for (int i = 0; i < 24; i++)
            {
                HourCombo.Items.Add(i > 9 ? i.ToString() : "0" + i.ToString());
            }
            for (int i = 0; i < 60; i++)
            {
                MinuteCombo.Items.Add(i > 9 ? i.ToString() : "0" + i.ToString());
            }
        }

        public string Day
        {
            get { return DayCombo.SelectedItem as string; }
            set { DayCombo.SelectedItem = value; }
        }

        public string Time
        {
            get
            {
                return HourCombo.SelectedItem.ToString() + ":" + MinuteCombo.SelectedItem.ToString();
            }
            set
            {
                string val = value;
                HourCombo.SelectedItem = val.Split(':')[0];
                MinuteCombo.SelectedItem = val.Split(':')[1];
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (Day == null || Time == "") return;
            DialogResult = true;
            Close();
        }
    }
}
