namespace PhongKhamNhi.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DoanhThu")]
    public partial class DoanhThu
    {
        [Key]
        [Column(Order = 0, TypeName = "date")]
        public DateTime NgayThangNam { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaChiNhanh { get; set; }

        public double? ThuDichVuKham { get; set; }

        public double? ThuXetNghiem { get; set; }

        public double? ThuBanThuoc { get; set; }

        public double? TongTien { get; set; }

        public virtual ChiNhanh ChiNhanh { get; set; }
    }
}
