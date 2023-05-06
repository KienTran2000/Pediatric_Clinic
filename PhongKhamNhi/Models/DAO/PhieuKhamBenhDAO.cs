using PagedList;
using PhongKhamNhi.Models.DTO;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DAO
{
    public class PhieuKhamBenhDAO
    {
        ModelPkNhi db;
        public PhieuKhamBenhDAO()
        {
            db = new ModelPkNhi();
        }

        public IEnumerable<PhieuKhamDTO> lstPhieuKb(int cn, string ten, int dv, int bs, int trangThai, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<PhieuKhamDTO>(string.Format("lstPhieuKb {0}, N'{1}', {2}, {3}, {4}",
                cn, ten, dv, bs, trangThai)
                ).ToPagedList<PhieuKhamDTO>(pageNum, pageSize);
            return lst;
        }
        public IEnumerable<PhieuKhamDTO> lstPhieuKb2(int cn, string ten, int dv, int bs, int trangThai, string tu, string den, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<PhieuKhamDTO>(string.Format("lstPhieuKb2 {0}, N'{1}', {2}, {3}, {4}, '{5}', '{6}'",
                cn, ten, dv, bs, trangThai, tu, den)
                ).ToPagedList<PhieuKhamDTO>(pageNum, pageSize);
            return lst;
        }

        public PhieuKhamThongKe lstPhieuKbThongKe(int cn, string ten, int dv, int bs, int trangThai, string tu, string den)
        {
            List<PhieuKhamDTO> lst = db.Database.SqlQuery<PhieuKhamDTO>(string.Format("lstPhieuKb2 {0}, N'{1}', {2}, {3}, {4}, '{5}', '{6}'",
                cn, ten, dv, bs, trangThai, tu, den)
                ).ToList<PhieuKhamDTO>();
            PhieuKhamThongKe p = new PhieuKhamThongKe();
            p.ChoKham = lst.FindAll(pk => pk.TrangThai == 1).Count;
            p.DaKham = lst.FindAll(pk => pk.TrangThai == 2).Count;
            return p;
        }

        public IEnumerable<PhieuKhamBenh> lichSuKham(int id, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<PhieuKhamBenh>(string.Format("SELECT * FROM PhieuKhamBenh WHERE MaBN = {0} ORDER BY MaPhieuKB DESC",
                id)
                ).ToPagedList<PhieuKhamBenh>(pageNum, pageSize);
            return lst;
        }

        public int SlPhieuKb(int cn)
        {
            var res = (from s in db.PhieuKhamBenhs where s.MaChiNhanh == cn && s.TrangThai == 0 select s);
            return res.Count();
        }
        public int SlPhieuDkXn(int cn)
        {
            var res = (from s in db.PhieuDKXNs where s.PhieuKhamBenh.MaChiNhanh == cn && s.TrangThai == 0 select s);
            return res.Count();
        }
        public int SlHoaDon(int cn)
        {
            var res = (from s in db.HoaDonBanThuocs where s.MaChiNhanh == cn && s.TrangThai == false select s);
            return res.Count();
        }
        public int SlPhieuKbOfBs(int cn, int bs)
        {
            var res = (from s in db.PhieuKhamBenhs where s.MaBS == bs && s.MaChiNhanh == cn && s.TrangThai == 1 select s);
            return res.Count();
        }

        public PhieuKhamBenh FindByID(int id)
        {
            return db.PhieuKhamBenhs.Find(id);
        }
        public int Insert(PhieuKhamBenh p)
        {
            db.PhieuKhamBenhs.Add(p);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return p.MaPhieuKB;
        }
        public int UpdateLeTan(PhieuKhamBenh p)
        {
            PhieuKhamBenh tmp = db.PhieuKhamBenhs.Find(p.MaPhieuKB);
            if (tmp != null)
            {
                tmp.DonGia = p.DonGia;
                tmp.MaBS = p.MaBS;
                tmp.MaDV = p.MaDV;
                tmp.MaNvLap = p.MaNvLap;
                tmp.ThoiGianLap = p.ThoiGianLap;
                db.SaveChanges();//luu vao o dia
            }
            return tmp.MaPhieuKB;
        }
        public int UpdateThuNgan(PhieuKhamBenh p)
        {
            PhieuKhamBenh tmp = db.PhieuKhamBenhs.Find(p.MaPhieuKB);
            if (tmp != null)
            {
                tmp.MaNvThu = p.MaNvThu;
                tmp.TrangThai = p.TrangThai;
                db.SaveChanges();//luu vao o dia
            }
            return tmp.MaPhieuKB;
        }
        public int Update(PhieuKhamBenh p)
        {
            PhieuKhamBenh tmp = db.PhieuKhamBenhs.Find(p.MaPhieuKB);
            if (tmp != null)
            {
                tmp.BieuHien = p.BieuHien;
                tmp.KetLuan = p.KetLuan;
                tmp.TrangThai = 2;
                db.SaveChanges();//luu vao o dia
            }
            return tmp.MaPhieuKB;
        }

        public int UpdatePay(PhieuKhamBenh p)
        {
            PhieuKhamBenh tmp = db.PhieuKhamBenhs.Find(p.MaPhieuKB);
            if (tmp != null)
            {
                tmp.AnhThanhToan = p.AnhThanhToan;
                db.SaveChanges();//luu vao o dia
            }
            return tmp.MaPhieuKB;
        }

        public int Delete(int id)
        {
            PhieuKhamBenh p = db.PhieuKhamBenhs.Find(id);
            if (p != null)
            {
                db.PhieuKhamBenhs.Remove(p);
                return db.SaveChanges();
            }
            else
                return -1;
        }

        public int DeleteByMaBN(int maBN)
        {
            List<PhieuKhamBenh> lst = (from s in db.PhieuKhamBenhs where s.MaBN == maBN select s).ToList();
            foreach(PhieuKhamBenh item in lst)
            {
                new PhieuDkXnDAO().DeleteByMaPK(item.MaPhieuKB);
                new ThuocDAO().DeleteByMaPK(item.MaPhieuKB);
            }
            db.PhieuKhamBenhs.RemoveRange(lst);
            return db.SaveChanges();
        }

        public IEnumerable<PhieuKhamDTO> lstPhieuKbByBn(int bn, string tu, string den, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<PhieuKhamDTO>(string.Format("lstPhieuKbByBn {0}, '{1}', '{2}'",
                bn, tu, den)
                ).ToPagedList<PhieuKhamDTO>(pageNum, pageSize);
            return lst;
        }

        public IEnumerable<LichSuKhamDTO> lichSuKhamByMaBs(int bs, int dv, string tu, string den, int pageNum, int pageSize)
        {
            var res = db.Database.SqlQuery<LichSuKhamDTO>(string.Format("lichSuKhamByMaBs {0}, {1}, '{2}', '{3}'",
                        bs, dv, tu, den)
                        ).ToPagedList<LichSuKhamDTO>(pageNum, pageSize);
            return res;
        }
    }
}