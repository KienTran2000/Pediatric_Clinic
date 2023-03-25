using PagedList;
using PhongKhamNhi.Models.DTO;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DAO
{
    public class BacSiDAO
    {
        ModelPkNhi db;
        public BacSiDAO()
        {
            db = new ModelPkNhi();
        }

        public IEnumerable<BacSi> GetListBs(int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<BacSi>("SELECT * FROM BacSi").ToPagedList<BacSi>(pageNum, pageSize);
            return lst;
        }

        public IEnumerable<BacSi> GetListBs(int ma, string ten, string gt, int cn, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<BacSi>(string.Format("GetListBs {0}, N'{1}', N'{2}', {3}",
                ma, ten, gt, cn)).ToPagedList<BacSi>(pageNum, pageSize);
            return lst;
        }

        public BacSi GetBacSi(int idTk)
        {
            var res = (from s in db.BacSis where s.IdTaiKhoan == idTk select s);
            if (res.Count() > 0)
                return res.FirstOrDefault();
            return null;
        }
        public List<BacSi> GetListBacSiByMaCn(int maCn)
        {
            var res = (from s in db.BacSis where s.MaChiNhanh == maCn select s);
            foreach(BacSi item in res)
            {
                item.PhieuDangKyKhams.Clear();
                item.PhieuDangKyKhams = (from s in db.PhieuDangKyKhams where s.TrangThai == true && s.MaBS == item.MaBS && s.ThoiGianHen >= DateTime.Now orderby s.ThoiGianHen select s).ToList();
            }
            return res.ToList();
        }
        public List<BacSiDTO> GetListTgKhamBs(int maCn)
        {
            List<BacSiDTO> lst = new List<BacSiDTO>();
            var res = db.Database.SqlQuery<BacSi>(string.Format("SELECT * FROM BacSi WHERE MaChiNhanh = {0}", maCn));
            foreach (BacSi item in res)
            {
                BacSiDTO bs = new BacSiDTO();
                bs.MaBS = item.MaBS;
                bs.HoTen = item.HoTen;
                bs.sldk = (from s in db.PhieuKhamBenhs where s.TrangThai == 1 && s.MaBS == item.MaBS && s.ThoiGianKham == null select s).ToList().Count;
                bs.sldk += (from s in db.PhieuDangKyKhams where s.ThoiGianHen > DateTime.Now && s.TrangThai == true && s.MaBS == item.MaBS select s).ToList().Count;
                lst.Add(bs);
            }
            return lst;
        }
        public BacSi FindByID(int id)
        {
            return db.BacSis.Find(id);
        }

        public List<BacSi> GetListBacSi()
        {
            DateTime tmp = DateTime.Now.AddDays(1);
            DateTime dt = new DateTime(tmp.Year, tmp.Month, tmp.Day, 7, 30, 0);

            var res = (from s in db.BacSis select s);
            foreach (BacSi item in res)
            {
                item.PhieuDangKyKhams.Clear();
                item.PhieuDangKyKhams = (from s in db.PhieuDangKyKhams
                                         where s.TrangThai == true && s.MaBS == item.MaBS && s.ThoiGianHen > dt
                                         orderby s.ThoiGianHen
                                         select s).ToList();
            }
            return res.ToList();
        }
        public IEnumerable<BacSi> List5Bs()
        {
            var lst = db.Database.SqlQuery<BacSi>("SELECT TOP 5 * FROM BacSi").ToList<BacSi>();
            return lst;
        }

        public List<BacSiDTO> GetListBacSiByMaCn2(int maCn, int type)
        {
            bool t = false;
            if (type == 1)
                t = true;
            var res = (from s in db.BacSis where s.MaChiNhanh == maCn && (type == 3 || s.Type == t) select s);
            List<BacSiDTO> lst = new List<BacSiDTO>();
            foreach (BacSi item in res)
            {
                lst.Add(new BacSiDTO(item));
            }
            return lst;
        }

        public int Update(BacSi nv)
        {
            BacSi tmp = db.BacSis.Find(nv.MaBS);
            if (tmp != null)
            {
                tmp.GioiTinh = nv.GioiTinh;
                tmp.HocVi = nv.HocVi;
                tmp.HoTen = nv.HoTen;
                tmp.Anh = nv.Anh;
                tmp.GioiThieu = nv.GioiThieu;
                tmp.NgaySinh = nv.NgaySinh;
                tmp.NgayVaoLam = nv.NgayVaoLam;
                tmp.Sdt = nv.Sdt;
                db.SaveChanges();//luu vao o dia
            }
            return tmp.MaBS;
        }

        public int UpdateByAdmin(BacSi bs)
        {
            BacSi tmp = db.BacSis.Find(bs.MaBS);
            if (tmp != null)
            {
                tmp.Anh = bs.Anh;
                tmp.GioiTinh = bs.GioiTinh;
                tmp.HocVi = bs.HocVi;
                tmp.HoTen = bs.HoTen;
                tmp.NgaySinh = bs.NgaySinh;
                tmp.NgayVaoLam = bs.NgayVaoLam;
                tmp.Sdt = bs.Sdt;
                tmp.MaChiNhanh = bs.MaChiNhanh;
                tmp.GioiThieu = bs.GioiThieu;
                db.SaveChanges();//luu vao o dia
            }
            return tmp.MaBS;
        }

        public int Insert(BacSi bs)
        {
            db.BacSis.Add(bs);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return bs.MaBS;
        }

        public int Delete(int id)
        {
            BacSi bs = db.BacSis.Find(id);
            if (bs != null)
            {
                db.BacSis.Remove(bs);
                return db.SaveChanges();
            }
            else
                return -1;
        }
    }
}