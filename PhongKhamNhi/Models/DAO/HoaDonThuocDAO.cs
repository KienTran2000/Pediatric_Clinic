using PagedList;
using PhongKhamNhi.Models.DTO;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DAO
{
    public class HoaDonThuocDAO
    {
        ModelPkNhi db;
        public HoaDonThuocDAO()
        {
            db = new ModelPkNhi();
        }

        public IEnumerable<HoaDonBanThuoc> ListHdThuoc(int cn, int maHd, string bn, string tu, string den, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<HoaDonBanThuoc>(string.Format("lstHdThuoc {0}, {1}, N'{2}', '{3}', '{4}'",
                cn, maHd, bn, tu, den)).ToPagedList<HoaDonBanThuoc>(pageNum, pageSize);
            return lst;
        }
        public List<CtHdThuocDTO> lstThuocByMaHd(int id)
        {
            var res = db.Database.SqlQuery<CtHdThuocDTO>(string.Format(
                "SELECT P.MaThuoc, T.TenThuoc, P.SoLuong, T.DonViTinh, P.DonGia FROM Thuoc_HoaDon P, Thuoc T WHERE P.MaHoaDon = {0} AND P.MaThuoc = T.MaThuoc", id));
            return res.ToList();
        }

        public HoaDonBanThuoc FindByID(int id)
        {
            return db.HoaDonBanThuocs.Find(id);
        }
        public int Insert(HoaDonBanThuoc p)
        {
            db.HoaDonBanThuocs.Add(p);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return p.MaHoaDon;
        }
        public int InsertCt(int t, int h, int sl, double dg)
        {
            Thuoc_HoaDon p = new Thuoc_HoaDon();
            p.MaThuoc = t;
            p.MaHoaDon = h;
            p.SoLuong = sl;
            p.DonGia = dg;
            db.Thuoc_HoaDon.Add(p);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return 1;
        }
        public int UpdatetongTien(int id, double t)
        {
            HoaDonBanThuoc dk = db.HoaDonBanThuocs.Find(id);
            if (dk != null)
            {
                dk.TongTien = t;
                db.SaveChanges();//luu vao o dia
            }
            return dk.MaHoaDon;
        }
        public int Update(HoaDonBanThuoc p)
        {
            HoaDonBanThuoc tmp = db.HoaDonBanThuocs.Find(p.MaHoaDon);
            if (tmp != null)
            {
                tmp.TenKH = p.TenKH;
                tmp.DiaChi = p.DiaChi;
                tmp.Sdt = p.Sdt;
                tmp.ThoiGian = p.ThoiGian;
                tmp.MaNvLap = p.MaNvLap;
                db.SaveChanges();//luu vao o dia
            }
            return tmp.MaHoaDon;
        }
        public int UpdateThuNgan(HoaDonBanThuoc p)
        {
            HoaDonBanThuoc tmp = db.HoaDonBanThuocs.Find(p.MaHoaDon);
            if (tmp != null)
            {
                tmp.MaNvThu = p.MaNvThu;
                tmp.TrangThai = p.TrangThai;
                db.SaveChanges();//luu vao o dia
            }
            return tmp.MaHoaDon;
        }
        public int Delete(int id)
        {
            HoaDonBanThuoc h = db.HoaDonBanThuocs.Find(id);
            if (h != null)
            {
                db.HoaDonBanThuocs.Remove(h);
                return db.SaveChanges();
            }
            else
                return -1;
        }
        public int DeleteThuoc(int id)
        {
            var res = db.Database.ExecuteSqlCommand(string.Format(
                "DELETE FROM [dbo].[Thuoc_HoaDon] WHERE MaHoaDon = {0}", id));
            return 1;
        }
    }
}