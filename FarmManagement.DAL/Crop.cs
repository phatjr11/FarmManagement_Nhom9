namespace FarmManagement.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Crop
    {
        public int CropID { get; set; }

        public int FarmID { get; set; }

        [Required]
        [StringLength(100)]
        public string CropName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PlantingDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExpectedHarvestDate { get; set; }

        public int? Quantity { get; set; }

        [StringLength(50)]
        public string Unit { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Farm Farm { get; set; }
        public decimal ImportPrice { get; set; }      // Chi phí giống
        public decimal ExpectedRevenue { get; set; }  // Doanh thu dự kiến
    }
}
