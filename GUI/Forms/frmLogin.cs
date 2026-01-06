using FarmManagement.BUS;
using FarmManagement.DAL;
using GUI.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GUI
{
    public partial class frmLogin : Form
    {
        // Khởi tạo SystemLogService từ tầng BUS
        private readonly SystemLogService _logService = new SystemLogService();
        // Khởi tạo UserService từ tầng BUS
        private readonly UserService _userService = new UserService();

        // Win32 API for draggability
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            LoadAssets();
            // Có thể đặt con trỏ chuột vào ô username ngay khi mở
            txtUsername.Focus();
        }

        private void LoadAssets()
        {
            try
            {
                // Path to resources relative to the project structure
                string resourcePath = System.IO.Path.Combine(Application.StartupPath, "..", "..", "Resources");
                
                string bgPath = System.IO.Path.Combine(resourcePath, "farm_bg.png");
                if (System.IO.File.Exists(bgPath)) pnlLeft.BackgroundImage = Image.FromFile(bgPath);

                string logoPath = System.IO.Path.Combine(resourcePath, "tractor_icon.png");
                if (System.IO.File.Exists(logoPath)) picLogo.Image = Image.FromFile(logoPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading assets: " + ex.Message);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text; // (Lưu ý mã hóa nếu có)

            if (string.IsNullOrWhiteSpace(username) || username == "Nhập email hoặc tên đăng nhập" ||
                string.IsNullOrWhiteSpace(password) || password == "Nhập mật khẩu")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            // 1. Gọi hàm Login để kiểm tra tài khoản
            User user = _userService.Login(username, password);

            if (user != null)
            {
                // --- BƯỚC 1: LƯU USER VÀO SESSION ---
                SessionService.CurrentUser = user;

                // --- BƯỚC 2: TẢI DANH SÁCH QUYỀN (Code mới thêm) ---
                if (user.RoleID > 0)
                {
                    SessionService.CurrentPermissions = _userService.GetPermissionsByRole(user.RoleID);
                }
                else
                {
                    SessionService.CurrentPermissions = new List<string>();
                }

                // --- BƯỚC 3: GHI LỊCH SỬ ĐĂNG NHẬP ---
                _logService.WriteLoginHistory(user.UserID, "Success");

                MessageBox.Show($"Xin chào, {user.FullName}!", "Thông báo");

                // --- BƯỚC 4: MỞ FORM CHÍNH ---
                frmMain frm = new frmMain();

                // Cập nhật giao diện Main dựa trên User vừa đăng nhập
                frm.SetUser(user);

                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowPass_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
        }

        private void lblDescription_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnQR_Click(object sender, EventArgs e)
        {
            frmQRScanner qrScanner = new frmQRScanner();
            qrScanner.ShowDialog();
        }

        private void lnkForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Vui lòng liên hệ quản trị viên hệ thống để khôi phục mật khẩu.\nEmail: admin@farm.com\nHotline: 1900-xxxx", 
                "Quên mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGoogle_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng đăng nhập bằng Google đang được phát triển và sẽ sớm ra mắt!", 
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMicrosoft_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng đăng nhập bằng Microsoft đang được phát triển và sẽ sớm ra mắt!", 
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lblRegister_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Để đăng ký tài khoản doanh nghiệp mới, vui lòng gửi yêu cầu đến bộ phận kinh doanh.\nTruy cập: www.farm.com/register", 
                "Đăng ký tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
