﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ch24ShoppingCartMVC.Models;

namespace Ch24ShoppingCartMVC.Controllers {
    public class OrderController : Controller
    {
        private OrderModel order = new OrderModel();

        [HttpGet]
        public ActionResult Index(string id)
        {
            //get list for drop down from temp data called products 

            SelectList products = NewMethod();
            if (products == null)
            {
                //CALL THE METHOD GetProductList 
                var list = order.GetProductsList(); //BHBHBH
                //CREATE THE SelectList products
                products = new SelectList(list, "ProductId", "Name", id); //BHBHBH
            }
            //if no URL parameter, get first product from list and refresh
            if (string.IsNullOrEmpty(id))
            {
                id = products.ElementAt(0).Value;
                //ASSIGN products to temp data called products
                TempData["products"] = products; //BHBHBH
                //Redirect to the action method Index of the Order controller with id parameter.
                return RedirectToAction("Index", "Order", new { id }); //BHBHBH
            }
            else
            { //get selected product and return in view method
              //Call the method GetOrderInfo to get an OrderViewModel object called model
                OrderViewModel model = order.GetOrderInfo(id); //BHBHBH
                                                               //Assign products to ProductsList property of model
                model.ProductsList = products; //BHBHBH
                //Assign the quantity of the SelectProduct of the model to 1
                model.SelectedProduct.Quantity = 1; //BHBHBH
                                                    //Send the model object to the view.
                return View(model);
            }
        }

        private SelectList NewMethod()
        {
            return (SelectList)TempData["products"];
        
        }

        [HttpPost] //post back - get selected ddl value and refresh
        public RedirectToRouteResult Index(FormCollection collection)
        {
            string pID = collection["ddlProducts"];
            //Redirect to the action method index of the Order controller with parameter the id assigned to pID
            return RedirectToAction("Index", "Order" , new { @id = pID }); 
        }      
    }
}
