using PagedList;
using PhongKhamNhi.Models.DTO;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DAO
{
    public class PhieuDkXnDAO
    {
        ModelPkNhi db;
        public PhieuDkXnDAO()
        {
            db = new ModelPkNhi();
        }

        public IEnumerable<PhieuDkXnDTO> lstPhieuDkXn(int cn, string bn, string tu, string den, string trangThai, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<PhieuDkXnDTO>(string.Format("lstPhieuDkXn {0}, N'{1}', '{2}', '{3}', '{4}'",
                cn, bn, trangThai, tu, den)
                ).ToPagedList<PhieuDkXnDTO>(pageNum, pageSize);
            return lst;
        }
        public IEnumerable<PhieuDkXnDTO> lstPhieuDkXn2(int cn, string bn, string tu, string den, string trangThai, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<PhieuDkXnDTO>(string.Format("lstPhieuDkXn2 {0}, N'{1}', '{2}', '{3}', '{4}'",
                cn, bn, trangThai, tu, den)
                ).ToPagedList<PhieuDkXnDTO>(pageNum, pageSize);
            return lst;
        }
        public List<XetNghiem> ListXnByMaPdk(int id)
        {
            var res = db.Database.SqlQuery<XetNghiem>(string.Format("SELECT X.* FROM XetNghiem X, KetQuaXN K WHERE K.MaPhieuDKXN = {0} AND K.MaXN = X.MaXN", id));
            return res.ToList();
        }
        public List<KqXnDTO> ListKqXn(int id)
        {
            var res = db.Database.SqlQuery<KqXnDTO>(string.Format("SELECT X.TenXN, X.TriSoBinhThuong, X.DonViTinh, K.KetQua, X.DonGia FROM XetNghiem X, KetQuaXN K WHERE K.MaPhieuDKXN = {0} AND K.MaXN = X.MaXN", id));
            return res.ToList();
        }
        public int SlPhieuDkXn(int cn)
        {
            var res = (from s in db.PhieuDKXNs where s.PhieuKhamBenh.MaChiNhanh == cn && s.TrangThai != 0 select s);
            return res.Count();
        }

        public PhieuDKXN FindByID(int id)
        {
            return db.PhieuDKXNs.Find(id);
        }
        public PhieuDKXN FindByMaPk(int id)
        {
            var res = (from s in db.PhieuDKXNs where s.MaPhieuKB == id select s);
            if (res.Count() > 0)
                return res.FirstOrDefault();
            return null;
        }
        public int Insert(PhieuDKXN p)
        {
            db.PhieuDKXNs.Add(p);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return p.MaPhieuDKXN;
        }
        public int InsertDetail(KetQuaXN k)
        {
            db.KetQuaXNs.Add(k);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return 1;
        }
        public int UpdateThuNgan(PhieuDKXN p)
        {
            PhieuDKXN tmp = db.PhieuDKXNs.Find(p.MaPhieuDKXN);
            if (tmp != null)
            {
                tmp.MaNV = p.MaNV;
                tmp.TrangThai = p.TrangThai;
                db.SaveChanges();//luu vao o dia
            }
            return tmp.MaPhieuDKXN;
        }
        public int UpdateNvXn(int id)
        {
            PhieuDKXN tmp = db.PhieuDKXNs.Find(id);
            if (tmp != null)
            {
                tmp.TrangThai = 2;
                db.SaveChanges();//luu vao o dia
            }
            return tmp.MaPhieuDKXN;
        }
    }
}