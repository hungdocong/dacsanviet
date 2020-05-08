using dacsanviet.Models.Business;
using dacsanviet.Models.DTO;
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
            var seen = new List<ProductDTO>();
            seen.Add(pro.getDetail(product_ID));
            
            //Lưu sp đã xem
            Session["productSeen"] = seen;
            
            //chi tiết sp
            ViewBag.getDetailProduct = pro.getDetail(product_ID);

            //lấy loại sp theo sp
            var cate = pro.GetProductCategoryByProducID(product_ID);
            ViewBag.getCategory = cate;

            //Lấy sp cùng loại
            ViewBag.getProductSameCategory = pro.GetProductSameCategory(cate.productCategory_ID);

            //Tăng lượt view sp
            pro.increaseViewProduct(product_ID);
            return View();
        }
    }
}