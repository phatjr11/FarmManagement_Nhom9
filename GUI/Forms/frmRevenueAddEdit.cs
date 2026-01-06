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
    public partial class frmRevenueAddEdit : Form
    {
        private readonly RevenueService _service = new RevenueService();
        private readonly FarmService _farmService = new FarmService();

        public int? RevenueID { get; set; }
        public frmRevenueAddEdit()
        {
            InitializeComponent();
        }

        private void frmRevenueAddEdit_Load(object sender, EventArgs e)
        {
            // 1. Load Nông trại
            cmbFarm.DataSource = _farmService.GetAll();
            cmbFarm.DisplayMember = "FarmName";
            cmbFarm.ValueMember = "FarmID";

            // 2. Load cứng loại doanh thu (theo dữ liệu của bạn)
            cmbType.Items.AddRange(new string[] { "Bán cây trồng", "Bán động vật", "Bán vật tư", "Dịch vụ", "Khác" });

            if (RevenueID.HasValue) // Chế độ Sửa
            {
                var item = _service.GetById(RevenueID.Value);
                if (item != null)
                {
                    cmbFarm.SelectedValue = item.FarmID;
                    cmbType.Text = item.RevenueType;
                    nudAmount.Value = (decimal)item.Amount;
                    dtpDate.Value = (DateTime)item.RevenueDate;
                    txtDescription.Text = item.Description;
                    txtNotes.Text = item.Notes;
                }
            }
            else // Chế độ Thêm
            {
                dtpDate.Value = DateTime.Now;
                cmbType.SelectedIndex = 0;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDescription.Text))
                {
                    MessageBox.Show("Vui lòng nhập mô tả!"); return;
                }

                Revenue item = new Revenue();
                if (RevenueID.HasValue) item.RevenueID = RevenueID.Value;

                item.FarmID = (int)cmbFarm.SelectedValue;
                item.RevenueType = cmbType.Text;
                item.Amount = nudAmount.Value;
                item.RevenueDate = dtpDate.Value;
                item.Description = txtDescription.Text;
                item.Notes = txtNotes.Text;

                _service.InsertUpdate(item);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
    }
}
