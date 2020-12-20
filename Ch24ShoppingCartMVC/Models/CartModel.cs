﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Ch24ShoppingCartMVC.Models;

namespace Ch24ShoppingCartMVC.Models{
    public class CartModel
    {
        //Data Access methods 
        private List<ProductViewModel> GetCartFromDataStore()
        {
            List<ProductViewModel> cart;
            object objCart = HttpContext.Current.Session["cart"];
            //Convert objCart to List<ProductViewModel>
            cart = (List<ProductViewModel>)objCart; 
            if (cart == null)
            {   //Create the object cart
                HttpContext.Current.Session["cart"] = new List<ProductViewModel>(); //BHBHBH
                                                                                    //Assign cart to the Session object cart
                cart = (List<ProductViewModel>)HttpContext.Current.Session["cart"]; 
            }
            return cart;
        }
        private ProductViewModel GetSelectedProduct(string id)
        {   //Create an OrderModel object called order
            OrderModel order = new OrderModel();//BHBHBH
            //Call the method GetSelectedProduct of the class OrderModel. Return the object returned by the call.
            return order.GetSelectedProduct(id); //BHBHBH
        }
        public CartViewModel GetCart(string id = "")
        {
            CartViewModel model = new CartViewModel();
            //Call the method GetCartFromDataStore
            model.Cart = GetCartFromDataStore(); //BHBHBH
            if (!string.IsNullOrEmpty(id))
                //Called the method GetSelectedProduct with parameter id and assign the return object to the AddedProduct
                model.AddedProduct = GetSelectedProduct(id);  //BHBHBH
            return model;
        }
        private void AddItemToDataStore(CartViewModel model)
        {   //Add the AddedProduct to the cart
            model.Cart.Add(model.AddedProduct); 
        }

        
        public void AddToCart(CartViewModel model)
        {
            if (model.AddedProduct.ProductID != null)
            {
                //Get the product id of the added product
                string id = model.AddedProduct.ProductID; //BHBHBH
                //Find the product in the car that matches the id using lambda expression.
                ProductViewModel inCart = model.Cart.Where(p => p.ProductID == id).FirstOrDefault(); //BHBHBH
                if (inCart == null)
                    //Call the method AddItemToDataStore
                    AddItemToDataStore(model); //BHBHBH
                else
                    //Increase the Quantity by the quantity of the added product
                    inCart.Quantity += model.AddedProduct.Quantity; 
            }
        }
                
       
    }    
}