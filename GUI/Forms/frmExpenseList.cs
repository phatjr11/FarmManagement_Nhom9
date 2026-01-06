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
    public partial class frmExpenseList : Form
    {
        private readonly ExpenseService _service = new ExpenseService();
        public frmExpenseList()
        {
            InitializeComponent();
        }

        private void frmExpenseList_Load(object sender, EventArgs e)
        {
            dgvExpense.AutoGenerateColumns = false;
            SetupGrid();
            LoadData();
            GridHelper.StyleGrid(dgvExpense);
        }
        private void SetupGrid()
        {
            dgvExpense.Columns.Clear();

            // ID ẩn
            dgvExpense.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ExpenseID", Visible = false });

            // Nông trại (Xử lý CellFormatting)
            dgvExpense.Columns.Add(new DataGridViewTextBoxColumn { Name = "colFarmName", HeaderText = "Nông Trại", Width = 150 });

            // Các cột dữ liệu
            dgvExpense.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ExpenseType", HeaderText = "Loại Chi", Width = 120 });
            dgvExpense.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Mô tả", Width = 150 });

            // Cột Tiền (Format tiền tệ)
            var colAmt = new DataGridViewTextBoxColumn { DataPropertyName = "Amount", HeaderText = "Số Tiền", Width = 120 };
            colAmt.DefaultCellStyle.Format = "#,##0";
            colAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvExpense.Columns.Add(colAmt);

            dgvExpense.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ExpenseDate", HeaderText = "Ngày Chi", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
            dgvExpense.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Notes", HeaderText = "Ghi chú", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            dgvExpense.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvExpense.ReadOnly = true;
        }

        private void LoadData()
        {
            try
            {
                // 1. Tải dữ liệu
                var list = _service.GetAll(); // hoặc _revenueService
                dgvExpense.DataSource = list;

                // 2. Tính tổng (Dùng Cast để an toàn tuyệt đối với SQL DECIMAL null)
                // Cách viết này ép kiểu từng dòng trước khi cộng, tránh mọi lỗi xung đột
                decimal total = list.Sum(x => (decimal?)x.Amount) ?? 0;

                // 3. Debug bằng MessageBox (Kiểm tra xem tính toán có ra số không)
                // Nếu hiện hộp thoại số tiền -> Code tính đúng.
                // MessageBox.Show("Tổng tính được là: " + total.ToString()); 

                // 4. Gán lên Label (BỎ lệnh if check null đi)
                // Nếu lblTotal chưa đặt tên đúng, dòng này sẽ Crash ngay -> Để mình biết mà sửa
                lblTotal.Text = $"Tổng Chi Phí: {total.ToString("#,##0")} VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chi tiết: " + ex.ToString());
            }
        }

        private void dgvExpense_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvExpense.Columns[e.ColumnIndex].Name == "colFarmName")
            {
                var item = dgvExpense.Rows[e.RowIndex].DataBoundItem as Expens;
                if (item != null && item.Farm != null)
                {
                    e.Value = item.Farm.FarmName;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmExpenseAddEdit frm = new frmExpenseAddEdit();
            if (frm.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvExpense.SelectedRows.Count > 0)
            {
                int id = (int)dgvExpense.SelectedRows[0].Cells[0].Value;
                frmExpenseAddEdit frm = new frmExpenseAddEdit();
                frm.ExpenseID = id;
                if (frm.ShowDialog() == DialogResult.OK) LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvExpense.SelectedRows.Count > 0)
            {
                int id = (int)dgvExpense.SelectedRows[0].Cells[0].Value;
                if (MessageBox.Show("Bạn có chắc muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _service.Delete(id);
                    LoadData();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
