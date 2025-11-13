using System.Windows;
using System.Windows.Controls;
using Business;
using Service;

namespace HospitalApp
{
    public partial class RoomListWindow : UserControl
    {
        private readonly RoomService _service = new();

        public RoomListWindow()
        {
            InitializeComponent();
            LoadRooms();
        }

        private void LoadRooms()
        {
            dgRooms.ItemsSource = _service.GetAll();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var win = new RoomDetailWindow();  // form thêm mới
            if (win.ShowDialog() == true)
                LoadRooms();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgRooms.SelectedItem is not Room selected)
            {
                MessageBox.Show("Please select a room to edit.");
                return;
            }

            var win = new RoomDetailWindow(selected);
            if (win.ShowDialog() == true)
                LoadRooms();
        }

        private void BtnToggle_Click(object sender, RoutedEventArgs e)
        {
            if (dgRooms.SelectedItem is not Room r)
            {
                MessageBox.Show("Please select a room.");
                return;
            }

            bool newStatus = !(r.IsAvailable ?? true);
            _service.SetAvailable(r.RoomId, newStatus);
            LoadRooms();

            MessageBox.Show($"Room {(newStatus ? "set as available" : "marked unavailable")} successfully!");
        }
    }
}
