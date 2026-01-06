namespace FarmManagement.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Animal
    {
        public int AnimalID { get; set; }

        public int FarmID { get; set; }

        [Required]
        [StringLength(100)]
        public string AnimalType { get; set; }

        [StringLength(100)]
        public string Breed { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PurchaseDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Farm Farm { get; set; }
        public decimal ImportPrice { get; set; } // Giá nhập
        public decimal SalePrice { get; set; }   // Giá bán
    }
}
