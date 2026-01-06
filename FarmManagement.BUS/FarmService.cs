using FarmManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FarmManagement.BUS
{
    public class FarmService
    {
        // 1. Gọi Service Log
        private readonly SystemLogService _logService = new SystemLogService();

        public List<Farm> GetAll()
        {
            using (var db = new FarmModel())
            {
                return db.Farms.ToList();
            }
        }

        public void InsertUpdate(Farm farm)
        {
            using (var db = new FarmModel())
            {
                if (farm.FarmID > 0) // --- SỬA ---
                {
                    var exist = db.Farms.FirstOrDefault(x => x.FarmID == farm.FarmID);
                    if (exist != null)
                    {
                        // Lưu lại tên cũ để ghi log
                        string oldName = exist.FarmName;

                        exist.FarmName = farm.FarmName;
                        exist.Owner = farm.Owner;
                        exist.Area = farm.Area;
                        exist.Location = farm.Location;
                        exist.Phone = farm.Phone;

                        db.SaveChanges();

                        // --- GHI LOG UPDATE ---
                        _logService.WriteAuditLog("Farms", "UPDATE", $"Sửa nông trại: {oldName} -> {farm.FarmName}");
                    }
                }
                else // --- THÊM MỚI ---
                {
                    db.Farms.Add(farm);
                    db.SaveChanges();

                    // --- GHI LOG INSERT ---
                    _logService.WriteAuditLog("Farms", "INSERT", $"Thêm mới nông trại: {farm.FarmName}");
                }
            }
        }

        public bool Delete(int id)
        {
            using (var db = new FarmModel())
            {
                var f = db.Farms.FirstOrDefault(x => x.FarmID == id);
                if (f == null) return false;

                // Lưu tên để ghi log
                string farmName = f.FarmName;

                // Kiểm tra ràng buộc khóa ngoại (ví dụ còn cây trồng thì không cho xóa)
                // if (db.Crops.Any(c => c.FarmID == id)) return false; 

                db.Farms.Remove(f);
                db.SaveChanges();

                // --- GHI LOG DELETE ---
                _logService.WriteAuditLog("Farms", "DELETE", $"Xóa nông trại: {farmName} (ID: {id})");

                return true;
            }
        }

        public Farm GetById(int id)
        {
            var farms = GetAll();
            return farms.FirstOrDefault(f => f.FarmID == id);
        }
    }
}