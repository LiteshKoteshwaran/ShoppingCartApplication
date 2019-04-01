using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBasket.Models;

namespace MyBasket.Controllers
{
    public class HomeController : Controller
    {
        MyBasketRepository myBasketRepository = new MyBasketRepository();
        // GET: Home
        public ActionResult Index()
        {
            return View(myBasketRepository.Products);
        }

        [HttpPost]
        public ActionResult Index(int currentPageIndex)
        {
            return View(myBasketRepository.Products); 
        }
    public ActionResult Categories()
        {
            return View(myBasketRepository.Categories);
        }

        public ActionResult Brands()
        {
            return View(myBasketRepository.Brands);
        }

        [ActionName("Category")]
        public ActionResult GetProductsForCategory(int id)
        {
            return View("Index", myBasketRepository.GetProductsByCategoryId(id));
        }

        [ActionName("Brand")]
        public ActionResult GetProductsForBrands(int id)
        {
            return View("Index", myBasketRepository.GetProductsByBrandId(id));
        }

    }
}