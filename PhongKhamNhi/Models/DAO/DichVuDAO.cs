using PagedList;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DAO
{
    public class DichVuDAO
    {
        ModelPkNhi db;
        public DichVuDAO()
        {
            db = new ModelPkNhi();
        }
        public List<DichVuKham> GetListDv()
        {
            return db.DichVuKhams.ToList();
        }
        public DichVuKham FindByID(int id)
        {
            return db.DichVuKhams.Find(id);
        }

        public int Insert(DichVuKham d)
        {
            db.DichVuKhams.Add(d);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return d.MaDV;
        }
        public int Update(DichVuKham d)
        {
            DichVuKham tmp = db.DichVuKhams.Find(d.MaDV);
            if (tmp != null)
            {
                tmp.TenDV = d.TenDV;
                tmp.DonGia = d.DonGia;
                tmp.Anh = d.Anh;
                tmp.MoTa = d.MoTa;
                db.SaveChanges();//luu vao o dia
            }
            return tmp.MaDV;
        }

        public int InsertXn(int dv, int xn)
        {
            var res = db.Database.ExecuteSqlCommand(string.Format(
                "INSERT INTO [dbo].[DichVuKham_XN] VALUES({0}, {1})",
                dv, xn));
            return 1;
        }
        public int DeleteXn(int id)
        {
            var res = db.Database.ExecuteSqlCommand(string.Format(
                "DELETE FROM [dbo].[DichVuKham_XN] WHERE MaDV = {0}", id));
            return 1;
        }

        public List<DichVuKham> GetList3Dv()
        {
            var lst = db.Database.SqlQuery<DichVuKham>("SELECT TOP 3 * FROM DichVuKham ORDER BY MaDV DESC").ToList<DichVuKham>();
            return lst;
        }

        public IEnumerable<DichVuKham> GetListDv(int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<DichVuKham>("SELECT * FROM DichVuKham").ToPagedList<DichVuKham>(pageNum, pageSize);
            return lst;
        }
    }
}