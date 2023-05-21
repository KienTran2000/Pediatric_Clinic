using PhongKhamNhi.Models.DAO;
using PhongKhamNhi.Models.Entities;
using System.Web.Mvc;

namespace PhongKhamNhi.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification
        public ActionResult Index(int pageNum = 1, int pageSize = 12)
        {
            BenhNhi bn = (BenhNhi)Session["patient"];
            return View(new ThongBaoDAO().ListThongBao(bn.MaBN, pageNum, pageSize));
        }

        public ActionResult Detail(int id)
        {
            ThongBaoDAO dao = new ThongBaoDAO();
            ThongBao tb = dao.FindByID(id);
            if (!tb.TrangThai)
            {
                tb.TrangThai = true;
                dao.Update(tb);
            }
            if(tb.Loai == 2)
                return Redirect("/Patient/AppointmentHistory");
            if (tb.Loai == 3)
                return Redirect("/Patient/History");
            return Redirect("/News/Detail/" + tb.MaBaiViet);
        }
    }
}