using Microsoft.AspNetCore.Mvc;
using Sach.Model.Models;
using Sach.Repository;

namespace BanSachCu.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class OrderDetailController : Controller
    {
        SachCuContext context = new SachCuContext();
        private OrderDetailRepository orderDetailRepo;
        public OrderDetailController()
        {
            orderDetailRepo = new OrderDetailRepository();
        }
        public IActionResult Index()
        {
            var OrderDetails = orderDetailRepo.GetAll().ToList();
            return View("Index", OrderDetails);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(OrderDetail orderDetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    orderDetailRepo.Insert(orderDetail);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(orderDetail);



        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var orderDetail = orderDetailRepo.GetById(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return View(orderDetail);
        }

        [HttpPost]
        public IActionResult Edit(OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                context.OrderDetails.Update(orderDetail);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderDetail);
        }

        public IActionResult Delete(int id)
        {
            var orderDetail = context.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            context.OrderDetails.Remove(orderDetail);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
