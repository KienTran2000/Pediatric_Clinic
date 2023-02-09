namespace PhongKhamNhi.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiKhoan()
        {
            BacSis = new HashSet<BacSi>();
            NhanViens = new HashSet<NhanVien>();
        }

        [Key]
        public int IdTaiKhoan { get; set; }

        public int? MaQuyen { get; set; }

        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [StringLength(200)]
        public string MatKhau { get; set; }

        public bool? TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BacSi> BacSis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhanVien> NhanViens { get; set; }

        public virtual Quyen Quyen { get; set; }
    }
}
