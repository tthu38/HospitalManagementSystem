using System.Windows;

namespace HospitalApp
{
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            this.Close();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            var win = new RegisterWindow();
            win.ShowDialog();   // ✅ mở dạng Dialog
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            // Ví dụ: làm mới trang hoặc hiển thị thông báo
            MessageBox.Show("You are already on the Home page.");
        }


    }
}
