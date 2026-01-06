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
    public partial class frmInventoryAddEdit : Form
    {
        private readonly InventoryService _service = new InventoryService();
        private readonly FarmService _farmService = new FarmService(); // Cần service này để load Farm

        public int? InventoryID { get; set; }
        public frmInventoryAddEdit()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void frmInventoryAddEdit_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Load Nông trại
                cmbFarm.DataSource = _farmService.GetAll();
                cmbFarm.DisplayMember = "FarmName";
                cmbFarm.ValueMember = "FarmID";

                // 2. Load loại vật tư (Gợi ý)
                cmbType.Items.AddRange(new string[] {
                    "Phân bón", "Hạt giống", "Thuốc", "Hóa chất", "Thức ăn chăn nuôi", "Dụng cụ", "Khác"
                });

                if (InventoryID.HasValue) // --- SỬA ---
                {
                    this.Text = "Cập nhật kho";
                    var item = _service.GetById(InventoryID.Value);
                    if (item != null)
                    {
                        if (item.FarmID != null) cmbFarm.SelectedValue = item.FarmID;
                        txtItemName.Text = item.ItemName;
                        cmbType.Text = item.ItemType;
                        nudQuantity.Value = item.Quantity ?? 0;
                        txtUnit.Text = item.Unit;
                        txtLocation.Text = item.Location; // Load vị trí
                        txtNotes.Text = item.Notes;
                        numUnitPrice.Value = item.UnitPrice; // Hiển thị giá
                    }
                }
                else // --- THÊM ---
                {
                    this.Text = "Nhập kho mới";
                    cmbType.SelectedIndex = 0;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtItemName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên vật tư!"); return;
                }

                Inventory item = new Inventory();
                if (InventoryID.HasValue) item.InventoryID = InventoryID.Value;

                // Gán dữ liệu từ giao diện vào đối tượng
                item.FarmID = (int)cmbFarm.SelectedValue;
                item.ItemName = txtItemName.Text;
                item.ItemType = cmbType.Text;
                item.Quantity = (int)nudQuantity.Value;
                item.Unit = txtUnit.Text;
                item.Location = txtLocation.Text;
                item.Notes = txtNotes.Text;
                item.UnitPrice = numUnitPrice.Value; // (Giả sử bạn dùng NumericUpDown cho giá)

                _service.InsertUpdate(item);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi lưu: " + ex.Message); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
