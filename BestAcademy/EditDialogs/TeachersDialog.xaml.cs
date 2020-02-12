using Domain;
using Domain.Models;
using EntityFramework.Extensions;
using System.Linq;
using System.Windows;

namespace BestAcademy.EditDialogs
{
    public partial class TeachersDialog : Window
    {
        AcademyContext db { get { return DIMock.AcademyContext; } }

        public TeachersDialog()
        {
            InitializeComponent();
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            if (db.ChangeTracker.HasChanges())
                db.SaveChanges();
            Data.ItemsSource = null;
            Data.ItemsSource = db.Teachers.ToList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Data.SelectedItem == null || Data.SelectedItem as Teacher == null) return;
            var teacher = Data.SelectedItem as Teacher;
            db.PlanRecords.Where(r => r.Subject.Teacher.Id == teacher.Id).Delete();
            db.ScheduleRecords.Where(r => r.Subject.Teacher.Id == teacher.Id).Delete();
            foreach (var subject in db.Subjects.Where(r => r.Teacher.Id == teacher.Id))
                subject.Teacher = null;
            db.Teachers.Where(r => r.Id == teacher.Id).Delete();
            UpdateGrid();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new TeacherAddEditDialog();
            dialog.Title = "Добавить преподавателя";
            if (dialog.ShowDialog() == true)
            {
                var teacher = new Teacher { Name = dialog.TeacherName };
                db.Teachers.Add(teacher);
                UpdateGrid();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (Data.SelectedItem == null || Data.SelectedItem as Teacher == null) return;
            var teacher = Data.SelectedItem as Teacher;

            var dialog = new TeacherAddEditDialog();
            dialog.Title = "Редактировать преподавателя";
            dialog.TeacherName = teacher.Name;
            if (dialog.ShowDialog() == true)
            {
                teacher.Name = dialog.TeacherName;
                UpdateGrid();
            }
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}