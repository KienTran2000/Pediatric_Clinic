using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.DTO;
using PhongKhamNhi.Models.Entities;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.BacSiArea.Controllers
{
    public class BacSiHomeController : Controller
    {
        // GET: BacSiArea/BacSiHome
        public ActionResult Index(string ten, string tu, string den, string dv, string trangThai, int pageNum = 1, int pageSize = 9)
        {
            if (dv == null)
                dv = "0";
            if (trangThai == null)
                trangThai = "3";
            ViewBag.ten = ten;
            ViewBag.dv = int.Parse(dv);
            ViewBag.tu = tu;
            ViewBag.den = den;
            ViewBag.trangThai = int.Parse(trangThai);
            if (tu == null)
                tu = "2020-11-19 12:00:00";
            if (den == null)
                den = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            PhieuKhamBenhDAO dao = new PhieuKhamBenhDAO();
            BacSi bs = (BacSi)Session["user"];
            ViewBag.ListDichVu = new DichVuDAO().GetListDv();
            return View(dao.lstPhieuKb2(bs.MaChiNhanh, ten, int.Parse(dv), bs.MaBS, int.Parse(trangThai), tu, den, pageNum, pageSize));
        }
        public ActionResult Edit(int id)
        {
            PhieuKhamBenh p = new PhieuKhamBenhDAO().FindByID(id);
            Session["maPk"] = id;
            PhieuDkXnDAO dao = new PhieuDkXnDAO();
            PhieuDKXN x = dao.FindByMaPk(id);
            if(x != null)
                ViewBag.PhieuDkXn = dao.ListKqXn(x.MaPhieuDKXN);
            List<ChiTietDonThuocDTO> lst = new ThuocDAO().lstThuocByMaPk(id);
            if (lst.Count > 0)
                ViewBag.DonThuoc = lst;
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(PhieuKhamBenh p)
        {
            if (ModelState.IsValid)
            {
                new PhieuKhamBenhDAO().Update(p);
                return RedirectToAction("Index", "BacSiHome");
            }
            return View(p);
        }
        public ActionResult PhieuXn(int dv)
        {
            List<XetNghiem> lst = new XetNghiemDAO().GetListXnByMaDv(dv);
            Session["dkxn"] = lst;
            return PartialView(lst);
        }
        public JsonResult ListXn(string key)
        {
            var data = new XetNghiemDAO().lstSearchXn(key);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddXn(int id)
        {
            bool b = false;
            List<XetNghiem> lst = (List<XetNghiem>)Session["dkxn"];
            foreach (XetNghiem item in lst)
            {
                if (item.MaXN == id)
                {
                    b = true;
                    break;
                }
            }
            if (!b)
            {
                XetNghiem x = new XetNghiem();
                x.MaXN = id;
                lst.Add(x);
            }
            Session["dkxn"] = lst;
            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteXn(string id)
        {
            int ma = int.Parse(id.Split('-')[1]);
            List<XetNghiem> lst = (List<XetNghiem>)Session["dkxn"];
            foreach(XetNghiem item in lst)
            {
                if(item.MaXN == ma)
                {
                    lst.Remove(item);
                    break;
                }
            }
            Session["dkxn"] = lst;
            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SavePhieuDkXn()
        {
            List<XetNghiem> lst = (List<XetNghiem>)Session["dkxn"];
            XetNghiemDAO dao = new XetNghiemDAO();
            double t = 0;
            foreach (XetNghiem item in lst)
            {
                XetNghiem x = dao.FindByID(item.MaXN);
                t += x.DonGia;
                item.DonGia = x.DonGia;
            }
            PhieuDKXN p = new PhieuDKXN();
            p.MaPhieuKB = (int)Session["maPk"];
            p.ThoiGianLap = DateTime.Now;
            p.TrangThai = 0;
            p.TongTien = t;
            PhieuDkXnDAO daoPxn = new PhieuDkXnDAO();
            int maPxn = daoPxn.Insert(p);
            foreach (XetNghiem item in lst)
            {
                KetQuaXN k = new KetQuaXN();
                k.MaPhieuDKXN = maPxn;
                k.MaXN = item.MaXN;
                k.DonGia = item.DonGia;
                k.MaNV = null;
                daoPxn.InsertDetail(k);
            }
            Session["dkxn"] = null;
            Session["iddkxn"] = maPxn;
            return PartialView(new PhieuDkXnDAO().ListXnByMaPdk(maPxn));
        }
        public ActionResult ViewResult()
        {
            int id = (int)Session["iddkxn"];
            return PartialView(new PhieuDkXnDAO().ListKqXn(id));
        }


        public ActionResult KeThuoc()
        {
            return PartialView();
        }
        public JsonResult ListThuoc(string key)
        {
            var data = new ThuocDAO().lstSearchThuoc(key);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChonThuoc(int idT, int sl, string cd)
        {
            List<ChiTietDonThuocDTO> lst = (List<ChiTietDonThuocDTO>)Session["donthuoc"];
            if (lst == null)
                lst = new List<ChiTietDonThuocDTO>();
            ChiTietDonThuocDTO c = new ChiTietDonThuocDTO();
            foreach(ChiTietDonThuocDTO item in lst)
            {
                if(item.MaThuoc == idT)
                {
                    item.SoLuong = sl;
                    item.CachDung = cd;
                    Session["donthuoc"] = lst;
                    return PartialView(lst);
                }    
            }
            c.MaThuoc = idT;
            c.SoLuong = sl;
            c.CachDung = cd;
            Thuoc t = new ThuocDAO().FindByID(idT);
            c.TenThuoc = t.TenThuoc;
            c.DonViTinh = t.DonViTinh;
            lst.Add(c);
            Session["donthuoc"] = lst;
            return PartialView(lst);
        }
        public JsonResult DeleteThuoc(string id)
        {
            int ma = int.Parse(id.Split('-')[1]);
            List<ChiTietDonThuocDTO> lst = (List<ChiTietDonThuocDTO>)Session["donthuoc"];
            foreach (ChiTietDonThuocDTO item in lst)
            {
                if (item.MaThuoc == ma)
                {
                    lst.Remove(item);
                    break;
                }
            }
            Session["donthuoc"] = lst;
            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditThuoc(string id)
        {
            int ma = int.Parse(id.Split('-')[1]);
            List<ChiTietDonThuocDTO> lst = (List<ChiTietDonThuocDTO>)Session["donthuoc"];
            ChiTietDonThuocDTO c = new ChiTietDonThuocDTO();
            foreach (ChiTietDonThuocDTO item in lst)
            {
                if (item.MaThuoc == ma)
                {
                    c = item;
                    break;
                }
            }
            return Json(new
            {
                data = c,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveDonThuoc()
        {
            List<ChiTietDonThuocDTO> lst = (List<ChiTietDonThuocDTO>)Session["donthuoc"];
            int maPk = (int)Session["maPk"];
            ThuocDAO dao = new ThuocDAO();
            foreach (ChiTietDonThuocDTO item in lst)
            {
                PhieuKham_Thuoc k = new PhieuKham_Thuoc();
                k.MaPhieuKB = maPk;
                k.MaThuoc = item.MaThuoc;
                k.SoLuong = item.SoLuong;
                k.CachDung = item.CachDung;
                dao.InsertCtDonThuoc(k);
            }
            Session["donthuoc"] = null;
            return PartialView(lst);
        }
        public ActionResult PrintDonThuoc()
        {
            int id = (int)Session["maPk"];
            return new ActionAsPdf("InDonThuoc", new { id = id });
        }
        public ActionResult InDonThuoc(int id)
        {
            List<ChiTietDonThuocDTO> lst = new ThuocDAO().lstThuocByMaPk(id);
            PhieuKhamBenh p = new PhieuKhamBenhDAO().FindByID(id);
            ViewBag.MaPhieuKB = id;
            ViewBag.cn = p.ChiNhanh.DiaChi;
            ViewBag.BenhNhi = p.BenhNhi;
            ViewBag.ngay = p.ThoiGianLap.ToString("dd/MM/yyyy");
            ViewBag.KetLuan = p.KetLuan;
            return View(lst);
        }


        public ActionResult PrintPkb(int id)
        {
            return new ActionAsPdf("InPkb", new { id = id });
        }
        public ActionResult InPkb(int id)
        {
            PhieuKhamBenh p = new PhieuKhamBenhDAO().FindByID(id);
            ViewBag.ngay = p.ThoiGianLap.ToString("dd/MM/yyyy");
            return View(p);
        }

        public ActionResult History(int id)
        {
            ViewBag.ten = new BenhNhiDAO().FindByID(id).HoTen;
            return View(new PhieuKhamBenhDAO().lichSuKham(id, 1, 11));
        }
        public ActionResult Detail(int id)
        {
            PhieuKhamBenh p = new PhieuKhamBenhDAO().FindByID(id);
            ViewBag.kqxn = new PhieuDkXnDAO().ListKqXn(p.MaPhieuKB);
            ViewBag.donThuoc = new ThuocDAO().lstThuocByMaPk(p.MaPhieuKB);
            return View(p);
        }

        public ActionResult SlPhieuKb()
        {
            BacSi bs = (BacSi)Session["user"];
            return PartialView(new PhieuKhamBenhDAO().SlPhieuKbOfBs(bs.MaChiNhanh, bs.MaBS));
        }
    }
}