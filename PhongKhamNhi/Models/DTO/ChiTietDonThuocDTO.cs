using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DTO
{
    public class ChiTietDonThuocDTO
    {
        public int MaThuoc { get; set; }
        public string TenThuoc { get; set; }
        public string DonViTinh { get; set; }
        public int SoLuong { get; set; }
        public string CachDung { get; set; }
    }
}