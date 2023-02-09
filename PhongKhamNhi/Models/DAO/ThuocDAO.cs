using PagedList;
using PhongKhamNhi.Models.DTO;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhongKhamNhi.Models.DAO
{
    public class ThuocDAO
    {
        ModelPkNhi db;
        public ThuocDAO()
        {
            db = new ModelPkNhi();
        }

        public List<Thuoc> lstSearchThuoc(string ten)
        {
            var res = db.Database.SqlQuery<Thuoc>(string.Format("lstSearchThuoc N'{0}'", ten));
            return res.ToList();
        }
        public List<ChiTietDonThuocDTO> lstThuocByMaPk(int id)
        {
            var res = db.Database.SqlQuery<ChiTietDonThuocDTO>(string.Format("SELECT P.MaThuoc, T.TenThuoc, P.SoLuong, T.DonViTinh, P.CachDung FROM PhieuKham_Thuoc P, Thuoc T WHERE P.MaPhieuKB = {0} AND P.MaThuoc = T.MaThuoc", id));
            return res.ToList();
        }
        public IEnumerable<DonThuocDTO> ListDonThuoc(int cn, int maPk, string bn, string tu, string den, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<DonThuocDTO>(string.Format("lstDonThuoc {0}, {1}, N'{2}', '{3}', '{4}'",
                cn, maPk, bn, tu, den)).ToPagedList<DonThuocDTO>(pageNum, pageSize);
            return lst;
        }
        public List<LoaiThuoc> GetListLoaiThuoc()
        {
            return db.LoaiThuocs.ToList();
        }
        public IEnumerable<Thuoc> lstThuoc(string ten, int lt, int pageNum, int pageSize)
        {
            var lst = db.Database.SqlQuery<Thuoc>(string.Format("lstThuoc N'{0}', {1}",
                ten, lt)
                ).ToPagedList<Thuoc>(pageNum, pageSize);
            return lst;
        }

        public List<ThuocBanDTO> ThongKeThuocBan(int year, int month, int maCn)
        {
            var res = db.Database.SqlQuery<ThuocBanDTO>(string.Format("ThongKeThuocBan {0}, {1}, {2}", year, month, maCn));
            return res.ToList();
        }

        public int InsertCtDonThuoc(PhieuKham_Thuoc t)
        {
            db.PhieuKham_Thuoc.Add(t);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return 1;
        }
        public int Insert(Thuoc t)
        {
            db.Thuocs.Add(t);//luu tren RAM
            db.SaveChanges();//luu vao o dia
            return 1;
        }
        public int Update(Thuoc p)
        {
            Thuoc tmp = db.Thuocs.Find(p.MaThuoc);
            if (tmp != null)
            {
                tmp.TenThuoc = p.TenThuoc;
                tmp.DonViTinh = p.DonViTinh;
                tmp.DonGia = p.DonGia;
                tmp.MaLoaiThuoc = p.MaLoaiThuoc;
                db.SaveChanges();//luu vao o dia
            }
            return tmp.MaThuoc;
        }
        public int Delete(int id)
        {
            Thuoc t = db.Thuocs.Find(id);
            if (t != null)
            {
                db.Thuocs.Remove(t);
                return db.SaveChanges();
            }
            else
                return -1;
        }

        public Thuoc FindByID(int id)
        {
            return db.Thuocs.Find(id);
        }
    }
}