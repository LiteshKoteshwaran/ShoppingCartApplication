using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBasket.Models
{
    public class MyCartRepository
    {
        MyBasketDataContext myBasketDataContext = new MyBasketDataContext();
        public void PopulateOrderAndLineItem(Order order)
        {
            myBasketDataContext.Orders.InsertOnSubmit(order);
            myBasketDataContext.SubmitChanges();
        }
    }
}