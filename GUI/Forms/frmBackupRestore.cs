using FarmManagement.BUS;
using System;
using System.Windows.Forms;

namespace GUI.Forms
{
    public partial class frmBackupRestore : Form
    {
        private readonly DatabaseService _dbService = new DatabaseService();

        public frmBackupRestore()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Sao lưu và Phục hồi Dữ liệu";
        }

        // --- XỬ LÝ BACKUP ---
        private void btnBrowseBackup_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "SQL Server Backup (*.bak)|*.bak";
            sfd.FileName = "FarmBackup_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".bak";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                txtBackupPath.Text = sfd.FileName;
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBackupPath.Text))
            {
                MessageBox.Show("Vui lòng chọn nơi lưu file backup!", "Thông báo");
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor; // Hiện đồng hồ cát
                _dbService.BackupDatabase(txtBackupPath.Text);
                MessageBox.Show("Sao lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        // --- XỬ LÝ RESTORE ---
        private void btnBrowseRestore_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "SQL Server Backup (*.bak)|*.bak";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtRestorePath.Text = ofd.FileName;
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRestorePath.Text))
            {
                MessageBox.Show("Vui lòng chọn file để phục hồi!", "Thông báo");
                return;
            }

            if (MessageBox.Show("CẢNH BÁO: Việc phục hồi sẽ ghi đè toàn bộ dữ liệu hiện tại bằng dữ liệu trong file backup. Bạn có chắc chắn muốn tiếp tục?",
                "Xác nhận nguy hiểm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    _dbService.RestoreDatabase(txtRestorePath.Text);
                    MessageBox.Show("Phục hồi dữ liệu thành công! Chương trình sẽ khởi động lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Khởi động lại app để nạp lại dữ liệu mới
                    Application.Restart();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }
    }
}