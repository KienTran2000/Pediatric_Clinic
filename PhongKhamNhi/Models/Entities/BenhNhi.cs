namespace PhongKhamNhi.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BenhNhi")]
    public partial class BenhNhi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BenhNhi()
        {
            BinhLuans = new HashSet<BinhLuan>();
            BinhLuanPhanHois = new HashSet<BinhLuanPhanHoi>();
            PhieuKhamBenhs = new HashSet<PhieuKhamBenh>();
            ThongBaos = new HashSet<ThongBao>();
        }

        [Key]
        public int MaBN { get; set; }

        [StringLength(30)]
        public string HoTen { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgaySinh { get; set; }

        [StringLength(3)]
        public string GioiTinh { get; set; }

        [StringLength(200)]
        public string DiaChi { get; set; }

        [StringLength(30)]
        public string TenThanNhan { get; set; }

        [StringLength(10)]
        public string SdtThanNhan { get; set; }

        [StringLength(200)]
        public string GhiChu { get; set; }

        [Required]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [Required]
        [StringLength(200)]
        public string MatKhau { get; set; }

        public string AnhDaiDien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuanPhanHoi> BinhLuanPhanHois { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuKhamBenh> PhieuKhamBenhs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongBao> ThongBaos { get; set; }
    }
}
