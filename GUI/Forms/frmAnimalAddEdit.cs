using FarmManagement.BUS;
using FarmManagement.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.Forms
{
    public partial class frmAnimalAddEdit : Form
    {
        private readonly AnimalService _animalService = new AnimalService();
        private readonly FarmService _farmService = new FarmService();

        public int? AnimalID { get; set; }

        public frmAnimalAddEdit()
        {
            InitializeComponent();
        }

        private void frmAnimalAddEdit_Load(object sender, EventArgs e)
        {
            try
            {
                // Load ComboBox Nông trại
                cmbFarm.DataSource = _farmService.GetAll();
                cmbFarm.DisplayMember = "FarmName";
                cmbFarm.ValueMember = "FarmID";

                if (AnimalID.HasValue) // --- TRƯỜNG HỢP SỬA ---
                {
                    this.Text = "Cập nhật Vật nuôi";
                    var animal = _animalService.GetById(AnimalID.Value);
                    if (animal != null)
                    {
                        if (animal.FarmID != 0) cmbFarm.SelectedValue = animal.FarmID;
                        txtAnimalType.Text = animal.AnimalType;
                        txtBreed.Text = animal.Breed;
                        nudQuantity.Value = animal.Quantity ?? 0;
                        dtpPurchaseDate.Value = animal.PurchaseDate ?? DateTime.Now;
                        txtStatus.Text = animal.Status;
                        txtNotes.Text = animal.Notes;

                        // --- MỚI: Hiển thị giá nhập cũ (nếu có) ---
                        // Cần kiểm tra tên control NumericUpDown bạn đã đặt là numImportPrice hay nudImportPrice?
                        // Ở đây mình dùng numImportPrice theo hướng dẫn trước.
                        if (numImportPrice != null)
                            numImportPrice.Value = animal.ImportPrice;
                    }
                }
                else // --- TRƯỜNG HỢP THÊM MỚI ---
                {
                    this.Text = "Thêm mới Vật nuôi";
                    dtpPurchaseDate.Value = DateTime.Now;

                    // Mặc định giá nhập = 0
                    if (numImportPrice != null) numImportPrice.Value = 0;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtAnimalType.Text))
                {
                    MessageBox.Show("Vui lòng nhập loại vật nuôi!");
                    return;
                }

                Animal animal = new Animal();
                if (AnimalID.HasValue) animal.AnimalID = AnimalID.Value;

                // Lấy dữ liệu từ giao diện
                animal.FarmID = (int)cmbFarm.SelectedValue;
                animal.AnimalType = txtAnimalType.Text;
                animal.Breed = txtBreed.Text;
                animal.Quantity = (int)nudQuantity.Value;
                animal.PurchaseDate = dtpPurchaseDate.Value;
                animal.Status = txtStatus.Text;
                animal.Notes = txtNotes.Text;

                // --- QUAN TRỌNG: Lấy Giá Nhập truyền xuống Service ---
                // Đây là dòng code quyết định việc có tạo phiếu chi hay không
                if (numImportPrice != null)
                {
                    animal.ImportPrice = numImportPrice.Value;
                }

                // Gọi Service để lưu (Service sẽ tự lo phần tạo phiếu chi)
                _animalService.InsertUpdate(animal);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}