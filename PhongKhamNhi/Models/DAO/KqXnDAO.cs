using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DAO
{
    public class KqXnDAO
    {
        ModelPkNhi db;
        public KqXnDAO()
        {
            db = new ModelPkNhi();
        }

        public List<KetQuaXN> ListKqXn(int id)
        {
            var res = (from s in db.KetQuaXNs where s.MaPhieuDKXN == id select s);
            return res.ToList();
        }

        public KetQuaXN Find(int maP, int maX)
        {
            var res = (from s in db.KetQuaXNs where s.MaPhieuDKXN == maP && s.MaXN == maX select s);
            if (res.Count() > 0)
                return res.FirstOrDefault();
            return null;
        }
        public int Update(KetQuaXN p)
        {
            KetQuaXN tmp = db.KetQuaXNs.Find(p.MaPhieuDKXN, p.MaXN);
            if (tmp != null)
            {
                tmp.KetQua = p.KetQua;
                db.SaveChanges();//luu vao o dia
            }
            return tmp.MaPhieuDKXN;
        }
    }
}