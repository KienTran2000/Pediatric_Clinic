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
            ViewBag.ThongKe = dao.lstPhieuKbThongKe(bs.MaChiNhanh, ten, int.Parse(dv), bs.MaBS, int.Parse(trangThai), tu, den);
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
        public ActionResult Edit(PhieuKhamBenh p, string tt)
        {
            if (ModelState.IsValid)
            {
                if (tt != null)
                    p.TrangThai = 2;
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
            h.TrangThai = true;
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

        
        public JsonResult AddThuoc(int id)
        {
            List<CtHdThuocDTO> lst = (List<CtHdThuocDTO>)Session["cart"];
            if (lst == null)
                lst = new List<CtHdThuocDTO>();
            Thuoc item = new ThuocDAO().FindByID(id);
            CtHdThuocDTO c = new CtHdThuocDTO();
            c.MaThuoc = item.MaThuoc;
            c.TenThuoc = item.TenThuoc;
            c.DonViTinh = item.DonViTinh;
            c.DonGia = item.DonGia;
            c.SoLuong = 1;
            lst.Add(c);
            double t = 0;
            foreach (CtHdThuocDTO i in lst)
            {
                t += i.DonGia * i.SoLuong;
            }
            Session["cart"] = lst;
            return Json(new
            {
                data = new ResultSum
                {
                    id = id,
                    value = c.DonGia,
                    totalMoney = t
                },
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteThuoc(int id)
        {
            List<CtHdThuocDTO> lst = (List<CtHdThuocDTO>)Session["cart"];
            foreach (CtHdThuocDTO i in lst)
            {
                if (i.MaThuoc == id)
                {
                    lst.Remove(i);
                    break;
                }
            }
            double t = 0;
            foreach (CtHdThuocDTO i in lst)
            {
                t += i.DonGia * i.SoLuong;
            }
            Session["cart"] = lst;
            return Json(new
            {
                data = new ResultSum
                {
                    totalMoney = t
                },
                status = true
            }, JsonRequestBehavior.AllowGet); ;
        }
        public JsonResult UpdateThuoc(int id, int quantity)
        {
            List<CtHdThuocDTO> lst = (List<CtHdThuocDTO>)Session["cart"];
            double s = 0;
            foreach (CtHdThuocDTO i in lst)
            {
                if (i.MaThuoc == id)
                {
                    i.SoLuong = quantity;
                    s = i.SoLuong * i.DonGia;
                    break;
                }
            }
            double t = 0;
            foreach (CtHdThuocDTO i in lst)
            {
                t += i.DonGia * i.SoLuong;
            }
            Session["cart"] = lst;
            return Json(new
            {
                data = new ResultSum
                {
                    id = id,
                    value = s,
                    totalMoney = t
                },
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
    public class ResultSum
    {
        public int id { get; set; }
        public double value { get; set; }
        public double totalMoney { get; set; }
    }
}