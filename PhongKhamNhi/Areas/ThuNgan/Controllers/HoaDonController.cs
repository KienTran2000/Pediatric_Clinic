using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.DTO;
using PhongKhamNhi.Models.Entities;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.ThuNgan.Controllers
{
    public class HoaDonController : Controller
    {
        // GET: ThuNgan/HoaDon
        public ActionResult Index(string maHd, string ten, string tu, string den, int type = 2, int pageNum = 1, int pageSize = 9)
        {
            ViewBag.ten = ten;
            ViewBag.tu = tu;
            ViewBag.den = den;
            ViewBag.type = type;
            if (maHd == null)
                maHd = "0";
            if (tu == null)
                tu = "2020-11-19 12:00:00";
            if (den == null)
                den = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            NhanVien nv = (NhanVien)Session["user"];
            return View(new HoaDonThuocDAO().ListHdThuoc(nv.MaChiNhanh, int.Parse(maHd), ten, tu, den, type, pageNum, pageSize));
        }
        public JsonResult ChangeStatus(int id)
        {
            HoaDonThuocDAO dao = new HoaDonThuocDAO();
            HoaDonBanThuoc p = dao.FindByID(id);
            NhanVien nv = (NhanVien)Session["user"];
            p.MaNvLap = nv.MaNV;
            DoanhThuDAO daodt = new DoanhThuDAO();
            DoanhThu d = daodt.Find(p.ThoiGian, p.MaChiNhanh);
            if (p.TrangThai == false)
            {
                p.TrangThai = true;
                if (d != null)
                {
                    d.ThuBanThuoc += p.TongTien;
                    d.TongTien += p.TongTien;
                    daodt.Update(d);
                }
                else
                {
                    d = new DoanhThu();
                    d.NgayThangNam = p.ThoiGian;
                    d.MaChiNhanh = p.MaChiNhanh;
                    d.ThuDichVuKham = 0;
                    d.ThuXetNghiem = 0;
                    d.ThuBanThuoc = p.TongTien;
                    d.TongTien = p.TongTien;
                    daodt.Insert(d);
                }
            }
            else if (p.TrangThai == true)
            {
                p.TrangThai = false;
                if (d != null)
                {
                    d.ThuBanThuoc -= p.TongTien;
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

        public ActionResult Edit(int id)
        {
            HoaDonThuocDAO dao = new HoaDonThuocDAO();
            List<CtHdThuocDTO> lst = dao.lstThuocByMaHd(id);
            HoaDonBanThuoc h = dao.FindByID(id);
            ViewBag.ListThuoc = lst;
            return View(h);
        }

        [HttpPost]
        public ActionResult Edit(HoaDonBanThuoc h)
        {
            HoaDonThuocDAO dao = new HoaDonThuocDAO();
            HoaDonBanThuoc p = dao.FindByID(h.MaHoaDon);
            NhanVien nv = (NhanVien)Session["user"];
            p.MaNvLap = nv.MaNV;
            DoanhThuDAO daodt = new DoanhThuDAO();
            DoanhThu d = daodt.Find(p.ThoiGian, p.MaChiNhanh);
            if (p.TrangThai == false)
            {
                p.TrangThai = true;
                if (d != null)
                {
                    d.ThuBanThuoc += p.TongTien;
                    d.TongTien += p.TongTien;
                    daodt.Update(d);
                }
                else
                {
                    d = new DoanhThu();
                    d.NgayThangNam = p.ThoiGian;
                    d.MaChiNhanh = p.MaChiNhanh;
                    d.ThuDichVuKham = 0;
                    d.ThuXetNghiem = 0;
                    d.ThuBanThuoc = p.TongTien;
                    d.TongTien = p.TongTien;
                    daodt.Insert(d);
                }
            }
            else if (p.TrangThai == true)
            {
                p.TrangThai = false;
                if (d != null)
                {
                    d.ThuBanThuoc -= p.TongTien;
                    d.TongTien -= p.TongTien;
                    daodt.Update(d);
                }
            }
            dao.UpdateThuNgan(p);
            return RedirectToAction("Index", "HoaDon");
        }

        public ActionResult Print(int id)
        {
            return new ActionAsPdf("In", new { id = id });
        }
        public ActionResult In(int id)
        {
            List<CtHdThuocDTO> lst = new HoaDonThuocDAO().lstThuocByMaHd(id);
            double t = 0;
            foreach (CtHdThuocDTO i in lst)
            {
                t += i.SoLuong * i.DonGia;
            }
            ViewBag.tong = t;
            HoaDonBanThuoc p = new HoaDonThuocDAO().FindByID(id);
            ViewBag.hd = p;
            ViewBag.ngay = p.ThoiGian.ToString("dd/MM/yyyy");
            return View(lst);
        }
    }
}