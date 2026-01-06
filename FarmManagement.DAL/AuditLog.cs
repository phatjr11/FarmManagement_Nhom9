namespace FarmManagement.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AuditLog")]
    public partial class AuditLog
    {
        [Key]
        public int AuditID { get; set; }

        public int UserID { get; set; }

        [StringLength(100)]
        public string TableName { get; set; }

        [StringLength(50)]
        public string Action { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public DateTime? ChangedDate { get; set; }

        public virtual User User { get; set; }
    }
}
