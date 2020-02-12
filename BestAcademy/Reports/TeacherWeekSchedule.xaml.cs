using Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace BestAcademy.Reports
{
    public partial class TeacherWeekSchedule : Window
    {
        AcademyContext db { get { return DIMock.AcademyContext; } }

        public TeacherWeekSchedule(int id)
        {
            InitializeComponent();
            var teacher = db.Teachers.Find(id);
            if (teacher == null) return;

            TeacherName.Text = teacher.Name;
            List<TeacherScheduleViewModel> items = new List<TeacherScheduleViewModel>();
            var records = db.ScheduleRecords.Include(r => r.Group).Include(r => r.Lesson).Include(r => r.Subject).Include(r => r.Subject.Teacher)
                .Where(r => r.Subject.Teacher.Id == id).ToList();
            foreach(var lesson in db.Lessons.ToList())
            {
                var item = new TeacherScheduleViewModel
                {
                    Day = lesson.Day,
                    Start = lesson.Start
                };
                var record = records.FirstOrDefault(r => r.Lesson.Id == lesson.Id);
                if (record != null)
                {
                    item.Group = record.Group.Name;
                    item.Subject = record.Subject.Name;
                }
                items.Add(item);
            }
            Table.ItemsSource = items;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}