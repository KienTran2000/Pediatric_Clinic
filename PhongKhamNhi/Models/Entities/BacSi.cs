namespace PhongKhamNhi.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("BacSi")]
    public partial class BacSi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BacSi()
        {
            PhieuDangKyKhams = new HashSet<PhieuDangKyKham>();
            PhieuKhamBenhs = new HashSet<PhieuKhamBenh>();
        }

        [Key]
        public int MaBS { get; set; }

        public int MaChiNhanh { get; set; }

        public int? IdTaiKhoan { get; set; }

        [StringLength(30)]
        public string HoTen { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgaySinh { get; set; }

        [StringLength(3)]
        public string GioiTinh { get; set; }

        [StringLength(10)]
        public string Sdt { get; set; }

        [StringLength(20)]
        public string HocVi { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayVaoLam { get; set; }

        [StringLength(200)]
        public string Anh { get; set; }

        public bool Type { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string GioiThieu { get; set; }

        public virtual ChiNhanh ChiNhanh { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuDangKyKham> PhieuDangKyKhams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuKhamBenh> PhieuKhamBenhs { get; set; }
    }
}
