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
    public partial class frmCropList : Form
    {
        // Gọi Service từ tầng BUS
        private readonly CropService _cropService = new CropService();

        public frmCropList()
        {
            InitializeComponent();
        }

        private void frmCropList_Load(object sender, EventArgs e)
        {
            // Tắt tự động tạo cột để mình tự cấu hình cho đẹp
            dgvCrop.AutoGenerateColumns = false;

            SetupDataGridView();
            LoadData();
            GridHelper.StyleGrid(dgvCrop);
        }
        // Thiết lập các cột cho lưới
        private void SetupDataGridView()
        {
            dgvCrop.Columns.Clear();

            // 0. Cột ID (Ẩn đi, dùng để xử lý Sửa/Xóa)
            var colId = new DataGridViewTextBoxColumn();
            colId.DataPropertyName = "CropID";
            colId.HeaderText = "ID";
            colId.Visible = false;
            dgvCrop.Columns.Add(colId);

            // 1. Cột Tên Nông Trại (Xử lý bằng CellFormatting)
            var colFarm = new DataGridViewTextBoxColumn();
            colFarm.Name = "colFarmName";
            colFarm.HeaderText = "Nông Trại";
            colFarm.Width = 150;
            dgvCrop.Columns.Add(colFarm);

            // 2. Tên Cây Trồng
            var colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "CropName";
            colName.HeaderText = "Tên Cây";
            colName.Width = 150;
            dgvCrop.Columns.Add(colName);

            // 3. Số lượng
            var colQty = new DataGridViewTextBoxColumn();
            colQty.DataPropertyName = "Quantity";
            colQty.HeaderText = "Số Lượng";
            colQty.Width = 80;
            colQty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // Căn phải cho số
            dgvCrop.Columns.Add(colQty);

            // 4. Đơn vị tính
            var colUnit = new DataGridViewTextBoxColumn();
            colUnit.DataPropertyName = "Unit";
            colUnit.HeaderText = "Đơn Vị";
            colUnit.Width = 80;
            dgvCrop.Columns.Add(colUnit);

            // 5. Ngày trồng
            var colPlantDate = new DataGridViewTextBoxColumn();
            colPlantDate.DataPropertyName = "PlantingDate";
            colPlantDate.HeaderText = "Ngày Trồng";
            colPlantDate.DefaultCellStyle.Format = "dd/MM/yyyy";
            colPlantDate.Width = 100;
            dgvCrop.Columns.Add(colPlantDate);

            // 6. Ngày thu hoạch dự kiến (Cột mới thêm)
            var colHarvestDate = new DataGridViewTextBoxColumn();
            colHarvestDate.DataPropertyName = "ExpectedHarvestDate";
            colHarvestDate.HeaderText = "Dự Kiến Thu Hoạch";
            colHarvestDate.DefaultCellStyle.Format = "dd/MM/yyyy";
            colHarvestDate.Width = 130;
            dgvCrop.Columns.Add(colHarvestDate);

            // 7. Trạng thái
            var colStatus = new DataGridViewTextBoxColumn();
            colStatus.DataPropertyName = "Status";
            colStatus.HeaderText = "Trạng Thái";
            colStatus.Width = 120;
            dgvCrop.Columns.Add(colStatus);

            // 8. Ghi chú (Cột mới thêm)
            var colNote = new DataGridViewTextBoxColumn();
            colNote.DataPropertyName = "Notes";
            colNote.HeaderText = "Ghi Chú";
            colNote.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Để cột này tự giãn hết phần còn lại
            dgvCrop.Columns.Add(colNote);

            // Cấu hình chung
            dgvCrop.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCrop.ReadOnly = true;
            dgvCrop.RowHeadersVisible = false; // Ẩn cột chỉ số dòng bên trái cùng cho đẹp
        }

        private void LoadData()
        {
            try
            {
                dgvCrop.DataSource = _cropService.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // SỰ KIỆN QUAN TRỌNG: Để biến đổi dữ liệu khi hiển thị
        // Bạn cần vào Design, chọn GridView, qua tab Event (hình tia sét), tìm CellFormatting và double click
        private void dgvCrop_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra nếu đang render cột "colFarmName"
            if (dgvCrop.Columns[e.ColumnIndex].Name == "colFarmName" && e.Value == null)
            {
                // Lấy đối tượng Crop tại dòng hiện tại
                var crop = dgvCrop.Rows[e.RowIndex].DataBoundItem as FarmManagement.DAL.Crop;

                if (crop != null && crop.Farm != null)
                {
                    e.Value = crop.Farm.FarmName; // Gán giá trị hiển thị là Tên Nông Trại
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmCropAddEdit frm = new frmCropAddEdit();
            frm.CropID = null; // Báo là Thêm mới

            // Nếu người dùng bấm Save và form trả về OK
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData(); // Load lại lưới để thấy dòng mới
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCrop.SelectedRows.Count > 0)
            {
                // Lấy ID từ cột ẩn (Cells[0] là cột ID mình đã add ở trên)
                int id = Convert.ToInt32(dgvCrop.SelectedRows[0].Cells[0].Value);

                frmCropAddEdit frm = new frmCropAddEdit();
                frm.CropID = id; // Truyền ID sang để sửa

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn cây trồng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCrop.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvCrop.SelectedRows[0].Cells[0].Value);
                string name = dgvCrop.SelectedRows[0].Cells[2].Value.ToString(); // Cột tên cây

                if (MessageBox.Show($"Bạn có chắc chắn muốn xóa cây '{name}' không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (_cropService.Delete(id))
                    {
                        MessageBox.Show("Xóa thành công!");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn cây trồng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra có chọn dòng nào không
            if (dgvCrop.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn loại cây trồng cần bán/thu hoạch!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Lấy dữ liệu dòng đang chọn
            var selectedCrop = dgvCrop.CurrentRow.DataBoundItem as FarmManagement.DAL.Crop;
            if (selectedCrop == null) return;

            // 3. Kiểm tra logic nghiệp vụ
            if (selectedCrop.Quantity <= 0 || selectedCrop.Status == "Đã thu hoạch")
            {
                MessageBox.Show("Sản lượng đã hết hoặc đã thu hoạch xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 4. Mở Form bán (frmSellCrop)
            // Truyền vào: Tên cây, Số lượng còn lại, Đơn vị tính
            using (var frm = new frmSellCrop($"Bán: {selectedCrop.CropName}", selectedCrop.Quantity ?? 0, selectedCrop.Unit))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // 5. Gọi Service thực hiện bán
                    bool result = _cropService.SellCrop(
                        selectedCrop.CropID,
                        frm.Quantity,
                        frm.PricePerUnit,
                        frm.SelectedDate
                    );

                    // 6. Thông báo kết quả
                    if (result)
                    {
                        MessageBox.Show($"Đã bán thành công!\nDoanh thu: {(frm.Quantity * frm.PricePerUnit):N0} VNĐ", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(); // Load lại lưới để cập nhật số lượng
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra trong quá trình xử lý!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
