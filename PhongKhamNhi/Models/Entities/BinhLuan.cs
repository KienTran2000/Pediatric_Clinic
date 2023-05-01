namespace PhongKhamNhi.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BinhLuan")]
    public partial class BinhLuan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BinhLuan()
        {
            BinhLuanPhanHois = new HashSet<BinhLuanPhanHoi>();
        }

        [Key]
        public int MaBinhLuan { get; set; }

        [StringLength(200)]
        public string NoiDung { get; set; }

        public DateTime? ThoiGianTao { get; set; }

        public int? MaBN { get; set; }

        public int? MaBaiViet { get; set; }

        public virtual BaiViet BaiViet { get; set; }

        public virtual BenhNhi BenhNhi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuanPhanHoi> BinhLuanPhanHois { get; set; }
    }
}
