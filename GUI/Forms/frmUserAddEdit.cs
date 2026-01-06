using FarmManagement.BUS;
using FarmManagement.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.Forms
{
    public partial class frmUserAddEdit : Form
    {
        private readonly UserService _service = new UserService();
        public int? UserID { get; set; }
        public frmUserAddEdit()
        {
            InitializeComponent();
        }

        private void frmUserAddEdit_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Load danh sách Quyền vào ComboBox
                cmbRole.DataSource = _service.GetAllRoles();
                cmbRole.DisplayMember = "RoleName"; // Hiển thị tên (VD: Staff_Crops)
                cmbRole.ValueMember = "RoleID";     // Giá trị ngầm là ID

                if (UserID.HasValue) // --- CHẾ ĐỘ SỬA ---
                {
                    this.Text = "Cập nhật tài khoản";
                    var u = _service.GetById(UserID.Value);
                    if (u != null)
                    {
                        txtFullName.Text = u.FullName;
                        txtUsername.Text = u.Username;
                        txtUsername.Enabled = false; // Không cho sửa tên đăng nhập

                        txtPassword.Text = ""; // Để trống
                        //txtPassword.PlaceholderText = "Nhập để đổi mật khẩu mới";

                        if (u.RoleID != 0) cmbRole.SelectedValue = u.RoleID;
                        txtEmail.Text = u.Email;

                        // Xử lý IsActive (Cột BIT trong SQL có thể null, nên cần kiểm tra)
                        chkActive.Checked = u.IsActive.GetValueOrDefault(true);
                    }
                }
                else // --- CHẾ ĐỘ THÊM ---
                {
                    this.Text = "Thêm tài khoản mới";
                    chkActive.Checked = true; // Mặc định là hoạt động
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi load: " + ex.Message); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate dữ liệu
                if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    MessageBox.Show("Vui lòng nhập Tên đăng nhập và Họ tên!"); return;
                }

                // Nếu là thêm mới thì bắt buộc nhập pass
                if (!UserID.HasValue && string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu cho tài khoản mới!"); return;
                }

                User u = new User();
                if (UserID.HasValue) u.UserID = UserID.Value;

                u.FullName = txtFullName.Text;
                u.Username = txtUsername.Text;
                // Password sẽ được truyền riêng vào hàm Service để xử lý logic
                u.RoleID = (int)cmbRole.SelectedValue;
                u.Email = txtEmail.Text;
                u.IsActive = chkActive.Checked;

                // Gọi hàm Service
                _service.InsertUpdate(u, txtPassword.Text);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi lưu: " + ex.Message); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
