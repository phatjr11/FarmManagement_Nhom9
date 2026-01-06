using FarmManagement.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity; // Dùng Include
using System.Linq;

namespace FarmManagement.BUS
{
    public class RevenueService
    {
        // Khai báo Service Log
        private readonly SystemLogService _logService = new SystemLogService();

        public List<Revenue> GetAll()
        {
            using (var db = new FarmModel())
            {
                return db.Revenues.Include("Farm").OrderByDescending(r => r.RevenueDate).ToList();
            }
        }

        public Revenue GetById(int id)
        {
            using (var db = new FarmModel())
            {
                return db.Revenues.FirstOrDefault(r => r.RevenueID == id);
            }
        }

        public void InsertUpdate(Revenue item)
        {
            using (var db = new FarmModel())
            {
                if (item.RevenueID > 0) // --- SỬA ---
                {
                    var exist = db.Revenues.FirstOrDefault(r => r.RevenueID == item.RevenueID);
                    if (exist != null)
                    {
                        decimal oldAmt = exist.Amount ?? 0;

                        exist.FarmID = item.FarmID;
                        exist.RevenueType = item.RevenueType;
                        exist.Amount = item.Amount;
                        exist.Description = item.Description;
                        exist.RevenueDate = item.RevenueDate;
                        exist.Notes = item.Notes;

                        db.SaveChanges();

                        // --- GHI LOG UPDATE ---
                        _logService.WriteAuditLog("Revenue", "UPDATE", $"Sửa khoản thu '{item.Description}': {oldAmt:N0} -> {item.Amount:N0}");
                    }
                }
                else // --- THÊM MỚI ---
                {
                    item.CreatedDate = DateTime.Now;
                    db.Revenues.Add(item);
                    db.SaveChanges();

                    // --- GHI LOG INSERT ---
                    _logService.WriteAuditLog("Revenue", "INSERT", $"Thêm khoản thu: {item.Description} - Số tiền: {item.Amount:N0}");
                }
            }
        }

        public bool Delete(int id)
        {
            using (var db = new FarmModel())
            {
                var item = db.Revenues.FirstOrDefault(r => r.RevenueID == id);
                if (item == null) return false;

                string desc = $"{item.Description} ({item.Amount:N0} VNĐ)";

                db.Revenues.Remove(item);
                db.SaveChanges();

                // --- GHI LOG DELETE ---
                _logService.WriteAuditLog("Revenue", "DELETE", $"Xóa khoản thu: {desc}");
                return true;
            }
        }
    }
}