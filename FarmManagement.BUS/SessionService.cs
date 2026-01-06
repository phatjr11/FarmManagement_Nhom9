using FarmManagement.DAL;
using System.Collections.Generic;
using System.Linq;

namespace FarmManagement.BUS
{
    public static class SessionService
    {
        // Lưu user đang đăng nhập
        public static User CurrentUser { get; set; }

        // Lưu danh sách quyền của user đó (để không phải truy vấn DB liên tục)
        public static List<string> CurrentPermissions { get; set; } = new List<string>();

        // Hàm gọi khi đăng nhập thành công
        public static void SetUser(User user)
        {
            CurrentUser = user;
            CurrentPermissions.Clear();

            // RoleID is int (not nullable), so just check if it's not zero (or use as is)
            if (user.RoleID != 0)
            {
                using (var db = new FarmModel())
                {
                    // Truy vấn bảng RolePermissions join với Permissions để lấy tên quyền
                    var perms = (from rp in db.RolePermissions
                                 join p in db.Permissions on rp.PermissionID equals p.PermissionID
                                 where rp.RoleID == user.RoleID
                                 select p.PermissionName).ToList();

                    CurrentPermissions.AddRange(perms);
                }
            }
        }

        // Hàm kiểm tra quyền (được gọi ở frmMain)
        public static bool HasPermission(string permissionName)
        {
            // Admin luôn đúng (nếu muốn hardcode), hoặc check trong list
            if (CurrentUser != null && CurrentUser.Role != null && CurrentUser.Role.RoleName == "Admin")
                return true;

            return CurrentPermissions.Contains(permissionName);
        }
    }
}