namespace GUI.Forms
{
    partial class frmDashboard
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
            this.pnlCards = new System.Windows.Forms.TableLayoutPanel();
            this.pnlProfitCard = new System.Windows.Forms.Panel();
            this.lblProfitTitle = new System.Windows.Forms.Label();
            this.lblProfit = new System.Windows.Forms.Label();
            this.pnlLowStockCard = new System.Windows.Forms.Panel();
            this.lblLowStockTitle = new System.Windows.Forms.Label();
            this.lblLowStock = new System.Windows.Forms.Label();
            this.pnlAnimalCard = new System.Windows.Forms.Panel();
            this.lblAnimalTitle = new System.Windows.Forms.Label();
            this.lblAnimalCount = new System.Windows.Forms.Label();
            this.pnlCropCard = new System.Windows.Forms.Panel();
            this.lblCropTitle = new System.Windows.Forms.Label();
            this.lblCropCount = new System.Windows.Forms.Label();
            this.pnlMainGrid = new System.Windows.Forms.TableLayoutPanel();
            this.pnlMiddleRow = new System.Windows.Forms.TableLayoutPanel();
            this.pnlChart = new System.Windows.Forms.Panel();
            this.lblChartTitle = new System.Windows.Forms.Label();
            this.pnlChartMock = new System.Windows.Forms.Panel();
            this.pnlTasks = new System.Windows.Forms.Panel();
            this.lblTasksTitle = new System.Windows.Forms.Label();
            this.dgvTasks = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlCards.SuspendLayout();
            this.pnlProfitCard.SuspendLayout();
            this.pnlLowStockCard.SuspendLayout();
            this.pnlAnimalCard.SuspendLayout();
            this.pnlCropCard.SuspendLayout();
            this.pnlMainGrid.SuspendLayout();
            this.pnlMiddleRow.SuspendLayout();
            this.pnlChart.SuspendLayout();
            this.pnlTasks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCards
            // 
            this.pnlCards.ColumnCount = 4;
            this.pnlCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlCards.Controls.Add(this.pnlProfitCard, 3, 0);
            this.pnlCards.Controls.Add(this.pnlLowStockCard, 2, 0);
            this.pnlCards.Controls.Add(this.pnlAnimalCard, 1, 0);
            this.pnlCards.Controls.Add(this.pnlCropCard, 0, 0);
            this.pnlCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCards.Location = new System.Drawing.Point(20, 20);
            this.pnlCards.Name = "pnlCards";
            this.pnlCards.RowCount = 1;
            this.pnlCards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlCards.Size = new System.Drawing.Size(960, 150);
            this.pnlCards.TabIndex = 0;
            // 
            // pnlProfitCard
            // 
            this.pnlProfitCard.BackColor = System.Drawing.Color.White;
            this.pnlProfitCard.Controls.Add(this.lblProfitTitle);
            this.pnlProfitCard.Controls.Add(this.lblProfit);
            this.pnlProfitCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProfitCard.Location = new System.Drawing.Point(730, 10);
            this.pnlProfitCard.Margin = new System.Windows.Forms.Padding(10);
            this.pnlProfitCard.Name = "pnlProfitCard";
            this.pnlProfitCard.Size = new System.Drawing.Size(220, 130);
            this.pnlProfitCard.TabIndex = 3;
            // 
            // lblProfitTitle
            // 
            this.lblProfitTitle.AutoSize = true;
            this.lblProfitTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblProfitTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblProfitTitle.Location = new System.Drawing.Point(15, 20);
            this.lblProfitTitle.Name = "lblProfitTitle";
            this.lblProfitTitle.Size = new System.Drawing.Size(128, 20);
            this.lblProfitTitle.TabIndex = 1;
            this.lblProfitTitle.Text = "Lợi nhuận (Tháng)";
            // 
            // lblProfit
            // 
            this.lblProfit.AutoSize = true;
            this.lblProfit.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblProfit.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblProfit.Location = new System.Drawing.Point(15, 60);
            this.lblProfit.Name = "lblProfit";
            this.lblProfit.Size = new System.Drawing.Size(109, 41);
            this.lblProfit.TabIndex = 0;
            this.lblProfit.Text = "0 VNĐ";
            // 
            // pnlLowStockCard
            // 
            this.pnlLowStockCard.BackColor = System.Drawing.Color.White;
            this.pnlLowStockCard.Controls.Add(this.lblLowStockTitle);
            this.pnlLowStockCard.Controls.Add(this.lblLowStock);
            this.pnlLowStockCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLowStockCard.Location = new System.Drawing.Point(490, 10);
            this.pnlLowStockCard.Margin = new System.Windows.Forms.Padding(10);
            this.pnlLowStockCard.Name = "pnlLowStockCard";
            this.pnlLowStockCard.Size = new System.Drawing.Size(220, 130);
            this.pnlLowStockCard.TabIndex = 2;
            // 
            // lblLowStockTitle
            // 
            this.lblLowStockTitle.AutoSize = true;
            this.lblLowStockTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLowStockTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblLowStockTitle.Location = new System.Drawing.Point(15, 20);
            this.lblLowStockTitle.Name = "lblLowStockTitle";
            this.lblLowStockTitle.Size = new System.Drawing.Size(127, 20);
            this.lblLowStockTitle.TabIndex = 1;
            this.lblLowStockTitle.Text = "Sắp hết trong kho";
            // 
            // lblLowStock
            // 
            this.lblLowStock.AutoSize = true;
            this.lblLowStock.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblLowStock.ForeColor = System.Drawing.Color.Crimson;
            this.lblLowStock.Location = new System.Drawing.Point(15, 60);
            this.lblLowStock.Name = "lblLowStock";
            this.lblLowStock.Size = new System.Drawing.Size(35, 41);
            this.lblLowStock.TabIndex = 0;
            this.lblLowStock.Text = "0";
            // 
            // pnlAnimalCard
            // 
            this.pnlAnimalCard.BackColor = System.Drawing.Color.White;
            this.pnlAnimalCard.Controls.Add(this.lblAnimalTitle);
            this.pnlAnimalCard.Controls.Add(this.lblAnimalCount);
            this.pnlAnimalCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAnimalCard.Location = new System.Drawing.Point(250, 10);
            this.pnlAnimalCard.Margin = new System.Windows.Forms.Padding(10);
            this.pnlAnimalCard.Name = "pnlAnimalCard";
            this.pnlAnimalCard.Size = new System.Drawing.Size(220, 130);
            this.pnlAnimalCard.TabIndex = 1;
            // 
            // lblAnimalTitle
            // 
            this.lblAnimalTitle.AutoSize = true;
            this.lblAnimalTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAnimalTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblAnimalTitle.Location = new System.Drawing.Point(15, 20);
            this.lblAnimalTitle.Name = "lblAnimalTitle";
            this.lblAnimalTitle.Size = new System.Drawing.Size(119, 20);
            this.lblAnimalTitle.TabIndex = 1;
            this.lblAnimalTitle.Text = "Tổng số vật nuôi";
            // 
            // lblAnimalCount
            // 
            this.lblAnimalCount.AutoSize = true;
            this.lblAnimalCount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblAnimalCount.ForeColor = System.Drawing.Color.Orange;
            this.lblAnimalCount.Location = new System.Drawing.Point(15, 60);
            this.lblAnimalCount.Name = "lblAnimalCount";
            this.lblAnimalCount.Size = new System.Drawing.Size(35, 41);
            this.lblAnimalCount.TabIndex = 0;
            this.lblAnimalCount.Text = "0";
            // 
            // pnlCropCard
            // 
            this.pnlCropCard.BackColor = System.Drawing.Color.White;
            this.pnlCropCard.Controls.Add(this.lblCropTitle);
            this.pnlCropCard.Controls.Add(this.lblCropCount);
            this.pnlCropCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCropCard.Location = new System.Drawing.Point(10, 10);
            this.pnlCropCard.Margin = new System.Windows.Forms.Padding(10);
            this.pnlCropCard.Name = "pnlCropCard";
            this.pnlCropCard.Size = new System.Drawing.Size(220, 130);
            this.pnlCropCard.TabIndex = 0;
            // 
            // lblCropTitle
            // 
            this.lblCropTitle.AutoSize = true;
            this.lblCropTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCropTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblCropTitle.Location = new System.Drawing.Point(15, 20);
            this.lblCropTitle.Name = "lblCropTitle";
            this.lblCropTitle.Size = new System.Drawing.Size(128, 20);
            this.lblCropTitle.TabIndex = 1;
            this.lblCropTitle.Text = "Tổng số cây trồng";
            // 
            // lblCropCount
            // 
            this.lblCropCount.AutoSize = true;
            this.lblCropCount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblCropCount.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblCropCount.Location = new System.Drawing.Point(15, 60);
            this.lblCropCount.Name = "lblCropCount";
            this.lblCropCount.Size = new System.Drawing.Size(35, 41);
            this.lblCropCount.TabIndex = 0;
            this.lblCropCount.Text = "0";
            // 
            // pnlMainGrid
            // 
            this.pnlMainGrid.ColumnCount = 1;
            this.pnlMainGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMainGrid.Controls.Add(this.pnlMiddleRow, 0, 0);
            this.pnlMainGrid.Controls.Add(this.pnlTasks, 0, 1);
            this.pnlMainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainGrid.Location = new System.Drawing.Point(20, 170);
            this.pnlMainGrid.Name = "pnlMainGrid";
            this.pnlMainGrid.RowCount = 2;
            this.pnlMainGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.pnlMainGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMainGrid.Size = new System.Drawing.Size(960, 430);
            this.pnlMainGrid.TabIndex = 1;
            // 
            // pnlMiddleRow
            // 
            this.pnlMiddleRow.ColumnCount = 2;
            this.pnlMiddleRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 99.16666F));
            this.pnlMiddleRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.8333333F));
            this.pnlMiddleRow.Controls.Add(this.pnlChart, 0, 0);
            this.pnlMiddleRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMiddleRow.Location = new System.Drawing.Point(0, 0);
            this.pnlMiddleRow.Margin = new System.Windows.Forms.Padding(0);
            this.pnlMiddleRow.Name = "pnlMiddleRow";
            this.pnlMiddleRow.RowCount = 1;
            this.pnlMiddleRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMiddleRow.Size = new System.Drawing.Size(960, 200);
            this.pnlMiddleRow.TabIndex = 0;
            // 
            // pnlChart
            // 
            this.pnlChart.BackColor = System.Drawing.Color.White;
            this.pnlChart.Controls.Add(this.lblChartTitle);
            this.pnlChart.Controls.Add(this.pnlChartMock);
            this.pnlChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChart.Location = new System.Drawing.Point(10, 10);
            this.pnlChart.Margin = new System.Windows.Forms.Padding(10);
            this.pnlChart.Name = "pnlChart";
            this.pnlChart.Size = new System.Drawing.Size(932, 180);
            this.pnlChart.TabIndex = 0;
            this.pnlChart.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlChart_Paint);
            // 
            // lblChartTitle
            // 
            this.lblChartTitle.AutoSize = true;
            this.lblChartTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblChartTitle.Location = new System.Drawing.Point(20, 22);
            this.lblChartTitle.Name = "lblChartTitle";
            this.lblChartTitle.Size = new System.Drawing.Size(182, 25);
            this.lblChartTitle.TabIndex = 0;
            this.lblChartTitle.Text = "Thống kê tháng này";
            // 
            // pnlChartMock
            // 
            this.pnlChartMock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.pnlChartMock.Location = new System.Drawing.Point(20, 50);
            this.pnlChartMock.Name = "pnlChartMock";
            this.pnlChartMock.Size = new System.Drawing.Size(900, 115);
            this.pnlChartMock.TabIndex = 1;
            // 
            // pnlTasks
            // 
            this.pnlTasks.BackColor = System.Drawing.Color.White;
            this.pnlTasks.Controls.Add(this.lblTasksTitle);
            this.pnlTasks.Controls.Add(this.dgvTasks);
            this.pnlTasks.Controls.Add(this.btnRefresh);
            this.pnlTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTasks.Location = new System.Drawing.Point(10, 210);
            this.pnlTasks.Margin = new System.Windows.Forms.Padding(10);
            this.pnlTasks.Name = "pnlTasks";
            this.pnlTasks.Size = new System.Drawing.Size(940, 210);
            this.pnlTasks.TabIndex = 1;
            // 
            // lblTasksTitle
            // 
            this.lblTasksTitle.AutoSize = true;
            this.lblTasksTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblTasksTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTasksTitle.Name = "lblTasksTitle";
            this.lblTasksTitle.Size = new System.Drawing.Size(187, 28);
            this.lblTasksTitle.TabIndex = 1;
            this.lblTasksTitle.Text = "Công việc hôm nay";
            // 
            // dgvTasks
            // 
            this.dgvTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTasks.BackgroundColor = System.Drawing.Color.White;
            this.dgvTasks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTasks.Location = new System.Drawing.Point(20, 70);
            this.dgvTasks.Name = "dgvTasks";
            this.dgvTasks.RowHeadersWidth = 51;
            this.dgvTasks.Size = new System.Drawing.Size(900, 90);
            this.dgvTasks.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.LimeGreen;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(820, 172);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 35);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // frmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 620);
            this.Controls.Add(this.pnlMainGrid);
            this.Controls.Add(this.pnlCards);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDashboard";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.frmDashboard_Load);
            this.pnlCards.ResumeLayout(false);
            this.pnlProfitCard.ResumeLayout(false);
            this.pnlProfitCard.PerformLayout();
            this.pnlLowStockCard.ResumeLayout(false);
            this.pnlLowStockCard.PerformLayout();
            this.pnlAnimalCard.ResumeLayout(false);
            this.pnlAnimalCard.PerformLayout();
            this.pnlCropCard.ResumeLayout(false);
            this.pnlCropCard.PerformLayout();
            this.pnlMainGrid.ResumeLayout(false);
            this.pnlMiddleRow.ResumeLayout(false);
            this.pnlChart.ResumeLayout(false);
            this.pnlChart.PerformLayout();
            this.pnlTasks.ResumeLayout(false);
            this.pnlTasks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlCards;
        private System.Windows.Forms.Panel pnlCropCard;
        private System.Windows.Forms.Label lblCropTitle;
        private System.Windows.Forms.Label lblCropCount;
        private System.Windows.Forms.Panel pnlAnimalCard;
        private System.Windows.Forms.Label lblAnimalTitle;
        private System.Windows.Forms.Label lblAnimalCount;
        private System.Windows.Forms.Panel pnlLowStockCard;
        private System.Windows.Forms.Label lblLowStockTitle;
        private System.Windows.Forms.Label lblLowStock;
        private System.Windows.Forms.Panel pnlProfitCard;
        private System.Windows.Forms.Label lblProfitTitle;
        private System.Windows.Forms.Label lblProfit;
        private System.Windows.Forms.TableLayoutPanel pnlMainGrid;
        private System.Windows.Forms.TableLayoutPanel pnlMiddleRow;
        private System.Windows.Forms.Panel pnlChart;
        private System.Windows.Forms.Label lblChartTitle;
        private System.Windows.Forms.Panel pnlChartMock;
        private System.Windows.Forms.Panel pnlTasks;
        private System.Windows.Forms.Label lblTasksTitle;
        private System.Windows.Forms.DataGridView dgvTasks;
        private System.Windows.Forms.Button btnRefresh;
    }
}