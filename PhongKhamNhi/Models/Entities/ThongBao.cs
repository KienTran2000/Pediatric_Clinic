namespace PhongKhamNhi.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongBao")]
    public partial class ThongBao
    {
        [Key]
        public int MaThongBao { get; set; }

        [StringLength(200)]
        public string NoiDung { get; set; }

        public DateTime? ThoiGianTao { get; set; }

        public int? MaBN { get; set; }

        public bool TrangThai { get; set; }

        public int? MaBaiViet { get; set; }

        public virtual BaiViet BaiViet { get; set; }

        public virtual BenhNhi BenhNhi { get; set; }
    }
}
