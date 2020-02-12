using Domain;
using Domain.Models;
using EntityFramework.Extensions;
using System.Linq;
using System.Windows;

namespace BestAcademy.EditDialogs
{
    public partial class GroupsDialog : Window
    {
        AcademyContext db { get { return DIMock.AcademyContext; } }

        public GroupsDialog()
        {
            InitializeComponent();
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            if (db.ChangeTracker.HasChanges())
                db.SaveChanges();
            Data.ItemsSource = null;
            Data.ItemsSource = db.Groups.ToList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Data.SelectedItem == null || Data.SelectedItem as Group == null) return;
            var group = Data.SelectedItem as Group;
            db.PlanRecords.Where(r => r.Group.Id == group.Id).Delete();
            db.ScheduleRecords.Where(r => r.Group.Id == group.Id).Delete();
            db.Groups.Where(r => r.Id == group.Id).Delete();
            UpdateGrid();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new GroupAddEditDialog();
            dialog.Title = "Добавить группу";
            if (dialog.ShowDialog() == true)
            {
                var group = new Group { Name = dialog.GroupName };
                db.Groups.Add(group);
                UpdateGrid();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (Data.SelectedItem == null || Data.SelectedItem as Group == null) return;
            var group = Data.SelectedItem as Group;

            var dialog = new GroupAddEditDialog();
            dialog.Title = "Редактировать группу";
            dialog.GroupName = group.Name;
            if (dialog.ShowDialog() == true)
            {
                group.Name = dialog.GroupName;
                UpdateGrid();
            }
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}