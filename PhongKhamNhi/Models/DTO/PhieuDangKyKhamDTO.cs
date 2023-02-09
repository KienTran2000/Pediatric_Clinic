using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DTO
{
    public class PhieuDangKyKhamDTO
    {
        public PhieuDangKyKham PhieuDkk { get; set; }
        public bool Trung { get; set; }

        public int MaPhieuDKK { get; set; }

        public int? MaNV { get; set; }

        public int? MaBS { get; set; }

        public DateTime? ThoiGianDKK { get; set; }

        public DateTime? ThoiGianHen { get; set; }

        public bool? TrangThai { get; set; }

        public int? MaBN { get; set; }

        public string HoTen { get; set; }

        public string SdtThanNhan { get; set; }

        public string TenBacSi { get; set; }
        public bool? Type { get; set; }
    }

    public class PhieuDangKyKhamDTO2
    {
        public int MaPhieuDKK { get; set; }

        public int MaBN { get; set; }

        public DateTime? ThoiGianDKK { get; set; }

        public string HoTen { get; set; }

        public string SdtThanNhan { get; set; }

        public DateTime ThoiGianHen { get; set; }

        public bool? TrangThai { get; set; }

        public bool? Type { get; set; }
    }
}