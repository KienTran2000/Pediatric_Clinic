using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index(int pageNum = 1, int pageSize = 12)
        {
            return View(new BaiVietDAO().lstBaiViet("", "2020-11-19 12:00:00", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), pageNum, pageSize));
        }
        public ActionResult Detail(int id)
        {
            ViewBag.maBN = 0;
            BenhNhi bn = (BenhNhi)Session["patient"];
            if(bn != null)
                ViewBag.maBN = bn.MaBN;
            ViewBag.DsBinhLuan = new BinhLuanDAO().lstBinhLuan(id);
            return View(new BaiVietDAO().FindByID(id));
        }

        public JsonResult NewsComment(Comment c)
        {
            BenhNhi bn = (BenhNhi)Session["patient"];
            BinhLuan bl = new BinhLuan();
            bl.MaBaiViet = c.MaBaiViet;
            bl.NoiDung = c.NoiDung;
            bl.ThoiGianTao = DateTime.Now;
            bl.MaBN = bn.MaBN;
            bl.MaBinhLuan = new BinhLuanDAO().Insert(bl);
            if (bl.MaBinhLuan > 0)
            {
                CommentRespone cr = new CommentRespone();
                cr.MaBinhLuan = bl.MaBinhLuan;
                cr.NoiDung = bl.NoiDung;
                cr.ThoiGianTao = bl.ThoiGianTao.Value;
                cr.TenThanNhan = bn.TenThanNhan;
                cr.MaBN = bn.MaBN;
                cr.Anh = bn.AnhDaiDien;
                return Json(new
                {
                    data = cr,
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                data = "",
                status = false
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReplyComment(Reply c)
        {
            BinhLuan comment = new BinhLuanDAO().FindByID(c.MaBinhLuan);
            BenhNhi bn = (BenhNhi)Session["patient"];
            BinhLuanPhanHoi bl = new BinhLuanPhanHoi();
            bl.MaBinhLuan = c.MaBinhLuan;
            bl.NoiDung = c.NoiDung;
            bl.ThoiGianTao = DateTime.Now;
            bl.MaBN = bn.MaBN;
            bl.MaPhanHoi = new BinhLuanPhanHoiDAO().Insert(bl);
            if (bl.MaPhanHoi > 0)
            {
                if(bn.MaBN != comment.MaBN)
                {
                    ThongBao tb = new ThongBao();
                    tb.MaBaiViet = comment.MaBaiViet;
                    tb.MaBN = comment.MaBN;
                    tb.ThoiGianTao = DateTime.Now;
                    tb.TrangThai = false;
                    tb.NoiDung = bn.TenThanNhan + " đã phản hồi bình luận của bạn trong bài viết '" + comment.BaiViet.TieuDe + "'";
                    tb.Loai = 1;
                    new ThongBaoDAO().Insert(tb);
                }    
                
                ReplyRespone cr = new ReplyRespone();
                cr.MaPhanHoi = bl.MaPhanHoi;
                cr.NoiDung = bl.NoiDung;
                cr.ThoiGianTao = bl.ThoiGianTao.Value;
                cr.TenThanNhan = bn.TenThanNhan;
                cr.MaBN = bn.MaBN;
                cr.Anh = bn.AnhDaiDien;
                return Json(new
                {
                    data = cr,
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                data = "",
                status = false
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteComment(int id)
        {
            new BinhLuanPhanHoiDAO().DeleteByMaBinhLuan(id);
            int res = new BinhLuanDAO().Delete(id);
            if (res > -1)
            {
                return Json(new
                {
                    data = "",
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                data = "",
                status = false
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteReply(int id)
        {
            int res = new BinhLuanPhanHoiDAO().Delete(id);
            if (res > -1)
            {
                return Json(new
                {
                    data = "",
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                data = "",
                status = false
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SlThongBao()
        {
            BenhNhi bn = (BenhNhi)Session["patient"];
            return PartialView(new ThongBaoDAO().SlThongBao(bn.MaBN));
        }
    }

    public class Comment
    {
        public int MaBaiViet { get; set; }
        public string NoiDung { get; set; }
    }

    public class CommentRespone
    {
        public int MaBinhLuan { get; set; }
        public int MaBN { get; set; }
        public string TenThanNhan { get; set; }
        public string NoiDung { get; set; }
        public DateTime ThoiGianTao { get; set; }
        public string Anh { get; set; }
    }

    public class Reply
    {
        public int MaBinhLuan { get; set; }
        public string NoiDung { get; set; }
    }

    public class ReplyRespone
    {
        public int MaPhanHoi { get; set; }
        public int MaBN { get; set; }
        public string TenThanNhan { get; set; }
        public string NoiDung { get; set; }
        public DateTime ThoiGianTao { get; set; }
        public string Anh { get; set; }
    }
}