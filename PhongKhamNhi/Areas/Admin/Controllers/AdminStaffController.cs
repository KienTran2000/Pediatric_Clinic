using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.Entities;
using System;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.Admin.Controllers
{
    public class AdminStaffController : Controller
    {
        // GET: Admin/AdminStaff
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
            return View(new NhanVienDAO().GetListNv(int.Parse(ma), ten, gt, int.Parse(cn), pageNum, pageSize));
        }
        [HttpPost]
        public ActionResult Index(FormCollection data, int pageNum = 1, int pageSize = 9)
        {
            NhanVienDAO dao = new NhanVienDAO();
            if (data.Count > 0)
            {
                string[] ids = data["checkBoxId"].Split(new char[] { ',' });
                foreach (string id in ids)
                {
                    dao.Delete(int.Parse(id));
                }
            }
            return View(dao.GetListNv(0, "", "", 0, pageNum, pageSize));
        }

        public ActionResult Create()
        {
            ViewBag.chiNhanh = new ChiNhanhDAO().ListChiNhanh();
            return View();
        }
        [HttpPost]
        public ActionResult Create(NhanVien nv, string chucVu, string gt, string username, string password, string cn)
        {
            if (ModelState.IsValid)
            {
                TaiKhoanDAO dao = new TaiKhoanDAO();
                if (dao.Login(username, password) != null)
                {
                    ModelState.AddModelError("", "Tên đăng nhập này đã được dùng. Vui lòng chọn một tên đăng nhập khác!");
                    ViewBag.username = username;
                    return View(nv);
                }
                else
                {
                    TaiKhoan tk = new TaiKhoan();
                    if (chucVu == "1")
                    {
                        tk.MaQuyen = 2;
                        nv.ChucVu = "Lễ tân";
                    }
                    else if (chucVu == "2")
                    {
                        tk.MaQuyen = 3;
                        nv.ChucVu = "Thu ngân";
                    }
                    else if (chucVu == "3")
                    {
                        tk.MaQuyen = 4;
                        nv.ChucVu = "KTV xét nghiệm";
                    }
                    else if (chucVu == "4")
                    {
                        tk.MaQuyen = 5;
                        nv.ChucVu = "Nhân viên thuốc";
                    }
                    tk.MatKhau = password;
                    tk.TenDangNhap = username;
                    tk.TrangThai = true;
                    nv.IdTaiKhoan = dao.Insert(tk);
                    nv.GioiTinh = gt;
                    nv.MaChiNhanh = int.Parse(cn);
                    new NhanVienDAO().Insert(nv);
                    return RedirectToAction("Index", "AdminStaff");
                }
            }
            return View(nv);
        }

        public ActionResult Edit(int id)
        {
            ViewBag.chiNhanh = new ChiNhanhDAO().ListChiNhanh();
            NhanVien nv = new NhanVienDAO().FindByID(id);
            ViewBag.tenDn = new TaiKhoanDAO().FindByID(nv.IdTaiKhoan.Value).TenDangNhap;
            return View(nv);
        }
        [HttpPost]
        public ActionResult Edit(NhanVien nv, DateTime ns, DateTime nvl, string gt, string cn)
        {
            if (ModelState.IsValid)
            {
                nv.GioiTinh = gt;
                nv.NgaySinh = ns;
                nv.NgayVaoLam = nvl;
                nv.MaChiNhanh = int.Parse(cn);
                new NhanVienDAO().Update(nv);
                return RedirectToAction("Index", "AdminStaff");
            }
            return View(nv);
        }

        public ActionResult EditPasswordStaff(int id)
        {
            return View(id);
        }
        [HttpPost]
        public ActionResult EditPasswordStaff(int id, string password)
        {
            if (ModelState.IsValid)
            {
                NhanVien bs = new NhanVienDAO().FindByID(id);
                TaiKhoanDAO dao = new TaiKhoanDAO();
                TaiKhoan tk = dao.FindByID(bs.IdTaiKhoan.Value);
                tk.MatKhau = password;
                dao.Update(tk);
                return RedirectToAction("Index", "AdminStaff");
            }
            return PartialView();
        }

        public ActionResult Delete(int id)
        {
            new NhanVienDAO().Delete(id);
            return RedirectToAction("Index", "AdminStaff");
        }
    }
}