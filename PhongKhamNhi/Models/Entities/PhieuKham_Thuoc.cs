namespace PhongKhamNhi.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PhieuKham_Thuoc
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaThuoc { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaPhieuKB { get; set; }

        public int? SoLuong { get; set; }

        [StringLength(100)]
        public string CachDung { get; set; }

        public virtual PhieuKhamBenh PhieuKhamBenh { get; set; }

        public virtual Thuoc Thuoc { get; set; }
    }
}
