using System;
using System.Windows.Forms;

namespace GUI.Forms
{
    public partial class frmSellAnimal : Form
    {
        // Các thuộc tính public để frmAnimalList truy cập
        public int Quantity { get; private set; }
        public decimal PricePerUnit { get; private set; }
        public DateTime SelectedDate { get; private set; }

        private int _maxQuantity;

        public frmSellAnimal(string title, int maxQuantity)
        {
            InitializeComponent();

            this.Text = "Bán vật nuôi";
            lblTitle.Text = title;
            _maxQuantity = maxQuantity;

            // Setup NumericUpDown Số lượng
            nudQuantity.Minimum = 1;
            nudQuantity.Maximum = maxQuantity > 0 ? maxQuantity : 1;
            nudQuantity.Value = 1;

            // Setup NumericUpDown Giá
            nudPrice.Maximum = 10000000000; // 10 tỷ
            nudPrice.ThousandsSeparator = true;
            nudPrice.Increment = 10000;

            dtpDate.Value = DateTime.Now;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (nudQuantity.Value > _maxQuantity)
            {
                MessageBox.Show($"Số lượng bán không được vượt quá số lượng tồn kho ({_maxQuantity})!");
                return;
            }

            if (nudPrice.Value <= 0)
            {
                var confirm = MessageBox.Show("Bạn đang để giá bán là 0. Bạn có chắc không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.No) return;
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