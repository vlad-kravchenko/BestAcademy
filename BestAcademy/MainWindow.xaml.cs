using BestAcademy.EditDialogs;
using BestAcademy.MainDialogs;
using Domain;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using BestAcademy.Controls;
using BestAcademy.Reports;

namespace BestAcademy
{
    public partial class MainWindow : Window
    {
        AcademyContext db { get { return DIMock.AcademyContext; } }
        List<Schedule> todayRecords = new List<Schedule>();

        public MainWindow()
        {
            InitializeComponent();
            DIMock.Init();
            GetTodaySchedule();
        }

        private void GetTodaySchedule()
        {
            lbToday.Items.Clear();
            todayRecords.Clear();
            string day = GetTodayDay();
            todayRecords = db.ScheduleRecords.Include(r => r.Group).Include(r => r.Lesson).Include(r => r.Subject).Include(r => r.Subject.Teacher).
                Where(r => r.Lesson.Day == day).ToList();
            if (todayRecords.Count == 0) return;
            var groups = todayRecords.GroupBy(r => r.Group.Id).OrderBy(r => r.Key);
            foreach (var group in groups)
            {
                var item = new GroupItem();
                item.GroupClasses = group.ToList();
                lbToday.Items.Add(item);
            }
        }

        private string GetTodayDay()
        {
            string day = string.Empty;
            switch (DateTime.Today.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    day = "Воскресенье";
                    break;
                case DayOfWeek.Monday:
                    day = "Понедельник";
                    break;
                case DayOfWeek.Tuesday:
                    day = "Вторник";
                    break;
                case DayOfWeek.Wednesday:
                    day = "Среда";
                    break;
                case DayOfWeek.Thursday:
                    day = "Четверг";
                    break;
                case DayOfWeek.Friday:
                    day = "Пятница";
                    break;
                case DayOfWeek.Saturday:
                    day = "Суббота";
                    break;
            }
            return day;
        }

        #region Menu

        private void Teachers_Click(object sender, RoutedEventArgs e)
        {
            new TeachersDialog().Show();
        }

        private void Subjects_Click(object sender, RoutedEventArgs e)
        {
            new SubjectsDialog().Show();
        }

        private void Groups_Click(object sender, RoutedEventArgs e)
        {
            new GroupsDialog().Show();
        }

        private void Lessons_Click(object sender, RoutedEventArgs e)
        {
            new LessonsDialog().Show();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            new Load().Show();
        }

        private void Schedule_Click(object sender, RoutedEventArgs e)
        {
            new GroupSchedule().Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            GetTodaySchedule();
        }

        private void TeacherLoad_Click(object sender, RoutedEventArgs e)
        {
            new TeacherLoad().Show();
        }

        private void Conflicts_Click(object sender, RoutedEventArgs e)
        {
            new Conflicts().Show();
        }

        #endregion
    }
}