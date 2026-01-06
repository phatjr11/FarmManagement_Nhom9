using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FarmManagement.BUS;
using FarmManagement.DAL;

namespace GUI.Forms
{
    public partial class frmActivityAddEdit : Form
    {
        // Thêm service kho
        private readonly InventoryService _inventoryService = new InventoryService();
        private readonly ActivityService _activityService = new ActivityService();
        private readonly FarmService _farmService = new FarmService();

        public int? ActivityID { get; set; }
        public frmActivityAddEdit()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cmbType.Text))
                {
                    MessageBox.Show("Vui lòng chọn hoặc nhập loại hoạt động!");
                    return;
                }

                Activity act = new Activity();
                // Chuẩn bị tham số kho
                int? invID = null;
                int qty = 0;
                if (ActivityID.HasValue) act.ActivityID = ActivityID.Value;

                act.FarmID = (int)cmbFarm.SelectedValue;
                act.ActivityType = cmbType.Text;
                act.Description = txtDesc.Text;
                act.ResponsiblePerson = txtPerson.Text;
                act.Status = cmbStatus.Text;
                act.StartDate = dtpStartDate.Value;

                // Xử lý ngày kết thúc dựa vào Checkbox
                if (dtpEndDate.Checked)
                    act.EndDate = dtpEndDate.Value;
                else
                    act.EndDate = null; // Lưu NULL vào CSDL

                if (chkUseInventory.Checked)
                {
                    if (cboInventory.SelectedValue != null)
                        invID = (int)cboInventory.SelectedValue;
                    qty = (int)nudQty.Value;
                }

                // Gọi hàm InsertUpdate mới
                _activityService.InsertUpdate(act, invID, qty);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }catch (Exception ex) { MessageBox.Show("Lỗi lưu: " + ex.Message); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmActivityAddEdit_Load(object sender, EventArgs e)
        {
            try
            {
                // Load danh sách Nông trại
                cmbFarm.DataSource = _farmService.GetAll();
                cmbFarm.DisplayMember = "FarmName";
                cmbFarm.ValueMember = "FarmID";

                // Load danh sách gợi ý cho ComboBox (Bạn có thể thêm tùy ý)
                string[] types = { "Gieo trồng", "Chăm sóc", "Bón phân", "Thu hoạch", "Tưới nước", "Khác" };
                cmbType.Items.AddRange(types);

                string[] statuses = { "Đang thực hiện", "Hoàn thành", "Đã hủy", "Chờ xử lý" };
                cmbStatus.Items.AddRange(statuses);
                cmbStatus.SelectedIndex = 0;
                // Load danh sách vật tư vào cboInventory
                var supplies = _inventoryService.GetAll();
                cboInventory.DataSource = supplies;
                cboInventory.DisplayMember = "ItemName"; // Hiển thị tên
                cboInventory.ValueMember = "InventoryID";

                // Ẩn/Hiện vùng nhập kho
                chkUseInventory.CheckedChanged += (s, ev) => {
                    cboInventory.Enabled = chkUseInventory.Checked;
                    nudQty.Enabled = chkUseInventory.Checked;
                };
                chkUseInventory.Checked = false; // Mặc định tắt
                if (ActivityID.HasValue) // --- CHẾ ĐỘ SỬA ---
                {
                    this.Text = "Cập nhật Hoạt động";
                    var act = _activityService.GetById(ActivityID.Value);
                    if (act != null)
                    {
                        if (act.FarmID != 0) cmbFarm.SelectedValue = act.FarmID;
                        cmbType.Text = act.ActivityType;
                        txtDesc.Text = act.Description;
                        txtPerson.Text = act.ResponsiblePerson;
                        cmbStatus.Text = act.Status;
                        dtpStartDate.Value = act.StartDate ?? DateTime.Now;

                        // Xử lý ngày kết thúc (Nếu NULL thì bỏ tick checkbox)
                        if (act.EndDate.HasValue)
                        {
                            dtpEndDate.Value = act.EndDate.Value;
                            dtpEndDate.Checked = true;
                        }
                        else
                        {
                            dtpEndDate.Checked = false;
                        }
                    }
                }
                else // --- CHẾ ĐỘ THÊM ---
                {
                    this.Text = "Thêm mới Hoạt động";
                    dtpStartDate.Value = DateTime.Now;
                    dtpEndDate.Checked = false; // Mặc định chưa có ngày kết thúc
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi load: " + ex.Message); }
        }
    }
}
