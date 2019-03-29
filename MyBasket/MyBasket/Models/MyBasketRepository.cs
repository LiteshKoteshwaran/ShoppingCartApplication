using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBasket.Models
{
    public class MyBasketRepository
    {
        MyBasketDataContext myBasketDataContext = new MyBasketDataContext();

        public List<Product> Products
        {
            get { return myBasketDataContext.Products.ToList(); }
        }

        public List<Brand> Brands
        {
            get { return myBasketDataContext.Brands.ToList(); }
        }

        public List<Category> Categories
        {
            get { return myBasketDataContext.Categories.ToList(); }
        }

        public List<Product> GetProductsByBrandId(int BrandId)
        {
            return myBasketDataContext.Products.ToList().FindAll(product => product.BrandID == BrandId);
        }

        public List<Product> GetProductsByCategoryId(int CategoryId)
        {
            return myBasketDataContext.Products.ToList().FindAll(product => product.CategoryID == CategoryId);
        }

        public Product GetProductById(int ProductId)
        {
            return myBasketDataContext.Products.ToList().Find(product => product.ID == ProductId);
        }
    }
}
