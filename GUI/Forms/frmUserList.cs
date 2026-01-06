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
    public partial class frmUserList : Form
    {
        private readonly UserService _service = new UserService();
        public frmUserList()
        {
            InitializeComponent();
        }

        private void frmUserList_Load(object sender, EventArgs e)
        {
            dgvUsers.AutoGenerateColumns = false;
            SetupGrid();
            LoadData();
            GridHelper.StyleGrid(dgvUsers);
        }
        private void SetupGrid()
        {
            dgvUsers.Columns.Clear();

            // ID (Ẩn)
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UserID", Visible = false });

            // Các cột hiển thị
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Username", HeaderText = "Tên Đăng Nhập", Width = 120 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FullName", HeaderText = "Họ Tên", Width = 150 });

            // Cột Role (Dùng CellFormatting để hiện tên Role)
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "colRole", HeaderText = "Vai Trò", Width = 120 });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Email", HeaderText = "Email", Width = 150 });

            // Cột Trạng thái (Checkbox cho IsActive)
            var colActive = new DataGridViewCheckBoxColumn();
            colActive.DataPropertyName = "IsActive";
            colActive.HeaderText = "Hoạt Động";
            colActive.Width = 80;
            dgvUsers.Columns.Add(colActive);

            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.ReadOnly = true;
        }

        private void LoadData()
        {
            dgvUsers.DataSource = _service.GetAll();
        }

        private void dgvUsers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvUsers.Columns[e.ColumnIndex].Name == "colRole")
            {
                var user = dgvUsers.Rows[e.RowIndex].DataBoundItem as User;
                // Kiểm tra user.Role có null không (do quan hệ foreign key)
                if (user != null && user.Role != null)
                {
                    e.Value = user.Role.RoleName;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmUserAddEdit frm = new frmUserAddEdit();
            if (frm.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                int id = (int)dgvUsers.SelectedRows[0].Cells[0].Value;
                frmUserAddEdit frm = new frmUserAddEdit();
                frm.UserID = id;
                if (frm.ShowDialog() == DialogResult.OK) LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                int id = (int)dgvUsers.SelectedRows[0].Cells[0].Value;
                string name = dgvUsers.SelectedRows[0].Cells[1].Value.ToString(); // Lấy tên user

                if (MessageBox.Show($"Bạn có chắc muốn xóa tài khoản '{name}' không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (_service.Delete(id))
                    {
                        MessageBox.Show("Đã xóa thành công!");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa tài khoản này (Có thể là Admin hoặc lỗi hệ thống)!");
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
