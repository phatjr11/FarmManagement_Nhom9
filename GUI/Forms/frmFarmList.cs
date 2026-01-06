using FarmManagement.BUS;
using FarmManagement.GUI;
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
    public partial class frmFarmList : Form
    {
        private readonly FarmService _farmService = new FarmService();
        public frmFarmList()
        {
            InitializeComponent();
        }

        private void frmFarmList_Load(object sender, EventArgs e)
        {
            // Tắt chế độ tự tạo cột để mình tự chỉnh cho đẹp (hoặc để true nếu lười)
            dgvFarm.AutoGenerateColumns = false;

            // Cấu hình cột thủ công (Tốt nhất nên làm ở giao diện Design, nhưng code ở đây cho nhanh)
            SetupDataGridView();

            LoadData();
            GridHelper.StyleGrid(dgvFarm);
        }
        private void SetupDataGridView()
        {
            // Nếu bạn lười config cột ở giao diện Design thì dùng code này
            dgvFarm.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "FarmID", Width = 50 });
            dgvFarm.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên Nông Trại", DataPropertyName = "FarmName", Width = 200 });
            dgvFarm.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Chủ sở hữu", DataPropertyName = "Owner", Width = 150 });
            dgvFarm.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Diện tích (ha)", DataPropertyName = "Area", Width = 100 });
            dgvFarm.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Vị trí", DataPropertyName = "Location", Width = 100 });
            dgvFarm.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Số điện thoại", DataPropertyName = "Phone", Width = 100 });
            dgvFarm.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Email", DataPropertyName = "Email", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }

        private void LoadData()
        {
            // Gọi BUS để lấy danh sách
            dgvFarm.DataSource = _farmService.GetAll();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvFarm.SelectedRows.Count > 0)
            {
                int selectedFarmID = Convert.ToInt32(dgvFarm.SelectedRows[0].Cells[0].Value);

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa nông trại này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        bool result = _farmService.Delete(selectedFarmID);
                        if (result)
                        {
                            MessageBox.Show("Xóa thành công!");
                            LoadData();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message); // Hiện lỗi nếu dính khóa ngoại (đang có cây trồng)
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Tạo form mới
            frmFarmAddEdit frm = new frmFarmAddEdit();
            frm.FarmID = null; // Báo là Thêm mới

            // Hiển thị dạng Dialog (Popup) và chờ kết quả
            if (frm.ShowDialog() == DialogResult.OK)
            {
                // Nếu user bấm Lưu thành công -> Load lại lưới
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn dòng nào chưa
            if (dgvFarm.SelectedRows.Count > 0)
            {
                // Lấy ID của dòng đang chọn (Giả sử cột ID là cột đầu tiên - Cells[0])
                // Bạn cần đảm bảo tên cột hoặc chỉ số cột chính xác
                int selectedFarmID = Convert.ToInt32(dgvFarm.SelectedRows[0].Cells[0].Value);

                frmFarmAddEdit frm = new frmFarmAddEdit();
                frm.FarmID = selectedFarmID; // Truyền ID sang để sửa

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData(); // Load lại lưới sau khi sửa xong
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nông trại cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
