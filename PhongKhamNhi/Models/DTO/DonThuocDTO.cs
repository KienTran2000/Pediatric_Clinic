using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DTO
{
    public class DonThuocDTO
    {
        public int MaPhieuKB { get; set; }
        public DateTime ThoiGianLap { get; set; }
        public int MaBN { get; set; }
        public string HoTen { get; set; }
        public string TenBs { get; set; }
    }
}