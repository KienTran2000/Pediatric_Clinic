using PagedList;
using PhongKhamNhi.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PhongKhamNhi.Models.DAO
{
    public class ThongBaoDAO
    {
        ModelPkNhi db;
        public ThongBaoDAO()
        {
            db = new ModelPkNhi();
        }

        public IEnumerable<ThongBao> ListThongBao(int maBN, int pageNum, int pageSize)
        {
            var res = (from s in db.ThongBaos where s.MaBN == maBN orderby s.MaThongBao descending select s);
            return res.ToList().ToPagedList<ThongBao>(pageNum, pageSize);
        }

        public int SlThongBao(int maBN)
        {
            var res = (from s in db.ThongBaos where s.MaBN == maBN && !s.TrangThai select s);
            return res.ToList().Count;
        }

        public ThongBao FindByID(int id)
        {
            return db.ThongBaos.Find(id);
        }

        public int Insert(ThongBao d)
        {
            db.ThongBaos.Add(d);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return d.MaThongBao;
        }

        public int Update(ThongBao tb)
        {
            ThongBao tmp = db.ThongBaos.Find(tb.MaThongBao);
            if (tmp != null)
            {
                tmp.TrangThai = tb.TrangThai;
                db.SaveChanges();//luu vao o dia
            }
            return tmp.MaThongBao;
        }

        public int Delete(int id)
        {
            ThongBao bn = db.ThongBaos.Find(id);
            if (bn != null)
            {
                db.ThongBaos.Remove(bn);
                return db.SaveChanges();
            }
            else
                return -1;
        }

        public int DeleteByMaBN(int maBN)
        {
            List<ThongBao> lst = (from s in db.ThongBaos where s.MaBN == maBN select s).ToList();
            db.ThongBaos.RemoveRange(lst);
            return db.SaveChanges();
        }

        public int DeleteByMaBV(int maBV)
        {
            List<ThongBao> lst = (from s in db.ThongBaos where s.MaBaiViet == maBV select s).ToList();
            db.ThongBaos.RemoveRange(lst);
            return db.SaveChanges();
        }
    }
}