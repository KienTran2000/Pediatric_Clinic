namespace PhongKhamNhi.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuDKXN")]
    public partial class PhieuDKXN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuDKXN()
        {
            KetQuaXNs = new HashSet<KetQuaXN>();
        }

        [Key]
        public int MaPhieuDKXN { get; set; }

        public int MaPhieuKB { get; set; }

        public int? MaNV { get; set; }

        public DateTime ThoiGianLap { get; set; }

        public byte? TrangThai { get; set; }

        public double? TongTien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KetQuaXN> KetQuaXNs { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public virtual PhieuKhamBenh PhieuKhamBenh { get; set; }
    }
}
