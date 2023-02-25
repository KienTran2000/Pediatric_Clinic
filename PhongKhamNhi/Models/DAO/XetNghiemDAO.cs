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

        public int Delete(int id)
        {
            XetNghiem xn = db.XetNghiems.Find(id);
            if (xn != null)
            {
                db.XetNghiems.Remove(xn);
                return db.SaveChanges();
            }
            else
                return -1;
        }

        public int Insert(XetNghiem xn)
        {
            db.XetNghiems.Add(xn);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return xn.MaXN;
        }

        public int Update(XetNghiem xn)
        {
            XetNghiem tmp = db.XetNghiems.Find(xn.MaXN);
            if (tmp != null)
            {
                tmp.TenXN = xn.TenXN;
                tmp.TriSoBinhThuong = xn.TriSoBinhThuong;
                tmp.DonViTinh = xn.DonViTinh;
                tmp.DonGia = xn.DonGia;
                db.SaveChanges();//luu vao o dia
            }
            return tmp.MaXN;
        }
    }
}