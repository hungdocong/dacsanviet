﻿using dacsanviet.Models.DTO;
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
        public List<ProductDTO> featureProduct()
        {
            var query = from pro in db.Products
                        join cate in db.ProductCategories on pro.productCategory_ID equals cate.productCategory_ID
                        select new ProductDTO()
                        {
                            product_ID = pro.product_ID,
                            productName = pro.productName,
                            metatitle = pro.metatitle,
                            productImage = pro.productImage,
                            price = pro.price,
                            promotionPrice = pro.promotionPrice,
                            ProductCategoryMetatitle = cate.metatitle,
                            nameProductCategory = cate.name
                        };

            return query.Take(10).ToList();
        } 

        //Lấy chi tiết sp
        public ProductDTO getDetail(long product_ID)
        {
            var query = from sto in db.Stores
                        join pro in db.Products on sto.product_ID equals pro.product_ID
                        join com in db.Companies on sto.company_ID equals com.company_ID
                        where pro.product_ID == product_ID
                        select new ProductDTO()
                        {
                            product_ID = pro.product_ID,
                            productName = pro.productName,
                            metatitle = pro.metatitle,
                            productImage = pro.productImage,
                            price = pro.price,
                            promotionPrice = pro.promotionPrice,
                            name = com.name
                        };
            return query.Single();
        }

        //tăng lượt xem sp
        public void increaseViewProduct(long product_ID)
        {
            var pro = db.Products.Find(product_ID);
            pro.viewCount += 1;
            db.SaveChanges();
        }

        //Lấy ra loại sp từ ID của sp
        public ProductCategory GetProductCategoryByProducID(long product_ID)
        {
            var pro = db.Products.Find(product_ID);
            return db.ProductCategories.Find(pro.productCategory_ID);
        }


        //lấy sp cùng loại
        public List<ProductDTO> GetProductSameCategory(long productCategory_ID)
        {
            var query = from pro in db.Products
                        join cate in db.ProductCategories on pro.productCategory_ID equals cate.productCategory_ID
                        where pro.productCategory_ID == productCategory_ID
                        select new ProductDTO()
                        {
                            product_ID = pro.product_ID,
                            productName = pro.productName,
                            metatitle = pro.metatitle,
                            productImage = pro.productImage,
                            price = pro.price,
                            promotionPrice = pro.promotionPrice,
                            ProductCategoryMetatitle = cate.metatitle,
                            nameProductCategory = cate.name,
                            viewCount = pro.viewCount
                        };
            return query.OrderBy(x => x.viewCount).ToList();
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
                        join cate in db.ProductCategories on pro.productCategory_ID equals cate.productCategory_ID
                        where store.company_ID == com.company_ID
                        select new ProductDTO()
                        {
                            product_ID = pro.product_ID,
                            productName = pro.productName,
                            metatitle = pro.metatitle,
                            productImage = pro.productImage,
                            price = pro.price,
                            promotionPrice = pro.promotionPrice,
                            image = comp.image,
                            ProductCategoryMetatitle = cate.metatitle,
                            nameProductCategory = cate.name
                        };
            return query.ToList();
        }

        //Tìm sp theo ID
        public Product findProduct(long product_ID)
        {
            return db.Products.Find(product_ID);
        }

        //lấy sp theo loại sp
        public List<ProductDTO> GetProductByCategory(long productCategory_ID)
        {
            var query = from pro in db.Products
                        join cate in db.ProductCategories on pro.productCategory_ID equals cate.productCategory_ID
                        where pro.productCategory_ID == productCategory_ID
                        select new ProductDTO()
                        {
                            product_ID = pro.product_ID,
                            productName = pro.productName,
                            metatitle = pro.metatitle,
                            productImage = pro.productImage,
                            price = pro.price,
                            promotionPrice = pro.promotionPrice,
                            ProductCategoryMetatitle = cate.metatitle,
                            nameProductCategory = cate.name
                        };
            return query.ToList();
        }

        //sản phẩm mới
        public List<ProductDTO> newProduct()
        {
            var query = from pro in db.Products
                        join cate in db.ProductCategories on pro.productCategory_ID equals cate.productCategory_ID
                        select new ProductDTO()
                        {
                            product_ID = pro.product_ID,
                            productName = pro.productName,
                            metatitle = pro.metatitle,
                            productImage = pro.productImage,
                            price = pro.price,
                            promotionPrice = pro.promotionPrice,
                            ProductCategoryMetatitle = cate.metatitle,
                            nameProductCategory = cate.name
                        };

            return query.Take(10).ToList();
        }

        //sản phẩm bán chạy
        public List<ProductDTO> topSellProduct()
        {
            var query = from pro in db.Products
                        join cate in db.ProductCategories on pro.productCategory_ID equals cate.productCategory_ID
                        select new ProductDTO()
                        {
                            product_ID = pro.product_ID,
                            productName = pro.productName,
                            metatitle = pro.metatitle,
                            productImage = pro.productImage,
                            price = pro.price,
                            promotionPrice = pro.promotionPrice,
                            ProductCategoryMetatitle = cate.metatitle,
                            nameProductCategory = cate.name,

                        };

            return query.Take(10).ToList();
        }
    }
}