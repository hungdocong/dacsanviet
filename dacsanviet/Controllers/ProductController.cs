using dacsanviet.Models.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dacsanviet.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Detail(long product_ID)
        {
            var pro = new ProductBusiness();
            ViewBag.getDetailProduct = pro.getDetail(product_ID);
            ViewBag.getCategory = pro.GetProductCategoryByProducID(product_ID);
            return View();
        }
    }
}