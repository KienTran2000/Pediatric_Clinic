using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DAO
{
    public class ChiNhanhDAO
    {
        ModelPkNhi db;
        public ChiNhanhDAO()
        {
            db = new ModelPkNhi();
        }
        public List<ChiNhanh> ListChiNhanh()
        {
            return db.ChiNhanhs.ToList();
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