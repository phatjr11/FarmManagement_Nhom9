using System;
using System.Collections.Generic;

namespace FarmManagement.BUS
{
    // Class này dùng để chứa kết quả tính toán doanh thu/chi phí
    public class ReportDTO
    {
        public decimal TotalRevenue { get; set; } // Tổng thu
        public decimal TotalExpense { get; set; } // Tổng chi
        public decimal Profit { get; set; }       // Lợi nhuận

        // Danh sách chi tiết để hiển thị lên bảng
        public List<ReportDetail> Details { get; set; }

        public ReportDTO()
        {
            Details = new List<ReportDetail>();
        }
    }

    public class ReportDetail
    {
        public string Type { get; set; }  // Ví dụ: Doanh thu, Chi phí
        public string Name { get; set; }  // Ví dụ: Bán lúa, Mua phân
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}