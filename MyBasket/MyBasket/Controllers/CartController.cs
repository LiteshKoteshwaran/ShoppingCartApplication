using MyBasket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBasket.Controllers
{
    public class CartController : Controller
    {
        MyBasketRepository myBasketRepository = new MyBasketRepository();

        MyCartRepository myCartRepository = new MyCartRepository();
        // GET: Cart
        public ActionResult Index()
        {
            return RedirectToAction("Index","Home");
        }

        public ActionResult CartTemplate()
        {
            return View();
        }

        public int AddToCart(int id)
        {
            Dictionary<int, LineItem> cart = (Dictionary<int, LineItem>)HttpContext.Session["Cart"];

            if (cart == null)
            {
                cart = new Dictionary<int, LineItem>();
                Session["Cart"] = cart;
            }

            if (cart.ContainsKey(id))
            {
                LineItem Item = cart[id];
                Item.Quantity++;
            }

            else
            {
                Product product = myBasketRepository.GetProductById(id);
                LineItem Item = new LineItem();
                Item.Product = product;
                Item.UnitPrice = (decimal)product.UnitPrice;
                Item.Discount = product.Discount;
                Item.Quantity = 1;
                cart[id] = Item;
            }
            return cart.Count;
        }

        public ActionResult CartDetails()
        {
            Dictionary<int, LineItem> cart = (Dictionary<int, LineItem>)Session["Cart"];
            return View(cart);
        }

        public ActionResult ViewCart()
        {
            Dictionary<int, LineItem> cart = (Dictionary<int, LineItem>)Session["Cart"];
            return View(cart.Values);
        }
        public ActionResult RemoveItemFromCart(int id)
        {
            Dictionary<int, LineItem> cart = (Dictionary<int, LineItem>)Session["Cart"];
            cart.Remove(id);
            return RedirectToAction("ViewCart");
        }

        public ActionResult EmptyCart()
        {
            Dictionary<int, LineItem> cart = (Dictionary<int, LineItem>)Session["Cart"];
            cart.Clear();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        [Authorize]
        public ActionResult CheckOut()
        {
            IEnumerable<LineItem> item = TempData["cart"] as IEnumerable<LineItem>; 
            return View(item);
        }
    }
}