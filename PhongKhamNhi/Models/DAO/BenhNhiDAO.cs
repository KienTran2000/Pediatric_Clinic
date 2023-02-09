using PagedList;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DAO
{
    public class BenhNhiDAO
    {
        ModelPkNhi db;
        public BenhNhiDAO()
        {
            db = new ModelPkNhi();
        }
        public BenhNhi FindBnByTen(string ten, string sdt)
        {
            var res = (from s in db.BenhNhis where s.HoTen == ten && s.SdtThanNhan == sdt select s);
            return res.FirstOrDefault();
        }
        public BenhNhi FindByID(int id)
        {
            return db.BenhNhis.Find(id);
        }
        public IEnumerable<BenhNhi> lstBn(string ten, string ns, string sdt, string dc, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<BenhNhi>(string.Format("lstBenhNhi N'{0}', '{1}', '{2}', N'{3}'",
                ten, ns, sdt, dc)
                ).ToPagedList<BenhNhi>(pageNum, pageSize);
            return lst;
        }

        public int Insert(BenhNhi b)
        {
            db.BenhNhis.Add(b);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return b.MaBN;
        }
        public int Update(BenhNhi b)
        {
            BenhNhi tmp = db.BenhNhis.Find(b.MaBN);
            if (tmp != null)
            {
                tmp = b;
                db.SaveChanges();//luu vao o dia
            }
            return tmp.MaBN;
        }
        public int Delete(int id)
        {
            BenhNhi bn = db.BenhNhis.Find(id);
            if (bn != null)
            {
                db.BenhNhis.Remove(bn);
                return db.SaveChanges();
            }
            else
                return -1;
        }

        public BenhNhi Login(string username, string password)
        {
            var res = (from s in db.BenhNhis where s.TenDangNhap == username && s.MatKhau == password select s);
            if (res.Count() > 0)
                return res.FirstOrDefault();
            return null;
        }
        public bool CheckPassword(int id, string pass)
        {
            var res = (from s in db.BenhNhis where s.MaBN == id && s.MatKhau == pass select s);
            if (res.Count() > 0)
                return true;
            return false;
        }
    }
}