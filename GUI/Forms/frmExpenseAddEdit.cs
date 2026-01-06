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
    public partial class frmExpenseAddEdit : Form
    {
        private readonly ExpenseService _service = new ExpenseService();
        private readonly FarmService _farmService = new FarmService();

        public int? ExpenseID { get; set; }
        public frmExpenseAddEdit()
        {
            InitializeComponent();
        }

        private void frmExpenseAddEdit_Load(object sender, EventArgs e)
        {
            try
            {
                // Load Nông trại
                cmbFarm.DataSource = _farmService.GetAll();
                cmbFarm.DisplayMember = "FarmName";
                cmbFarm.ValueMember = "FarmID";

                // Load Loại chi phí (Dựa theo dữ liệu mẫu trong ảnh của bạn)
                cmbType.Items.AddRange(new string[] {
                    "Mua hạt giống",
                    "Phân bón",
                    "Thuốc trừ sâu",
                    "Công lao động",
                    "Điện nước",
                    "Khác"
                });

                if (ExpenseID.HasValue) // Sửa
                {
                    var item = _service.GetById(ExpenseID.Value);
                    if (item != null)
                    {
                        cmbFarm.SelectedValue = item.FarmID;
                        cmbType.Text = item.ExpenseType;
                        nudAmount.Value = item.Amount ?? 0;
                        dtpDate.Value = item.ExpenseDate ?? DateTime.Now;
                        txtDesc.Text = item.Description;
                        txtNotes.Text = item.Notes;
                    }
                }
                else // Thêm
                {
                    dtpDate.Value = DateTime.Now;
                    cmbType.SelectedIndex = 0;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Expens ex = new Expens();
                if (ExpenseID.HasValue) ex.ExpenseID = ExpenseID.Value;

                ex.FarmID = (int)cmbFarm.SelectedValue;
                ex.ExpenseType = cmbType.Text;
                ex.Amount = nudAmount.Value;
                ex.ExpenseDate = dtpDate.Value;
                ex.Description = txtDesc.Text;
                ex.Notes = txtNotes.Text;

                _service.InsertUpdate(ex);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception err) { MessageBox.Show("Lỗi: " + err.Message); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
