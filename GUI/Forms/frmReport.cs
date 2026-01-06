using FarmManagement.BUS; // Gọi BUS
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FarmManagement.GUI.Forms
{
    public partial class frmReport : Form
    {
        private readonly ReportService _service = new ReportService();

        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            // Mặc định chọn từ đầu tháng này đến hôm nay
            DateTime now = DateTime.Now;
            dtpFrom.Value = new DateTime(now.Year, now.Month, 1);
            dtpTo.Value = now;

            SetupGrid();
            LoadData();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Gọi Service để lấy DTO
                var data = _service.GetReport(dtpFrom.Value, dtpTo.Value);

                // Hiển thị số liệu tổng
                lblRevenue.Text = data.TotalRevenue.ToString("#,##0") + " VNĐ";
                lblExpense.Text = data.TotalExpense.ToString("#,##0") + " VNĐ";
                lblProfit.Text = data.Profit.ToString("#,##0") + " VNĐ";

                // Đổi màu lợi nhuận
                lblProfit.ForeColor = data.Profit >= 0 ? Color.Blue : Color.Red;

                // Hiển thị danh sách chi tiết xuống bảng
                dgvReport.DataSource = data.Details;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void SetupGrid()
        {
            dgvReport.AutoGenerateColumns = false;
            dgvReport.Columns.Clear();

            // Cột Ngày
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "Date", HeaderText = "Ngày", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });

            // Cột Loại (Thu/Chi)
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "Type", HeaderText = "Loại", Width = 80 });

            // Cột Mô tả
            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = "Name", HeaderText = "Mô tả khoản tiền", Width = 200, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            // Cột Số tiền
            var colAmt = new DataGridViewTextBoxColumn
            { DataPropertyName = "Amount", HeaderText = "Số Tiền", Width = 150 };
            colAmt.DefaultCellStyle.Format = "#,##0";
            colAmt.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvReport.Columns.Add(colAmt);
        }
    }
}