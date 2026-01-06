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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        // Hàm này sẽ được gọi từ frmLogin sau khi đăng nhập thành công
        public void SetUser(User user)
        {
            // Cập nhật thông tin giao diện hiện đại
            if (lblUserName != null) lblUserName.Text = user.FullName;
            if (lblUserRole != null) lblUserRole.Text = user.Role?.RoleName ?? "Thành viên";
            
            // Cập nhật tiêu đề cũ (cho tính tương thích)
            this.Text = $"{user.FullName} ({user.Role?.RoleName})";

            // PHÂN QUYỀN DỰA TRÊN SESSION
            ApplyPermissions();
        }
        private void ApplyPermissions()
        {
            // Ẩn/Hiện các nút Sidebar dựa trên quyền
            if (btnFarms != null) btnFarms.Visible = SessionService.HasPermission("View_Farms");
            if (btnCrops != null) btnCrops.Visible = SessionService.HasPermission("View_Crops");
            if (btnAnimals != null) btnAnimals.Visible = SessionService.HasPermission("View_Animals");
            if (btnInventory != null) btnInventory.Visible = SessionService.HasPermission("View_Inventory");
            
            // New permission logic for modern sidebar
            if (btnUserList != null) btnUserList.Visible = SessionService.HasPermission("View_Users") || SessionService.CurrentUser?.Role?.RoleName == "Admin";
            if (btnRevenue != null) btnRevenue.Visible = SessionService.HasPermission("View_Revenue");
            if (btnExpense != null) btnExpense.Visible = SessionService.HasPermission("View_Expenses");
            if (btnReport != null) btnReport.Visible = SessionService.HasPermission("View_Reports") || SessionService.HasPermission("Total_Report");
            if (btnSystemLog != null) btnSystemLog.Visible = SessionService.CurrentUser?.Role?.RoleName == "Admin";
            if (btnBackup != null) btnBackup.Visible = SessionService.CurrentUser?.Role?.RoleName == "Admin";
            if (btnActivityLog != null) btnActivityLog.Visible = true; 

            // Hide legacy UI components
            if (menuStrip1 != null) menuStrip1.Visible = false;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (SessionService.CurrentUser != null)
            {
                lblDateWeather.Text = $"{DateTime.Now.ToString("dddd, dd 'tháng' MM")} • Nắng nhẹ, 28°C";
                ShowDashboard();
            }
            else
            {
                // Nếu chưa đăng nhập thì quay lại login
                Application.Restart();
            }
        }

        private void SetActiveButton(Button btn)
        {
            // Reset tất cả nút
            foreach (Control ctrl in pnlSidebar.Controls)
            {
                if (ctrl is Button b)
                {
                    b.BackColor = Color.Transparent;
                    b.ForeColor = Color.DimGray;
                }
            }
            // Highlight nút được chọn
            btn.BackColor = Color.FromArgb(215, 245, 215);
            btn.ForeColor = Color.LimeGreen;
            lblHeaderTitle.Text = btn.Text.Trim();
        }

        // Hàm dùng chung để mở form con
        private void OpenMdiChild<T>() where T : Form, new()
        {
            // 1. Tìm xem form đã mở chưa
            foreach (Form f in this.MdiChildren)
            {
                if (f is T) // Kiểm tra kiểu form
                {
                    f.Activate(); // Nếu mở rồi thì focus
                    return;
                }
            }

            // 2. Nếu chưa mở thì tạo mới
            T frm = new T();
            frm.MdiParent = this;

            // Xử lý riêng cho Dashboard nếu cần giao diện đặc biệt
            if (frm is frmDashboard)
            {
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Dock = DockStyle.Fill;
            }

            frm.Show();
        }

        private void mnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Đăng xuất khỏi hệ thống?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // 1. Xóa session
                FarmManagement.BUS.SessionService.CurrentUser = null;
                FarmManagement.BUS.SessionService.CurrentPermissions = null;

                // 2. Đóng form chính
                this.Close();

                // 3. Hiện lại form đăng nhập
                // (Lưu ý: Trong Program.cs bạn phải xử lý khéo léo, hoặc đơn giản là Restart app)
                // Cách đơn giản nhất để quay lại login sạch sẽ:
                Application.Restart();
            }
        }

        private void mnExit_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Tắt hẳn chương trình
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Đảm bảo tắt hẳn ứng dụng khi đóng form chính
            Application.Exit();
        }

        private void ShowDashboard()
        {
            if (btnDashboard != null) SetActiveButton(btnDashboard);
            OpenMdiChild<frmDashboard>();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ShowDashboard();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            mnLogout_Click(sender, e);
        }

        private void mnFarms_Click(object sender, EventArgs e)
        {
            if (btnFarms != null) SetActiveButton(btnFarms);
            OpenMdiChild<frmFarmList>();
        }

        private void mnCrops_Click(object sender, EventArgs e)
        {
            if (btnCrops != null) SetActiveButton(btnCrops);
            OpenMdiChild<frmCropList>();
        }

        private void mnAnimals_Click(object sender, EventArgs e)
        {
            if (btnAnimals != null) SetActiveButton(btnAnimals);
            OpenMdiChild<frmAnimalList>();
        }

        private void mnInventory_Click(object sender, EventArgs e)
        {
            if (btnInventory != null) SetActiveButton(btnInventory);
            OpenMdiChild<frmInventoryList>();
        }

        private void mnRevenue_Click_1(object sender, EventArgs e)
        {
            if (btnRevenue != null) SetActiveButton(btnRevenue);
            OpenMdiChild<frmRevenueList>();
        }

        private void mnExpense_Click(object sender, EventArgs e)
        {
            if (btnExpense != null) SetActiveButton(btnExpense);
            OpenMdiChild<frmExpenseList>();
        }

        private void mnUserList_Click(object sender, EventArgs e)
        {
            if (btnUserList != null) SetActiveButton(btnUserList);
            OpenMdiChild<frmUserList>();
        }

        private void mnActivities_Click(object sender, EventArgs e)
        {
            if (btnActivityLog != null) SetActiveButton(btnActivityLog);
            OpenMdiChild<frmActivityList>();
        }

        private void mnSystemLog_Click(object sender, EventArgs e)
        {
            if (btnSystemLog != null) SetActiveButton(btnSystemLog);
            // 1. Kiểm tra form đã mở chưa (Tránh mở trùng)
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "frmSystemLog")
                {
                    f.Activate();
                    return;
                }
            }

            // 2. Mở form mới
            // (Đảm bảo bạn đã tạo frmSystemLog như hướng dẫn trước đó)
            GUI.Forms.frmSystemLog frm = new GUI.Forms.frmSystemLog();
            frm.MdiParent = this;
            frm.Show();
        }

        private void mnReport_Click(object sender, EventArgs e)
        {
            if (btnReport != null) SetActiveButton(btnReport);
            // Kiểm tra form mở chưa
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "frmReport") { f.Activate(); return; }
            }

            // Nếu chưa thì mở mới
            FarmManagement.GUI.Forms.frmReport frm = new FarmManagement.GUI.Forms.frmReport();
            frm.MdiParent = this;
            frm.Show();
        }

        private void mnChangePass_Click(object sender, EventArgs e)
        {
            // Mở form dưới dạng Dialog (Cửa sổ con bắt buộc xử lý xong mới được quay lại form cha)
            GUI.Forms.frmChangePassword frm = new GUI.Forms.frmChangePassword();
            frm.ShowDialog();
        }

        private void mnBackup_Click(object sender, EventArgs e)
        {
            GUI.Forms.frmBackupRestore frm = new GUI.Forms.frmBackupRestore();
            frm.ShowDialog();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            // Mở phần cài đặt hoặc đổi mật khẩu
            mnChangePass_Click(sender, e);
        }

        private void btnQRScanner_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnQRScanner);
            // Kiểm tra form đã mở chưa
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "frmQRScanner") { f.Activate(); return; }
            }

            // Mở form QR Scanner mới
            GUI.Forms.frmQRScanner frm = new GUI.Forms.frmQRScanner();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
