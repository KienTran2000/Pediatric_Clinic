using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DTO
{
    public class PhieuKhamDTO
    {
        public DateTime? ThoiGianLap { get; set; }
        public DateTime? ThoiGianKham { get; set; }
        public int MaPhieuKB { get; set; }

        public string HoTen { get; set; }

        public DateTime NgaySinh { get; set; }

        public string TenDV { get; set; }

        public double DonGia { get; set; }

        public string TenBs { get; set; }

        public byte TrangThai { get; set; }

    }
}