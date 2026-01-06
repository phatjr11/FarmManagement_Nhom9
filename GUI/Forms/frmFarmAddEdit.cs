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
    public partial class frmFarmAddEdit : Form
    {
        private readonly FarmService _farmService = new FarmService();

        // Biến này để nhận ID từ form danh sách truyền sang
        // Nếu null = Thêm mới, Nếu có số = Sửa
        public int? FarmID { get; set; }
        public frmFarmAddEdit()
        {
            InitializeComponent();
        }

        private void frmFarmAddEdit_Load(object sender, EventArgs e)
        {
            if (FarmID.HasValue) // CHẾ ĐỘ SỬA
            {
                this.Text = "Chỉnh sửa Nông Trại";
                // 1. Gọi BUS lấy thông tin cũ
                var farm = _farmService.GetById(FarmID.Value);
                if (farm != null)
                {
                    // 2. Đổ dữ liệu lên các ô
                    txtFarmName.Text = farm.FarmName;
                    txtOwner.Text = farm.Owner;
                    txtArea.Text = farm.Area.ToString();
                    txtLocation.Text = farm.Location;
                    txtPhone.Text = farm.Phone;
                    txtEmail.Text = farm.Email;
                }
            }
            else // CHẾ ĐỘ THÊM MỚI
            {
                this.Text = "Thêm mới Nông Trại";
                txtFarmName.Focus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validate (Kiểm tra dữ liệu nhập)
                if (string.IsNullOrWhiteSpace(txtFarmName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên nông trại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFarmName.Focus();
                    return;
                }

                // 2. Tạo đối tượng Farm để lưu
                Farm farm = new Farm();

                // Nếu là sửa thì phải gán đúng ID cũ, nếu thêm thì ID tự tăng (không cần gán hoặc = 0)
                if (FarmID.HasValue)
                    farm.FarmID = FarmID.Value;

                // Lấy dữ liệu từ TextBox
                farm.FarmName = txtFarmName.Text.Trim();
                farm.Owner = txtOwner.Text.Trim();
                farm.Location = txtLocation.Text.Trim();
                farm.Phone = txtPhone.Text.Trim();
                farm.Email = txtEmail.Text.Trim();

                // Xử lý số liệu (Diện tích)
                if (float.TryParse(txtArea.Text, out float area))
                    farm.Area = area;
                else
                    farm.Area = 0; // Hoặc báo lỗi nếu bắt buộc nhập số

                // 3. Gọi BUS để Lưu
                _farmService.InsertUpdate(farm);

                MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK; // Báo cho form cha biết là đã xong
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
