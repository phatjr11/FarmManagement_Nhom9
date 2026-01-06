using System;
using System.Windows.Forms;

namespace GUI.Forms
{
    public partial class frmUsageInventory : Form
    {
        public int Quantity { get; private set; }
        public string ActionType { get; private set; } // "USE" hoặc "SELL"
        public decimal Price { get; private set; } // Chỉ dùng khi Bán
        public string Reason { get; private set; }

        private int _maxQty;

        public frmUsageInventory(string itemName, int maxQty, string unit)
        {
            InitializeComponent();
            this.Text = "Xuất kho vật tư";
            lblInfo.Text = $"{itemName} (Tồn: {maxQty} {unit})";
            lblUnit.Text = unit;
            _maxQty = maxQty;

            nudQuantity.Maximum = maxQty > 0 ? maxQty : 1;
            rdoUse.Checked = true; // Mặc định là Sử dụng nội bộ
            txtReason.Text = "Bón cho cây / Cho ăn...";
        }

        private void rdoSell_CheckedChanged(object sender, EventArgs e)
        {
            // Nếu chọn Bán -> Cho nhập Giá. Nếu dùng -> Khóa giá
            nudPrice.Enabled = rdoSell.Checked;
            if (!rdoSell.Checked) nudPrice.Value = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (nudQuantity.Value > _maxQty)
            {
                MessageBox.Show("Vượt quá tồn kho!");
                return;
            }

            Quantity = (int)nudQuantity.Value;
            ActionType = rdoSell.Checked ? "SELL" : "USE";
            Price = nudPrice.Value;
            Reason = txtReason.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}