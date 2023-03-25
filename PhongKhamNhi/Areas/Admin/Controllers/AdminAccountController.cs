using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.Admin.Controllers
{
    public class AdminAccountController : Controller
    {
        // GET: Admin/AdminAccount
        public ActionResult Index()
        {
            TaiKhoan tk = (TaiKhoan)Session["user"];
            return View(tk);
        }
        [HttpPost]
        public ActionResult Index(TaiKhoan tmp, string tenDn, string password, string newpassword)
        {
            TaiKhoanDAO dao = new TaiKhoanDAO();
            TaiKhoan tk = dao.FindByID(tmp.IdTaiKhoan);
            if (dao.Login(tk.TenDangNhap, password) == null)
            {
                ModelState.AddModelError("", "Mật khẩu hiện tại không chính xác!");
                ViewBag.tenDn = tenDn;
                return View(tmp);
            }
            tk.TenDangNhap = tenDn;
            if (newpassword != "")
                tk.MatKhau = newpassword;
            dao.Update(tk);
            Session["user"] = tk;
            Session["username"] = tk.TenDangNhap;
            return RedirectToAction("Index", "AdminAccount");
        }
    }
}