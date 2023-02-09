using System.Web.Mvc;

namespace PhongKhamNhi.Areas.NvXn
{
    public class NvXnAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NvXn";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "NvXn_default",
                "NvXn/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}