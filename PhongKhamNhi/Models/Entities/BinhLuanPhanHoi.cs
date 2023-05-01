namespace PhongKhamNhi.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BinhLuanPhanHoi")]
    public partial class BinhLuanPhanHoi
    {
        [Key]
        public int MaPhanHoi { get; set; }

        [StringLength(200)]
        public string NoiDung { get; set; }

        public DateTime? ThoiGianTao { get; set; }

        public int? MaBN { get; set; }

        public int? MaBinhLuan { get; set; }

        public virtual BenhNhi BenhNhi { get; set; }

        public virtual BinhLuan BinhLuan { get; set; }
    }
}
