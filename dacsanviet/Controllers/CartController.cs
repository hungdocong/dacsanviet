using dacsanviet.Models.Business;
using dacsanviet.Models.DTO;
using dacsanviet.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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

        //Sửa số lượng sp trong giỏ hàng
        public JsonResult Edit(string cartModel)
        {
            var ed = new JavaScriptSerializer().Deserialize<List<CartDTO>>(cartModel);
            var productSec = (List<CartDTO>)Session[CartSession];

            //if (ed.Exists(x => x.quantity <= 0))
            //{
                //foreach(var item in orSec)
                //{
                //    var err = ed.SingleOrDefault(x => x.food.ID == item.food.ID);
                //    orSec.RemoveAll(x => x.food.ID == err.food.ID);
                //}
            //    SetAlert("Số lượng món ăn không thể bằng 0 hoặc nhỏ hơn 0", "error");
            //    return Json(new
            //    {
            //        status = true
            //    });
            //}
            foreach (var item in productSec)
            {
                var product_id = ed.SingleOrDefault(x => x.Product.product_ID == item.Product.product_ID);
                if (product_id != null)
                {
                    item.Quantity = product_id.Quantity;
                }

            }

            Session[CartSession] = productSec;
            return Json(new
            {
                status = true
            });
        }

        //Xóa giỏ hàng
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        //Xóa sp trong giỏ hàng
        public JsonResult Delete(long id)
        {
            var sec = (List<CartDTO>)Session[CartSession];
            sec.RemoveAll(x => x.Product.product_ID == id);
            Session[CartSession] = sec;
            return Json(new
            {
                status = true
            });
        }
    }
}