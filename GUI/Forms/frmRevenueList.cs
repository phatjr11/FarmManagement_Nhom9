using FarmManagement.BUS;
using FarmManagement.DAL;
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
    public partial class frmRevenueList : Form
    {
        private readonly RevenueService _revenueService = new RevenueService();
        public frmRevenueList()
        {
            InitializeComponent();
        }

        private void frmRevenueList_Load(object sender, EventArgs e)
        {
            dgvRevenue.AutoGenerateColumns = false; // Tắt tự động tạo cột
            SetupDataGridView();
            LoadData();
            GridHelper.StyleGrid(dgvRevenue);
        }
        // --- 1. CẤU HÌNH LƯỚI ---
        private void SetupDataGridView()
        {
            dgvRevenue.Columns.Clear();

            // Cột 0: ID (Ẩn đi)
            var colId = new DataGridViewTextBoxColumn();
            colId.DataPropertyName = "RevenueID";
            colId.HeaderText = "ID";
            colId.Visible = false;
            dgvRevenue.Columns.Add(colId);

            // Cột 1: Nông trại (Sẽ dùng CellFormatting để hiện tên)
            var colFarm = new DataGridViewTextBoxColumn();
            colFarm.Name = "colFarmName"; // Đặt tên để tí nữa gọi
            colFarm.HeaderText = "Nông Trại";
            colFarm.Width = 150;
            dgvRevenue.Columns.Add(colFarm);

            // Cột 2: Loại doanh thu
            var colType = new DataGridViewTextBoxColumn();
            colType.DataPropertyName = "RevenueType";
            colType.HeaderText = "Loại Thu";
            colType.Width = 120;
            dgvRevenue.Columns.Add(colType);

            // Cột 3: Mô tả ngắn
            var colDesc = new DataGridViewTextBoxColumn();
            colDesc.DataPropertyName = "Description";
            colDesc.HeaderText = "Mô Tả";
            colDesc.Width = 150;
            dgvRevenue.Columns.Add(colDesc);

            // Cột 4: Số tiền (Quan trọng: Format tiền tệ)
            var colAmt = new DataGridViewTextBoxColumn();
            colAmt.DataPropertyName = "Amount";
            colAmt.HeaderText = "Số Tiền (VNĐ)";
            colAmt.Width = 120;
            colAmt.DefaultCellStyle.Format = "#,##0"; // Ví dụ: 10,000,000
            colAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; // Căn phải
            dgvRevenue.Columns.Add(colAmt);

            // Cột 5: Ngày thu
            var colDate = new DataGridViewTextBoxColumn();
            colDate.DataPropertyName = "RevenueDate";
            colDate.HeaderText = "Ngày Thu";
            colDate.Width = 100;
            colDate.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvRevenue.Columns.Add(colDate);

            // Cột 6: Ghi chú
            var colNote = new DataGridViewTextBoxColumn();
            colNote.DataPropertyName = "Notes";
            colNote.HeaderText = "Ghi Chú";
            colNote.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvRevenue.Columns.Add(colNote);

            // Cấu hình chung
            dgvRevenue.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRevenue.ReadOnly = true;
        }

        // --- 2. TẢI DỮ LIỆU & TÍNH TỔNG ---
        private void LoadData()
        {
            try
            {
                var list = _revenueService.GetAll();
                dgvRevenue.DataSource = list;

                // Tính tổng tiền
                decimal total = list.Sum(x => x.Amount) ?? 0;

                // Giả sử bạn có Label tên lblTotal ở dưới đáy form
                if (lblTotal != null)
                {
                    lblTotal.Text = $"Tổng Doanh Thu: {total.ToString("#,##0")} VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void dgvRevenue_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvRevenue.Columns[e.ColumnIndex].Name == "colFarmName")
            {
                var item = dgvRevenue.Rows[e.RowIndex].DataBoundItem as Revenue;
                if (item != null && item.Farm != null)
                {
                    e.Value = item.Farm.FarmName;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmRevenueAddEdit frm = new frmRevenueAddEdit();
            frm.RevenueID = null; // Báo là thêm mới

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData(); // Load lại để thấy dòng mới và cập nhật Tổng tiền
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvRevenue.SelectedRows.Count > 0)
            {
                // Lấy ID từ cột ẩn (Cells[0])
                int id = Convert.ToInt32(dgvRevenue.SelectedRows[0].Cells[0].Value);

                frmRevenueAddEdit frm = new frmRevenueAddEdit();
                frm.RevenueID = id; // Truyền ID sang để sửa

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng doanh thu cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvRevenue.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvRevenue.SelectedRows[0].Cells[0].Value);
                string desc = dgvRevenue.SelectedRows[0].Cells[3].Value.ToString(); // Lấy mô tả để hiện thông báo cho rõ

                if (MessageBox.Show($"Bạn có chắc muốn xóa khoản thu '{desc}' không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (_revenueService.Delete(id))
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
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData(); 
        }
    }
}
