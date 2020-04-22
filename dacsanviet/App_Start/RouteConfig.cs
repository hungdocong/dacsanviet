using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace dacsanviet
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Xác nhận thay đổi mật khẩu
            routes.MapRoute(
                name: "confirm change pass",
                url: "confirm-change-pass",
                defaults: new { controller = "Login", action = "Confirm_ForgetPass", id = UrlParameter.Optional }
            );

            //Đăng xuất
            routes.MapRoute(
                name: "sign out",
                url: "logout",
                defaults: new { controller = "Login", action = "Logout", id = UrlParameter.Optional }
            );

            //xác nhận email đăng ký thành công
            routes.MapRoute(
                name: "Email register success",
                url: "activation-acount-and-verify-your-e-mail",
                defaults: new { controller = "Login", action = "Register_Success", id = UrlParameter.Optional }
            );

            //gửi email xác nhận đăng ký
            routes.MapRoute(
                name: "Email confirm",
                url: "xac-nhan-e-mail",
                defaults: new { controller = "Login", action = "Register_Confirm", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
