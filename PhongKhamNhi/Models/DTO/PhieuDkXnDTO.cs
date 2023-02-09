using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DTO
{
    public class PhieuDkXnDTO
    {
        public int MaPhieuDKXN { get; set; }
        public int MaPhieuKB { get; set; }
        public DateTime ThoiGianLap { get; set; }
        public string HoTen { get; set; }
        public byte TrangThai { get; set; }

        public double TongTien { get; set; }
    }
}