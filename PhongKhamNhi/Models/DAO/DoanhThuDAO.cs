using PhongKhamNhi.Models.DTO;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DAO
{
    public class DoanhThuDAO
    {
        ModelPkNhi db;
        public DoanhThuDAO()
        {
            db = new ModelPkNhi();
        }

        public DoanhThu Find(DateTime d, int cn)
        {
            var res = db.Database.SqlQuery<DoanhThu>(string.Format("SELECT * FROM DoanhThu WHERE MaChiNhanh = {0} AND NgayThangNam = '{1}'",
                cn, d.ToString("yyyy-MM-dd")));
            if (res != null)
                return res.FirstOrDefault();
            return null;
        }
        //public int Insert(DoanhThu d)
        //{
        //    db.DoanhThus.Add(d);//luu tren RAM
        //    db.SaveChanges();//luu vao o dia
        //    return 1;
        //}
        //public int Update(DoanhThu d)
        //{
        //    DoanhThu tmp = db.DoanhThus.Find(d.NgayThangNam, d.MaChiNhanh);
        //    if (tmp != null)
        //    {
        //        tmp.ThuDichVuKham = d.ThuDichVuKham;
        //        tmp.ThuBanThuoc = d.ThuBanThuoc;
        //        tmp.ThuXetNghiem = d.ThuXetNghiem;
        //        tmp.TongTien = d.TongTien;
        //        db.SaveChanges();//luu vao o dia
        //    }
        //    return 1;
        //}

        public int Insert(DoanhThu d)
        {
            db.Database.ExecuteSqlCommand(string.Format("InsertDoanhThu '{0}', {1}, {2}, {3}, {4}, {5}",
                d.NgayThangNam.ToString("yyyy-MM-dd"), d.MaChiNhanh, d.ThuDichVuKham, d.ThuXetNghiem, d.ThuBanThuoc, d.TongTien
                ));
            return 1;
        }
        public int Update(DoanhThu d)
        {
            db.Database.ExecuteSqlCommand(string.Format("CapNhatDoanhThu '{0}', {1}, {2}, {3}, {4}, {5}",
                d.NgayThangNam.ToString("yyyy-MM-dd"), d.MaChiNhanh, d.ThuDichVuKham, d.ThuXetNghiem, d.ThuBanThuoc, d.TongTien
                ));
            return 1;
        }
        

        public List<StatisticsDTO> StatisticsByMonth(int year, int month, int maCn)
        {
            var lst = db.Database.SqlQuery<StatisticsDTO>(String.Format("SELECT * FROM Func_ThongKeDoanhThu({0}, {1}, {2})", year, month, maCn)
                ).ToList();
            return lst;
        }
        public List<int> GetYearOrder()
        {
            var lst = db.Database.SqlQuery<int>("SELECT DISTINCT YEAR(NgayThangNam) FROM DoanhThu ORDER BY YEAR(NgayThangNam) DESC"
                ).ToList();
            return lst;
        }
        public double GetTotal(int year, int month, int maCn)
        {
            try
            {
                var t = db.Database.SqlQuery<double>(String.Format("SELECT SUM(DoanhThu) FROM Func_ThongKeDoanhThu({0}, {1}, {2})", year, month, maCn)
                ).ToList();
                return t[0];
            }
            catch { }
            return 0;
        }

        public double DoanhThuThuocBan(int year, int month, int maCn)
        {
            try
            {
                var t = db.Database.SqlQuery<double>(String.Format("DoanhThuThuocBan {0}, {1}, {2}", year, month, maCn)
                ).ToList();
                return t[0];
            }
            catch { }
            return 0;
        }
    }
}