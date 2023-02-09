using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(TaiKhoan t)
        {
            TaiKhoanDAO dao = new TaiKhoanDAO();
            TaiKhoan tk = dao.Login(t.TenDangNhap, t.MatKhau);
            if (tk != null)
            {
                if(tk.TrangThai == false)
                {
                    ModelState.AddModelError("", "Tài khoản này đã bị khóa!");
                    return View(t);
                }
                else
                {
                    if (tk.MaQuyen == 0)
                    {
                        Session["user"] = tk;
                        Session["username"] = tk.TenDangNhap;
                        return RedirectToAction("Index", "AdminHome", new { Area = "Admin" });
                    }
                    else if (tk.MaQuyen == 1)
                    {
                        BacSi bs = new BacSiDAO().GetBacSi(tk.IdTaiKhoan);
                        if(bs != null)
                        {
                            Session["user"] = bs;
                            Session["hoTen"] = bs.HoTen;
                            return RedirectToAction("Index", "BacSiHome", new { Area = "BacSiArea" });
                        }
                        else
                        {
                            ModelState.AddModelError("", "Tài khoản này đã bị khóa!");
                            return View(t);
                        }
                    }
                    else
                    {
                        NhanVien nv = new NhanVienDAO().getNhanVien(tk.IdTaiKhoan);
                        if (nv != null)
                        {
                            Session["user"] = nv;
                            Session["hoTen"] = nv.HoTen;
                            switch (tk.MaQuyen)
                            {
                                case 2:
                                    return RedirectToAction("Index", "LeTanHome", new { Area = "LeTan" });
                                    break;
                                case 3:
                                    return RedirectToAction("Index", "ThuNganHome", new { Area = "ThuNgan" });
                                    break;
                                case 4:
                                    return RedirectToAction("Index", "NvXnHome", new { Area = "NvXn" });
                                    break;
                                case 5:
                                    return RedirectToAction("Index", "NvBtHome", new { Area = "NvBt" });
                                    break;
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Tài khoản này đã bị khóa!");
                            return View(t);
                        }
                    }
                }
                
            }
            ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng!");
            return View(t);
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            Session["hoTen"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}