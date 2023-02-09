using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.DTO;
using PhongKhamNhi.Models.Entities;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.ThuNgan.Controllers
{
    public class DkXnController : Controller
    {
        // GET: ThuNgan/DkXn
        public ActionResult Index(string bn, string tu, string den, string trangThai, int pageNum = 1, int pageSize = 9)
        {
            ViewBag.bn = bn;
            ViewBag.tu = tu;
            ViewBag.den = den;
            ViewBag.trangThai = trangThai;
            if (tu == null)
                tu = "2020-11-19 12:00:00";
            if (den == null)
                den = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            NhanVien nv = (NhanVien)Session["user"];
            return View(new PhieuDkXnDAO().lstPhieuDkXn(nv.MaChiNhanh, bn, tu, den, trangThai, pageNum, pageSize));
        }
        public JsonResult ChangeStatus(int id)
        {
            PhieuDkXnDAO dao = new PhieuDkXnDAO();
            PhieuDKXN p = dao.FindByID(id);
            NhanVien nv = (NhanVien)Session["user"];
            p.MaNV = nv.MaNV;
            DoanhThuDAO daodt = new DoanhThuDAO();
            DoanhThu d = daodt.Find(p.ThoiGianLap, nv.MaChiNhanh);
            if (p.TrangThai == 0)
            {
                p.TrangThai = 1;
                if (d != null)
                {
                    d.ThuXetNghiem += p.TongTien;
                    d.TongTien += p.TongTien;
                    daodt.Update(d);
                }
                else
                {
                    d = new DoanhThu();
                    d.NgayThangNam = p.ThoiGianLap;
                    d.MaChiNhanh = nv.MaChiNhanh;
                    d.ThuDichVuKham = 0;
                    d.ThuXetNghiem = p.TongTien;
                    d.ThuBanThuoc = 0;
                    d.TongTien = p.TongTien;
                    daodt.Insert(d);
                }
            }     
            else if (p.TrangThai == 1)
            {
                p.TrangThai = 0;
                if (d != null)
                {
                    d.ThuXetNghiem -= p.TongTien;
                    d.TongTien -= p.TongTien;
                    daodt.Update(d);
                }
            }    
            dao.UpdateThuNgan(p);
            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrintDkXn(int id)
        {
            return new ActionAsPdf("InDkXn", new { id = id });
        }
        public ActionResult InDkXn(int id)
        {
            PhieuDkXnDAO dao = new PhieuDkXnDAO();
            List<KqXnDTO> lst = dao.ListKqXn(id);
            PhieuDKXN x = dao.FindByID(id);
            ViewBag.PhieuDkXn = x;
            ViewBag.ngay = x.ThoiGianLap.ToString("dd/MM/yyyy");
            PhieuKhamBenh p = new PhieuKhamBenhDAO().FindByID(x.MaPhieuKB);
            ViewBag.Pk = p.MaPhieuKB;
            ViewBag.cn = p.ChiNhanh.DiaChi;
            ViewBag.BenhNhi = p.BenhNhi;
            return View(lst);
        }
    }
}