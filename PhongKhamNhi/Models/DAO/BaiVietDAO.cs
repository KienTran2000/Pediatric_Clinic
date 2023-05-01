using PagedList;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DAO
{
    public class BaiVietDAO
    {
        ModelPkNhi db;
        public BaiVietDAO()
        {
            db = new ModelPkNhi();
        }

        public IEnumerable<BaiViet> lstBaiViet(string tieuDe, string tu, string den, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<BaiViet>(string.Format("lstBaiViet N'{0}', '{1}', '{2}'",
                tieuDe, tu, den)
                ).ToPagedList<BaiViet>(pageNum, pageSize);
            return lst;
        }

        public BaiViet FindByID(int id)
        {
            return db.BaiViets.Find(id);
        }

        public int Insert(BaiViet d)
        {
            db.BaiViets.Add(d);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return d.MaBaiViet;
        }
        public int Update(BaiViet d)
        {
            BaiViet tmp = db.BaiViets.Find(d.MaBaiViet);
            if (tmp != null)
            {
                tmp.TieuDe = d.TieuDe;
                tmp.NoiDung = d.NoiDung;
                tmp.AnhDaiDien = d.AnhDaiDien;
                db.SaveChanges();//luu vao o dia
            }
            return tmp.MaBaiViet;
        }

        public int Delete(int id)
        {
            BaiViet bn = db.BaiViets.Find(id);
            if (bn != null)
            {
                db.BaiViets.Remove(bn);
                return db.SaveChanges();
            }
            else
                return -1;
        }
    }
}