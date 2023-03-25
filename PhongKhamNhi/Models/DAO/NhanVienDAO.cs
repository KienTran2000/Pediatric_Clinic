using PagedList;
using PhongKhamNhi.Models.Entities;
using System.Collections.Generic;
using System.Linq;

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
                tmp.MaChiNhanh = nv.MaChiNhanh;
                db.SaveChanges();//luu vao o dia
            }
            return tmp.MaNV;
        }

        public IEnumerable<NhanVien> GetListNv(int ma, string ten, string gt, int cn, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<NhanVien>(string.Format("GetListNv {0}, N'{1}', N'{2}', {3}",
                ma, ten, gt, cn)).ToPagedList<NhanVien>(pageNum, pageSize);
            return lst;
        }

        public NhanVien FindByID(int id)
        {
            return db.NhanViens.Find(id);
        }

        public int Insert(NhanVien nv)
        {
            db.NhanViens.Add(nv);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return nv.MaNV;
        }

        public int Delete(int id)
        {
            NhanVien nv = db.NhanViens.Find(id);
            if (nv != null)
            {
                db.NhanViens.Remove(nv);
                return db.SaveChanges();
            }
            else
                return -1;
        }
    }
}