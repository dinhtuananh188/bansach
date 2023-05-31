using Microsoft.AspNetCore.Mvc;
using Sach.Model.Models;

using Sach.Repository;


namespace BanSachCu.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class ProductCategoryController : Controller
    {
        SachCuContext context = new SachCuContext();
        private ProductCategoryRepository productCategoryRepo;
        public ProductCategoryController()
        {
            productCategoryRepo = new ProductCategoryRepository();
        }
        public IActionResult Index()
        {
            var productCategories = productCategoryRepo.GetAll().ToList();
            return View("Index", productCategories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductCategory productCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    productCategoryRepo.Insert(productCategory);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(productCategory);



        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var productCategory = productCategoryRepo.GetById(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            return View(productCategory);
        }

        [HttpPost]
        public IActionResult Edit(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                context.ProductCategories.Update(productCategory);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productCategory);
        }

        public IActionResult Delete(int id)
        {
            var productCategory = context.ProductCategories.Find(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            context.ProductCategories.Remove(productCategory);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
