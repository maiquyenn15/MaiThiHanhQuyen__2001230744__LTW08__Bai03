namespace LTWeb08_Bai03.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbl_Employee")]
    public partial class tbl_Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Gender { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        public int? DeptId { get; set; }

        public virtual tbl_Deparment tbl_Deparment { get; set; }
    }
}
