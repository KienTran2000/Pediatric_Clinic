using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.DTO;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Controllers
{
    public class PatientController : Controller
    {
        // GET: BenhNhi
        public ActionResult Index()
        {
            return View((BenhNhi)Session["patient"]);
        }
        [HttpPost]
        public ActionResult Index(BenhNhi model, string gt, DateTime ns)
        {
            model.GioiTinh = gt;
            model.NgaySinh = ns;
            BenhNhi bn = (BenhNhi)Session["patient"];
            model.MatKhau = bn.MatKhau;
            model.GhiChu = bn.GhiChu;
            new BenhNhiDAO().Update(model);
            Session["patient"] = model;
            return RedirectToAction("Index", "Patient");
        }

        public ActionResult Registrie()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registrie(BenhNhi model, string GioiTinh)
        {
            if (ModelState.IsValid)
            {
                model.GioiTinh = GioiTinh;
                new BenhNhiDAO().Insert(model);
                return RedirectToAction("Login", "Patient");
            }
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(BenhNhi model)
        {
            BenhNhiDAO dao = new BenhNhiDAO();
            BenhNhi bn = dao.Login(model.TenDangNhap, model.MatKhau);
            if (bn != null)
            {
                Session["patient"] = bn;
                Session["fullname"] = bn.HoTen;
                return RedirectToAction("Index", "Patient");
            }
            ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng!");
            return View(model);
        }

        public ActionResult Appointment()
        {
            List<ChiNhanh> lst = new ChiNhanhDAO().ListChiNhanh();
            ViewBag.ListChiNhanh = lst;
            ViewBag.ListBacSi = new BacSiDAO().GetListBacSiByMaCn2(lst[0].MaChiNhanh, 3);
            BenhNhi bn = (BenhNhi)Session["patient"];
            ViewBag.tgmin = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            //ViewBag.ListBacSi = new BacSiDAO().GetListBacSi();
            return View(bn);
        }
        [HttpPost]
        public ActionResult Appointment(int MaBN, DateTime ThoiGianHen, string gioHen, string bacSi, string loiNhan, int chiNhanh, int type)
        {
            PhieuDangKyKham p = new PhieuDangKyKham();
            p.MaChiNhanh = chiNhanh;
            p.LoiNhan = loiNhan;
            p.MaBN = MaBN;
            p.MaBS = int.Parse(bacSi);
            p.ThoiGianDKK = DateTime.Now;
            if (type == 1)
                p.Type = true;
            else
                p.Type = false;
            string[] str = gioHen.Split('h');
            p.ThoiGianHen = new DateTime(ThoiGianHen.Year, ThoiGianHen.Month, ThoiGianHen.Day, int.Parse(str[0]), int.Parse(str[1]), 0);
            p.TrangThai = false;
            new PhieuDangKyKhamDAO().Insert(p);
            return RedirectToAction("AppointmentHistory", "Patient");
        }
        public JsonResult GetBacSiByCN(int maCn, int type)
        {
            List<BacSiDTO> lst = new BacSiDAO().GetListBacSiByMaCn2(maCn, type);
            return Json(new
            {
                data = lst,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckTime(int bacSi, int chiNhanh, DateTime time, int type)
        {
            AppointmentTime at = new AppointmentTime(type);
            if (bacSi != 0)
            {
                List<PhieuDangKyKham> lst = new PhieuDangKyKhamDAO().FindDkkByMaBs2(bacSi, chiNhanh, time);
                foreach (PhieuDangKyKham item in lst)// 21/2/2022 8:00:00 AM
                {
                    string s = item.ThoiGianHen.Hour + "h" + item.ThoiGianHen.Minute;
                    if (at.lst.Contains(s))
                        at.lst.Remove(s);
                }
            }
            else
                at.lst.Clear();

            return Json(new
            {
                data = at.lst,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckAppointment()
        {
            bool res = false;
            BenhNhi bn = (BenhNhi)Session["patient"];
            if (new PhieuDangKyKhamDAO().SlPhieuDk(bn.MaBN) > 0)
                res = true;
            return Json(new
            {
                data = res,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AppointmentHistory(string tu, string den, int pageNum = 1, int pageSize = 6)
        {
            ViewBag.tu = tu;
            ViewBag.den = den;
            if (tu == null || tu == "")
                tu = "2020-11-19 12:00:00";
            if (den == null || den == "")
                den = "2030-11-19 12:00:00";
            BenhNhi bn = (BenhNhi)Session["patient"];
            ViewBag.HoTen = new BenhNhiDAO().FindByID(bn.MaBN).HoTen;
            return View(new PhieuDangKyKhamDAO().lstLichHenByBn(bn.MaBN, tu, den, pageNum, pageSize));
        }
        public ActionResult AppointmentDetail(int id)
        {
            PhieuDangKyKham p = new PhieuDangKyKhamDAO().FindByID(id);
            ViewBag.tgHen = p.ThoiGianHen.ToString("yyyy-MM-dd");
            ViewBag.tgmin = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            ViewBag.ListBacSi = new BacSiDAO().GetListBacSi();
            int type = 0;
            if (p.Type)
                type = 1;
            AppointmentTime at = new AppointmentTime(type);
            List<PhieuDangKyKham> lst = new PhieuDangKyKhamDAO().FindDkkByMaBs2(p.MaBS, p.MaChiNhanh, p.ThoiGianHen);
            foreach (PhieuDangKyKham item in lst)// 21/2/2022 8:00:00 AM
            {
                if (item.MaPhieuDKK != p.MaPhieuDKK)
                {
                    string s = item.ThoiGianHen.Hour + "h" + item.ThoiGianHen.Minute;
                    if (at.lst.Contains(s))
                        at.lst.Remove(s);
                }
            }
            ViewBag.ListGioHen = at.lst;
            ViewBag.gioHen = p.ThoiGianHen.Hour + "h" + p.ThoiGianHen.Minute;
            return View(p);
        }
        [HttpPost]
        public ActionResult AppointmentDetail(int MaPhieuDKK, DateTime ThoiGianHen, string gioHen, string bacSi, string loiNhan)
        {
            PhieuDangKyKhamDAO dao = new PhieuDangKyKhamDAO();
            PhieuDangKyKham p = dao.FindByID(MaPhieuDKK);
            p.LoiNhan = loiNhan;
            p.MaBS = int.Parse(bacSi);
            p.ThoiGianDKK = DateTime.Now;
            string[] str = gioHen.Split('h');
            p.ThoiGianHen = new DateTime(ThoiGianHen.Year, ThoiGianHen.Month, ThoiGianHen.Day, int.Parse(str[0]), int.Parse(str[1]), 0);
            new PhieuDangKyKhamDAO().Update(p);
            return RedirectToAction("AppointmentHistory", "Patient");
        }

        public ActionResult DeleteAppointment(int id)
        {
            new PhieuDangKyKhamDAO().Delete(id);
            return RedirectToAction("AppointmentHistory", "Patient");
        }

        public ActionResult History(string tu, string den, int pageNum = 1, int pageSize = 6)
        {
            ViewBag.tu = tu;
            ViewBag.den = den;
            if (tu == null || tu == "")
                tu = "2020-11-19 12:00:00";
            if (den == null || den == "")
                den = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            BenhNhi bn = (BenhNhi)Session["patient"];
            ViewBag.HoTen = new BenhNhiDAO().FindByID(bn.MaBN).HoTen;
            return View(new PhieuKhamBenhDAO().lstPhieuKbByBn(bn.MaBN, tu, den, pageNum, pageSize));
        }
        public ActionResult HistoryDetail(int id)
        {
            BenhNhi bn = (BenhNhi)Session["patient"];
            ViewBag.HoTen = new BenhNhiDAO().FindByID(bn.MaBN).HoTen;
            PhieuKhamBenh p = new PhieuKhamBenhDAO().FindByID(id);
            PhieuDkXnDAO dao = new PhieuDkXnDAO();
            PhieuDKXN x = dao.FindByMaPk(id);
            if(x != null)
            ViewBag.PhieuDkXn = dao.ListKqXn(x.MaPhieuDKXN);
            List<ChiTietDonThuocDTO> lst = new ThuocDAO().lstThuocByMaPk(id);
            ViewBag.DonThuoc = lst;
            return View(p);
        }

        public ActionResult EditPassword()
        {
            return View((BenhNhi)Session["patient"]);
        }
        [HttpPost]
        public ActionResult EditPassword(string passwordOld, string passwordNew)
        {
            BenhNhi bn = (BenhNhi)Session["patient"];
            BenhNhiDAO dao = new BenhNhiDAO();
            if (dao.CheckPassword(bn.MaBN, passwordOld))
            {
                bn.MatKhau = passwordNew;
                dao.Update(bn);
                return RedirectToAction("Index", "Patient");
            }
            ModelState.AddModelError("", "Mật khẩu hiện tại không đúng!");
            ViewBag.passwordOld = passwordOld;
            ViewBag.passwordNew = passwordNew;
            return View(bn);
        }

        public ActionResult Logout()
        {
            Session["patient"] = null;
            Session["fullname"] = null;
            return RedirectToAction("Login", "Patient");
        }
    }
}