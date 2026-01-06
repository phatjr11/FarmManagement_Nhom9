using FarmManagement.DAL; // Để dùng FarmModel
using System;
using System.Collections.Generic;
using System.Linq;

namespace FarmManagement.BUS
{
    public class ReportService
    {
        public ReportDTO GetReport(DateTime fromDate, DateTime toDate)
        {
            using (var db = new FarmModel())
            {
                var dto = new ReportDTO();

                // Chỉnh thời gian toDate về cuối ngày (23:59:59) để lấy đủ dữ liệu ngày đó
                DateTime endOfToDate = toDate.Date.AddDays(1).AddTicks(-1);

                // 1. Lấy dữ liệu Doanh thu (Revenue)
                var revenues = db.Revenues
                    .Where(r => r.RevenueDate >= fromDate && r.RevenueDate <= endOfToDate)
                    .ToList();

                // 2. Lấy dữ liệu Chi phí (Expenses)
                var expenses = db.Expenses
                    .Where(e => e.ExpenseDate >= fromDate && e.ExpenseDate <= endOfToDate)
                    .ToList();

                // 3. Tính tổng
                dto.TotalRevenue = revenues.Sum(r => r.Amount ?? 0);
                dto.TotalExpense = expenses.Sum(e => e.Amount ?? 0);
                dto.Profit = dto.TotalRevenue - dto.TotalExpense;

                // 4. Gộp chi tiết vào danh sách để hiện lên bảng
                // Thêm các khoản thu
                foreach (var r in revenues)
                {
                    dto.Details.Add(new ReportDetail
                    {
                        Type = "Thu",
                        Name = r.Description ?? r.RevenueType,
                        Amount = r.Amount ?? 0,
                        Date = r.RevenueDate ?? DateTime.MinValue
                    });
                }

                // Thêm các khoản chi
                foreach (var e in expenses)
                {
                    dto.Details.Add(new ReportDetail
                    {
                        Type = "Chi",
                        Name = e.Description ?? e.ExpenseType,
                        Amount = e.Amount ?? 0,
                        Date = e.ExpenseDate ?? DateTime.MinValue
                    });
                }

                // Sắp xếp theo ngày
                dto.Details = dto.Details.OrderByDescending(x => x.Date).ToList();

                return dto;
            }
        }
    }
}