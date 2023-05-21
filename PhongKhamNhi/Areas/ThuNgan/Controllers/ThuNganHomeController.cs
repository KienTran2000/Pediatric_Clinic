using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.Entities;
using Rotativa;
using System;
using System.Web.Mvc;

namespace PhongKhamNhi.Areas.ThuNgan.Controllers
{
    public class ThuNganHomeController : Controller
    {
        // GET: ThuNgan/ThuNganHome
        public ActionResult Index(string ten, string dv, string bs, string trangThai, int pageNum = 1, int pageSize = 9)
        {
            if (dv == null)
                dv = "0";
            if (bs == null)
                bs = "0";
            if (trangThai == null)
                trangThai = "3";
            ViewBag.ten = ten;
            ViewBag.dv = int.Parse(dv);
            ViewBag.bs = int.Parse(bs);
            ViewBag.trangThai = int.Parse(trangThai);
            PhieuKhamBenhDAO dao = new PhieuKhamBenhDAO();
            NhanVien nv = (NhanVien)Session["user"];
            ViewBag.ListBacSi = new BacSiDAO().GetListBacSiByMaCn(nv.MaChiNhanh);
            ViewBag.ListDichVu = new DichVuDAO().GetListDv();
            return View(dao.lstPhieuKb(nv.MaChiNhanh, ten, int.Parse(dv), int.Parse(bs), int.Parse(trangThai), pageNum, pageSize));
        }
        public JsonResult ChangeStatus(int id)
        {
            PhieuKhamBenhDAO dao = new PhieuKhamBenhDAO();
            PhieuKhamBenh p = dao.FindByID(id);
            NhanVien nv = (NhanVien)Session["user"];
            p.MaNvLap = nv.MaNV;
            DoanhThuDAO daodt = new DoanhThuDAO();
            DoanhThu d = daodt.Find(p.ThoiGianLap, p.MaChiNhanh, p.Type ? 2 : 1);
            if (p.TrangThai == 0)
            {
                p.TrangThai = 1;
                if (d != null)
                {
                    d.ThuDichVuKham += p.DonGia;
                    d.TongTien += p.DonGia;
                    daodt.Update(d);
                }
                else
                {
                    d = new DoanhThu();
                    d.NgayThangNam = p.ThoiGianLap;
                    d.MaChiNhanh = p.MaChiNhanh;
                    d.ThuDichVuKham = p.DonGia;
                    d.ThuXetNghiem = 0;
                    d.ThuBanThuoc = 0;
                    d.TongTien = p.DonGia;
                    d.Loai = p.Type ? 2 : 1;
                    daodt.Insert(d);
                }
                ThongBao tb = new ThongBao();
                tb.MaBN = p.MaBN;
                tb.ThoiGianTao = DateTime.Now;
                tb.TrangThai = false;
                tb.NoiDung = "Bạn đã thanh toán " + p.DonGia.ToString("N0") + " VNĐ cho phiếu khám bệnh với dịch vụ: " + p.DichVuKham.TenDV;
                tb.Loai = 3;
                new ThongBaoDAO().Insert(tb);
            }    
            else if (p.TrangThai == 1)
            {
                p.TrangThai = 0;
                if (d != null)
                {
                    d.ThuDichVuKham -= p.DonGia;
                    d.TongTien -= p.DonGia;
                    daodt.Update(d);
                }
            }    
                
            dao.UpdateThuNgan(p);
            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            PhieuKhamBenhDAO dao = new PhieuKhamBenhDAO();
            PhieuKhamBenh p = dao.FindByID(id);
            ViewBag.BenhNhi = new BenhNhiDAO().FindByID(p.MaBN);
            return View(p);
        }

        [HttpPost]
        public ActionResult Edit(PhieuKhamBenh pk)
        {
            PhieuKhamBenhDAO dao = new PhieuKhamBenhDAO();
            PhieuKhamBenh p = dao.FindByID(pk.MaPhieuKB);
            NhanVien nv = (NhanVien)Session["user"];
            p.MaNvLap = nv.MaNV;
            DoanhThuDAO daodt = new DoanhThuDAO();
            DoanhThu d = daodt.Find(DateTime.Now, p.MaChiNhanh, p.Type ? 2 : 1);
            if (p.TrangThai == 0)
            {
                p.TrangThai = 1;
                if (d != null)
                {
                    d.ThuDichVuKham += p.DonGia;
                    d.TongTien += p.DonGia;
                    daodt.Update(d);
                }
                else
                {
                    d = new DoanhThu();
                    d.NgayThangNam = DateTime.Now;
                    d.MaChiNhanh = p.MaChiNhanh;
                    d.ThuDichVuKham = p.DonGia;
                    d.ThuXetNghiem = 0;
                    d.ThuBanThuoc = 0;
                    d.TongTien = p.DonGia;
                    if (pk.Type)
                        d.Loai = 2;
                    else
                        d.Loai = 1;
                    daodt.Insert(d);
                }
                ThongBao tb = new ThongBao();
                tb.MaBN = p.MaBN;
                tb.ThoiGianTao = DateTime.Now;
                tb.TrangThai = false;
                tb.NoiDung = "Bạn đã thanh toán " + p.DonGia.ToString("N0") + " cho phiếu khám bệnh với dịch vụ: " + p.DichVuKham.TenDV;
                tb.Loai = 3;
                new ThongBaoDAO().Insert(tb);
            }
            else if (p.TrangThai == 1)
            {
                p.TrangThai = 0;
                if (d != null)
                {
                    d.ThuDichVuKham -= p.DonGia;
                    d.TongTien -= p.DonGia;
                    daodt.Update(d);
                }
            }

            dao.UpdateThuNgan(p);
            return RedirectToAction("Index", "ThuNganHome");
        }

        public ActionResult Print(int id)
        {
            return new ActionAsPdf("In", new { id = id });
        }
        public ActionResult In(int id)
        {
            PhieuKhamBenh p = new PhieuKhamBenhDAO().FindByID(id);
            ViewBag.ngay = p.ThoiGianLap.ToString("dd/MM/yyyy");
            return View(p);
        }

        public ActionResult SlPhieuKb()
        {
            NhanVien nv = (NhanVien)Session["user"];
            return PartialView(new PhieuKhamBenhDAO().SlPhieuKb(nv.MaChiNhanh));
        }
        public ActionResult SlPhieuDkXn()
        {
            NhanVien nv = (NhanVien)Session["user"];
            return PartialView(new PhieuKhamBenhDAO().SlPhieuDkXn(nv.MaChiNhanh));
        }
        public ActionResult SlHoaDon()
        {
            NhanVien nv = (NhanVien)Session["user"];
            return PartialView(new PhieuKhamBenhDAO().SlHoaDon(nv.MaChiNhanh));
        }
    }
}