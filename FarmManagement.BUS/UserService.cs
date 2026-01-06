using FarmManagement.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations; // Để dùng AddOrUpdate
using System.Linq;

namespace FarmManagement.BUS
{
    public class UserService
    {
        // 1. Lấy danh sách nhân viên (Kèm tên Quyền hạn/Role)
        public List<User> GetAll()
        {
            using (var db = new FarmModel())
            {
                // Include("Role") để lấy tên chức vụ (Admin, Staff...)
                // Lưu ý: Trong Model EF, bảng Roles thường sinh ra property tên là "Role" trong class User
                return db.Users.Include("Role").OrderBy(u => u.FullName).ToList();
            }
        }

        public User GetById(int id)
        {
            using (var db = new FarmModel())
            {
                return db.Users.FirstOrDefault(u => u.UserID == id);
            }
        }

        // 2. Lấy danh sách Quyền (Roles) để đổ vào ComboBox
        public List<Role> GetAllRoles()
        {
            using (var db = new FarmModel())
            {
                return db.Roles.ToList();
            }
        }

        // 3. Thêm hoặc Sửa
        // tham số passwordInput: Mật khẩu người dùng nhập vào (để xử lý riêng)
        public void InsertUpdate(User user, string passwordInput)
        {
            using (var db = new FarmModel())
            {
                if (user.UserID > 0) // --- SỬA ---
                {
                    var exist = db.Users.FirstOrDefault(u => u.UserID == user.UserID);
                    if (exist != null)
                    {
                        exist.FullName = user.FullName;
                        exist.RoleID = user.RoleID;
                        exist.Email = user.Email;
                        exist.IsActive = user.IsActive; // Khớp với SQL BIT

                        // LOGIC MẬT KHẨU:
                        // Nếu có nhập mật khẩu mới thì cập nhật, không thì giữ nguyên mật khẩu cũ
                        if (!string.IsNullOrEmpty(passwordInput))
                        {
                            exist.PasswordHash = passwordInput;
                            // Lưu ý: Thực tế nên mã hóa MD5/SHA tại đây trước khi gán
                        }

                        exist.UpdatedDate = DateTime.Now; // Cập nhật ngày sửa

                        db.SaveChanges();
                    }
                }
                else // --- THÊM MỚI ---
                {
                    user.CreatedDate = DateTime.Now;
                    user.PasswordHash = passwordInput; // Gán mật khẩu
                                                       // user.IsActive mặc định là true nếu form không chỉnh

                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }
        }

        public bool Delete(int id)
        {
            using (var db = new FarmModel())
            {
                var user = db.Users.FirstOrDefault(u => u.UserID == id);
                if (user == null) return false;

                // Không cho xóa tài khoản admin gốc
                if (user.Username.ToLower() == "admin") return false;

                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
        }

        // Hàm kiểm tra đăng nhập (Dùng cho form Login sau này)
        public User Login(string username, string password)
        {
            using (var db = new FarmModel())
            {
                // --- SỬA Ở ĐÂY: Thêm .Include("Role") ---
                // Để nó lấy luôn tên chức vụ đi kèm với user
                return db.Users
                         .Include("Role")
                         .FirstOrDefault(u => u.Username == username && u.PasswordHash == password && u.IsActive == true);
            }
        }
        // --- HÀM MỚI: Lấy danh sách quyền hạn ---
        public List<string> GetPermissionsByRole(int roleId)
        {
            using (var db = new FarmModel())
            {
                // Join bảng RolePermission với Permission để lấy tên quyền (PermissionName)
                var query = from rp in db.RolePermissions
                            join p in db.Permissions on rp.PermissionID equals p.PermissionID
                            where rp.RoleID == roleId
                            select p.PermissionName;

                return query.ToList();
                // Kết quả sẽ là list: {"View_Farms", "Add_Crops", "View_Reports"...}
            }
        }
        public bool ChangePassword(int userId, string oldPass, string newPass)
        {
            using (var db = new FarmModel())
            {
                // 1. Tìm user theo ID
                var user = db.Users.FirstOrDefault(u => u.UserID == userId);
                if (user == null) return false;

                // 2. Kiểm tra mật khẩu cũ (Lưu ý: Nếu bạn có mã hóa MD5/SHA thì phải mã hóa oldPass trước khi so sánh)
                // Ở đây đang giả định bạn lưu password dạng thô (plain text) như lúc làm Login
                if (user.PasswordHash != oldPass)
                {
                    return false; // Mật khẩu cũ không khớp
                }

                // 3. Cập nhật mật khẩu mới
                user.PasswordHash = newPass;

                // (Tùy chọn) Cập nhật ngày sửa
                // user.UpdatedDate = DateTime.Now;

                db.SaveChanges();
                return true;
            }
        }
    }
}