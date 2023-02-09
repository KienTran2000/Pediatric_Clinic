namespace PhongKhamNhi.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("DichVuKham")]
    public partial class DichVuKham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DichVuKham()
        {
            PhieuKhamBenhs = new HashSet<PhieuKhamBenh>();
            XetNghiems = new HashSet<XetNghiem>();
        }

        [Key]
        public int MaDV { get; set; }

        [StringLength(50)]
        public string TenDV { get; set; }

        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string MoTa { get; set; }

        public double DonGia { get; set; }

        [StringLength(200)]
        public string Anh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuKhamBenh> PhieuKhamBenhs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<XetNghiem> XetNghiems { get; set; }
    }
}
