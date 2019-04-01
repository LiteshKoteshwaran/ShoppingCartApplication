using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBasket.Models
{
    public class CustomerRepository
    {
        MyBasketDataContext myBasketDataContext = new MyBasketDataContext();
        public static bool UserExists = false;
        public Customer IsValidUser(Customer user)
        {
            if (myBasketDataContext.Customers.Any(cust => cust.Email == user.Email && cust.Password == user.Password))
            {
                UserExists = true;
            }
            Customer customer = myBasketDataContext.Customers.FirstOrDefault(cust => cust.Email == user.Email);
            return customer;
        }

    }
}