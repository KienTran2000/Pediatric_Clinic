namespace PhongKhamNhi.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KetQuaXN")]
    public partial class KetQuaXN
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaPhieuDKXN { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaXN { get; set; }

        public int? MaNV { get; set; }

        [StringLength(50)]
        public string KetQua { get; set; }

        public double DonGia { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public virtual PhieuDKXN PhieuDKXN { get; set; }

        public virtual XetNghiem XetNghiem { get; set; }
    }
}
