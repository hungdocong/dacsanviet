using dacsanviet.Models.Business;
using dacsanviet.Models.DTO;
using dacsanviet.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dacsanviet.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult AddCart(long product_ID, int quantity)
        {
            var product = new ProductBusiness().findProduct(product_ID);
            var cart = Session[CartSession];
            if (cart != null)//Nếu giỏ đã chứa sản phẩm
            {
                var list = (List<CartDTO>)cart;
                if (list.Exists(x => x.Product.product_ID == product_ID))//nếu giỏ đã chứa sản phẩm có ID = BookID
                {
                    foreach (var item in list)
                    {
                        if (item.Product.product_ID == product_ID)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //Tạo đối tượng mới
                    var item = new CartDTO();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
            }
            else//nếu giỏ hàng trống
            {
                var item = new CartDTO();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartDTO>();
                list.Add(item);

                Session[CartSession] = list;
            }
            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}