using FarmManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net; // Để lấy IP máy tính

namespace FarmManagement.BUS
{
    public class SystemLogService
    {
        // 1. Ghi lịch sử đăng nhập
        public void WriteLoginHistory(int userId, string status)
        {
            using (var db = new FarmModel())
            {
                var log = new LoginHistory();
                log.UserID = userId;
                log.LoginTime = DateTime.Now;
                log.Status = status; // "Success" hoặc "Failed"
                log.IPAddress = GetLocalIPAddress(); // Hàm lấy IP máy

                db.LoginHistories.Add(log);
                db.SaveChanges();
            }
        }

        // 2. Ghi lịch sử thao tác dữ liệu (Thêm/Sửa/Xóa)
        public void WriteAuditLog(string tableName, string action, string desc)
        {
            try
            {
                using (var db = new FarmModel())
                {
                    // Lấy User hiện tại từ Session
                    if (SessionService.CurrentUser == null) return;

                    var log = new AuditLog();
                    log.UserID = SessionService.CurrentUser.UserID;
                    log.TableName = tableName;
                    log.Action = action; // INSERT, UPDATE, DELETE
                    log.OldValue = desc; // Tạm dùng cột này để ghi mô tả ngắn gọn
                    log.ChangedDate = DateTime.Now;

                    db.AuditLogs.Add(log);
                    db.SaveChanges();
                }
            }
            catch { /* Lỗi ghi log thì bỏ qua, không làm crash app */ }
        }

        // 3. Lấy danh sách lịch sử đăng nhập (để hiển thị lên form)
        public List<LoginHistory> GetLoginHistories()
        {
            using (var db = new FarmModel())
            {
                // Include("User") để hiện tên người đăng nhập
                return db.LoginHistories.Include("User").OrderByDescending(x => x.LoginTime).ToList();
            }
        }

        // 4. Lấy danh sách Audit Log
        public List<AuditLog> GetAuditLogs()
        {
            using (var db = new FarmModel())
            {
                return db.AuditLogs.Include("User").OrderByDescending(x => x.ChangedDate).ToList();
            }
        }

        // Hàm phụ: Lấy IP máy tính hiện tại
        private string GetLocalIPAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                return "127.0.0.1";
            }
            catch { return "Unknown"; }
        }
    }
}