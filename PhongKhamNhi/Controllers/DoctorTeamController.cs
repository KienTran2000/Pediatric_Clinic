using PhongKhamNhi.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhongKhamNhi.Controllers
{
    public class DoctorTeamController : Controller
    {
        // GET: DoctorTeam
        public ActionResult Index(int pageNum = 1, int pageSize = 12)
        {
            return View(new BacSiDAO().GetListBs(pageNum, pageSize));
        }

        public ActionResult Detail(int id)
        {
            BacSiDAO dao = new BacSiDAO();
            ViewBag.lstBs = dao.List5Bs();
            return View(dao.FindByID(id));
        }
    }
}