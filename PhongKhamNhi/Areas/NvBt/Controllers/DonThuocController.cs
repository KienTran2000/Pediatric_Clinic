using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.DTO;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.NvBt.Controllers
{
    public class DonThuocController : Controller
    {
        // GET: NvBt/DonThuoc
        public ActionResult Index(string maPk, string ten, string tu, string den, int pageNum = 1, int pageSize = 9)
        {
            ViewBag.ten = ten;
            ViewBag.tu = tu;
            ViewBag.den = den;
            if (maPk == null)
                maPk = "0";
            if (tu == null)
                tu = "2020-11-19 12:00:00";
            if (den == null)
                den = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            NhanVien nv = (NhanVien)Session["user"];
            return View(new ThuocDAO().ListDonThuoc(nv.MaChiNhanh, int.Parse(maPk), ten, tu, den, pageNum, pageSize));
        }

        public ActionResult CreateByMaPk(int id)
        {
            PhieuKhamBenh p = new PhieuKhamBenhDAO().FindByID(id);
            HoaDonBanThuoc h = new HoaDonBanThuoc();
            h.TenKH = p.BenhNhi.TenThanNhan;
            h.Sdt = p.BenhNhi.SdtThanNhan;
            h.DiaChi = p.BenhNhi.DiaChi;
            List<CtHdThuocDTO> lst = new List<CtHdThuocDTO>();
            List<ChiTietDonThuocDTO> lstD = new ThuocDAO().lstThuocByMaPk(id);
            double t = 0;
            foreach (ChiTietDonThuocDTO i in lstD)
            {
                Thuoc th = new ThuocDAO().FindByID(i.MaThuoc);
                CtHdThuocDTO c = new CtHdThuocDTO();
                c.MaThuoc = i.MaThuoc;
                c.SoLuong = i.SoLuong;
                c.TenThuoc = i.TenThuoc;
                c.DonViTinh = i.DonViTinh;
                c.DonGia = th.DonGia;
                lst.Add(c);
                t += i.SoLuong * th.DonGia;
            }
            ViewBag.tong = t;
            ViewBag.ListThuoc = lst;
            Session["cart"] = lst;
            return View(h);
        }
        [HttpPost]
        public ActionResult CreateByMaPk(HoaDonBanThuoc h)
        {
            h.ThoiGian = DateTime.Now;
            h.TrangThai = false;
            NhanVien nv = (NhanVien)Session["user"];
            h.MaChiNhanh = nv.MaChiNhanh;
            h.MaNvLap = nv.MaNV;
            HoaDonThuocDAO dao = new HoaDonThuocDAO();
            int id = dao.Insert(h);
            List<CtHdThuocDTO> lst = (List<CtHdThuocDTO>)Session["cart"];
            double t = 0;
            foreach (CtHdThuocDTO i in lst)
            {
                dao.InsertCt(i.MaThuoc, id, i.SoLuong, i.DonGia);
                t += i.DonGia * i.SoLuong;
            }
            dao.UpdatetongTien(id, t);
            return RedirectToAction("Index", "NvBtHome");
        }
    }
}