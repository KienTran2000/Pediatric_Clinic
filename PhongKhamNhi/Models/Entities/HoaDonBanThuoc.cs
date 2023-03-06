namespace PhongKhamNhi.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDonBanThuoc")]
    public partial class HoaDonBanThuoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDonBanThuoc()
        {
            Thuoc_HoaDon = new HashSet<Thuoc_HoaDon>();
        }

        [Key]
        public int MaHoaDon { get; set; }

        public int MaChiNhanh { get; set; }

        [StringLength(30)]
        public string TenKH { get; set; }

        [StringLength(10)]
        public string Sdt { get; set; }

        [StringLength(200)]
        public string DiaChi { get; set; }

        public DateTime ThoiGian { get; set; }

        public bool? TrangThai { get; set; }

        public int? MaNvLap { get; set; }

        public int? MaNvThu { get; set; }

        public double TongTien { get; set; }
        public bool Type { get; set; }
        [StringLength(500)]
        public string AnhThanhToan { get; set; }

        public int? MaPhieuKham { get; set; }

        public virtual ChiNhanh ChiNhanh { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public virtual NhanVien NhanVien1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Thuoc_HoaDon> Thuoc_HoaDon { get; set; }
    }
}
