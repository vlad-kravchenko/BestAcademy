using Domain;
using Domain.Models;
using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BestAcademy.MainDialogs
{
    public partial class Load : Window
    {
        AcademyContext db { get { return DIMock.AcademyContext; } }
        int MaxHours { get { return DIMock.Weeks * db.Lessons.Count(); } }
        List<int> hours = new List<int>() { 0, 35, 70, 105, 140, 175, 210, 245, 280, 315, 350 };
        List<Plan> plans = new List<Plan>();
        int total = 0;

        public Load()
        {
            InitializeComponent();
            foreach (var group in db.Groups) GroupsCombo.Items.Add(group);
        }

        private void GroupsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.RowDefinitions.Clear();
            plans = db.PlanRecords.Include(p => p.Group).Include(p => p.Subject).ToList()
                .Where(p => p.Group.Id == (GroupsCombo.SelectedItem as Group).Id).ToList();
            int c = 0;
            var subjects = db.Subjects.Include(s => s.Teacher).ToList();
            foreach (var subject in subjects)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition());
                ComboBox box = new ComboBox();
                box.VerticalAlignment = VerticalAlignment.Center;
                box.HorizontalAlignment = HorizontalAlignment.Left;
                foreach (var hour in hours) box.Items.Add(hour);
                box.Tag = subject;
                box.Margin = new Thickness(15, 1, 1, 1);
                box.Width = 50;
                box.SelectionChanged += HoursChanged;
                MainGrid.Children.Add(box);
                Grid.SetRow(box, c);
                Grid.SetColumn(box, 0);
                if (plans.Count > 0)
                {
                    var record = plans.FirstOrDefault(p => p.Subject.Id == subject.Id);
                    box.SelectedItem = record?.Hours;
                }

                TextBlock block = new TextBlock();
                block.VerticalAlignment = VerticalAlignment.Center;
                block.HorizontalAlignment = HorizontalAlignment.Left;
                block.Text = $"{subject.Name} ({subject.Teacher?.Name})";
                MainGrid.Children.Add(block);
                Grid.SetRow(block, c);
                Grid.SetColumn(block, 1);

                c++;
            }
            RecountTotal();
        }

        private void HoursChanged(object sender, SelectionChangedEventArgs e)
        {
            RecountTotal();
            ResetSchedule(sender as ComboBox);
        }

        private void ResetSchedule(ComboBox sender)
        {
            int group = (GroupsCombo.SelectedItem as Group).Id;
            int subject = (sender.Tag as Subject).Id;
            var hours = db.PlanRecords.FirstOrDefault(r => r.Group.Id == group && r.Subject.Id == subject)?.Hours;
            int current = Convert.ToInt32(sender.SelectedItem);
            if (current < hours)
                db.ScheduleRecords.Where(r => r.Group.Id == group && r.Subject.Id == subject).Delete();
        }

        private void RecountTotal()
        {
            total = 0;
            foreach (var child in MainGrid.Children)
            {
                if (child is ComboBox && (child as ComboBox).SelectedItem != null)
                {
                    total += Convert.ToInt32((child as ComboBox).SelectedItem);
                }
            }
            Total.Text = $"Всего: {total} часов (из {MaxHours})";
            if (total > MaxHours) Total.Background = Brushes.Red;
            else Total.Background = Brushes.White;
        }

        private async void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (total > MaxHours) return;
            foreach (var child in MainGrid.Children)
            {
                if (child is ComboBox)
                {
                    var box = child as ComboBox;
                    var record = plans.FirstOrDefault(p => p.Subject.Id == (box.Tag as Subject).Id);
                    if (record == null)
                    {
                        record = new Plan
                        {
                            Subject = box.Tag as Subject,
                            Group = GroupsCombo.SelectedItem as Group
                        };
                    }
                    record.Hours = box.SelectedItem != null ? Convert.ToInt32(box.SelectedItem) : 0;
                    db.PlanRecords.AddOrUpdate(record);
                }
            }
            await db.SaveChangesAsync();
            MessageBox.Show("Готово!");
        }
    }
}
