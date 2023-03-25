using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.Entities;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.Admin.Controllers
{
    public class AdminDoctorController : Controller
    {
        // GET: Admin/AdminDoctor
        public ActionResult Index(string ma, string ten, string gt, string cn, int pageNum = 1, int pageSize = 9)
        {
            ViewBag.ma = ma;
            ViewBag.ten = ten;
            ViewBag.gt = gt;
            if (ma == null || ma == "")
                ma = "0";
            if (cn == null || cn == "")
                cn = "0";
            ViewBag.cn = cn;
            ViewBag.chiNhanh = new ChiNhanhDAO().ListChiNhanh();
            return View(new BacSiDAO().GetListBs(int.Parse(ma), ten, gt, int.Parse(cn), pageNum, pageSize));
        }
        [HttpPost]
        public ActionResult Index(FormCollection data, int pageNum = 1, int pageSize = 9)
        {
            BacSiDAO dao = new BacSiDAO();
            if (data.Count > 0)
            {
                string[] ids = data["checkBoxId"].Split(new char[] { ',' });
                foreach (string id in ids)
                {
                    dao.Delete(int.Parse(id));
                }
            }
            return View(dao.GetListBs(0, "", "", 0, pageNum, pageSize));
        }

        public ActionResult Create()
        {
            ViewBag.chiNhanh = new ChiNhanhDAO().ListChiNhanh();
            return View();
        }
        [HttpPost]
        public ActionResult Create(BacSi bs, string gt, HttpPostedFileBase photo, string username, string password, string cn)
        {
            if (ModelState.IsValid)
            {
                TaiKhoanDAO dao = new TaiKhoanDAO();
                if (dao.Login(username, password) != null)
                {
                    ModelState.AddModelError("", "Tên đăng nhập này đã được dùng. Vui lòng chọn một tên đăng nhập khác!");
                    ViewBag.username = username;
                    return View(bs);
                }
                else
                {
                    TaiKhoan tk = new TaiKhoan();
                    tk.MaQuyen = 1;
                    tk.MatKhau = password;
                    tk.TenDangNhap = username;
                    tk.TrangThai = true;
                    bs.IdTaiKhoan = dao.Insert(tk);
                    if (photo != null && photo.ContentLength > 0)
                    {
                        var path = Path.Combine(Server.MapPath("~/Content/assets/img/doctors/"), System.IO.Path.GetFileName(photo.FileName));
                        photo.SaveAs(path);
                        bs.Anh = photo.FileName;
                    }
                    bs.GioiTinh = gt;
                    bs.MaChiNhanh = int.Parse(cn);
                    new BacSiDAO().Insert(bs);
                    return RedirectToAction("Index", "AdminDoctor");
                }
            }
            return View(bs);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.chiNhanh = new ChiNhanhDAO().ListChiNhanh();
            BacSi bs = new BacSiDAO().FindByID(id);
            ViewBag.tenDn = new TaiKhoanDAO().FindByID(bs.IdTaiKhoan.Value).TenDangNhap;
            return View(bs);
        }
        [HttpPost]
        public ActionResult Edit(BacSi bs, DateTime ns, DateTime nvl, string gt, HttpPostedFileBase photo, string cn)
        {
            if (ModelState.IsValid)
            {
                if (photo != null && photo.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Content/assets/img/doctors/"), System.IO.Path.GetFileName(photo.FileName));
                    photo.SaveAs(path);
                    bs.Anh = photo.FileName;
                }
                bs.GioiTinh = gt;
                bs.NgaySinh = ns;
                bs.NgayVaoLam = nvl;
                bs.MaChiNhanh = int.Parse(cn);
                new BacSiDAO().UpdateByAdmin(bs);
                return RedirectToAction("Index", "AdminDoctor");
            }
            return View(bs);
        }

        public ActionResult EditPasswordDoctor(int id)
        {
            return View(id);
        }
        [HttpPost]
        public ActionResult EditPasswordDoctor(int id, string password)
        {
            if (ModelState.IsValid)
            {
                BacSi bs = new BacSiDAO().FindByID(id);
                TaiKhoanDAO dao = new TaiKhoanDAO();
                TaiKhoan tk = dao.FindByID(bs.IdTaiKhoan.Value);
                tk.MatKhau = password;
                dao.Update(tk);
                return RedirectToAction("Index", "AdminDoctor");
            }
            return PartialView();
        }

        public ActionResult History(int id, string dv, string tu, string den, int pageNum = 1, int pageSize = 9)
        {
            if (dv == null || dv == "")
                dv = "0";
            ViewBag.id = id;
            ViewBag.dv = int.Parse(dv);
            ViewBag.tu = tu;
            ViewBag.den = den;
            if (tu == null || tu == "")
                tu = "2020-11-19 12:00:00";
            if (den == null || den == "")
                den = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.ten = new BacSiDAO().FindByID(id).HoTen;
            ViewBag.ListDichVu = new DichVuDAO().GetListDv();
            return View(new PhieuKhamBenhDAO().lichSuKhamByMaBs(id, int.Parse(dv), tu, den, pageNum, pageSize));
        }

        public ActionResult Delete(int id)
        {
            new BacSiDAO().Delete(id);
            return RedirectToAction("Index", "AdminDoctor");
        }
    }
}