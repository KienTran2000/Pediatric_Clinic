using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DAO
{
    public class NhanVienDAO
    {
        ModelPkNhi db;
        public NhanVienDAO()
        {
            db = new ModelPkNhi();
        }
        public NhanVien getNhanVien(int idTk)
        {
            var res = (from s in db.NhanViens where s.IdTaiKhoan == idTk select s);
            if (res.Count() > 0)
                return res.FirstOrDefault();
            return null;
        }

        public int Update(NhanVien nv)
        {
            NhanVien tmp = db.NhanViens.Find(nv.MaNV);
            if (tmp != null)
            {
                tmp.GioiTinh = nv.GioiTinh;
                tmp.ChucVu = nv.ChucVu;
                tmp.HoTen = nv.HoTen;
                tmp.NgaySinh = nv.NgaySinh;
                tmp.NgayVaoLam = nv.NgayVaoLam;
                tmp.Sdt = nv.Sdt;
                db.SaveChanges();//luu vao o dia
            }
            return tmp.MaNV;
        }
    }
}