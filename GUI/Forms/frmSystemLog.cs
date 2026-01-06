using FarmManagement.BUS;
using FarmManagement.GUI; // Cần dòng này để gọi được GridHelper
using System;
using System.Windows.Forms;

namespace GUI.Forms
{
    public partial class frmSystemLog : Form
    {
        private readonly SystemLogService _service = new SystemLogService();

        public frmSystemLog()
        {
            InitializeComponent();
        }

        private void frmSystemLog_Load(object sender, EventArgs e)
        {
            // Cấu hình giao diện cho cả 2 bảng
            SetupGridLogin();
            SetupGridAudit();

            // Tải dữ liệu
            LoadData();
        }

        private void SetupGridLogin()
        {
            // 1. Cấu hình cơ bản
            dgvLogin.AutoGenerateColumns = false;
            dgvLogin.DataSource = null;
            dgvLogin.Columns.Clear();

            // 2. --- GỌI HÀM TRANG TRÍ (Thay thế cho code thủ công) ---
            // Gọi hàm này xong là bảng sẽ đẹp, tự động ReadOnly, FullRowSelect...
            GridHelper.StyleGrid(dgvLogin);

            // 3. Thêm cột
            dgvLogin.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Người dùng", Name = "colUser", Width = 150 });
            dgvLogin.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "LoginTime", HeaderText = "Thời gian", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm:ss" } });
            dgvLogin.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "IPAddress", HeaderText = "IP Máy", Width = 120 });
            dgvLogin.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Trạng thái", Width = 100 });
        }

        private void SetupGridAudit()
        {
            // 1. Cấu hình cơ bản
            dgvAudit.AutoGenerateColumns = false;
            dgvAudit.Columns.Clear();

            // 2. --- GỌI HÀM TRANG TRÍ CHO BẢNG THỨ 2 ---
            GridHelper.StyleGrid(dgvAudit);

            // 3. Thêm cột
            dgvAudit.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Người thực hiện", Name = "colUserAudit", Width = 150 });
            dgvAudit.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Action", HeaderText = "Hành động", Width = 100 });
            dgvAudit.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TableName", HeaderText = "Bảng dữ liệu", Width = 150 });
            dgvAudit.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "OldValue", HeaderText = "Mô tả chi tiết", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvAudit.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ChangedDate", HeaderText = "Thời gian", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm:ss" } });
        }

        private void LoadData()
        {
            dgvLogin.DataSource = _service.GetLoginHistories();
            dgvAudit.DataSource = _service.GetAuditLogs();
        }

        private void dgvLogin_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvLogin.Columns[e.ColumnIndex].Name == "colUser")
            {
                var item = dgvLogin.Rows[e.RowIndex].DataBoundItem as FarmManagement.DAL.LoginHistory;
                if (item != null && item.User != null) e.Value = item.User.FullName;
            }
        }

        private void dgvAudit_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvAudit.Columns[e.ColumnIndex].Name == "colUserAudit")
            {
                var item = dgvAudit.Rows[e.RowIndex].DataBoundItem as FarmManagement.DAL.AuditLog;
                if (item != null && item.User != null) e.Value = item.User.FullName;
            }
        }
    }
}