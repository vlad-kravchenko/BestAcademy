using System.Windows;

namespace BestAcademy.EditDialogs
{
    public partial class TeacherAddEditDialog : Window
    {
        public TeacherAddEditDialog()
        {
            InitializeComponent();
        }

        public string TeacherName
        {
            get { return TeacherNameTb.Text; }
            set { TeacherNameTb.Text = value; }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (TeacherName == "") return;
            DialogResult = true;
            Close();
        }
    }
}
