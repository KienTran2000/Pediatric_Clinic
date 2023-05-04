namespace PhongKhamNhi.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("BaiViet")]
    public partial class BaiViet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BaiViet()
        {
            BinhLuans = new HashSet<BinhLuan>();
            ThongBaos = new HashSet<ThongBao>();
        }

        [Key]
        public int MaBaiViet { get; set; }

        [StringLength(100)]
        public string TieuDe { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string NoiDung { get; set; }

        [StringLength(200)]
        public string AnhDaiDien { get; set; }

        public DateTime? ThoiGianTao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongBao> ThongBaos { get; set; }
    }
}
