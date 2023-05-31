using Microsoft.AspNetCore.Mvc;
using Sach.Model.Models;
using Sach.Repository;

namespace BanSachCu.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class CustomerController : Controller
    {
        SachCuContext context = new SachCuContext();
        private CustomerRepository customerRepo;
        public CustomerController()
        {
            customerRepo = new CustomerRepository();
        }
        public IActionResult Index()
        {
            var Customers = customerRepo.GetAll().ToList();
            return View("Index", Customers);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    customerRepo.Insert(customer);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(customer);



        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customer = customerRepo.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                context.Customers.Update(customer);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        public IActionResult Delete(int id)
        {
            var customer = context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            context.Customers.Remove(customer);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
