using FarmManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FarmManagement.BUS
{
    // Class chứa dữ liệu để gửi ra màn hình
    public class DashboardDTO
    {
        public int TotalFarms { get; set; }
        public int TotalCrops { get; set; }
        public int TotalAnimals { get; set; }
        public int LowStockItems { get; set; } // Số lượng vật tư sắp hết
        public decimal CurrentMonthRevenue { get; set; } // Thu tháng này
        public decimal CurrentMonthExpense { get; set; } // Chi tháng này
    }

    public class DashboardService
    {
        // Hàm lấy số liệu thống kê
        public DashboardDTO GetSummary()
        {
            using (var db = new FarmModel())
            {
                var dto = new DashboardDTO();
                DateTime today = DateTime.Now;
                DateTime startMonth = new DateTime(today.Year, today.Month, 1);

                // 1. Đếm số lượng cơ bản
                dto.TotalFarms = db.Farms.Count();
                dto.TotalCrops = db.Crops.Count(c => c.Status == "Đang trồng"); // Chỉ đếm cây đang trồng
                dto.TotalAnimals = db.Animals.Count();

                // 2. Đếm vật tư sắp hết hàng (Ví dụ: Tồn kho < 10)
                dto.LowStockItems = db.Inventories.Count(x => x.Quantity < 10);

                // 3. Tính tiền tháng này (Fix lỗi null bằng ?? 0)
                dto.CurrentMonthRevenue = db.Revenues
                    .Where(r => r.RevenueDate >= startMonth)
                    .Sum(r => (decimal?)r.Amount) ?? 0;

                dto.CurrentMonthExpense = db.Expenses
                    .Where(e => e.ExpenseDate >= startMonth)
                    .Sum(e => (decimal?)e.Amount) ?? 0;

                return dto;
            }
        }

        // Hàm lấy danh sách việc cần làm hôm nay (Cho bảng nhắc việc)
        public List<Activity> GetTasksForToday()
        {
            using (var db = new FarmModel())
            {
                // 1. Lấy tất cả công việc chưa hoàn thành (sắp xếp việc mới nhất lên đầu)
                // Lưu ý: Dùng .Include("Farm") để lát nữa hiện tên Nông trại
                var query = db.Activities
                              .Include("Farm")
                              .Where(a => a.Status != "Hoàn thành" && a.Status != "Đã hủy")
                              .OrderByDescending(a => a.StartDate)
                              .ToList(); // Tải về bộ nhớ trước để xử lý lọc C# cho dễ

                // 2. Lấy User hiện tại
                var currentUser = SessionService.CurrentUser;
                if (currentUser == null || currentUser.Role == null)
                    return query; // Chưa đăng nhập thì cứ hiện hết (hoặc trả về rỗng tùy bạn)

                string roleName = currentUser.Role.RoleName;

                // 3. --- BẮT ĐẦU LỌC THEO VAI TRÒ ---

                // TRƯỜNG HỢP A: Ông Trồng Trọt (Staff_Crops)
                if (roleName == "Staff_Crops")
                {
                    // Chỉ lấy việc có từ khóa liên quan cây cối
                    string[] cropKeywords = { "Gieo", "Trồng", "Tưới", "Bón", "Thu hoạch", "Cây", "Lúa", "Ngô", "Rau" };

                    return query.Where(a => ContainsKeyword(a, cropKeywords)).ToList();
                }

                // TRƯỜNG HỢP B: Ông Chăn Nuôi (Staff_Animals)
                if (roleName == "Staff_Animals")
                {
                    // Chỉ lấy việc có từ khóa liên quan vật nuôi
                    string[] animalKeywords = { "Cho ăn", "Tiêm", "Vệ sinh", "Chuồng", "Gà", "Lợn", "Bò", "Vịt", "Trứng" };

                    return query.Where(a => ContainsKeyword(a, animalKeywords)).ToList();
                }

                // TRƯỜNG HỢP C: Admin, Giám sát (Staff_Activities), Kế toán
                // -> Được xem tất cả (giữ nguyên list ban đầu)
                return query;
            }
        }
        private bool ContainsKeyword(Activity act, string[] keywords)
        {
            // Kiểm tra trong Loại công việc (ActivityType) HOẶC Mô tả (Description)
            string textToCheck = (act.ActivityType + " " + act.Description).ToLower();

            foreach (var key in keywords)
            {
                if (textToCheck.Contains(key.ToLower())) return true;
            }
            return false;
        }
    }
}