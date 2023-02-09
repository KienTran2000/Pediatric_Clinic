namespace PhongKhamNhi.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuDangKyKham")]
    public partial class PhieuDangKyKham
    {
        [Key]
        public int MaPhieuDKK { get; set; }

        public int MaChiNhanh { get; set; }

        public int? MaNV { get; set; }

        public int MaBS { get; set; }

        public DateTime? ThoiGianDKK { get; set; }

        [StringLength(30)]
        public string HoTen { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgaySinh { get; set; }

        [StringLength(10)]
        public string Sdt { get; set; }

        public DateTime ThoiGianHen { get; set; }

        [StringLength(200)]
        public string LoiNhan { get; set; }

        public bool? TrangThai { get; set; }

        public virtual BacSi BacSi { get; set; }

        public virtual ChiNhanh ChiNhanh { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public int? MaBN { get; set; }
        public virtual BenhNhi BenhNhi { get; set; }
        public bool Type { get; set; }
    }
}
