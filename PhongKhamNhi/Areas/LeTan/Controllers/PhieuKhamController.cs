using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.LeTan.Controllers
{
    public class PhieuKhamController : Controller
    {
        // GET: LeTan/PhieuKham
        public ActionResult Index(string ten, string dv, string bs, string trangThai, int pageNum = 1, int pageSize = 9)
        {
            if (dv == null)
                dv = "0";
            if (bs == null)
                bs = "0";
            if (trangThai == null)
                trangThai = "3";
            ViewBag.ten = ten;
            ViewBag.dv = int.Parse(dv);
            ViewBag.bs = int.Parse(bs);
            ViewBag.trangThai = int.Parse(trangThai);
            PhieuKhamBenhDAO dao = new PhieuKhamBenhDAO();
            NhanVien nv = (NhanVien)Session["user"];
            ViewBag.ListBacSi = new BacSiDAO().GetListBacSiByMaCn(nv.MaChiNhanh);
            ViewBag.ListDichVu = new DichVuDAO().GetListDv();
            return View(dao.lstPhieuKb(nv.MaChiNhanh, ten, int.Parse(dv), int.Parse(bs), int.Parse(trangThai), pageNum, pageSize));
        }
        [HttpPost]
        public ActionResult Index(FormCollection data, int pageNum = 1, int pageSize = 9)
        {
            PhieuKhamBenhDAO dao = new PhieuKhamBenhDAO();
            if (data.Count > 0)
            {
                string[] ids = data["checkBoxId"].Split(new char[] { ',' });
                foreach (string id in ids)
                {
                    dao.Delete(int.Parse(id));
                }
            }
            NhanVien nv = (NhanVien)Session["user"];
            return View(dao.lstPhieuKb(nv.MaChiNhanh, "", 0, 0, 3, pageNum, pageSize));
        }
        public ActionResult CreateByPdk(int id)
        {
            PhieuDangKyKham p = new PhieuDangKyKhamDAO().FindByID(id);
            BenhNhi bn = new BenhNhiDAO().FindBnByTen(p.HoTen, p.Sdt);
            if (bn == null)
                return RedirectToAction("Notification", "PhieuKham");
            PhieuKhamBenh pk = new PhieuKhamBenh();
            pk.MaBS = p.MaBS;
            pk.MaBN = bn.MaBN;
            pk.ThoiGianKham = p.ThoiGianHen;
            ViewBag.bacSi = new BacSiDAO().FindByID(pk.MaBS).HoTen;
            ViewBag.benhNhi = bn;
            ViewBag.ListDv = new DichVuDAO().GetListDv();
            return View(pk);
        }
        [HttpPost]
        public ActionResult CreateByPdk(PhieuKhamBenh p, int dichVu)
        {
            if (ModelState.IsValid)
            {
                NhanVien nv = (NhanVien)Session["user"];
                p.MaChiNhanh = nv.MaChiNhanh;
                p.MaNvLap = nv.MaNV;
                p.MaDV = dichVu;
                p.DonGia = new DichVuDAO().FindByID(p.MaDV).DonGia;
                p.ThoiGianLap = DateTime.Now;
                p.TrangThai = 0;
                new PhieuKhamBenhDAO().Insert(p);
                return RedirectToAction("Index", "PhieuKham");
            }
            //...
            return View(p);
        }
        public JsonResult GetDonGia(int id)
        {
            double g = new DichVuDAO().FindByID(id).DonGia;
            return Json(new
            {
                data = g,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Notification()
        {
            return View();
        }

        public ActionResult Create(int id)
        {
            BenhNhi bn = new BenhNhiDAO().FindByID(id);
            PhieuKhamBenh pk = new PhieuKhamBenh();
            pk.MaBN = bn.MaBN;
            NhanVien nv = (NhanVien)Session["user"];
            ViewBag.benhNhi = bn;
            ViewBag.ngaySinh = bn.NgaySinh.ToString().Split(' ')[0];
            ViewBag.ListDv = new DichVuDAO().GetListDv();
            ViewBag.ListBacSi = new BacSiDAO().GetListTgKhamBs(nv.MaChiNhanh);
            return View(pk);
        }
        [HttpPost]
        public ActionResult Create(PhieuKhamBenh p, int dichVu, int bs)
        {
            if (ModelState.IsValid)
            {
                NhanVien nv = (NhanVien)Session["user"];
                p.MaChiNhanh = nv.MaChiNhanh;
                p.MaNvLap = nv.MaNV;
                p.MaDV = dichVu;
                p.MaBS = bs;
                p.DonGia = new DichVuDAO().FindByID(p.MaDV).DonGia;
                p.TrangThai = 0;
                p.ThoiGianLap = DateTime.Now;
                new PhieuKhamBenhDAO().Insert(p);
                return RedirectToAction("Index", "PhieuKham");
            }
            return View(p);
        }

        public ActionResult Edit(int id)
        {
            PhieuKhamBenh p = new PhieuKhamBenhDAO().FindByID(id);
            ViewBag.ListDv = new DichVuDAO().GetListDv();
            ViewBag.ListBacSi = new BacSiDAO().GetListTgKhamBs(p.MaChiNhanh);
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(PhieuKhamBenh tmp, int dichVu, int bs)
        {
            PhieuKhamBenh p = new PhieuKhamBenhDAO().FindByID(tmp.MaPhieuKB);
            p.MaDV = dichVu;
            p.MaBS = bs;
            p.ThoiGianLap = DateTime.Now;
            NhanVien nv = (NhanVien)Session["user"];
            p.MaNvLap = nv.MaNV;
            p.DonGia = new DichVuDAO().FindByID(p.MaDV).DonGia;
            new PhieuKhamBenhDAO().UpdateLeTan(p);
            return RedirectToAction("Index", "PhieuKham");
        }

        public ActionResult Delete(int id)
        {
            new PhieuKhamBenhDAO().Delete(id);
            return RedirectToAction("Index", "PhieuKham");
        }
    }
}