using PhongKhamNhi.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PhongKhamNhi.Models.DAO
{
    public class BinhLuanDAO
    {
        ModelPkNhi db;
        public BinhLuanDAO()
        {
            db = new ModelPkNhi();
        }

        public List<BinhLuan> lstBinhLuan(int maBaiViet)
        {
            var res = (from s in db.BinhLuans where s.MaBaiViet == maBaiViet orderby s.MaBinhLuan descending select s);
            return res.ToList();
        }

        public BinhLuan FindByID(int id)
        {
            return db.BinhLuans.Find(id);
        }

        public int Insert(BinhLuan d)
        {
            db.BinhLuans.Add(d);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return d.MaBinhLuan;
        }
        public int Update(BinhLuan d)
        {
            BinhLuan tmp = db.BinhLuans.Find(d.MaBinhLuan);
            if (tmp != null)
            {
                tmp.NoiDung = d.NoiDung;
                db.SaveChanges();//luu vao o dia
            }
            return tmp.MaBinhLuan;
        }

        public int Delete(int id)
        {
            BinhLuan bn = db.BinhLuans.Find(id);
            if (bn != null)
            {
                db.BinhLuans.Remove(bn);
                return db.SaveChanges();
            }
            else
                return -1;
        }

        public int DeleteByMaBN(int maBN)
        {
            List<BinhLuan> lst = (from s in db.BinhLuans where s.MaBN == maBN select s).ToList();
            foreach(BinhLuan item in lst)
            {
                new BinhLuanPhanHoiDAO().DeleteByMaBinhLuan(item.MaBinhLuan);
            }
            db.BinhLuans.RemoveRange(lst);
            return db.SaveChanges();
        }

        public int DeleteByMaBV(int maBV)
        {
            List<BinhLuan> lst = (from s in db.BinhLuans where s.MaBaiViet == maBV select s).ToList();
            foreach (BinhLuan item in lst)
            {
                new BinhLuanPhanHoiDAO().DeleteByMaBinhLuan(item.MaBinhLuan);
            }
            db.BinhLuans.RemoveRange(lst);
            return db.SaveChanges();
        }
    }
}