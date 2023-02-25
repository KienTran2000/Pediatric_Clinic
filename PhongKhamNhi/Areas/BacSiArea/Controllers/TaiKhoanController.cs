using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.BacSiArea.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: BacSiArea/TaiKhoan
        public ActionResult Index()
        {
            BacSi nv = (BacSi)Session["user"];
            ViewBag.tenDn = new TaiKhoanDAO().FindByID(nv.IdTaiKhoan.Value).TenDangNhap;
            return View(nv);
        }
        [HttpPost]
        public ActionResult Index(NhanVien tmp, string tenDn, string password, string newpassword)
        {
            BacSi nv = (BacSi)Session["user"];
            nv.Sdt = tmp.Sdt;
            TaiKhoanDAO dao = new TaiKhoanDAO();
            TaiKhoan tk = dao.FindByID(nv.IdTaiKhoan.Value);
            if (dao.Login(tk.TenDangNhap, password) == null)
            {
                ModelState.AddModelError("", "Mật khẩu hiện tại không chính xác!");
                ViewBag.tenDn = tenDn;
                return View(nv);
            }
            new BacSiDAO().Update(nv);
            tk.TenDangNhap = tenDn;
            if (newpassword != "")
                tk.MatKhau = newpassword;
            dao.Update(tk);
            Session["user"] = nv;
            Session["hoTen"] = nv.HoTen;
            return RedirectToAction("Index", "StaffHome");
        }
    }
}