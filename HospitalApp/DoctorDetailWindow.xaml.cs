using System.Windows;
using Business;
using Service;

namespace HospitalApp
{
    public partial class DoctorDetailWindow : Window
    {
        private readonly DoctorService _doctorService = new();
        private readonly DepartmentService _deptService = new();
        private readonly Doctor? _editingDoctor;

        public DoctorDetailWindow() : this(null) { }

        public DoctorDetailWindow(Doctor? doctor)
        {
            InitializeComponent();

            cbDepartment.ItemsSource = _deptService.GetAll();
            cbDepartment.DisplayMemberPath = "Name";

            _editingDoctor = doctor;

            if (_editingDoctor != null)
            {
                Title = "Edit Doctor";
                txtName.Text = _editingDoctor.FullName;
                txtSpec.Text = _editingDoctor.Specialization;
                txtUsername.Text = _editingDoctor.Username;
                txtPassword.Password = _editingDoctor.Password;

                if (_editingDoctor.Department != null)
                {
                    cbDepartment.SelectedItem = _editingDoctor.Department;
                }
                else
                {
                    foreach (var item in cbDepartment.Items)
                    {
                        if (item is Department d && d.DepartmentId == _editingDoctor.DepartmentId)
                        {
                            cbDepartment.SelectedItem = d;
                            break;
                        }
                    }
                }
            }
            else
            {
                Title = "Add Doctor";
                cbDepartment.SelectedIndex = 0;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Password) ||
                cbDepartment.SelectedItem is not Department dept)
            {
                MessageBox.Show("Please fill all required fields (Name, Username, Password, Department).");
                return;
            }

            if (_editingDoctor == null)
            {
                var doctor = new Doctor
                {
                    FullName = txtName.Text.Trim(),
                    DepartmentId = dept.DepartmentId,
                    Specialization = txtSpec.Text.Trim(),
                    Username = txtUsername.Text.Trim(),
                    Password = txtPassword.Password.Trim(),
                    IsActive = true
                };
                _doctorService.Create(doctor);
                MessageBox.Show("Doctor added successfully!");
            }
            else
            {
                _editingDoctor.FullName = txtName.Text.Trim();
                _editingDoctor.DepartmentId = dept.DepartmentId;
                _editingDoctor.Specialization = txtSpec.Text.Trim();
                _editingDoctor.Username = txtUsername.Text.Trim();
                _editingDoctor.Password = txtPassword.Password.Trim();

                _doctorService.Update(_editingDoctor);
                MessageBox.Show("Doctor updated successfully!");
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
