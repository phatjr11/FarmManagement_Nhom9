using FarmManagement.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity; // Dùng Include
using System.Linq;

namespace FarmManagement.BUS
{
    public class ActivityService
    {
        // Khai báo Service Log
        private readonly SystemLogService _logService = new SystemLogService();

        public List<Activity> GetAll()
        {
            using (var db = new FarmModel())
            {
                return db.Activities.Include("Farm").OrderByDescending(a => a.StartDate).ToList();
            }
        }

        public Activity GetById(int id)
        {
            using (var db = new FarmModel())
            {
                return db.Activities.FirstOrDefault(a => a.ActivityID == id);
            }
        }

        public void InsertUpdate(Activity activity, int? inventoryId = null, int quantityUsed = 0)
        {
            using (var db = new FarmModel())
            {
                // 1. Lưu Activity như bình thường
                if (activity.ActivityID > 0)
                {
                    var exist = db.Activities.Find(activity.ActivityID);
                    if (exist != null)
                    {
                        exist.FarmID = activity.FarmID;
                        exist.ActivityType = activity.ActivityType;
                        exist.Description = activity.Description;
                        exist.StartDate = activity.StartDate;
                        exist.EndDate = activity.EndDate;
                        exist.ResponsiblePerson = activity.ResponsiblePerson;
                        exist.Status = activity.Status;
                        db.SaveChanges();
                        _logService.WriteAuditLog("Activities", "UPDATE", $"Cập nhật: {activity.Description}");
                    }
                }
                else // Thêm mới
                {
                    activity.CreatedDate = DateTime.Now;
                    db.Activities.Add(activity);
                    db.SaveChanges();
                    _logService.WriteAuditLog("Activities", "INSERT", $"Thêm mới: {activity.Description}");

                    // 2. XỬ LÝ TRỪ KHO (Chỉ làm khi Thêm mới)
                    if (inventoryId.HasValue && quantityUsed > 0)
                    {
                        var item = db.Inventories.Find(inventoryId.Value);
                        if (item != null)
                        {
                            if (item.Quantity >= quantityUsed)
                            {
                                item.Quantity -= quantityUsed;
                                item.UpdatedDate = DateTime.Now;

                                // Ghi chú vào Activity để biết đã dùng gì
                                // (Cần reload activity để update description nếu muốn, hoặc ghi log riêng)
                                _logService.WriteAuditLog("Inventory", "USE", $"Dùng {quantityUsed} {item.Unit} {item.ItemName} cho hoạt động #{activity.ActivityID}");

                                db.SaveChanges();
                            }
                            else
                            {
                                // Có thể throw exception hoặc bỏ qua nếu kho không đủ
                                // Ở đây ta chọn cách an toàn là không trừ, chỉ ghi log cảnh báo
                            }
                        }
                    }
                }
            }
        }

        public bool Delete(int id)
        {
            using (var db = new FarmModel())
            {
                var act = db.Activities.FirstOrDefault(a => a.ActivityID == id);
                if (act == null) return false;

                string desc = act.Description;

                db.Activities.Remove(act);
                db.SaveChanges();

                // --- GHI LOG DELETE ---
                _logService.WriteAuditLog("Activities", "DELETE", $"Xóa công việc: {desc}");
                return true;
            }
        }
    }
}