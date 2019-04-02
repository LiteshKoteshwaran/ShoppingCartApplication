using MyBasket.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBasket.Controllers
{
    public class CartController : Controller
    {
        private static int index = 0;
        MyBasketRepository myBasketRepository = new MyBasketRepository();

        MyCartRepository myCartRepository = new MyCartRepository();
        // GET: Cart
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
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
        [HttpGet]
        public ActionResult CheckOut()
        {
            Dictionary<int, LineItem> cart = (Dictionary<int, LineItem>)HttpContext.Session["Cart"];

            Customer customer = (Customer)Session["User"];
            Order order = new Order();
            order.CustomerID = customer.ID;
            decimal? total = 0;
            List<LineItem> itemList = new List<LineItem>();
            foreach (var ele in cart.Values)
            {
                LineItem list = new LineItem();
                list.ProductID = ele.ProductID;
                list.Quantity = ele.Quantity;
                list.UnitPrice = ele.UnitPrice;
                list.Discount = ele.Discount;
                total += (ele.Quantity * ele.UnitPrice * (100 - ele.Discount) / 100);
                order.LineItems.Add(list);
                itemList.Add(list);
            }
            order.OrderDate = DateTime.Now;
            order.Status = "Pending";

            TempData["order"] = order;
            TempData["items"] = itemList;
            TempData["total"] = total;
            new MyCartRepository().PopulateOrderAndLineItem(order);

            return RedirectToAction("PlacedOders");
        }

        public ActionResult PlacedOders()
        {
            return View();
        }
    }
}