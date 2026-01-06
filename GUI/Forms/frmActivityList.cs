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
    public partial class frmActivityList : Form
    {
        private readonly ActivityService _activityService = new ActivityService();
        public frmActivityList()
        {
            InitializeComponent();
        }

        private void frmActivityList_Load(object sender, EventArgs e)
        {
            dgvActivity.AutoGenerateColumns = false;
            SetupGrid();
            LoadData();
            GridHelper.StyleGrid(dgvActivity);
        }
        private void SetupGrid()
        {
            dgvActivity.Columns.Clear();

            // ID (Ẩn)
            dgvActivity.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ActivityID", Visible = false });

            // Nông trại
            dgvActivity.Columns.Add(new DataGridViewTextBoxColumn { Name = "colFarmName", HeaderText = "Nông Trại", Width = 150 });

            // Loại & Mô tả
            dgvActivity.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ActivityType", HeaderText = "Loại HĐ", Width = 120 });
            dgvActivity.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Description", HeaderText = "Mô tả chi tiết", Width = 200 });

            // Ngày tháng
            dgvActivity.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StartDate", HeaderText = "Bắt đầu", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
            dgvActivity.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "EndDate", HeaderText = "Kết thúc", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });

            // Người phụ trách & Trạng thái
            dgvActivity.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ResponsiblePerson", HeaderText = "Người phụ trách", Width = 130 });
            dgvActivity.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Trạng thái", Width = 100 });

            dgvActivity.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvActivity.ReadOnly = true;
        }

        private void LoadData()
        {
            dgvActivity.DataSource = _activityService.GetAll();
        }

        private void dgvActivity_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
            // 1. Hiển thị tên nông trại
            if (dgvActivity.Columns[e.ColumnIndex].Name == "colFarmName")
            {
                var item = dgvActivity.Rows[e.RowIndex].DataBoundItem as Activity;
                if (item != null && item.Farm != null)
                {
                    e.Value = item.Farm.FarmName;
                }
            }
            
            // 2. (Tùy chọn) Tô màu cho trạng thái "Hoàn thành"
            
            if (dgvActivity.Columns[e.ColumnIndex].DataPropertyName == "Status")
            {
                if (e.Value != null && e.Value.ToString() == "Hoàn thành")
                {
                    e.CellStyle.ForeColor = Color.Green;
                    e.CellStyle.Font = new Font(dgvActivity.Font, FontStyle.Bold);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmActivityAddEdit frm = new frmActivityAddEdit();
            if (frm.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvActivity.SelectedRows.Count > 0)
            {
                int id = (int)dgvActivity.SelectedRows[0].Cells[0].Value;
                frmActivityAddEdit frm = new frmActivityAddEdit();
                frm.ActivityID = id;
                if (frm.ShowDialog() == DialogResult.OK) LoadData();
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvActivity.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvActivity.SelectedRows[0].Cells[0].Value);
                string name = dgvActivity.SelectedRows[0].Cells[2].Value.ToString(); // Cột tên cây

                if (MessageBox.Show($"Bạn có chắc chắn muốn xóa cây '{name}' không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (_activityService.Delete(id))
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
    }
}
