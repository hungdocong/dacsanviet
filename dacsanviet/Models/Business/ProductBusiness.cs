using dacsanviet.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dacsanviet.Models.Business
{
    public class ProductBusiness
    {
        private DacSanVietEntities db = new DacSanVietEntities();

        //sản phẩm đặc trưng
        public List<Product> featureProduct()
        {
            return db.Products.Take(10).ToList();
        } 

        //sản phẩm gợi ý
        public List<Product> recommendProduct()
        {
            return db.Products.ToList();
        }

        // lấy loại sản phẩm
        public List<ProductCategory> getCategories()
        {
            return db.ProductCategories.Take(4).ToList();
        }

        //lấy sp theo loại sp
        public List<Product> GetProductByCategory(long productCategory_ID)
        {
            return db.Products.Where(x => x.productCategory_ID == productCategory_ID).ToList();
        }

        //sản phẩm mới
        public List<Product> newProduct()
        {
            return db.Products.Take(10).ToList();
        }

        //sản phẩm bán chạy
        public List<Product> topSellProduct()
        {
            return db.Products.Take(10).ToList();
        }
    }
}