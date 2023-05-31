using Microsoft.AspNetCore.Mvc;
using Sach.Model.Models;
using Sach.Repository;

namespace BanSachCu.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class OrderController : Controller
    {
        SachCuContext context = new SachCuContext();
        private OrderRepository orderRepo;
        public OrderController()
        {
            orderRepo = new OrderRepository();
        }
        public IActionResult Index()
        {
            var Orders = orderRepo.GetAll().ToList();
            return View("Index", Orders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    orderRepo.Insert(order);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(order);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var order = orderRepo.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        public IActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                context.Orders.Update(order);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        public IActionResult Delete(int id)
        {
            var order = context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            context.Orders.Remove(order);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}
