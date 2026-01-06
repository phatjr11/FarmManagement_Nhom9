using System;
using System.Windows.Forms;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace GUI.Forms
{
    public partial class frmSellCrop : Form
    {
        // Các biến public để Form cha (List) lấy dữ liệu
        public int Quantity { get; private set; }
        public decimal PricePerUnit { get; private set; }
        public DateTime SelectedDate { get; private set; }

        private int _maxQuantity;

        public frmSellCrop(string title, int maxQuantity, string unit)
        {
            InitializeComponent();

            this.Text = "Thu hoạch & Bán nông sản";
            lblTitle.Text = title; // Ví dụ: Bán: Lúa ST25
            lblUnit.Text = $"Đơn vị: {unit}"; // Hiển thị đơn vị tính (Kg, Tạ...)

            _maxQuantity = maxQuantity;

            // Cấu hình ô Số lượng
            nudQuantity.Minimum = 1;
            nudQuantity.Maximum = maxQuantity > 0 ? maxQuantity : 1;
            nudQuantity.Value = maxQuantity > 0 ? maxQuantity : 1; // Mặc định bán hết luôn cho tiện

            // Cấu hình ô Giá
            nudPrice.Maximum = 10000000000;
            nudPrice.ThousandsSeparator = true;
            nudPrice.Increment = 1000; // Bước nhảy 1000đ

            dtpDate.Value = DateTime.Now;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (nudQuantity.Value > _maxQuantity)
            {
                MessageBox.Show($"Sản lượng bán không được vượt quá tồn kho ({_maxQuantity})!");
                return;
            }

            if (nudPrice.Value <= 0)
            {
                if (MessageBox.Show("Giá bán đang là 0 đồng. Bạn có chắc không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return;
            }

            Quantity = (int)nudQuantity.Value;
            PricePerUnit = nudPrice.Value;
            SelectedDate = dtpDate.Value;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}