namespace PhongKhamNhi.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            HoaDonBanThuocs = new HashSet<HoaDonBanThuoc>();
            HoaDonBanThuocs1 = new HashSet<HoaDonBanThuoc>();
            KetQuaXNs = new HashSet<KetQuaXN>();
            PhieuDangKyKhams = new HashSet<PhieuDangKyKham>();
            PhieuDKXNs = new HashSet<PhieuDKXN>();
            PhieuKhamBenhs = new HashSet<PhieuKhamBenh>();
            PhieuKhamBenhs1 = new HashSet<PhieuKhamBenh>();
        }

        [Key]
        public int MaNV { get; set; }

        public int MaChiNhanh { get; set; }

        public int? MaTo { get; set; }

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
        public string ChucVu { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayVaoLam { get; set; }

        public virtual ChiNhanh ChiNhanh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonBanThuoc> HoaDonBanThuocs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonBanThuoc> HoaDonBanThuocs1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KetQuaXN> KetQuaXNs { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }

        public virtual ToNhanVien ToNhanVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuDangKyKham> PhieuDangKyKhams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuDKXN> PhieuDKXNs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuKhamBenh> PhieuKhamBenhs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuKhamBenh> PhieuKhamBenhs1 { get; set; }
    }
}
