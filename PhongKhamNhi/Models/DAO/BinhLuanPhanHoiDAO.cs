using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DAO
{
    public class BinhLuanPhanHoiDAO
    {
        ModelPkNhi db;
        public BinhLuanPhanHoiDAO()
        {
            db = new ModelPkNhi();
        }

        public List<BinhLuanPhanHoi> lstBinhLuan(int maBinhLuan)
        {
            var res = (from s in db.BinhLuanPhanHois where s.MaBinhLuan == maBinhLuan select s);
            return res.ToList();
        }

        public BinhLuanPhanHoi FindByID(int id)
        {
            return db.BinhLuanPhanHois.Find(id);
        }

        public int Insert(BinhLuanPhanHoi d)
        {
            db.BinhLuanPhanHois.Add(d);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return d.MaPhanHoi;
        }
        //public int Update(BinhLuanPhanHoi d)
        //{
        //    BinhLuan tmp = db.BinhLuans.Find(d.MaBinhLuan);
        //    if (tmp != null)
        //    {
        //        tmp.NoiDung = d.NoiDung;
        //        db.SaveChanges();//luu vao o dia
        //    }
        //    return tmp.MaBinhLuan;
        //}

        public int Delete(int id)
        {
            BinhLuanPhanHoi bn = db.BinhLuanPhanHois.Find(id);
            if (bn != null)
            {
                db.BinhLuanPhanHois.Remove(bn);
                return db.SaveChanges();
            }
            else
                return -1;
        }

        public int DeleteByMaBinhLuan(int maBinhLuan)
        {
            List<BinhLuanPhanHoi> lst = (from s in db.BinhLuanPhanHois where s.MaBinhLuan == maBinhLuan select s).ToList();
            db.BinhLuanPhanHois.RemoveRange(lst);
            return db.SaveChanges();
        }

        public int DeleteByMaBN(int maBN)
        {
            List<BinhLuanPhanHoi> lst = (from s in db.BinhLuanPhanHois where s.MaBN == maBN select s).ToList();
            db.BinhLuanPhanHois.RemoveRange(lst);
            return db.SaveChanges();
        }
    }
}