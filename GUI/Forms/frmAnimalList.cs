using FarmManagement.BUS;
using FarmManagement.DAL; // Nếu cần dùng Model từ DAL
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
    public partial class frmAnimalList : Form
    {
        // Khởi tạo Service
        private readonly AnimalService _animalService = new AnimalService();

        public frmAnimalList()
        {
            InitializeComponent();
        }

        private void frmAnimalList_Load(object sender, EventArgs e)
        {
            dgvAnimal.AutoGenerateColumns = false; // Tắt tự động tạo cột
            SetupDataGridView();
            LoadData();
            // Nếu bạn có class GridHelper để style thì giữ nguyên dòng dưới
            // GridHelper.StyleGrid(dgvAnimal); 
        }

        // --- 1. CẤU HÌNH CỘT CHO LƯỚI ---
        private void SetupDataGridView()
        {
            dgvAnimal.Columns.Clear();

            // Cột ID (Ẩn)
            var colId = new DataGridViewTextBoxColumn();
            colId.DataPropertyName = "AnimalID";
            colId.HeaderText = "ID";
            colId.Visible = false;
            dgvAnimal.Columns.Add(colId);

            // Cột Tên Nông Trại
            var colFarm = new DataGridViewTextBoxColumn();
            colFarm.Name = "colFarmName";
            colFarm.HeaderText = "Thuộc Nông Trại";
            colFarm.Width = 150;
            dgvAnimal.Columns.Add(colFarm);

            // Loại vật nuôi
            var colType = new DataGridViewTextBoxColumn();
            colType.DataPropertyName = "AnimalType";
            colType.HeaderText = "Loại Vật Nuôi";
            colType.Width = 120;
            dgvAnimal.Columns.Add(colType);

            // Giống
            var colBreed = new DataGridViewTextBoxColumn();
            colBreed.DataPropertyName = "Breed";
            colBreed.HeaderText = "Giống";
            colBreed.Width = 120;
            dgvAnimal.Columns.Add(colBreed);

            // Số lượng
            var colQty = new DataGridViewTextBoxColumn();
            colQty.DataPropertyName = "Quantity";
            colQty.HeaderText = "Số Lượng";
            colQty.Width = 80;
            colQty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvAnimal.Columns.Add(colQty);

            // Ngày nhập
            var colDate = new DataGridViewTextBoxColumn();
            colDate.DataPropertyName = "PurchaseDate";
            colDate.HeaderText = "Ngày Nhập";
            colDate.DefaultCellStyle.Format = "dd/MM/yyyy";
            colDate.Width = 100;
            dgvAnimal.Columns.Add(colDate);

            // Trạng thái
            var colStatus = new DataGridViewTextBoxColumn();
            colStatus.DataPropertyName = "Status";
            colStatus.HeaderText = "Trạng Thái";
            colStatus.Width = 100;
            dgvAnimal.Columns.Add(colStatus);

            // Ghi chú
            var colNote = new DataGridViewTextBoxColumn();
            colNote.DataPropertyName = "Notes";
            colNote.HeaderText = "Ghi Chú";
            colNote.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvAnimal.Columns.Add(colNote);

            // Cấu hình chung
            dgvAnimal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAnimal.ReadOnly = true;
        }

        // --- 2. HÀM TẢI DỮ LIỆU ---
        private void LoadData()
        {
            try
            {
                dgvAnimal.DataSource = _animalService.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // --- 3. XỬ LÝ HIỂN THỊ TÊN FARM ---
        private void dgvAnimal_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dgvAnimal.Columns[e.ColumnIndex].Name == "colFarmName")
                {
                    var animal = dgvAnimal.Rows[e.RowIndex].DataBoundItem as Animal;
                    if (animal != null)
                    {
                        if (animal.Farm != null)
                            e.Value = animal.Farm.FarmName;
                        else
                            e.Value = "---";
                    }
                }
            }
            catch { }
        }

        // --- 4. CÁC NÚT CHỨC NĂNG ---
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAnimalAddEdit frm = new frmAnimalAddEdit();
            frm.AnimalID = null;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvAnimal.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvAnimal.SelectedRows[0].Cells[0].Value);
                frmAnimalAddEdit frm = new frmAnimalAddEdit();
                frm.AnimalID = id;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn vật nuôi cần sửa!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAnimal.SelectedRows.Count > 0)
            {
                // Lấy ID từ cột 0, lấy Tên loại từ cột 2 (dựa theo thứ tự add cột ở trên)
                int id = Convert.ToInt32(dgvAnimal.SelectedRows[0].Cells[0].Value);
                string type = dgvAnimal.SelectedRows[0].Cells[2].Value?.ToString();

                if (MessageBox.Show($"Bạn có chắc muốn xóa '{type}' không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (_animalService.Delete(id))
                    {
                        MessageBox.Show("Xóa thành công!");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại! Có thể dữ liệu đang được sử dụng.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        // --- 5. CHỨC NĂNG BÁN VẬT NUÔI ---
        private void btnSell_Click(object sender, EventArgs e)
        {
            // Kiểm tra chọn dòng
            if (dgvAnimal.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn vật nuôi cần bán!");
                return;
            }

            var selectedAnimal = dgvAnimal.CurrentRow.DataBoundItem as Animal;
            if (selectedAnimal == null) return;

            // Logic kiểm tra số lượng
            if (selectedAnimal.Quantity <= 0 || selectedAnimal.Status == "Đã bán")
            {
                MessageBox.Show("Đàn vật nuôi này đã hết hoặc đã bán!");
                return;
            }

            // Gọi form nhập tiền (frmInputMoney)
            // Truyền vào: Tiêu đề, Số lượng tối đa có thể bán
            using (var frm = new frmSellAnimal($"Bán: {selectedAnimal.AnimalType} - {selectedAnimal.Breed}", selectedAnimal.Quantity ?? 0))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // LƯU Ý: Hàm SellAnimal trong Service phải trả về bool và nhận 4 tham số
                    bool result = _animalService.SellAnimal(
                        selectedAnimal.AnimalID,
                        frm.Quantity,      // Lấy từ Form nhập
                        frm.PricePerUnit,  // Lấy từ Form nhập
                        frm.SelectedDate   // Lấy từ Form nhập
                    );

                    if (result)
                    {
                        MessageBox.Show($"Bán thành công {frm.Quantity} con!\nTổng thu: {(frm.Quantity * frm.PricePerUnit):N0} VNĐ", "Thông báo");

                        // Gọi LoadData() để cập nhật lại lưới (số lượng giảm đi)
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi: Không thể bán (Kiểm tra lại số lượng hoặc dữ liệu)!", "Lỗi");
                    }
                }
            }
        }
    }
}