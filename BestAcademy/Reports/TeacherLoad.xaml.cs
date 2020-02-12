using Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BestAcademy.Reports
{
    public partial class TeacherLoad : Window
    {
        AcademyContext db { get { return DIMock.AcademyContext; } }

        public TeacherLoad()
        {
            InitializeComponent();

            List<TeacherLoadViewModel> loadItems = new List<TeacherLoadViewModel>();
            foreach(var teacher in db.Teachers.ToList())
            {
                var schedRecords = db.ScheduleRecords.Include(r => r.Subject).Include(r => r.Subject.Teacher).Where(r => r.Subject.Teacher.Id == teacher.Id).ToList();
                var planRecords = db.PlanRecords.Include(r => r.Subject).Include(r => r.Subject.Teacher).Where(r => r.Subject.Teacher.Id == teacher.Id).ToList();
                TeacherLoadViewModel item = new TeacherLoadViewModel
                {
                    Id = teacher.Id,
                    Name = teacher.Name,
                    OveralLoadPlan = planRecords.Sum(r => r.Hours),
                    WeekLoadPlan = planRecords.Sum(r => r.Hours) / DIMock.Weeks,
                    OveralLoad = schedRecords.Count * DIMock.Weeks,
                    WeekLoad = schedRecords.Count
                };
                loadItems.Add(item);
            }
            Table.ItemsSource = loadItems;
        }

        private void Table_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Table.SelectedItem != null)
            {
                new TeacherWeekSchedule((Table.SelectedItem as TeacherLoadViewModel).Id).Show();
            }
        }
    }
}