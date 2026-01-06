namespace GUI.Forms
{
    partial class frmQRScanner
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
            this.pnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCamera = new System.Windows.Forms.Panel();
            this.lblCameraTitle = new System.Windows.Forms.Label();
            this.pnlCameraPreview = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnStopScan = new System.Windows.Forms.Button();
            this.btnStartScan = new System.Windows.Forms.Button();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblInfoTitle = new System.Windows.Forms.Label();
            this.lblProductInfo = new System.Windows.Forms.Label();
            this.pnlManual = new System.Windows.Forms.Panel();
            this.btnManualInput = new System.Windows.Forms.Button();
            this.txtManualInput = new System.Windows.Forms.TextBox();
            this.lblManualTitle = new System.Windows.Forms.Label();
            this.pnlHistory = new System.Windows.Forms.Panel();
            this.btnClearHistory = new System.Windows.Forms.Button();
            this.lstHistory = new System.Windows.Forms.ListBox();
            this.lblHistoryTitle = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnlCamera.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.pnlManual.SuspendLayout();
            this.pnlHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.ColumnCount = 2;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlMain.Controls.Add(this.pnlCamera, 0, 0);
            this.pnlMain.Controls.Add(this.pnlInfo, 1, 0);
            this.pnlMain.Controls.Add(this.pnlManual, 0, 1);
            this.pnlMain.Controls.Add(this.pnlHistory, 1, 1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(20, 50);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 2;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.pnlMain.Size = new System.Drawing.Size(960, 530);
            this.pnlMain.TabIndex = 0;
            this.pnlMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            // 
            // pnlCamera
            // 
            this.pnlCamera.BackColor = System.Drawing.Color.White;
            this.pnlCamera.Controls.Add(this.pnlControls);
            this.pnlCamera.Controls.Add(this.lblStatus);
            this.pnlCamera.Controls.Add(this.pnlCameraPreview);
            this.pnlCamera.Controls.Add(this.lblCameraTitle);
            this.pnlCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCamera.Location = new System.Drawing.Point(10, 10);
            this.pnlCamera.Margin = new System.Windows.Forms.Padding(10);
            this.pnlCamera.Name = "pnlCamera";
            this.pnlCamera.Size = new System.Drawing.Size(460, 298);
            this.pnlCamera.TabIndex = 0;
            // 
            // lblCameraTitle
            // 
            this.lblCameraTitle.AutoSize = true;
            this.lblCameraTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblCameraTitle.Location = new System.Drawing.Point(15, 15);
            this.lblCameraTitle.Name = "lblCameraTitle";
            this.lblCameraTitle.Size = new System.Drawing.Size(132, 28);
            this.lblCameraTitle.TabIndex = 0;
            this.lblCameraTitle.Text = "Quét mã QR";
            // 
            // pnlCameraPreview
            // 
            this.pnlCameraPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlCameraPreview.Location = new System.Drawing.Point(20, 55);
            this.pnlCameraPreview.Name = "pnlCameraPreview";
            this.pnlCameraPreview.Size = new System.Drawing.Size(420, 160);
            this.pnlCameraPreview.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(20, 225);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(150, 20);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Sẵn sàng quét mã QR";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.btnStopScan);
            this.pnlControls.Controls.Add(this.btnStartScan);
            this.pnlControls.Location = new System.Drawing.Point(20, 250);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(420, 35);
            this.pnlControls.TabIndex = 3;
            // 
            // btnStartScan
            // 
            this.btnStartScan.BackColor = System.Drawing.Color.LimeGreen;
            this.btnStartScan.FlatAppearance.BorderSize = 0;
            this.btnStartScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartScan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnStartScan.ForeColor = System.Drawing.Color.White;
            this.btnStartScan.Location = new System.Drawing.Point(0, 0);
            this.btnStartScan.Name = "btnStartScan";
            this.btnStartScan.Size = new System.Drawing.Size(120, 35);
            this.btnStartScan.TabIndex = 0;
            this.btnStartScan.Text = "Bắt đầu quét";
            this.btnStartScan.UseVisualStyleBackColor = false;
            this.btnStartScan.Click += new System.EventHandler(this.btnStartScan_Click);
            // 
            // btnStopScan
            // 
            this.btnStopScan.BackColor = System.Drawing.Color.Crimson;
            this.btnStopScan.Enabled = false;
            this.btnStopScan.FlatAppearance.BorderSize = 0;
            this.btnStopScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopScan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnStopScan.ForeColor = System.Drawing.Color.White;
            this.btnStopScan.Location = new System.Drawing.Point(130, 0);
            this.btnStopScan.Name = "btnStopScan";
            this.btnStopScan.Size = new System.Drawing.Size(120, 35);
            this.btnStopScan.TabIndex = 1;
            this.btnStopScan.Text = "Dừng quét";
            this.btnStopScan.UseVisualStyleBackColor = false;
            this.btnStopScan.Click += new System.EventHandler(this.btnStopScan_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.Gray;
            this.btnExit.Location = new System.Drawing.Point(960, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(35, 35);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "✕";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pnlInfo
            // 
            this.pnlInfo.BackColor = System.Drawing.Color.White;
            this.pnlInfo.Controls.Add(this.lblProductInfo);
            this.pnlInfo.Controls.Add(this.lblInfoTitle);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInfo.Location = new System.Drawing.Point(490, 10);
            this.pnlInfo.Margin = new System.Windows.Forms.Padding(10);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(460, 298);
            this.pnlInfo.TabIndex = 1;
            // 
            // lblInfoTitle
            // 
            this.lblInfoTitle.AutoSize = true;
            this.lblInfoTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblInfoTitle.Location = new System.Drawing.Point(15, 15);
            this.lblInfoTitle.Name = "lblInfoTitle";
            this.lblInfoTitle.Size = new System.Drawing.Size(184, 28);
            this.lblInfoTitle.TabIndex = 0;
            this.lblInfoTitle.Text = "Thông tin sản phẩm";
            // 
            // lblProductInfo
            // 
            this.lblProductInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblProductInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblProductInfo.Location = new System.Drawing.Point(20, 55);
            this.lblProductInfo.Name = "lblProductInfo";
            this.lblProductInfo.Size = new System.Drawing.Size(420, 220);
            this.lblProductInfo.TabIndex = 1;
            this.lblProductInfo.Text = "Quét mã QR để xem thông tin sản phẩm...";
            // 
            // pnlManual
            // 
            this.pnlManual.BackColor = System.Drawing.Color.White;
            this.pnlManual.Controls.Add(this.btnManualInput);
            this.pnlManual.Controls.Add(this.txtManualInput);
            this.pnlManual.Controls.Add(this.lblManualTitle);
            this.pnlManual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlManual.Location = new System.Drawing.Point(10, 328);
            this.pnlManual.Margin = new System.Windows.Forms.Padding(10);
            this.pnlManual.Name = "pnlManual";
            this.pnlManual.Size = new System.Drawing.Size(460, 192);
            this.pnlManual.TabIndex = 2;
            // 
            // lblManualTitle
            // 
            this.lblManualTitle.AutoSize = true;
            this.lblManualTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblManualTitle.Location = new System.Drawing.Point(15, 15);
            this.lblManualTitle.Name = "lblManualTitle";
            this.lblManualTitle.Size = new System.Drawing.Size(165, 25);
            this.lblManualTitle.TabIndex = 0;
            this.lblManualTitle.Text = "Nhập mã thủ công";
            // 
            // txtManualInput
            // 
            this.txtManualInput.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtManualInput.Location = new System.Drawing.Point(20, 55);
            this.txtManualInput.Name = "txtManualInput";
            this.txtManualInput.Size = new System.Drawing.Size(420, 32);
            this.txtManualInput.TabIndex = 1;
            // 
            // btnManualInput
            // 
            this.btnManualInput.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnManualInput.FlatAppearance.BorderSize = 0;
            this.btnManualInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManualInput.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnManualInput.ForeColor = System.Drawing.Color.White;
            this.btnManualInput.Location = new System.Drawing.Point(20, 100);
            this.btnManualInput.Name = "btnManualInput";
            this.btnManualInput.Size = new System.Drawing.Size(120, 35);
            this.btnManualInput.TabIndex = 2;
            this.btnManualInput.Text = "Tra cứu";
            this.btnManualInput.UseVisualStyleBackColor = false;
            this.btnManualInput.Click += new System.EventHandler(this.btnManualInput_Click);
            // 
            // pnlHistory
            // 
            this.pnlHistory.BackColor = System.Drawing.Color.White;
            this.pnlHistory.Controls.Add(this.btnClearHistory);
            this.pnlHistory.Controls.Add(this.lstHistory);
            this.pnlHistory.Controls.Add(this.lblHistoryTitle);
            this.pnlHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHistory.Location = new System.Drawing.Point(490, 328);
            this.pnlHistory.Margin = new System.Windows.Forms.Padding(10);
            this.pnlHistory.Name = "pnlHistory";
            this.pnlHistory.Size = new System.Drawing.Size(460, 192);
            this.pnlHistory.TabIndex = 3;
            // 
            // lblHistoryTitle
            // 
            this.lblHistoryTitle.AutoSize = true;
            this.lblHistoryTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblHistoryTitle.Location = new System.Drawing.Point(15, 15);
            this.lblHistoryTitle.Name = "lblHistoryTitle";
            this.lblHistoryTitle.Size = new System.Drawing.Size(107, 25);
            this.lblHistoryTitle.TabIndex = 0;
            this.lblHistoryTitle.Text = "Lịch sử quét";
            // 
            // lstHistory
            // 
            this.lstHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstHistory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstHistory.FormattingEnabled = true;
            this.lstHistory.ItemHeight = 20;
            this.lstHistory.Location = new System.Drawing.Point(20, 55);
            this.lstHistory.Name = "lstHistory";
            this.lstHistory.Size = new System.Drawing.Size(420, 60);
            this.lstHistory.TabIndex = 1;
            // 
            // btnClearHistory
            // 
            this.btnClearHistory.BackColor = System.Drawing.Color.Gray;
            this.btnClearHistory.FlatAppearance.BorderSize = 0;
            this.btnClearHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearHistory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnClearHistory.ForeColor = System.Drawing.Color.White;
            this.btnClearHistory.Location = new System.Drawing.Point(320, 130);
            this.btnClearHistory.Name = "btnClearHistory";
            this.btnClearHistory.Size = new System.Drawing.Size(120, 35);
            this.btnClearHistory.TabIndex = 2;
            this.btnClearHistory.Text = "Xóa lịch sử";
            this.btnClearHistory.UseVisualStyleBackColor = false;
            this.btnClearHistory.Click += new System.EventHandler(this.btnClearHistory_Click);
            // 
            // frmQRScanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmQRScanner";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Text = "Quét mã QR";
            this.Load += new System.EventHandler(this.frmQRScanner_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.pnlMain.ResumeLayout(false);
            this.pnlCamera.ResumeLayout(false);
            this.pnlCamera.PerformLayout();
            this.pnlControls.ResumeLayout(false);
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            this.pnlManual.ResumeLayout(false);
            this.pnlManual.PerformLayout();
            this.pnlHistory.ResumeLayout(false);
            this.pnlHistory.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlMain;
        private System.Windows.Forms.Panel pnlCamera;
        private System.Windows.Forms.Label lblCameraTitle;
        private System.Windows.Forms.Panel pnlCameraPreview;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Button btnStopScan;
        private System.Windows.Forms.Button btnStartScan;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label lblProductInfo;
        private System.Windows.Forms.Label lblInfoTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel pnlManual;
        private System.Windows.Forms.Button btnManualInput;
        private System.Windows.Forms.TextBox txtManualInput;
        private System.Windows.Forms.Label lblManualTitle;
        private System.Windows.Forms.Panel pnlHistory;
        private System.Windows.Forms.Button btnClearHistory;
        private System.Windows.Forms.ListBox lstHistory;
        private System.Windows.Forms.Label lblHistoryTitle;
    }
}
