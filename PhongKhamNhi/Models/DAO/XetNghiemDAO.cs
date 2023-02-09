using PagedList;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DAO
{
    public class XetNghiemDAO
    {
        ModelPkNhi db;
        public XetNghiemDAO()
        {
            db = new ModelPkNhi();
        }

        public IEnumerable<XetNghiem> lstXn(string ten, int dv, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<XetNghiem>(string.Format("lstXetNghiem N'{0}', {1}",
                ten, dv)
                ).ToPagedList<XetNghiem>(pageNum, pageSize);
            return lst;
        }
        public List<XetNghiem> GetListXnByMaDv(int maDv)
        {
            var res = db.Database.SqlQuery<XetNghiem>(string.Format("SELECT X.* FROM XetNghiem X, DichVuKham_XN C WHERE C.MaDV = {0} AND C.MaXN = X.MaXN", maDv));
            return res.ToList();
        }
        public List<XetNghiem> lstSearchXn(string ten)
        {
            var lst = db.Database.SqlQuery<XetNghiem>(string.Format("lstSearchXn N'{0}'", ten)
                ).ToList<XetNghiem>();
            return lst;
        }

        public XetNghiem FindByID(int id)
        {
            return db.XetNghiems.Find(id);
        }
    }
}