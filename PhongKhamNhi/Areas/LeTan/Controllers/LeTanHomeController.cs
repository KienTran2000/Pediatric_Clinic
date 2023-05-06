using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.DTO;
using PhongKhamNhi.Models.Entities;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.LeTan.Controllers
{
    //[AuthorizationLogin]
    public class LeTanHomeController : Controller
    {
        // GET: LeTan/LeTanHome
        public ActionResult Index(string sdt, string tgDk, string tgHen, string trangThai, string type, int pageNum = 1, int pageSize = 9)
        {
            ViewBag.sdt = sdt;
            ViewBag.tgDk = tgDk;
            ViewBag.tgHen = tgHen;
            ViewBag.trangThai = trangThai;
            ViewBag.type = type;
            PhieuDangKyKhamDAO dao = new PhieuDangKyKhamDAO();
            NhanVien nv = (NhanVien)Session["user"];
            return View(dao.lstDk(nv.MaChiNhanh, sdt, tgDk, tgHen, trangThai, type, pageNum, pageSize));
        }
        [HttpPost]
        public ActionResult Index(FormCollection data, int pageNum = 1, int pageSize = 9)
        {
            PhieuDangKyKhamDAO dao = new PhieuDangKyKhamDAO();
            if (data.Count > 0)
            {
                string[] ids = data["checkBoxId"].Split(new char[] { ',' });
                foreach (string id in ids)
                {
                    dao.Delete(int.Parse(id));
                }
            }
            NhanVien nv = (NhanVien)Session["user"];
            return View(dao.lstDk(nv.MaChiNhanh, "", "", "", "", "", pageNum, pageSize));
        }

        public ActionResult Create()
        {
            List<ChiNhanh> lst = new ChiNhanhDAO().ListChiNhanh();
            ViewBag.ListChiNhanh = lst;
            ViewBag.ListBacSi = new BacSiDAO().GetListBacSiByMaCn(lst[0].MaChiNhanh);
            ViewBag.tgHenMin = DateTime.Now.AddDays(1).ToString("yyyy-MM-ddTHH:mm");
            return View();
        }
        [HttpPost]
        public ActionResult Create(PhieuDangKyKham tmp, string chiNhanh, string bs)
        {
            int maCn = int.Parse(chiNhanh);

            tmp.MaChiNhanh = maCn;
            tmp.MaBS = int.Parse(bs);
            NhanVien nv = (NhanVien)Session["user"];
            tmp.MaNV = nv.MaNV;
            tmp.ThoiGianDKK = DateTime.Now;
            tmp.TrangThai = false;
            PhieuDangKyKhamDAO dao = new PhieuDangKyKhamDAO();
            dao.Insert(tmp);
            return RedirectToAction("Index", "LeTanHome");
        }

        public JsonResult CheckTime(int bacSi, int chiNhanh, DateTime time, bool type)
        {
            int t = 0;
            if (type)
                t = 1;
            AppointmentTime at = new AppointmentTime(t);
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

        public ActionResult LoadLichHen(int cn, DateTime tgHen)
        {
            List<BacSi> lst = new BacSiDAO().GetListBacSiByMaCn(cn);
            
            ResultLichHen res = new ResultLichHen();
            List<int> ls = new List<int>();
            res.bs = lst[0].MaBS;
            List<PhieuDangKyKham> lstLh = new PhieuDangKyKhamDAO().FindDkkByMaBs(cn, res.bs);
            foreach (PhieuDangKyKham item in lstLh)
            {
                TimeSpan t = item.ThoiGianHen - tgHen;
                if (Math.Abs(t.TotalMinutes) <= 30)
                    ls.Add(item.MaPhieuDKK);
            }
            res.pdk = ls;
            ViewBag.ListLh = res;
            return View(lst);
        }

        public ActionResult LoadBacSi(int cn)
        {
            List<BacSi> lst = new BacSiDAO().GetListBacSiByMaCn(cn);
            return View(lst);
        }


        public ActionResult Edit(int id)
        {
            PhieuDangKyKham p = new PhieuDangKyKhamDAO().FindByID(id);
            ViewBag.ns = p.NgaySinh.ToString("yyyy-MM-dd");
            ViewBag.tgHen = p.ThoiGianHen.ToString("yyyy-MM-dd");
            ViewBag.tgmin = p.ThoiGianHen.ToString("yyyy-MM-dd");
            if (DateTime.Now > p.ThoiGianHen.AddDays(1))
                ViewBag.tgmin = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            //List<ChiNhanh> lst = new ChiNhanhDAO().ListChiNhanh();
            //ViewBag.ListChiNhanh = lst;
            ViewBag.ListBacSi = new BacSiDAO().GetListBacSiByMaCnAndType(p.MaChiNhanh, p.Type);
            int t = 0;
            if (p.Type)
                t = 1;
            AppointmentTime at = new AppointmentTime(t);
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
        public ActionResult Edit(PhieuDangKyKham p, DateTime ThoiGianHen, string gioHen, string tt, string bs)
        {
            PhieuDangKyKhamDAO dao = new PhieuDangKyKhamDAO();
            PhieuDangKyKham tmp = dao.FindByID(p.MaPhieuDKK);
            string[] str = gioHen.Split('h');
            tmp.ThoiGianHen = new DateTime(ThoiGianHen.Year, ThoiGianHen.Month, ThoiGianHen.Day, int.Parse(str[0]), int.Parse(str[1]), 0);
            if (tt == "0")
                tmp.TrangThai = false;
            else
                tmp.TrangThai = true;
            tmp.MaBS = int.Parse(bs);
            NhanVien nv = (NhanVien)Session["user"];
            tmp.MaNV = nv.MaNV;
            tmp.LoiNhan = p.LoiNhan;
            new PhieuDangKyKhamDAO().Update(tmp);

            //p.NgaySinh = NgaySinh;
            //p.ThoiGianHen = ThoiGianHen;
            //if (tt == "0")
            //    p.TrangThai = false;
            //else
            //    p.TrangThai = true;
            //p.MaChiNhanh = int.Parse(chiNhanh);
            //p.MaBS = int.Parse(bs);
            //NhanVien nv = (NhanVien)Session["user"];
            //p.MaNV = nv.MaNV;
            //new PhieuDangKyKhamDAO().UpdateBySERIALIZABLE(p);
            return RedirectToAction("Index", "LeTanHome");
        }

        public ActionResult Delete(int id)
        {
            new PhieuDangKyKhamDAO().Delete(id);
            return RedirectToAction("Index", "LeTanHome");
        }

        public ActionResult SlPhieuDk()
        {
            NhanVien nv = (NhanVien)Session["user"];
            return PartialView(new PhieuDangKyKhamDAO().SlPhieuDk(nv.MaChiNhanh));
        }

        public ActionResult EditPasswordBN(int id)
        {
            return View(id);
        }
        [HttpPost]
        public ActionResult EditPasswordBN(int id, string password)
        {
            if (ModelState.IsValid)
            {
                BenhNhiDAO dao = new BenhNhiDAO();
                BenhNhi bn = dao.FindByID(id);
                bn.MatKhau = password;
                dao.Update(bn);
                return RedirectToAction("Index", "LeTanHome");
            }
            return PartialView();
        }

        public ActionResult Print(int id)
        {
            return new ActionAsPdf("In", new { id = id });
        }
        public ActionResult In(int id)
        {
            ViewBag.ngay = DateTime.Now.ToString("dd/MM/yyyy");
            return View(new PhieuDangKyKhamDAO().FindByID(id));
        }
    }
    public class ResultLichHen
    {
        public int bs { get; set; }
        public List<int> pdk { get; set; }
    }

    
}