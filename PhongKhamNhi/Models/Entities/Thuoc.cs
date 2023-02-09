namespace PhongKhamNhi.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Thuoc")]
    public partial class Thuoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Thuoc()
        {
            PhieuKham_Thuoc = new HashSet<PhieuKham_Thuoc>();
            Thuoc_HoaDon = new HashSet<Thuoc_HoaDon>();
            ThuocBans = new HashSet<ThuocBan>();
        }

        [Key]
        public int MaThuoc { get; set; }

        public int MaLoaiThuoc { get; set; }

        [StringLength(50)]
        public string TenThuoc { get; set; }

        [StringLength(10)]
        public string DonViTinh { get; set; }

        public double DonGia { get; set; }

        public virtual LoaiThuoc LoaiThuoc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuKham_Thuoc> PhieuKham_Thuoc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Thuoc_HoaDon> Thuoc_HoaDon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThuocBan> ThuocBans { get; set; }
    }
}
