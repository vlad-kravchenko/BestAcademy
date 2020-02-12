using Domain;
using Domain.Models;
using System.Windows;

namespace BestAcademy.EditDialogs
{
    public partial class SubjectAddEditDialog : Window
    {
        AcademyContext db { get { return DIMock.AcademyContext; } }

        public SubjectAddEditDialog()
        {
            InitializeComponent();
            TeachersCombo.Items.Add("");
            foreach (var teacher in db.Teachers) TeachersCombo.Items.Add(teacher);
            TeachersCombo.DisplayMemberPath = "Name";
        }

        public string SubjectName
        {
            get { return SubjectNameTb.Text; }
            set { SubjectNameTb.Text = value; }
        }

        public Teacher Teacher
        {
            get { return TeachersCombo.SelectedItem as Teacher; }
            set { TeachersCombo.SelectedItem = value; }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectName == "") return;

            DialogResult = true;
            Close();
        }
    }
}
