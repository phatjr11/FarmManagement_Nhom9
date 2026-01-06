using FarmManagement.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FarmManagement.BUS
{
    public class CropService
    {
        // Khai báo Service Log
        private readonly SystemLogService _logService = new SystemLogService();

        public List<Crop> GetAll()
        {
            using (var db = new FarmModel())
            {
                return db.Crops.Include("Farm").ToList();
            }
        }

        public Crop GetById(int id)
        {
            using (var db = new FarmModel())
            {
                return db.Crops.FirstOrDefault(c => c.CropID == id);
            }
        }

        // Hàm Thêm/Sửa (Đã thêm logic tự động tạo Phiếu Chi)
        public void InsertUpdate(Crop crop)
        {
            using (var db = new FarmModel())
            {
                if (crop.CropID > 0) // --- SỬA ---
                {
                    var exist = db.Crops.FirstOrDefault(c => c.CropID == crop.CropID);
                    if (exist != null)
                    {
                        string oldInfo = $"{exist.CropName} ({exist.Status})";

                        exist.FarmID = crop.FarmID;
                        exist.CropName = crop.CropName;
                        exist.PlantingDate = crop.PlantingDate;
                        exist.ExpectedHarvestDate = crop.ExpectedHarvestDate;
                        exist.Quantity = crop.Quantity;
                        exist.Unit = crop.Unit;
                        exist.Status = crop.Status;
                        exist.Notes = crop.Notes;
                        exist.UpdatedDate = DateTime.Now;

                        // 1. CẬP NHẬT GIÁ NHẬP (QUAN TRỌNG)
                        exist.ImportPrice = crop.ImportPrice;

                        // 2. TỰ ĐỘNG SỬA PHIẾU CHI (Logic chặt chẽ)
                        // Tìm phiếu chi liên quan đến cây trồng này
                        var linkedExpense = db.Expenses.FirstOrDefault(e =>
                                                e.SourceType == "CROP" &&
                                                e.RelatedID == exist.CropID);

                        if (linkedExpense != null)
                        {
                            // Cập nhật lại số tiền (Đối với cây trồng, ImportPrice thường là Tổng chi phí đợt gieo trồng)
                            linkedExpense.Amount = exist.ImportPrice;
                            linkedExpense.Description = $"Chi phí giống: {exist.CropName} (Đã cập nhật)";
                        }

                        db.SaveChanges();

                        // --- GHI LOG UPDATE ---
                        _logService.WriteAuditLog("Crops", "UPDATE", $"Sửa cây: {oldInfo} -> Giá: {crop.ImportPrice:N0}");
                    }
                }
                else // --- THÊM MỚI ---
                {
                    crop.CreatedDate = DateTime.Now;

                    // 1. Lưu cây trồng trước
                    db.Crops.Add(crop);
                    db.SaveChanges(); // Để lấy CropID

                    // 2. TỰ ĐỘNG TẠO PHIẾU CHI
                    if (crop.ImportPrice > 0)
                    {
                        var expense = new Expens() // Lưu ý: Tên class DAL của bạn là Expens
                        {
                            FarmID = crop.FarmID,
                            ExpenseType = "Mua cây giống",
                            Description = $"Chi phí giống/phân bón ban đầu: {crop.CropName}",
                            Amount = crop.ImportPrice, // Lưu ý: Với cây, đây thường là tổng chi phí nhập
                            ExpenseDate = DateTime.Now,

                            // Liên kết chặt chẽ
                            SourceType = "CROP",
                            RelatedID = crop.CropID
                        };

                        db.Expenses.Add(expense);
                        db.SaveChanges();
                    }

                    // --- GHI LOG INSERT ---
                    _logService.WriteAuditLog("Crops", "INSERT", $"Thêm cây: {crop.CropName} - Chi phí: {crop.ImportPrice:N0}");
                }
            }
        }

        public bool Delete(int id)
        {
            using (var db = new FarmModel())
            {
                var crop = db.Crops.FirstOrDefault(c => c.CropID == id);
                if (crop == null) return false;

                string name = crop.CropName;

                db.Crops.Remove(crop);
                db.SaveChanges();

                _logService.WriteAuditLog("Crops", "DELETE", $"Xóa cây trồng: {name} (ID: {id})");
                return true;
            }
        }
        public bool SellCrop(int cropId, int quantitySold, decimal pricePerUnit, DateTime sellDate)
        {
            try
            {
                using (var db = new FarmModel())
                {
                    var crop = db.Crops.Find(cropId);
                    if (crop == null) return false;

                    int currentQty = crop.Quantity ?? 0;
                    if (currentQty < quantitySold) return false;

                    // 1. Trừ số lượng
                    crop.Quantity = currentQty - quantitySold;

                    // 2. Cập nhật trạng thái
                    if (crop.Quantity == 0)
                    {
                        crop.Status = "Đã thu hoạch"; // Hoặc "Đã bán hết"
                    }
                    crop.UpdatedDate = DateTime.Now;

                    // 3. Tạo phiếu Thu (Revenue)
                    decimal totalRevenue = quantitySold * pricePerUnit;
                    if (totalRevenue > 0)
                    {
                        var revenue = new Revenue()
                        {
                            FarmID = crop.FarmID,
                            RevenueType = "Bán nông sản",
                            Description = $"Bán {quantitySold} {crop.Unit} {crop.CropName} (Giá: {pricePerUnit:N0}/{crop.Unit})",
                            Amount = totalRevenue,
                            RevenueDate = sellDate,
                            SourceType = "CROP",
                            RelatedID = crop.CropID
                        };
                        db.Revenues.Add(revenue);
                    }

                    db.SaveChanges();

                    // Ghi Log
                    _logService.WriteAuditLog("Crops", "SELL",
                        $"Bán nông sản: {crop.CropName} - SL: {quantitySold} - Thu: {totalRevenue:N0}");

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}