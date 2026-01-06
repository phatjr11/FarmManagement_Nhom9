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
    public partial class frmInventoryList : Form
    {
        private readonly InventoryService _service = new InventoryService();
        public frmInventoryList()
        {
            InitializeComponent();
        }

        private void frmInventoryList_Load(object sender, EventArgs e)
        {
            dgvInventory.AutoGenerateColumns = false;
            SetupGrid();
            LoadData();
            GridHelper.StyleGrid(dgvInventory);
        }
        private void SetupGrid()
        {
            dgvInventory.Columns.Clear();

            // 0. ID (Ẩn)
            dgvInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "InventoryID", Visible = false });

            // 1. Cột Nông Trại (Quan trọng: Cần CellFormatting)
            dgvInventory.Columns.Add(new DataGridViewTextBoxColumn { Name = "colFarmName", HeaderText = "Nông Trại", Width = 150 });

            // 2. Tên vật tư
            dgvInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ItemName", HeaderText = "Tên Vật Tư", Width = 180 });

            // 3. Loại
            dgvInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ItemType", HeaderText = "Loại", Width = 120 });

            // 4. Số lượng (Căn phải)
            var colQty = new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Số Lượng", Width = 90 };
            colQty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvInventory.Columns.Add(colQty);

            // 5. Đơn vị
            dgvInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Unit", HeaderText = "Đơn Vị", Width = 70 });

            // 6. Vị trí kho (Mới thêm)
            dgvInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Location", HeaderText = "Vị Trí", Width = 100 });

            // 7. Ghi chú
            dgvInventory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Notes", HeaderText = "Ghi Chú", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            dgvInventory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInventory.ReadOnly = true;
        }

        private void LoadData()
        {
            dgvInventory.DataSource = _service.GetAll();
        }

        private void dgvInventory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvInventory.Columns[e.ColumnIndex].Name == "colFarmName")
            {
                var item = dgvInventory.Rows[e.RowIndex].DataBoundItem as Inventory;
                if (item != null && item.Farm != null)
                {
                    e.Value = item.Farm.FarmName;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmInventoryAddEdit frm = new frmInventoryAddEdit();
            if (frm.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count > 0)
            {
                int id = (int)dgvInventory.SelectedRows[0].Cells[0].Value;
                frmInventoryAddEdit frm = new frmInventoryAddEdit();
                frm.InventoryID = id;
                if (frm.ShowDialog() == DialogResult.OK) LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count > 0)
            {
                int id = (int)dgvInventory.SelectedRows[0].Cells[0].Value;
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvInventory.CurrentRow == null) return;

            var item = dgvInventory.CurrentRow.DataBoundItem as Inventory;
            if (item == null || item.Quantity <= 0)
            {
                MessageBox.Show("Hết hàng!"); return;
            }

            using (var frm = new frmUsageInventory(item.ItemName, item.Quantity ?? 0, item.Unit))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    bool result = _service.UseInventory(
                        item.InventoryID,
                        frm.Quantity,
                        frm.ActionType,
                        frm.Price,
                        frm.Reason
                    );

                    if (result)
                    {
                        MessageBox.Show("Xuất kho thành công!");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi xuất kho!");
                    }
                }
            }
        }
    }
}
