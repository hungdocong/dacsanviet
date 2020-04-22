using dacsanviet.Models.Business;
using dacsanviet.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dacsanviet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var menuModel = new MenuBusiness();

            //Menu
            ViewBag.GetMenu = menuModel.getMenus();//menu mẹ
            ViewBag.GetParentMenu = menuModel.getParentMenu();//menu con

            //Product
            var proModel = new ProductBusiness();
            ViewBag.featureProduct = proModel.featureProduct();//sản phẩm đặc trưng
            ViewBag.getCategory = proModel.getCategories();//lấy loại sản phẩm
            ViewBag.newProduct = proModel.newProduct();//sp mới
            ViewBag.topSellProduct = proModel.topSellProduct(); //sp bán chạy

            return View();
        }

        //load sp theo loại sp
        public JsonResult getProductByCategory(long productCategory_ID)
        {
            var proModel = new ProductBusiness();
            List<Product> products = proModel.GetProductByCategory(productCategory_ID);
            return Json(products.Select(x => new{
                product_ID = x.product_ID,
                productName = x.productName,
                price = x.price,
                promotionPrice = x.promotionPrice,
                productImage = x.productImage,
                productCategory_ID = x.productCategory_ID
            }
            ), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Page404()
        {
            return View();
        }


    }
}