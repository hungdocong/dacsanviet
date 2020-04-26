using dacsanviet.Models.DTO;
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

        // lấy loại sản phẩm, lấy 3 dữ liệu đầu
        public List<ProductCategory> getCategories()
        {
            return db.ProductCategories.Take(3).ToList();
        }

        //Lấy loại sp, 3 cái cuối
        public List<ProductCategory> getLastCategory()
        {
            var data = db.ProductCategories.ToList();
            List<ProductCategory> lst = new List<ProductCategory>();
            int dem = 0, n = data.Count;
            for(int i = (n - 1); i > 0; i--)
            {
                dem += 1;
                lst.Add(data[i]);
                if (dem == 3)
                    break;
            }
            return lst;
        }

        //Lấy sp theo company
        public List<ProductDTO> getProductByCompany()
        {
            //Chỉ lấy một bản ghi trong bảng Company
            var company = db.Companies.ToList();

            Random rand = new Random();
            var skip = (int)(rand.NextDouble() * company.Count());
            var com = company.OrderBy(x => x.company_ID).Skip(skip).Take(1).First();
         
            var query = from store in db.Stores
                        join pro in db.Products on store.product_ID equals pro.product_ID
                        join comp in db.Companies on store.company_ID equals comp.company_ID
                        where store.company_ID == com.company_ID
                        select new ProductDTO()
                        {
                            product_ID = pro.product_ID,
                            productName = pro.productName,
                            metatitle = pro.metatitle,
                            productImage = pro.productImage,
                            price = pro.price,
                            promotionPrice = pro.promotionPrice,
                            image = comp.image
                        };
            return query.ToList();
        }

        //Tìm sp theo ID
        public Product findProduct(long product_ID)
        {
            return db.Products.Find(product_ID);
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