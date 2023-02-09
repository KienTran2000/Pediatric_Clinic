using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DAO
{
    public class TaiKhoanDAO
    {
        ModelPkNhi db;
        public TaiKhoanDAO()
        {
            db = new ModelPkNhi();
        }
        public TaiKhoan Login(string username, string password)
        {
            var res = (from s in db.TaiKhoans where s.TenDangNhap == username && s.MatKhau == password select s);
            if (res.Count() > 0)
                return res.FirstOrDefault();
            return null;
        }
        //public bool IsAdmin(string username, string password)
        //{
        //    var res = (from s in db.Admins where s.Username == username && s.Password == password && s.AdminType == "Admin" select s);
        //    if (res.Count() > 0)
        //        return true;
        //    return false;
        //}
    }
}