namespace FarmManagement.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Activity
    {
        public int ActivityID { get; set; }

        public int FarmID { get; set; }

        [Required]
        [StringLength(100)]
        public string ActivityType { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [StringLength(100)]
        public string ResponsiblePerson { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual Farm Farm { get; set; }
    }
}
