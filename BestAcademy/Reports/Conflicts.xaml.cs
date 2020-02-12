using Domain;
using Domain.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace BestAcademy.Reports
{
    public partial class Conflicts : Window
    {
        AcademyContext db { get { return DIMock.AcademyContext; } }

        public Conflicts()
        {
            InitializeComponent();
            UpdateTable();
        }

        private void UpdateTable()
        {
            List<Schedule> conflicts = new List<Schedule>();
            var teachers = db.Teachers.ToList();
            foreach (var teacher in teachers)
            {
                var lessons = db.ScheduleRecords.Include(r => r.Group).Include(r => r.Lesson).Include(r => r.Subject).Include(r => r.Subject.Teacher)
                    .Where(r => r.Subject.Teacher.Id == teacher.Id).ToList();
                foreach (var lesson in lessons)
                {
                    int id = lesson.Lesson.Id;
                    if (lessons.Count(l => l.Lesson.Id == id) > 1)
                        conflicts.AddRange(lessons.Where(l => l.Lesson.Id == id).ToList());
                }
            }
            if (conflicts.Count > 0)
            {
                List<ConflictViewModel> list = new List<ConflictViewModel>();
                foreach(var conflict in conflicts)
                {
                    if (!list.Any(l=>l.ScheduleRecordId == conflict.Id))
                    {
                        var item = new ConflictViewModel
                        {
                            ScheduleRecordId = conflict.Id,
                            Group = conflict.Group.Name,
                            Lesson = conflict.Lesson.ToString(),
                            Subject = conflict.Subject.Name,
                            Teacher = conflict.Subject.Teacher.Name
                        };
                        list.Add(item);
                    }
                }
                Table.ItemsSource = list;
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            UpdateTable();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}