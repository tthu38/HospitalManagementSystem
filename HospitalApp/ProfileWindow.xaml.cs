using System.Windows;

namespace HospitalApp
{
    public partial class ProfileWindow : Window
    {
        public ProfileWindow(string info)
        {
            InitializeComponent();
            txtInfo.Text = info;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
