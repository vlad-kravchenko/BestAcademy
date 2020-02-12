using System.Windows;

namespace BestAcademy.EditDialogs
{
    public partial class GroupAddEditDialog : Window
    {
        public GroupAddEditDialog()
        {
            InitializeComponent();
        }

        public string GroupName
        {
            get { return GroupNameTb.Text; }
            set { GroupNameTb.Text = value; }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (GroupName == "") return;
            DialogResult = true;
            Close();
        }
    }
}
