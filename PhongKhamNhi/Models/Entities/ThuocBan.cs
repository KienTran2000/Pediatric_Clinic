namespace PhongKhamNhi.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThuocBan")]
    public partial class ThuocBan
    {
        [Key]
        [Column(Order = 0, TypeName = "date")]
        public DateTime NgayThangNam { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaChiNhanh { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaThuoc { get; set; }

        public int? SoLuongBan { get; set; }

        public virtual ChiNhanh ChiNhanh { get; set; }

        public virtual Thuoc Thuoc { get; set; }
    }
}
