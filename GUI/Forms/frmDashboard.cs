using FarmManagement.BUS;
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
    public partial class frmDashboard : Form
    {
        private readonly DashboardService _service = new DashboardService();
        public frmDashboard()
        {
            InitializeComponent();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            // Cấu hình lưới
            dgvTasks.AutoGenerateColumns = false;
            SetupGrid();

            StyleGrid(dgvTasks);
            
            // Vẽ biểu đồ
            pnlChartMock.Paint += PnlChartMock_Paint;
            
            
       
            
            // Tải dữ liệu
            LoadData();
        }

        private void PnlChartMock_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int width = pnlChartMock.Width;
            int height = pnlChartMock.Height;
            int padding = 30;
            int chartWidth = width - (padding * 2);
            int chartHeight = height - (padding * 2);

            // Vẽ trục X
            Pen axisPen = new Pen(Color.FromArgb(200, 200, 200), 1);
            g.DrawLine(axisPen, padding, height - padding, width - padding, height - padding);

            // Vẽ 2 cột đơn giản (Thu và Chi)
            int barWidth = 40;
            int gap = (chartWidth - (barWidth * 2)) / 3;

            // Cột Thu (xanh lá)
            int thuHeight = 60;
            g.FillRectangle(new SolidBrush(Color.LimeGreen), 
                padding + gap, height - padding - thuHeight, barWidth, thuHeight);
            
            // Label Thu
            Font labelFont = new Font("Segoe UI", 8);
            g.DrawString("Thu", labelFont, Brushes.Gray, padding + gap + 5, height - padding + 5);

            // Cột Chi (xám)
            int chiHeight = 40;
            g.FillRectangle(new SolidBrush(Color.LightGray), 
                padding + gap * 2 + barWidth, height - padding - chiHeight, barWidth, chiHeight);
            
            // Label Chi
            g.DrawString("Chi", labelFont, Brushes.Gray, padding + gap * 2 + barWidth + 5, height - padding + 5);
        }

   
        
        private void SetupGrid()
        {
            dgvTasks.Columns.Clear();
            
            // Cấu hình các cột với chiều rộng cân đối hơn
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                HeaderText = "Nông Trại", 
                Name = "colFarm", 
                Width = 200,
                ReadOnly = true
            });
            
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                HeaderText = "Công Việc", 
                DataPropertyName = "ActivityType", 
                Width = 250,
                ReadOnly = true
            });
            
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                HeaderText = "Người Phụ Trách", 
                DataPropertyName = "ResponsiblePerson", 
                Width = 200,
                ReadOnly = true
            });
            
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                HeaderText = "Trạng Thái", 
                DataPropertyName = "Status", 
                Width = 200,
                ReadOnly = true
            });

            dgvTasks.ReadOnly = true;
            dgvTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
            // Thêm event handler để hiển thị tên nông trại
            dgvTasks.CellFormatting += dgvTasks_CellFormatting;
        }
        private void StyleGrid(DataGridView dgv)
        {
            // Cấu hình chung
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.BackgroundColor = Color.White;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.RowHeadersVisible = false;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ReadOnly = true;

            // Font chữ đẹp hơn
            dgv.Font = new Font("Segoe UI", 10);

            // Header (Tiêu đề cột) - Màu Xanh Lá
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 204, 113);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 40;

            // Rows (Dòng dữ liệu)
            dgv.RowTemplate.Height = 30;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245); // Màu xám nhạt xen kẽ
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 76, 60); // Màu Cam/Đỏ khi chọn
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;

            // Tự động giãn cột
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void LoadData()
        {
            try
            {
                // 1. Lấy số liệu tổng hợp
                var summary = _service.GetSummary();

                lblCropCount.Text = summary.TotalCrops.ToString();
                lblAnimalCount.Text = summary.TotalAnimals.ToString();
                lblLowStock.Text = summary.LowStockItems.ToString();

                decimal profit = summary.CurrentMonthRevenue - summary.CurrentMonthExpense;
                lblProfit.Text = profit.ToString("#,##0") + "đ";

                if (summary.LowStockItems > 0) lblLowStock.ForeColor = Color.Crimson;
                else lblLowStock.ForeColor = Color.LimeGreen;

                // 2. Lấy danh sách việc hôm nay
                // Dòng này bây giờ đã "thông minh" hơn, tự động trả về list đã lọc
                dgvTasks.DataSource = _service.GetTasksForToday();
            }
            catch (Exception ex)
            {
                // Không hiện MessageBox lỗi ở đây để tránh phiền khi vừa mở app
                Console.WriteLine(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvTasks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvTasks.Columns[e.ColumnIndex].Name == "colFarm")
            {
                var item = dgvTasks.Rows[e.RowIndex].DataBoundItem as FarmManagement.DAL.Activity;
                if (item != null && item.Farm != null) 
                {
                    e.Value = item.Farm.FarmName;
                }
            }
        }

        private void pnlChart_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
