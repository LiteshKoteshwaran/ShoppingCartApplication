using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBasket.Models;
using System.Web.Security;

namespace MyBasket.Controllers
{
    public class CustomerController : Controller
    {
        MyBasketDataContext myBasketDataContext = new MyBasketDataContext();
        // GET: Customer
        public ActionResult Register()
        {
            return View(new Customer());
        }
        [HttpPost]
        public ActionResult Register(Customer customer, string ReturnUrl)
        {
            myBasketDataContext.Customers.InsertOnSubmit(customer);
            myBasketDataContext.SubmitChanges();
            Session["UserName"] = customer.Name;
            return Redirect(ReturnUrl == null ? "/" : ReturnUrl);
        }
        public ActionResult Login()
        {
            return View(new Customer());
        }
        [HttpPost]
        public ActionResult Login(Customer user, string ReturnUrl, bool RememberMe = false)
        {
            Customer Customer = new CustomerRepository().IsValidUser(user);
            if (CustomerRepository.UserExists)
            {
                Session["User"] = Customer;
                FormsAuthentication.SetAuthCookie(Customer.Name, RememberMe);
                return Redirect(ReturnUrl == null ? "/" : ReturnUrl);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove("User");
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }
    }
}