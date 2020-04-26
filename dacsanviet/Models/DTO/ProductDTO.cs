using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dacsanviet.Models.DTO
{
    public class ProductDTO
    {
        public long product_ID { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public string metatitle { get; set; }
        public string descriptions { get; set; }
        public string productImage { get; set; }
        public string moreImage { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<decimal> promotionPrice { get; set; }
        public Nullable<bool> includeVAT { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<long> productCategory_ID { get; set; }
        public string detail { get; set; }
        public Nullable<int> warranty { get; set; }
        public string createdDate { get; set; }
        public string createdBy { get; set; }
        public string modifiedDate { get; set; }
        public string modifiedBy { get; set; }
        public string metaKeywords { get; set; }
        public string metaDescription { get; set; }
        public Nullable<bool> status { get; set; }
        public string topHot { get; set; }
        public Nullable<int> viewCount { get; set; }
        public long company_ID { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string image { get; set; }
    }
}