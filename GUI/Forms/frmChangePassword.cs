using FarmManagement.BUS;
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
    public partial class frmChangePassword : Form
    {
        private readonly UserService _userService = new UserService();
        public frmChangePassword()
        {
            InitializeComponent();

            // Căn giữa màn hình khi hiện lên
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Không cho phóng to thu nhỏ
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Đổi Mật Khẩu";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 1. Validate dữ liệu
            if (string.IsNullOrWhiteSpace(txtOldPass.Text) ||
                string.IsNullOrWhiteSpace(txtNewPass.Text) ||
                string.IsNullOrWhiteSpace(txtConfirmPass.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtNewPass.Text != txtConfirmPass.Text)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Gọi Service để đổi pass
            int currentUserId = SessionService.CurrentUser.UserID; // Lấy ID người đang đăng nhập

            bool result = _userService.ChangePassword(currentUserId, txtOldPass.Text, txtNewPass.Text);

            if (result)
            {
                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Đóng form
            }
            else
            {
                MessageBox.Show("Mật khẩu cũ không đúng! Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            bool show = !chkShowPass.Checked;
            // Nếu check thì hiện chữ (UseSystemPasswordChar = false)
            // Nếu không check thì hiện sao (UseSystemPasswordChar = true)
            // Lưu ý logic ngược một chút tùy thuộc vào property bạn dùng ('PasswordChar' hay 'UseSystemPasswordChar')

            char passChar = chkShowPass.Checked ? '\0' : '*';
            txtOldPass.PasswordChar = passChar;
            txtNewPass.PasswordChar = passChar;
            txtConfirmPass.PasswordChar = passChar;
        }
    }
}
