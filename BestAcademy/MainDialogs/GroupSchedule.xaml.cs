using BestAcademy.MainDialogs;
using Domain;
using Domain.Models;
using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BestAcademy
{
    public partial class GroupSchedule : Window
    {
        List<GroupScheduleCreateViewModel> list = new List<GroupScheduleCreateViewModel>();

        AcademyContext db { get { return DIMock.AcademyContext; } }

        public GroupSchedule()
        {
            InitializeComponent();
            foreach (var group in db.Groups) GroupsCombo.Items.Add(group);
        }

        private void GroupsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            list = new List<GroupScheduleCreateViewModel>();
            UpdateTable();
            var group = GroupsCombo.SelectedItem as Group;
            var lessons = db.Lessons.ToList();
            var sched = db.ScheduleRecords.Include(p => p.Group).Include(p => p.Subject).Include(p => p.Subject.Teacher).Include(p => p.Lesson)
                .Where(s => s.Group.Id == group.Id).ToList();
            foreach (var lesson in lessons)
            {
                var item = new GroupScheduleCreateViewModel
                {
                    GroupId = group.Id,
                    LessonId = lesson.Id,
                    LessonName = lesson.ToString()
                };
                list.Add(item);
            }
            foreach(var record in sched)
            {
                var item = list.FirstOrDefault(r => r.LessonId == record.Lesson.Id);
                if (item != null)
                {
                    item.SubjectId = record.Subject.Id;
                    item.SubjectName = record.Subject.Name;
                    item.TeacherName = record.Subject.Teacher.Name;
                    item.TeacherId = record.Subject.Teacher.Id;
                }
            }
            UpdateTable();

            PlanListBox.Items.Clear();
            var planRecords = db.PlanRecords.Include(p => p.Group).Include(p => p.Subject).Include(p => p.Subject.Teacher).Where(p => p.Group.Id == group.Id).ToList();
            foreach (var planRecord in planRecords)
            {
                int count = planRecord.Hours / DIMock.Weeks;
                int countUsed = list.Count(r => r.SubjectId == planRecord.Subject.Id);
                for (int i = 0; i < count - countUsed; i++)
                {
                    PlanListBox.Items.Add(planRecord.Subject);
                    PlanListBox.DisplayMemberPath = "FullName";
                }
            }
            PlanListBox.Items.SortDescriptions.Clear();
            PlanListBox.Items.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
        }

        private void PlanListBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (PlanListBox.SelectedItem == null) return;
            DragDrop.DoDragDrop(PlanListBox, PlanListBox.SelectedItem as Subject, DragDropEffects.Copy);
        }

        private void PlanListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in Table.Items)
            {
                if (Table.ItemContainerGenerator.ContainerFromItem(item) is DataGridRow row)
                    row.Background = Brushes.White;
            }
            if (PlanListBox.SelectedItem == null) return;
            try
            {
                var group = GroupsCombo.SelectedItem as Group;
                var subject = PlanListBox.SelectedItem as Subject;
                var teacher = db.Teachers.FirstOrDefault(t => t.Id == subject.Teacher.Id);
                var subjects = db.Subjects.Where(s => s.Teacher.Id == teacher.Id).ToList();
                List<Schedule> records = new List<Schedule>();
                foreach (var sub in subjects)
                {
                    records.AddRange(db.ScheduleRecords.Where(r => r.Subject.Id == sub.Id && r.Group.Id != group.Id).ToList());
                }
                var lessons = records.Select(r => r.Lesson.Id);
                foreach (var lesson in lessons)
                {
                    var item = list.FirstOrDefault(l => l.LessonId == lesson);
                    var row = Table.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                    row.Background = Brushes.Red;
                }
                foreach (var item in list)
                {
                    if (item.TeacherId != teacher.Id) continue;
                    if (Table.ItemContainerGenerator.ContainerFromItem(item) is DataGridRow row)
                        row.Background = Brushes.Red;
                }
            }
            catch (NotSupportedException) { }
        }

        private async void Apply_Click(object sender, RoutedEventArgs e)
        {
            var group = GroupsCombo.SelectedItem as Group;
            db.ScheduleRecords.Where(r => r.Group.Id == group.Id).Delete();
            foreach(var item in list)
            {
                if (item.SubjectName == null) continue;
                var record = new Schedule
                {
                    Group = group,
                    Lesson = db.Lessons.Find(item.LessonId),
                    Subject = db.Subjects.Find(item.SubjectId)
                };
                db.ScheduleRecords.Add(record);
            }
            await db.SaveChangesAsync();
            MessageBox.Show("Готово!");
        }

        private void DataGridRow_Drop(object sender, DragEventArgs e)
        {
            var row = sender as DataGridRow;
            var rowData = row.DataContext as GroupScheduleCreateViewModel;
            var subject = ((Subject)e.Data.GetData(typeof(Subject)));
            if (subject == null || rowData.SubjectName != null) return;

            rowData.SubjectId = subject.Id;
            rowData.TeacherId = subject.Teacher.Id;
            rowData.SubjectName = subject.Name;
            rowData.TeacherName = subject.Teacher.Name;
            UpdateTable();
            PlanListBox.Items.Remove(subject);
        }

        private void DataGridRow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                var row = sender as DataGridRow;
                var rowData = row.DataContext as GroupScheduleCreateViewModel;
                if (rowData.SubjectName == null) return;

                PlanListBox.Items.Add(db.Subjects.Find(rowData.SubjectId));
                PlanListBox.Items.SortDescriptions.Clear();
                PlanListBox.Items.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
                rowData.SubjectId = -1;
                rowData.TeacherId = -1;
                rowData.SubjectName = null;
                rowData.TeacherName = null;
                UpdateTable();
            }
        }

        private void UpdateTable()
        {
            Table.ItemsSource = null;
            Table.ItemsSource = list;
        }
    }
}