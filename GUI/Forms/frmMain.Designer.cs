namespace GUI.Forms
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlUserSection = new System.Windows.Forms.Panel();
            this.lblUserRole = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.picUserAvatar = new System.Windows.Forms.PictureBox();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnFinance = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.btnAnimals = new System.Windows.Forms.Button();
            this.btnCrops = new System.Windows.Forms.Button();
            this.btnFarms = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.lblLogoSystem = new System.Windows.Forms.Label();
            this.picSystemLogo = new System.Windows.Forms.PictureBox();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblDateWeather = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.picSearchIcon = new System.Windows.Forms.PictureBox();
            this.btnNotification = new System.Windows.Forms.Button();
            this.btnUserList = new System.Windows.Forms.Button();
            this.btnActivityLog = new System.Windows.Forms.Button();
            this.btnRevenue = new System.Windows.Forms.Button();
            this.btnExpense = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnSystemLog = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnQRScanner = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.lblStatus = new System.Windows.Forms.StatusStrip();
            this.lblHeaderManagement = new System.Windows.Forms.Label();
            this.lblHeaderFinance = new System.Windows.Forms.Label();
            this.lblHeaderSystem = new System.Windows.Forms.Label();
            this.pnlSidebar.SuspendLayout();
            this.pnlUserSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUserAvatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSystemLogo)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(253)))), ((int)(((byte)(248)))));
            this.pnlSidebar.Controls.Add(this.picSystemLogo);
            this.pnlSidebar.Controls.Add(this.lblHeaderManagement);
            this.pnlSidebar.Controls.Add(this.btnDashboard);
            this.pnlSidebar.Controls.Add(this.btnFarms);
            this.pnlSidebar.Controls.Add(this.btnCrops);
            this.pnlSidebar.Controls.Add(this.btnAnimals);
            this.pnlSidebar.Controls.Add(this.btnInventory);
            this.pnlSidebar.Controls.Add(this.btnUserList);
            this.pnlSidebar.Controls.Add(this.btnActivityLog);
            this.pnlSidebar.Controls.Add(this.lblHeaderFinance);
            this.pnlSidebar.Controls.Add(this.btnRevenue);
            this.pnlSidebar.Controls.Add(this.btnExpense);
            this.pnlSidebar.Controls.Add(this.btnReport);
            this.pnlSidebar.Controls.Add(this.lblHeaderSystem);
            this.pnlSidebar.Controls.Add(this.btnSystemLog);
            this.pnlSidebar.Controls.Add(this.btnBackup);
            this.pnlSidebar.Controls.Add(this.btnQRScanner);
            this.pnlSidebar.Controls.Add(this.btnSettings);
            this.pnlSidebar.Controls.Add(this.btnLogout);
            this.pnlSidebar.Controls.Add(this.pnlUserSection);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(260, 1000);
            this.pnlSidebar.AutoScroll = true;
            this.pnlSidebar.TabIndex = 0;
            // 
            // pnlUserSection
            // 
            this.pnlUserSection.Controls.Add(this.lblUserRole);
            this.pnlUserSection.Controls.Add(this.lblUserName);
            this.pnlUserSection.Controls.Add(this.picUserAvatar);
            this.pnlUserSection.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlUserSection.Location = new System.Drawing.Point(0, 940);
            this.pnlUserSection.Name = "pnlUserSection";
            this.pnlUserSection.Size = new System.Drawing.Size(260, 80);
            this.pnlUserSection.TabIndex = 9;
            // 
            // lblUserRole
            // 
            this.lblUserRole.AutoSize = true;
            this.lblUserRole.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblUserRole.ForeColor = System.Drawing.Color.Gray;
            this.lblUserRole.Location = new System.Drawing.Point(70, 45);
            this.lblUserRole.Name = "lblUserRole";
            this.lblUserRole.Size = new System.Drawing.Size(56, 19);
            this.lblUserRole.TabIndex = 2;
            this.lblUserRole.Text = "Quản lý";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.Color.Black;
            this.lblUserName.Location = new System.Drawing.Point(70, 20);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(102, 23);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "Minh Hoàng";
            // 
            // picUserAvatar
            // 
            this.picUserAvatar.Image = global::GUI.Properties.Resources.avata;
            this.picUserAvatar.Location = new System.Drawing.Point(15, 15);
            this.picUserAvatar.Name = "picUserAvatar";
            this.picUserAvatar.Size = new System.Drawing.Size(50, 50);
            this.picUserAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picUserAvatar.TabIndex = 0;
            this.picUserAvatar.TabStop = false;
            // 
            // btnSettings
            // 
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnSettings.ForeColor = System.Drawing.Color.DimGray;
            this.btnSettings.Location = new System.Drawing.Point(10, 890);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(240, 50);
            this.btnSettings.TabIndex = 8;
            this.btnSettings.Text = "    Đổi mật khẩu";
            this.btnSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.DimGray;
            this.btnLogout.Location = new System.Drawing.Point(10, 940);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(240, 50);
            this.btnLogout.TabIndex = 9;
            this.btnLogout.Text = "    Đình chỉ / Thoát";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnUserList
            // 
            this.btnUserList.FlatAppearance.BorderSize = 0;
            this.btnUserList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserList.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnUserList.ForeColor = System.Drawing.Color.DimGray;
            this.btnUserList.Location = new System.Drawing.Point(10, 390);
            this.btnUserList.Name = "btnUserList";
            this.btnUserList.Size = new System.Drawing.Size(240, 50);
            this.btnUserList.TabIndex = 10;
            this.btnUserList.Text = "    Người dùng";
            this.btnUserList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserList.UseVisualStyleBackColor = true;
            this.btnUserList.Visible = false;
            this.btnUserList.Click += new System.EventHandler(this.mnUserList_Click);
            // 
            // btnActivityLog
            // 
            this.btnActivityLog.FlatAppearance.BorderSize = 0;
            this.btnActivityLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActivityLog.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnActivityLog.ForeColor = System.Drawing.Color.DimGray;
            this.btnActivityLog.Location = new System.Drawing.Point(10, 440);
            this.btnActivityLog.Name = "btnActivityLog";
            this.btnActivityLog.Size = new System.Drawing.Size(240, 50);
            this.btnActivityLog.TabIndex = 11;
            this.btnActivityLog.Text = "    Nhật ký HĐ";
            this.btnActivityLog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActivityLog.UseVisualStyleBackColor = true;
            this.btnActivityLog.Click += new System.EventHandler(this.mnActivities_Click);
            // 
            // btnRevenue
            // 
            this.btnRevenue.FlatAppearance.BorderSize = 0;
            this.btnRevenue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRevenue.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnRevenue.ForeColor = System.Drawing.Color.DimGray;
            this.btnRevenue.Location = new System.Drawing.Point(10, 590);
            this.btnRevenue.Name = "btnRevenue";
            this.btnRevenue.Size = new System.Drawing.Size(240, 50);
            this.btnRevenue.TabIndex = 12;
            this.btnRevenue.Text = "    Doanh thu";
            this.btnRevenue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRevenue.UseVisualStyleBackColor = true;
            this.btnRevenue.Click += new System.EventHandler(this.mnRevenue_Click_1);
            // 
            // btnExpense
            // 
            this.btnExpense.FlatAppearance.BorderSize = 0;
            this.btnExpense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExpense.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnExpense.ForeColor = System.Drawing.Color.DimGray;
            this.btnExpense.Location = new System.Drawing.Point(10, 640);
            this.btnExpense.Name = "btnExpense";
            this.btnExpense.Size = new System.Drawing.Size(240, 50);
            this.btnExpense.TabIndex = 13;
            this.btnExpense.Text = "    Chi phí";
            this.btnExpense.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExpense.UseVisualStyleBackColor = true;
            this.btnExpense.Click += new System.EventHandler(this.mnExpense_Click);
            // 
            // btnReport
            // 
            this.btnReport.FlatAppearance.BorderSize = 0;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnReport.ForeColor = System.Drawing.Color.DimGray;
            this.btnReport.Location = new System.Drawing.Point(10, 690);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(240, 50);
            this.btnReport.TabIndex = 14;
            this.btnReport.Text = "    Báo cáo";
            this.btnReport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.mnReport_Click);
            // 
            // btnSystemLog
            // 
            this.btnSystemLog.FlatAppearance.BorderSize = 0;
            this.btnSystemLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSystemLog.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnSystemLog.ForeColor = System.Drawing.Color.DimGray;
            this.btnSystemLog.Location = new System.Drawing.Point(10, 790);
            this.btnSystemLog.Name = "btnSystemLog";
            this.btnSystemLog.Size = new System.Drawing.Size(240, 50);
            this.btnSystemLog.TabIndex = 15;
            this.btnSystemLog.Text = "    Nhật ký HT";
            this.btnSystemLog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSystemLog.UseVisualStyleBackColor = true;
            this.btnSystemLog.Click += new System.EventHandler(this.mnSystemLog_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.FlatAppearance.BorderSize = 0;
            this.btnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackup.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnBackup.ForeColor = System.Drawing.Color.DimGray;
            this.btnBackup.Location = new System.Drawing.Point(10, 840);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(240, 50);
            this.btnBackup.TabIndex = 16;
            this.btnBackup.Text = "    Sao lưu/PH";
            this.btnBackup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.mnBackup_Click);
            // 
            // btnQRScanner
            // 
            this.btnQRScanner.FlatAppearance.BorderSize = 0;
            this.btnQRScanner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQRScanner.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnQRScanner.ForeColor = System.Drawing.Color.DimGray;
            this.btnQRScanner.Location = new System.Drawing.Point(10, 490);
            this.btnQRScanner.Name = "btnQRScanner";
            this.btnQRScanner.Size = new System.Drawing.Size(240, 50);
            this.btnQRScanner.TabIndex = 17;
            this.btnQRScanner.Text = "    Quét mã QR";
            this.btnQRScanner.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQRScanner.UseVisualStyleBackColor = true;
            this.btnQRScanner.Click += new System.EventHandler(this.btnQRScanner_Click);
            // 
            // lblHeaderManagement
            // 
            this.lblHeaderManagement.AutoSize = true;
            this.lblHeaderManagement.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHeaderManagement.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.lblHeaderManagement.Location = new System.Drawing.Point(15, 110);
            this.lblHeaderManagement.Name = "lblHeaderManagement";
            this.lblHeaderManagement.Size = new System.Drawing.Size(75, 20);
            this.lblHeaderManagement.Text = "QUẢN LÝ";
            // 
            // lblHeaderFinance
            // 
            this.lblHeaderFinance.AutoSize = true;
            this.lblHeaderFinance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHeaderFinance.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.lblHeaderFinance.Location = new System.Drawing.Point(15, 560);
            this.lblHeaderFinance.Name = "lblHeaderFinance";
            this.lblHeaderFinance.Size = new System.Drawing.Size(86, 20);
            this.lblHeaderFinance.Text = "THỐNG KÊ";
            // 
            // lblHeaderSystem
            // 
            this.lblHeaderSystem.AutoSize = true;
            this.lblHeaderSystem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHeaderSystem.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.lblHeaderSystem.Location = new System.Drawing.Point(15, 760);
            this.lblHeaderSystem.Name = "lblHeaderSystem";
            this.lblHeaderSystem.Size = new System.Drawing.Size(90, 20);
            this.lblHeaderSystem.Text = "HỆ THỐNG";
            // 
            // btnInventory
            // 
            this.btnInventory.FlatAppearance.BorderSize = 0;
            this.btnInventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInventory.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnInventory.ForeColor = System.Drawing.Color.DimGray;
            this.btnInventory.Location = new System.Drawing.Point(10, 340);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(240, 50);
            this.btnInventory.TabIndex = 6;
            this.btnInventory.Text = "    Kho vật tư";
            this.btnInventory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInventory.UseVisualStyleBackColor = true;
            this.btnInventory.Click += new System.EventHandler(this.mnInventory_Click);
            // 
            // btnAnimals
            // 
            this.btnAnimals.FlatAppearance.BorderSize = 0;
            this.btnAnimals.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnimals.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnAnimals.ForeColor = System.Drawing.Color.DimGray;
            this.btnAnimals.Location = new System.Drawing.Point(10, 290);
            this.btnAnimals.Name = "btnAnimals";
            this.btnAnimals.Size = new System.Drawing.Size(240, 50);
            this.btnAnimals.TabIndex = 5;
            this.btnAnimals.Text = "    Vật nuôi";
            this.btnAnimals.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnimals.UseVisualStyleBackColor = true;
            this.btnAnimals.Click += new System.EventHandler(this.mnAnimals_Click);
            // 
            // btnCrops
            // 
            this.btnCrops.FlatAppearance.BorderSize = 0;
            this.btnCrops.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrops.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCrops.ForeColor = System.Drawing.Color.DimGray;
            this.btnCrops.Location = new System.Drawing.Point(10, 240);
            this.btnCrops.Name = "btnCrops";
            this.btnCrops.Size = new System.Drawing.Size(240, 50);
            this.btnCrops.TabIndex = 4;
            this.btnCrops.Text = "    Cây trồng";
            this.btnCrops.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCrops.UseVisualStyleBackColor = true;
            this.btnCrops.Click += new System.EventHandler(this.mnCrops_Click);
            // 
            // btnFarms
            // 
            this.btnFarms.FlatAppearance.BorderSize = 0;
            this.btnFarms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFarms.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnFarms.ForeColor = System.Drawing.Color.DimGray;
            this.btnFarms.Location = new System.Drawing.Point(10, 190);
            this.btnFarms.Name = "btnFarms";
            this.btnFarms.Size = new System.Drawing.Size(240, 50);
            this.btnFarms.TabIndex = 3;
            this.btnFarms.Text = "    Nông trại";
            this.btnFarms.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFarms.UseVisualStyleBackColor = true;
            this.btnFarms.Click += new System.EventHandler(this.mnFarms_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(245)))), ((int)(((byte)(215)))));
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnDashboard.ForeColor = System.Drawing.Color.LimeGreen;
            this.btnDashboard.Location = new System.Drawing.Point(10, 140);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(240, 50);
            this.btnDashboard.TabIndex = 2;
            this.btnDashboard.Text = "    Tổng quan";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // lblLogoSystem
            // 
            this.lblLogoSystem.AutoSize = true;
            this.lblLogoSystem.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblLogoSystem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblLogoSystem.Location = new System.Drawing.Point(60, 42);
            this.lblLogoSystem.Name = "lblLogoSystem";
            this.lblLogoSystem.Size = new System.Drawing.Size(188, 37);
            this.lblLogoSystem.TabIndex = 1;
            this.lblLogoSystem.Text = "Hệ thống Quản lý";
            // 
            // picSystemLogo
            // 
            this.picSystemLogo.Image = global::GUI.Properties.Resources.Save;
            this.picSystemLogo.Location = new System.Drawing.Point(15, 40);
            this.picSystemLogo.Name = "picSystemLogo";
            this.picSystemLogo.Size = new System.Drawing.Size(40, 40);
            this.picSystemLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSystemLogo.TabIndex = 0;
            this.picSystemLogo.TabStop = false;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.btnNotification);
            this.pnlHeader.Controls.Add(this.pnlSearch);
            this.pnlHeader.Controls.Add(this.lblDateWeather);
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(260, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1020, 100);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblDateWeather
            // 
            this.lblDateWeather.AutoSize = true;
            this.lblDateWeather.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDateWeather.ForeColor = System.Drawing.Color.Gray;
            this.lblDateWeather.Location = new System.Drawing.Point(25, 65);
            this.lblDateWeather.Name = "lblDateWeather";
            this.lblDateWeather.Size = new System.Drawing.Size(236, 20);
            this.lblDateWeather.TabIndex = 1;
            this.lblDateWeather.Text = "Thứ Hai, 05 tháng 01 • Nắng nhẹ, 28°C";
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblHeaderTitle.Location = new System.Drawing.Point(20, 20);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(171, 41);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "Tổng quan";
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.picSearchIcon);
            this.pnlSearch.Location = new System.Drawing.Point(620, 30);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(300, 40);
            this.pnlSearch.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(40, 10);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(250, 23);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.Text = "Tìm kiếm nông trại, vật tư...";
            // 
            // picSearchIcon
            // 
            this.picSearchIcon.Location = new System.Drawing.Point(10, 10);
            this.picSearchIcon.Name = "picSearchIcon";
            this.picSearchIcon.Size = new System.Drawing.Size(20, 20);
            this.picSearchIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSearchIcon.TabIndex = 0;
            this.picSearchIcon.TabStop = false;
            // 
            // btnNotification
            // 
            this.btnNotification.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnNotification.FlatAppearance.BorderSize = 0;
            this.btnNotification.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNotification.Location = new System.Drawing.Point(940, 30);
            this.btnNotification.Name = "btnNotification";
            this.btnNotification.Size = new System.Drawing.Size(40, 40);
            this.btnNotification.TabIndex = 3;
            this.btnNotification.UseVisualStyleBackColor = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlSidebar);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản lý Nông nghiệp";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            
            // Hide legacy UI safely
            if (this.menuStrip1 != null) this.menuStrip1.Visible = false;
            if (this.lblStatus != null) this.lblStatus.Visible = false;

            this.pnlSidebar.ResumeLayout(false);
            this.pnlSidebar.PerformLayout();
            this.pnlUserSection.ResumeLayout(false);
            this.pnlUserSection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUserAvatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSystemLogo)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSearchIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.PictureBox picSystemLogo;
        private System.Windows.Forms.Label lblLogoSystem;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnFarms;
        private System.Windows.Forms.Button btnCrops;
        private System.Windows.Forms.Button btnAnimals;
        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.Button btnFinance;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnUserList;
        private System.Windows.Forms.Button btnActivityLog;
        private System.Windows.Forms.Button btnRevenue;
        private System.Windows.Forms.Button btnExpense;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnSystemLog;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Label lblHeaderManagement;
        private System.Windows.Forms.Label lblHeaderFinance;
        private System.Windows.Forms.Label lblHeaderSystem;
        private System.Windows.Forms.Panel pnlUserSection;
        private System.Windows.Forms.PictureBox picUserAvatar;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblUserRole;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblDateWeather;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.PictureBox picSearchIcon;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnNotification;

        // Keep existing menu items for permission logic compatibility (can be hidden/not used in UI)
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnSystem;
        private System.Windows.Forms.ToolStripMenuItem mnLogout;
        private System.Windows.Forms.ToolStripMenuItem mnExit;
        private System.Windows.Forms.ToolStripMenuItem mnManage;
        private System.Windows.Forms.ToolStripMenuItem mnFarms;
        private System.Windows.Forms.ToolStripMenuItem mnCrops;
        private System.Windows.Forms.ToolStripMenuItem mnAnimals;
        private System.Windows.Forms.ToolStripMenuItem mnReport;
        private System.Windows.Forms.StatusStrip lblStatus; // Keep for now
        private System.Windows.Forms.ToolStripMenuItem mnActivities;
        private System.Windows.Forms.ToolStripMenuItem mnChangePass;
        private System.Windows.Forms.Button btnQRScanner;
        private System.Windows.Forms.ToolStripMenuItem mnFinanceGroup;
        private System.Windows.Forms.ToolStripMenuItem mnInventory;
        private System.Windows.Forms.ToolStripMenuItem mnRevenue;
        private System.Windows.Forms.ToolStripMenuItem mnExpense;
        private System.Windows.Forms.ToolStripMenuItem mnUserList;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusUser;
        private System.Windows.Forms.ToolStripMenuItem mnSystemLog;
        private System.Windows.Forms.ToolStripMenuItem mnBackup;
    }
}