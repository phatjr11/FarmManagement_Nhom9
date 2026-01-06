using FarmManagement.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FarmManagement.BUS
{
    public class InventoryService
    {
        // Khai báo Service Log
        private readonly SystemLogService _logService = new SystemLogService();

        public List<Inventory> GetAll()
        {
            using (var db = new FarmModel())
            {
                return db.Inventories.Include("Farm").OrderBy(x => x.ItemName).ToList();
            }
        }

        public Inventory GetById(int id)
        {
            using (var db = new FarmModel())
            {
                return db.Inventories.FirstOrDefault(x => x.InventoryID == id);
            }
        }

        // --- THAY THẾ HÀM InsertUpdate CŨ BẰNG HÀM NÀY ---
        public void InsertUpdate(Inventory item)
        {
            using (var db = new FarmModel())
            {
                if (item.InventoryID > 0) // --- SỬA ---
                {
                    var exist = db.Inventories.FirstOrDefault(x => x.InventoryID == item.InventoryID);
                    if (exist != null)
                    {
                        string oldQty = $"{exist.Quantity} {exist.Unit}";

                        exist.FarmID = item.FarmID;
                        exist.ItemName = item.ItemName;
                        exist.ItemType = item.ItemType;
                        exist.Quantity = item.Quantity;
                        exist.Unit = item.Unit;
                        exist.Location = item.Location;
                        exist.Notes = item.Notes;
                        exist.UpdatedDate = DateTime.Now;

                        // Cập nhật đơn giá (Nếu có thay đổi)
                        exist.UnitPrice = item.UnitPrice;

                        // Nếu cần cập nhật lại phiếu chi cũ, logic sẽ phức tạp hơn vì nhập kho có thể nhiều lần
                        // Ở đây ta tạm thời chỉ cập nhật thông tin vật tư.

                        db.SaveChanges();
                        _logService.WriteAuditLog("Inventory", "UPDATE", $"Cập nhật kho '{item.ItemName}': {oldQty} -> {item.Quantity} {item.Unit}");
                    }
                }
                else // --- THÊM MỚI (NHẬP KHO) ---
                {
                    item.CreatedDate = DateTime.Now;
                    db.Inventories.Add(item);
                    db.SaveChanges(); // Lưu để lấy ID

                    // --- TỰ ĐỘNG TẠO PHIẾU CHI (EXPENSE) ---
                    // Tổng tiền = Số lượng * Đơn giá
                    decimal totalCost = (item.Quantity ?? 0) * item.UnitPrice;

                    if (totalCost > 0)
                    {
                        var expense = new Expens()
                        {
                            FarmID = item.FarmID,
                            ExpenseType = "Mua vật tư",
                            Description = $"Nhập kho: {item.ItemName} ({item.Quantity} {item.Unit})",
                            Amount = totalCost,
                            ExpenseDate = DateTime.Now,
                            SourceType = "INVENTORY",
                            RelatedID = item.InventoryID
                        };
                        db.Expenses.Add(expense);
                        db.SaveChanges();
                    }

                    _logService.WriteAuditLog("Inventory", "INSERT", $"Nhập kho: {item.ItemName} - Tổng tiền: {totalCost:N0}");
                }
            }
        }

        // --- THÊM HÀM MỚI NÀY VÀO CUỐI CLASS ---
        public bool UseInventory(int inventoryId, int quantityUsed, string actionType, decimal priceIfSold, string reason)
        {
            try
            {
                using (var db = new FarmModel())
                {
                    var item = db.Inventories.Find(inventoryId);
                    if (item == null) return false;

                    int currentQty = item.Quantity ?? 0;
                    if (currentQty < quantityUsed) return false;

                    // 1. Trừ kho
                    item.Quantity = currentQty - quantityUsed;
                    item.UpdatedDate = DateTime.Now;

                    // 2. Xử lý theo loại hành động
                    if (actionType == "SELL") // Nếu là BÁN -> Tạo phiếu Thu
                    {
                        decimal totalRevenue = quantityUsed * priceIfSold;
                        if (totalRevenue > 0)
                        {
                            var revenue = new Revenue()
                            {
                                FarmID = item.FarmID,
                                RevenueType = "Bán vật tư",
                                Description = $"Bán {quantityUsed} {item.Unit} {item.ItemName}",
                                Amount = totalRevenue,
                                RevenueDate = DateTime.Now,
                                SourceType = "INVENTORY",
                                RelatedID = item.InventoryID
                            };
                            db.Revenues.Add(revenue);
                        }
                        _logService.WriteAuditLog("Inventory", "SELL", $"Bán vật tư: {item.ItemName} - SL: {quantityUsed}");
                    }
                    else // Nếu là SỬ DỤNG (Cho cây/con) -> Chỉ trừ kho, ghi log
                    {
                        _logService.WriteAuditLog("Inventory", "USE", $"Sử dụng nội bộ: {item.ItemName} - SL: {quantityUsed} ({reason})");
                    }

                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            using (var db = new FarmModel())
            {
                var item = db.Inventories.FirstOrDefault(x => x.InventoryID == id);
                if (item == null) return false;

                string name = item.ItemName;

                db.Inventories.Remove(item);
                db.SaveChanges();

                // --- GHI LOG DELETE ---
                _logService.WriteAuditLog("Inventory", "DELETE", $"Xóa vật tư khỏi kho: {name}");

                return true;
            }
        }
    }
}