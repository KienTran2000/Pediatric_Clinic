namespace PhongKhamNhi.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuKhamBenh")]
    public partial class PhieuKhamBenh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuKhamBenh()
        {
            PhieuDKXNs = new HashSet<PhieuDKXN>();
            PhieuKham_Thuoc = new HashSet<PhieuKham_Thuoc>();
        }

        [Key]
        public int MaPhieuKB { get; set; }

        public int MaBN { get; set; }

        public int MaDV { get; set; }

        public int MaBS { get; set; }

        public int MaChiNhanh { get; set; }

        public DateTime ThoiGianLap { get; set; }

        [StringLength(200)]
        public string BieuHien { get; set; }

        [StringLength(200)]
        public string KetLuan { get; set; }

        public byte? TrangThai { get; set; }

        public double DonGia { get; set; }

        public DateTime? ThoiGianKham { get; set; }

        public int? MaNvLap { get; set; }

        public int? MaNvThu { get; set; }

        public virtual BacSi BacSi { get; set; }

        public virtual BenhNhi BenhNhi { get; set; }

        public virtual ChiNhanh ChiNhanh { get; set; }

        public virtual DichVuKham DichVuKham { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public virtual NhanVien NhanVien1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuDKXN> PhieuDKXNs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuKham_Thuoc> PhieuKham_Thuoc { get; set; }
    }
}
