using System;
using System.Drawing;
using System.Windows.Forms;
using FarmManagement.BUS;
using FarmManagement.DAL;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GUI.Forms
{
    public partial class frmQRScanner : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private BarcodeReader barcodeReader;
        private bool isProcessing = false;
        private object syncLock = new object();

        // Win32 API for draggability
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }
        
        public frmQRScanner()
        {
            InitializeComponent();
            
            // Tối ưu hóa BarcodeReader
            barcodeReader = new BarcodeReader
            {
                AutoRotate = true,
                Options = new ZXing.Common.DecodingOptions
                {
                    TryHarder = true, // Quét kỹ hơn
                    PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE } // Chỉ tập trung vào QR
                }
            };
        }

        private void frmQRScanner_Load(object sender, EventArgs e)
        {
            // Kiểm tra camera có sẵn
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0)
                {
                    lblStatus.Text = "Không tìm thấy camera. Vui lòng sử dụng nhập thủ công.";
                    lblStatus.ForeColor = Color.Orange;
                    btnStartScan.Enabled = false;
                }
                else
                {
                    lblStatus.Text = $"Đã phát hiện {videoDevices.Count} camera. Sẵn sàng quét.";
                    lblStatus.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo camera: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnStartScan.Enabled = false;
            }
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                // Clone frame để hiển thị và xử lý
                Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
                
                // Cập nhật giao diện preview
                if (pnlCameraPreview.InvokeRequired)
                {
                    pnlCameraPreview.BeginInvoke(new Action(() => UpdatePreview(bitmap)));
                }
                else
                {
                    UpdatePreview(bitmap);
                }

                // Gửi sang tiến trình ngầm để giải mã QR (không đợi, không làm đơ UI)
                if (!isProcessing)
                {
                    Task.Run(() => TryDecode(bitmap));
                }
            }
            catch
            {
                // Bỏ qua lỗi frame
            }
        }

        private void UpdatePreview(Bitmap bitmap)
        {
            if (pnlCameraPreview.BackgroundImage != null)
                pnlCameraPreview.BackgroundImage.Dispose();
            
            pnlCameraPreview.BackgroundImage = (Bitmap)bitmap.Clone();
        }

        private void TryDecode(Bitmap bitmap)
        {
            // Lock để đảm bảo chỉ có 1 task xử lý tại 1 thời điểm
            lock (syncLock)
            {
                if (isProcessing) return;
                isProcessing = true;
            }

            try
            {
                using (Bitmap decodeTarget = (Bitmap)bitmap.Clone())
                {
                    var result = barcodeReader.Decode(decodeTarget);
                    
                    if (result != null)
                    {
                        // Tìm thấy mã QR!
                        this.BeginInvoke(new Action(() =>
                        {
                            StopCamera();
                            ProcessQRCode(result.Text);
                        }));
                    }
                }
            }
            catch
            {
                // Bỏ qua lỗi giải mã
            }
            finally
            {
                bitmap.Dispose(); // Quan trọng: Giải phóng bộ nhớ của clone gốc
                isProcessing = false;
            }
        }

        private void btnStartScan_Click(object sender, EventArgs e)
        {
            try
            {
                if (videoSource != null && videoSource.IsRunning)
                {
                    StopCamera();
                }
                else
                {
                    StartCamera();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thao tác camera: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StartCamera()
        {
            if (videoDevices == null || videoDevices.Count == 0) return;

            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.NewFrame += VideoSource_NewFrame;
            videoSource.Start();
            
            btnStartScan.Text = "Dừng quét";
            btnStartScan.BackColor = Color.Crimson;
            btnStopScan.Enabled = true;
            lblStatus.Text = "Đang quét... Đưa mã QR vào camera";
            lblStatus.ForeColor = Color.Green;
        }

        private void StopCamera()
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                // videoSource.WaitForStop(); // Không dùng WaitForStop ở UI thread để tránh đơ
                videoSource = null;
            }
            
            btnStartScan.Text = "Bắt đầu quét";
            btnStartScan.BackColor = Color.LimeGreen;
            btnStopScan.Enabled = false;
            lblStatus.Text = "Đã dừng quét";
            lblStatus.ForeColor = Color.Gray;
        }

        private void btnStopScan_Click(object sender, EventArgs e)
        {
            StopCamera();
        }

        private void btnManualInput_Click(object sender, EventArgs e)
        {
            string qrCode = txtManualInput.Text.Trim();
            if (string.IsNullOrEmpty(qrCode))
            {
                MessageBox.Show("Vui lòng nhập mã QR!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ProcessQRCode(qrCode);
        }

        private void ProcessQRCode(string qrData)
        {
            try
            {
                // Hiển thị thông tin cơ bản
                lblProductInfo.Text = $"Mã QR: {qrData}\n\n";
                
                // Thử tra cứu thông tin từ database
                if (qrData.StartsWith("CROP-"))
                {
                    int cropId = int.Parse(qrData.Replace("CROP-", ""));
                    var cropService = new CropService();
                    var crop = cropService.GetById(cropId);
                    
                    if (crop != null)
                    {
                        lblProductInfo.Text += $"Loại: Cây trồng\n";
                        lblProductInfo.Text += $"Tên: {crop.CropName}\n";
                        lblProductInfo.Text += $"Ngày trồng: {crop.PlantingDate:dd/MM/yyyy}\n";
                        lblProductInfo.Text += $"Thu hoạch dự kiến: {crop.ExpectedHarvestDate:dd/MM/yyyy}\n";
                        lblProductInfo.Text += $"Trạng thái: {crop.Status}\n";
                    }
                    else
                    {
                        lblProductInfo.Text += "Không tìm thấy thông tin cây trồng.";
                    }
                }
                else if (qrData.StartsWith("ANIMAL-"))
                {
                    int animalId = int.Parse(qrData.Replace("ANIMAL-", ""));
                    var animalService = new AnimalService();
                    var animal = animalService.GetById(animalId);
                    
                    if (animal != null)
                    {
                        lblProductInfo.Text += $"Loại: Vật nuôi\n";
                        lblProductInfo.Text += $"Tên: {animal.AnimalType}\n";
                        lblProductInfo.Text += $"Giống: {animal.Breed}\n";
                        lblProductInfo.Text += $"Ngày nhập: {animal.PurchaseDate:dd/MM/yyyy}\n";
                        lblProductInfo.Text += $"Số lượng: {animal.Quantity}\n";
                        lblProductInfo.Text += $"Trạng thái: {animal.Status}\n";
                    }
                    else
                    {
                        lblProductInfo.Text += "Không tìm thấy thông tin vật nuôi.";
                    }
                }
                else
                {
                    lblProductInfo.Text += "Định dạng mã QR không hợp lệ.\n";
                    lblProductInfo.Text += "Vui lòng sử dụng mã có định dạng: CROP-{ID} hoặc ANIMAL-{ID}";
                }
                
                // Thêm vào lịch sử
                lstHistory.Items.Insert(0, $"{DateTime.Now:HH:mm:ss} - {qrData}");
                if (lstHistory.Items.Count > 10)
                    lstHistory.Items.RemoveAt(10);
            }
            catch (Exception ex)
            {
                lblProductInfo.Text = $"Lỗi khi xử lý mã QR:\n{ex.Message}";
            }
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            lstHistory.Items.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopCamera();
            base.OnFormClosing(e);
        }
    }
}
