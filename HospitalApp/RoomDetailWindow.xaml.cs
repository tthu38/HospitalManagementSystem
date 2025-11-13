using Business;
using Service;
using System.Windows;
using System.Windows.Controls;

namespace HospitalApp
{
    public partial class RoomDetailWindow : Window
    {
        private readonly RoomService _service = new();
        private readonly Room? _editingRoom;

        // Constructor mặc định (add)
        public RoomDetailWindow() : this(null) { }

        // Constructor có tham số (edit)
        public RoomDetailWindow(Room? room)
        {
            InitializeComponent();
            _editingRoom = room;

            if (_editingRoom != null)
            {
                Title = "Edit Room";
                txtNumber.Text = _editingRoom.RoomNumber;
                foreach (ComboBoxItem item in cbType.Items)
                {
                    if (item.Content.ToString() == _editingRoom.RoomType)
                    {
                        cbType.SelectedItem = item;
                        break;
                    }
                }
            }
            else
            {
                Title = "Add Room";
                cbType.SelectedIndex = 0;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumber.Text) || cbType.SelectedItem is not ComboBoxItem typeItem)
            {
                MessageBox.Show("Please fill in all fields (room number and type).");
                return;
            }

            if (_editingRoom == null)
            {
                // Thêm mới
                var r = new Room
                {
                    RoomNumber = txtNumber.Text.Trim(),
                    RoomType = typeItem.Content.ToString(),
                    IsAvailable = true
                };
                _service.Create(r);
                MessageBox.Show("Room added successfully!");
            }
            else
            {
                // Cập nhật
                _editingRoom.RoomNumber = txtNumber.Text.Trim();
                _editingRoom.RoomType = typeItem.Content.ToString();
                _service.Update(_editingRoom);
                MessageBox.Show("Room updated successfully!");
            }

            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
