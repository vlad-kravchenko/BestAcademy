using Domain;
using Domain.Models;
using EntityFramework.Extensions;
using System.Linq;
using System.Windows;

namespace BestAcademy.EditDialogs
{
    public partial class LessonsDialog : Window
    {
        AcademyContext db { get { return DIMock.AcademyContext; } }

        public LessonsDialog()
        {
            InitializeComponent();
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            if (db.ChangeTracker.HasChanges())
                db.SaveChanges();
            Data.ItemsSource = null;
            Data.ItemsSource = db.Lessons.ToList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Data.SelectedItem == null || Data.SelectedItem as Lesson == null) return;
            var lesson = Data.SelectedItem as Lesson;
            db.ScheduleRecords.Where(r => r.Lesson.Id == lesson.Id).Delete();
            db.Lessons.Where(r => r.Id == lesson.Id).Delete();
            UpdateGrid();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new LessonAddEditDialog();
            dialog.Title = "Добавить занятие";
            if (dialog.ShowDialog() == true)
            {
                var lesson = new Lesson { Day = dialog.Day, Start = dialog.Time };
                db.Lessons.Add(lesson);
                UpdateGrid();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (Data.SelectedItem == null || Data.SelectedItem as Lesson == null) return;
            var lesson = Data.SelectedItem as Lesson;

            var dialog = new LessonAddEditDialog();
            dialog.Title = "Редактировать занятие";
            dialog.Day = lesson.Day;
            dialog.Time = lesson.Start;
            if (dialog.ShowDialog() == true)
            {
                lesson.Day = dialog.Day;
                lesson.Start = dialog.Time;
                UpdateGrid();
            }
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}