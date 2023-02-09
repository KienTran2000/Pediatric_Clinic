namespace PhongKhamNhi.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("XetNghiem")]
    public partial class XetNghiem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public XetNghiem()
        {
            KetQuaXNs = new HashSet<KetQuaXN>();
            DichVuKhams = new HashSet<DichVuKham>();
        }

        [Key]
        public int MaXN { get; set; }

        [StringLength(50)]
        public string TenXN { get; set; }

        [StringLength(20)]
        public string TriSoBinhThuong { get; set; }

        [StringLength(20)]
        public string DonViTinh { get; set; }

        public double DonGia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KetQuaXN> KetQuaXNs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DichVuKham> DichVuKhams { get; set; }
    }
}
