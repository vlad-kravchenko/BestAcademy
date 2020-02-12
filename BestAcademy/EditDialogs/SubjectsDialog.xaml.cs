using Domain;
using Domain.Models;
using EntityFramework.Extensions;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace BestAcademy.EditDialogs
{
    public partial class SubjectsDialog : Window
    {
        AcademyContext db { get { return DIMock.AcademyContext; } }

        public SubjectsDialog()
        {
            InitializeComponent();
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            if (db.ChangeTracker.HasChanges())
                db.SaveChanges();
            Data.ItemsSource = null;
            Data.ItemsSource = db.Subjects.Include(s => s.Teacher).ToList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Data.SelectedItem == null || Data.SelectedItem as Subject == null) return;
            var subject = Data.SelectedItem as Subject;
            db.PlanRecords.Where(r => r.Subject.Id == subject.Id).Delete();
            db.ScheduleRecords.Where(r => r.Subject.Id == subject.Id).Delete();
            db.Subjects.Where(r => r.Id == subject.Id).Delete();
            UpdateGrid();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SubjectAddEditDialog();
            dialog.Title = "Добавить дисциплину";
            if (dialog.ShowDialog() == true)
            {
                var subject = new Subject { Name = dialog.SubjectName, Teacher = dialog.Teacher };
                db.Subjects.Add(subject);
                UpdateGrid();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (Data.SelectedItem == null || Data.SelectedItem as Subject == null) return;
            var subject = Data.SelectedItem as Subject;

            var dialog = new SubjectAddEditDialog();
            dialog.Title = "Редактировать дисциплину";
            dialog.SubjectName = subject.Name;
            dialog.Teacher = subject.Teacher;
            if (dialog.ShowDialog() == true)
            {
                subject.Name = dialog.SubjectName;
                subject.Teacher = dialog.Teacher;
                UpdateGrid();
            }
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}