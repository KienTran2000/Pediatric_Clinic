using PagedList;
using PhongKhamNhi.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PhongKhamNhi.Models.DAO
{
    public class ChiNhanhDAO
    {
        ModelPkNhi db;
        public ChiNhanhDAO()
        {
            db = new ModelPkNhi();
        }

        public List<ChiNhanh> ListAllChiNhanh()
        {
            return db.ChiNhanhs.ToList();
        }

        public List<ChiNhanh> ListChiNhanh()
        {
            var res = (from s in db.ChiNhanhs where s.DangHoatDong select s);
            return res.ToList();
        }

        public IEnumerable<ChiNhanh> ListCN(int pageNum, int pageSize)
        {
            var lst = db.ChiNhanhs.ToList();
            return lst.ToPagedList<ChiNhanh>(pageNum, pageSize);
        }

        public ChiNhanh FindByID(int id)
        {
            return db.ChiNhanhs.Find(id);
        }

        public int Insert(ChiNhanh nv)
        {
            db.ChiNhanhs.Add(nv);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return nv.MaChiNhanh;
        }

        public int Update(ChiNhanh nv)
        {
            ChiNhanh tmp = db.ChiNhanhs.Find(nv.MaChiNhanh);
            if (tmp != null)
            {
                tmp.TenChiNhanh = nv.TenChiNhanh;
                tmp.DiaChi = nv.DiaChi;
                tmp.DangHoatDong = nv.DangHoatDong;
                db.SaveChanges();//luu vao o dia
            }
            return tmp.MaChiNhanh;
        }

        public int Delete(int id)
        {
            ChiNhanh cn = db.ChiNhanhs.Find(id);
            if (cn != null)
            {
                db.ChiNhanhs.Remove(cn);
                return db.SaveChanges();
            }
            else
                return -1;
        }
    }
}