using Domain;
using Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BestAcademy.Controls
{
    public partial class GroupItem : UserControl
    {
        AcademyContext db { get { return DIMock.AcademyContext; } }
        public static readonly DependencyProperty RecordProperty = DependencyProperty.Register("GroupClasses", typeof(IEnumerable<Schedule>), typeof(GroupItem));

        public IEnumerable<Schedule> GroupClasses
        {
            get
            {
                return (IEnumerable<Schedule>)GetValue(RecordProperty);
            }
            set
            {
                SetValue(RecordProperty, value);
                UpdateGraphics(value);
            }
        }

        private void UpdateGraphics(IEnumerable<Schedule> records)
        {
            GroupName.Text = records.First().Group.Name;

            string today = records.First().Lesson.Day;
            var todayLessons = db.Lessons.Where(r => r.Day == today).OrderBy(r => r.Id).ToList();
            List<GroupScheduleViewModel> items = new List<GroupScheduleViewModel>();
            foreach (var lesson in todayLessons)
            {
                var item = new GroupScheduleViewModel
                {
                    Day = lesson.Day,
                    Start = lesson.Start
                };
                var record = records.FirstOrDefault(r => r.Lesson.Id == lesson.Id);
                if (record != null)
                {
                    item.Subject = record.Subject.Name;
                    item.Teacher = record.Subject.Teacher.Name;
                }
                items.Add(item);
            }
            Table.ItemsSource = items;
        }

        public GroupItem()
        {
            InitializeComponent();
        }
    }
}
