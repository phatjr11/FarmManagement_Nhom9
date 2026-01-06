using System.Drawing;
using System.Windows.Forms;

namespace FarmManagement.GUI
{
    public static class GridHelper
    {
        public static void StyleGrid(DataGridView dgv)
        {
            // 1. Cấu hình chung
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.BackgroundColor = Color.White;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.RowHeadersVisible = false;
            dgv.EnableHeadersVisualStyles = false; // Để chỉnh màu Header
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;

            // 2. Font chữ
            dgv.Font = new Font("Segoe UI", 10);

            // 3. Header (Tiêu đề cột) - Màu Xanh Lá
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 204, 113); // Xanh Emerald
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.ColumnHeadersHeight = 40;

            // 4. Dòng dữ liệu
            dgv.RowTemplate.Height = 30;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245); // Xám nhạt xen kẽ

            // Màu khi chọn dòng (Màu Cam đỏ cho nổi bật)
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 76, 60);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // 5. Tự động giãn cột
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}