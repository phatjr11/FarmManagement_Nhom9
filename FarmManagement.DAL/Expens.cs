namespace FarmManagement.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Expenses")]
    public partial class Expens
    {
        [Key]
        public int ExpenseID { get; set; }

        public int FarmID { get; set; }

        [Required]
        [StringLength(100)]
        public string ExpenseType { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExpenseDate { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual Farm Farm { get; set; }
        public int? RelatedID { get; set; }     // ID của con vật/cây trồng liên quan
        public string SourceType { get; set; }  // Loại nguồn: 'ANIMAL', 'CROP', 'INVENTORY'
    }
}
