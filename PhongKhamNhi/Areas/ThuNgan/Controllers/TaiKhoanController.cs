using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.Entities;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.ThuNgan.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: ThuNgan/TaiKhoan
        public ActionResult Index()
        {
            NhanVien nv = (NhanVien)Session["user"];
            ViewBag.tenDn = new TaiKhoanDAO().FindByID(nv.IdTaiKhoan.Value).TenDangNhap;
            return View(nv);
        }
        [HttpPost]
        public ActionResult Index(NhanVien tmp, string tenDn, string password, string newpassword)
        {
            NhanVien nv = (NhanVien)Session["user"];
            //nv.GioiTinh = gt;
            //nv.HoTen = tmp.HoTen;
            //nv.NgaySinh = ns;
            nv.Sdt = tmp.Sdt;
            TaiKhoanDAO dao = new TaiKhoanDAO();
            TaiKhoan tk = dao.FindByID(nv.IdTaiKhoan.Value);
            if (dao.Login(tk.TenDangNhap, password) == null)
            {
                ModelState.AddModelError("", "Mật khẩu hiện tại không chính xác!");
                ViewBag.tenDn = tenDn;
                return View(nv);
            }
            new NhanVienDAO().Update(nv);
            tk.TenDangNhap = tenDn;
            if (newpassword != "")
                tk.MatKhau = newpassword;
            dao.Update(tk);
            Session["user"] = nv;
            Session["hoTen"] = nv.HoTen;
            return RedirectToAction("Index", "TaiKhoan");
        }
    }
}