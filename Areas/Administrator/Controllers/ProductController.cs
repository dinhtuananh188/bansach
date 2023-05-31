using Microsoft.AspNetCore.Mvc;
using Sach.Model.Models;

using Sach.Repository;

namespace BanSachCu.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class ProductController : Controller
    {
        SachCuContext context = new SachCuContext();
        private ProductRepository productRepo;
        public ProductController()
        {
            productRepo = new ProductRepository();
        }
        public IActionResult Index()
        {
            var products = productRepo.GetAll().ToList();
            return View("Index", products);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    productRepo.Insert(product);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(product);



        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = productRepo.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                context.Products.Update(product);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
