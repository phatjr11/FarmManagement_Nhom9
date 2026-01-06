using FarmManagement.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity; // Dùng Include
using System.Linq;

namespace FarmManagement.BUS
{
    public class ExpenseService
    {
        // Khai báo Service Log
        private readonly SystemLogService _logService = new SystemLogService();

        public List<Expens> GetAll()
        {
            using (var db = new FarmModel())
            {
                return db.Expenses.Include("Farm").OrderByDescending(e => e.ExpenseDate).ToList();
            }
        }

        public Expens GetById(int id)
        {
            using (var db = new FarmModel())
            {
                return db.Expenses.FirstOrDefault(e => e.ExpenseID == id);
            }
        }

        public void InsertUpdate(Expens item)
        {
            using (var db = new FarmModel())
            {
                if (item.ExpenseID > 0) // --- SỬA ---
                {
                    var exist = db.Expenses.FirstOrDefault(e => e.ExpenseID == item.ExpenseID);
                    if (exist != null)
                    {
                        // Lưu lại số tiền cũ
                        decimal oldAmt = exist.Amount ?? 0;

                        exist.FarmID = item.FarmID;
                        exist.ExpenseType = item.ExpenseType;
                        exist.Amount = item.Amount;
                        exist.Description = item.Description;
                        exist.ExpenseDate = item.ExpenseDate;
                        exist.Notes = item.Notes;

                        // Cập nhật ngày sửa nếu model có
                        // exist.UpdatedDate = DateTime.Now;

                        db.SaveChanges();

                        // --- GHI LOG UPDATE ---
                        _logService.WriteAuditLog("Expenses", "UPDATE", $"Sửa khoản chi '{item.Description}': {oldAmt:N0} -> {item.Amount:N0}");
                    }
                }
                else // --- THÊM MỚI ---
                {
                    item.CreatedDate = DateTime.Now;
                    db.Expenses.Add(item);
                    db.SaveChanges();

                    // --- GHI LOG INSERT ---
                    _logService.WriteAuditLog("Expenses", "INSERT", $"Thêm khoản chi: {item.Description} - Số tiền: {item.Amount:N0}");
                }
            }
        }

        public bool Delete(int id)
        {
            using (var db = new FarmModel())
            {
                var item = db.Expenses.FirstOrDefault(e => e.ExpenseID == id);
                if (item == null) return false;

                string desc = $"{item.Description} ({item.Amount:N0} VNĐ)";

                db.Expenses.Remove(item);
                db.SaveChanges();

                // --- GHI LOG DELETE ---
                _logService.WriteAuditLog("Expenses", "DELETE", $"Xóa khoản chi: {desc}");
                return true;
            }
        }
    }
}