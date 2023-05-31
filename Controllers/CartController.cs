using BanSachCu.Models;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sach.Model.Models;
using Sach.Repository;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;

namespace BanSachCu.Controllers
{
    public class CartController : Controller
    {
            SachCuContext context = new SachCuContext();
            private ProductRepository productRepo;
            private OrderRepository orderRepo;
            public CartController()
            {
                productRepo = new ProductRepository();
                orderRepo = new OrderRepository();
            }
            public IActionResult Index()
            {
                return View();
            }

            [HttpPost]
            public IActionResult AddToCart(int id)
            {
                List<CartItem> cart;
                if (HttpContext.Session.Get<List<CartItem>>("Cart") == null)
                {
                    cart = new List<CartItem>();
                    cart.Add(new CartItem { ProductOrder = productRepo.GetById(id), Quantity = 1 });
                }
                else //trong giỏ hàng đã có sản phẩm rồi
                {
                    cart = (List<CartItem>)HttpContext.Session.Get<List<CartItem>>("Cart");
                    CartItem cartItem = cart.SingleOrDefault(x => x.ProductOrder.Id == id);
                    if (cartItem != null) //đã có sản phẩm này trong giỏ hàng rồi
                    {
                        cartItem.Quantity++;
                    }
                    else //chưa có sản phẩm này trogn giỏ hàng.
                    {
                        cart.Add(new CartItem { ProductOrder = productRepo.GetById(id), Quantity = 1 });
                    }
                }
                //Cập nhật giỏ hàng
                HttpContext.Session.Set<List<CartItem>>("Cart", cart);
                //Trả về số lượng hàng trong giỏ
                return Json(new { total = cart.Sum(x => x.Quantity) });
            }
            public IActionResult ShopCart()
            {
                return View();
            }
            [HttpPost]
            public IActionResult RemoveFromCart(int id)
            {
                List<CartItem> cart = HttpContext.Session.Get<List<CartItem>>("Cart");
                CartItem itemToRemove = cart.FirstOrDefault(x => x.ProductOrder.Id == id);
                if (itemToRemove != null)
                {
                    cart.Remove(itemToRemove);
                    HttpContext.Session.Set("Cart", cart);
                }
                return RedirectToAction("ShopCart");
            }
            [HttpPost]
            public IActionResult UpdateCartItem(int id, int quantity, string increment, string decrement)
            {
                List<CartItem> cart = HttpContext.Session.Get<List<CartItem>>("Cart");
                CartItem itemToUpdate = cart.FirstOrDefault(x => x.ProductOrder.Id == id);
                if (itemToUpdate != null)
                {
                    if (!string.IsNullOrEmpty(increment))
                    {
                        itemToUpdate.Quantity++;
                    }
                    else if (!string.IsNullOrEmpty(decrement))
                    {
                        if (itemToUpdate.Quantity > 1)
                        {
                            itemToUpdate.Quantity--;
                        }
                    }
                    itemToUpdate.Quantity = quantity;
                    HttpContext.Session.Set("Cart", cart);
                }
                return RedirectToAction("ShopCart");
            }


        [HttpPost]
        public IActionResult Checkout([Bind("CustomerName,CustomerAddress,CustomerMobile,CustomerMessage,PaymentMethod")] Order order)
        {
            double totalAmount = Convert.ToDouble(Request.Form["tongTien"]);
            ViewBag.TongTien = totalAmount;

            try
            {
                if (ModelState.IsValid)
                {
                    orderRepo.Insert(order);

                    return RedirectToAction("ThankYou");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            var orders = orderRepo.GetAll().ToList();
            var tupleModel = new Tuple<Order, List<Order>>(order, orders);
            return View(tupleModel);
        }
        public ActionResult ThankYou()
            {
            List<CartItem> cart = HttpContext.Session.Get<List<CartItem>>("Cart");
            double totalAmount = (double)cart.Sum(x => x.ProductOrder.Price * x.Quantity);

            Order order = context.Orders.LastOrDefault();
            if (order != null)
            {
                OrderViewModel model = new OrderViewModel
                {
                    CustomerName = order.CustomerName,
                    CustomerAddress = order.CustomerAddress,
                    CustomerMobile = order.CustomerMobile,
                    CustomerMessage = order.CustomerMessage,
                    PaymentMethod = order.PaymentMethod,
                    TotalAmount = totalAmount
                };

                return View(model);
            }

            return View();
        }

    }
}