using System.Web.Mvc;

namespace PhongKhamNhi.Areas.NvBt
{
    public class NvBtAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NvBt";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "NvBt_default",
                "NvBt/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}