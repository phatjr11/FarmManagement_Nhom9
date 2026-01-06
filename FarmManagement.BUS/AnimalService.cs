using FarmManagement.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace FarmManagement.BUS
{
    public class AnimalService
    {
        // Khai báo Service Log
        private readonly SystemLogService _logService = new SystemLogService();

        public List<Animal> GetAll()
        {
            using (var db = new FarmModel())
            {
                return db.Animals.Include("Farm").ToList();
            }
        }

        public Animal GetById(int id)
        {
            using (var db = new FarmModel())
            {
                return db.Animals.FirstOrDefault(a => a.AnimalID == id);
            }
        }

        // Hàm Thêm/Sửa (Đã fix lỗi không lưu giá khi Sửa + Tự cập nhật phiếu Chi)
        public void InsertUpdate(Animal animal)
        {
            using (var db = new FarmModel())
            {
                if (animal.AnimalID > 0) // --- TRƯỜNG HỢP SỬA ---
                {
                    var exist = db.Animals.FirstOrDefault(a => a.AnimalID == animal.AnimalID);
                    if (exist != null)
                    {
                        string oldInfo = $"{exist.AnimalType} - {exist.Breed} ({exist.Quantity})";

                        // Cập nhật thông tin cơ bản
                        exist.FarmID = animal.FarmID;
                        exist.AnimalType = animal.AnimalType;
                        exist.Breed = animal.Breed;
                        exist.Quantity = animal.Quantity;
                        exist.PurchaseDate = animal.PurchaseDate;
                        exist.Status = animal.Status;
                        exist.Notes = animal.Notes;
                        exist.UpdatedDate = DateTime.Now;

                        // 1. CẬP NHẬT GIÁ NHẬP (Phần bạn đang thiếu)
                        exist.ImportPrice = animal.ImportPrice;

                        // 2. TỰ ĐỘNG SỬA PHIẾU CHI (Logic chặt chẽ)
                        // Tìm phiếu chi liên quan đến con vật này
                        var linkedExpense = db.Expenses.FirstOrDefault(e =>
                                                e.SourceType == "ANIMAL" &&
                                                e.RelatedID == exist.AnimalID);

                        if (linkedExpense != null)
                        {
                            // Tính lại tổng tiền mới = Giá mới * Số lượng mới
                            decimal newTotal = (exist.ImportPrice) * (exist.Quantity ?? 0);

                            linkedExpense.Amount = newTotal;
                            linkedExpense.Description = $"Chi phí nhập: {exist.AnimalType} - {exist.Breed} (Đã cập nhật)";
                            // Nếu cần thiết thì cập nhật cả ngày chi
                            // linkedExpense.ExpenseDate = exist.PurchaseDate; 
                        }

                        db.SaveChanges(); // Lưu cả Con vật và Phiếu chi cùng lúc

                        // Ghi log
                        _logService.WriteAuditLog("Animals", "UPDATE",
                            $"Sửa vật nuôi: {oldInfo} -> SL: {exist.Quantity}, Giá: {exist.ImportPrice:N0}");
                    }
                }
                else // --- TRƯỜNG HỢP THÊM MỚI ---
                {
                    // (Code phần thêm mới giữ nguyên như cũ)
                    animal.CreatedDate = DateTime.Now;
                    db.Animals.Add(animal);
                    db.SaveChanges();

                    decimal totalCost = animal.ImportPrice * (animal.Quantity ?? 0);
                    if (totalCost > 0)
                    {
                        var expense = new Expens()
                        {
                            FarmID = animal.FarmID,
                            ExpenseType = "Mua vật nuôi",
                            Description = $"Chi phí nhập: {animal.AnimalType} - {animal.Breed}",
                            Amount = totalCost,
                            ExpenseDate = DateTime.Now,
                            SourceType = "ANIMAL",
                            RelatedID = animal.AnimalID
                        };
                        db.Expenses.Add(expense);
                        db.SaveChanges();
                    }
                    _logService.WriteAuditLog("Animals", "INSERT", $"Thêm: {animal.AnimalType} - Tổng: {totalCost:N0}");
                }
            }
        }

        // --- HÀM MỚI: BÁN VẬT NUÔI (Tự động tạo Phiếu Thu) ---
        public bool SellAnimal(int animalId, int quantitySold, decimal pricePerUnit, DateTime sellDate)
        {
            try
            {
                using (var db = new FarmModel())
                {
                    var animal = db.Animals.Find(animalId);
                    if (animal == null) return false;

                    // Kiểm tra tồn kho
                    int currentQty = animal.Quantity ?? 0;
                    if (currentQty < quantitySold) return false; // Không đủ để bán

                    // 1. TRỪ SỐ LƯỢNG
                    animal.Quantity = currentQty - quantitySold;

                    // 2. CẬP NHẬT TRẠNG THÁI
                    // Nếu bán hết sạch -> Đổi thành "Đã bán"
                    if (animal.Quantity == 0)
                    {
                        animal.Status = "Đã bán";
                    }
                    // Nếu vẫn còn -> Giữ nguyên trạng thái (ví dụ: Khỏe mạnh)

                    animal.UpdatedDate = DateTime.Now;

                    // 3. TẠO PHIẾU THU (Revenue)
                    // Tổng tiền = Số lượng bán * Giá 1 con
                    decimal totalRevenue = quantitySold * pricePerUnit;

                    if (totalRevenue > 0)
                    {
                        var revenue = new Revenue()
                        {
                            FarmID = animal.FarmID,
                            RevenueType = "Bán vật nuôi",
                            // Sửa lại string nội suy cho khớp biến
                            Description = $"Bán {quantitySold} con {animal.AnimalType} - {animal.Breed} (Giá: {pricePerUnit})",
                            Amount = totalRevenue,
                            RevenueDate = sellDate,
                            SourceType = "ANIMAL",
                            RelatedID = animal.AnimalID
                        };

                        db.Revenues.Add(revenue);
                    }

                    db.SaveChanges();

                    // Ghi log
                    _logService.WriteAuditLog("Animals", "SELL",
                        $"Xuất chuồng {quantitySold} con {animal.AnimalType} - Thu về: {totalRevenue:N0}");

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            using (var db = new FarmModel())
            {
                var animal = db.Animals.FirstOrDefault(a => a.AnimalID == id);
                if (animal == null) return false;

                string desc = $"{animal.AnimalType} - {animal.Breed}";

                db.Animals.Remove(animal);
                db.SaveChanges();

                // --- GHI LOG DELETE ---
                _logService.WriteAuditLog("Animals", "DELETE", $"Xóa vật nuôi: {desc}");

                return true;
            }
        }
    }
}