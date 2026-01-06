using FarmManagement.DAL;
using System;
using System.Data.SqlClient;

namespace FarmManagement.BUS
{
    public class DatabaseService
    {
        // Thay đổi tên Database nếu bạn đặt khác
        private string _dbName = "FarmManagementDB";

        public bool BackupDatabase(string filePath)
        {
            using (var db = new FarmModel())
            {
                try
                {
                    // Lệnh SQL sao lưu
                    string cmd = $"BACKUP DATABASE [{_dbName}] TO DISK = '{filePath}'";
                    db.Database.ExecuteSqlCommand(System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction, cmd);
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi Backup: " + ex.Message);
                }
            }
        }

        public bool RestoreDatabase(string filePath)
        {
            // Để Restore, ta không được kết nối vào chính DB đó.
            // Phải lấy chuỗi kết nối gốc, sửa thành kết nối vào 'master'
            string connectionString = "";
            using (var db = new FarmModel())
            {
                connectionString = db.Database.Connection.ConnectionString;
            }

            // Đổi kết nối sang Master để thực hiện lệnh
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            builder.InitialCatalog = "master"; // Chuyển sang master

            using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
            {
                try
                {
                    conn.Open();

                    // 1. Chuyển DB về chế độ đơn người dùng (để ngắt các kết nối khác)
                    string sql1 = $"ALTER DATABASE [{_dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
                    SqlCommand cmd1 = new SqlCommand(sql1, conn);
                    cmd1.ExecuteNonQuery();

                    // 2. Thực hiện Restore
                    string sql2 = $"RESTORE DATABASE [{_dbName}] FROM DISK = '{filePath}' WITH REPLACE";
                    SqlCommand cmd2 = new SqlCommand(sql2, conn);
                    cmd2.ExecuteNonQuery();

                    // 3. Chuyển lại chế độ đa người dùng
                    string sql3 = $"ALTER DATABASE [{_dbName}] SET MULTI_USER";
                    SqlCommand cmd3 = new SqlCommand(sql3, conn);
                    cmd3.ExecuteNonQuery();

                    return true;
                }
                catch (Exception ex)
                {
                    // Nếu lỗi, cố gắng chuyển lại Multi_User để hệ thống không bị treo
                    try
                    {
                        string sqlFix = $"ALTER DATABASE [{_dbName}] SET MULTI_USER";
                        SqlCommand cmdFix = new SqlCommand(sqlFix, conn);
                        cmdFix.ExecuteNonQuery();
                    }
                    catch { }

                    throw new Exception("Lỗi Restore: " + ex.Message);
                }
            }
        }
    }
}