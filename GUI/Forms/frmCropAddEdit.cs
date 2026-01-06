using FarmManagement.BUS;
using FarmManagement.DAL;
using System;
using System.Windows.Forms;

namespace GUI.Forms
{
    public partial class frmCropAddEdit : Form
    {
        private readonly CropService _cropService = new CropService();
        private readonly FarmService _farmService = new FarmService();

        public int? CropID { get; set; }

        public frmCropAddEdit()
        {
            InitializeComponent();
        }

        private void FillFarmCombobox()
        {
            var listFarms = _farmService.GetAll();
            cmbFarm.DataSource = listFarms;
            cmbFarm.DisplayMember = "FarmName";
            cmbFarm.ValueMember = "FarmID";
        }

        private void frmCropAddEdit_Load(object sender, EventArgs e)
        {
            try
            {
                FillFarmCombobox();

                if (CropID.HasValue) // --- CHẾ ĐỘ SỬA ---
                {
                    this.Text = "Cập nhật thông tin Cây trồng";
                    var crop = _cropService.GetById(CropID.Value);

                    if (crop != null)
                    {
                        if (crop.FarmID != 0) cmbFarm.SelectedValue = crop.FarmID;
                        txtCropName.Text = crop.CropName;
                        dtpPlantingDate.Value = crop.PlantingDate ?? DateTime.Now;
                        txtStatus.Text = crop.Status;
                        nudQuantity.Value = crop.Quantity ?? 0;
                        txtUnit.Text = crop.Unit;
                        dtpHarvestDate.Value = crop.ExpectedHarvestDate ?? DateTime.Now.AddMonths(3);
                        txtNotes.Text = crop.Notes;

                        // --- MỚI: Hiển thị giá nhập cũ ---
                        // Đảm bảo bạn đã thêm control numImportPrice trên giao diện
                        if (numImportPrice != null)
                            numImportPrice.Value = crop.ImportPrice;
                    }
                }
                else // --- CHẾ ĐỘ THÊM MỚI ---
                {
                    this.Text = "Thêm mới Cây trồng";
                    dtpPlantingDate.Value = DateTime.Now;
                    dtpHarvestDate.Value = DateTime.Now.AddMonths(3);
                    nudQuantity.Value = 0;

                    // Mặc định chi phí = 0
                    if (numImportPrice != null) numImportPrice.Value = 0;

                    if (cmbFarm.Items.Count > 0) cmbFarm.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCropName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên cây trồng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCropName.Focus();
                    return;
                }

                Crop crop = new Crop();
                if (CropID.HasValue) crop.CropID = CropID.Value;

                // --- GÁN DỮ LIỆU TỪ GIAO DIỆN ---
                crop.FarmID = (int)cmbFarm.SelectedValue;
                crop.CropName = txtCropName.Text.Trim();
                crop.PlantingDate = dtpPlantingDate.Value;
                crop.Status = txtStatus.Text.Trim();
                crop.Quantity = (int)nudQuantity.Value;
                crop.Unit = txtUnit.Text.Trim();
                crop.ExpectedHarvestDate = dtpHarvestDate.Value;
                crop.Notes = txtNotes.Text.Trim();

                // --- QUAN TRỌNG: Lấy Chi phí giống truyền xuống Service ---
                if (numImportPrice != null)
                {
                    crop.ImportPrice = numImportPrice.Value;
                }

                // Gọi Service lưu
                _cropService.InsertUpdate(crop);

                MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}