using Microsoft.AspNetCore.Mvc;
using Sach.Model.Models;
using Sach.Repository;

namespace BanSachCu.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class BookRestorationController : Controller
    {
        
        SachCuContext context = new SachCuContext();
        private BookRestorationRepository bookRestorationRepo;
        public BookRestorationController()
        {
            bookRestorationRepo = new BookRestorationRepository();
        }
        public IActionResult Index()
        {
            var BookRestorations = bookRestorationRepo.GetAll().ToList();
            return View("Index", BookRestorations);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(BookRestoration bookRestoration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bookRestorationRepo.Insert(bookRestoration);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(bookRestoration);



        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var bookRestoration = bookRestorationRepo.GetById(id);
            if (bookRestoration == null)
            {
                return NotFound();
            }
            return View(bookRestoration);
        }

        [HttpPost]
        public IActionResult Edit(BookRestoration bookRestoration)
        {
            if (ModelState.IsValid)
            {
                context.BookRestorations.Update(bookRestoration);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookRestoration);
        }

        public IActionResult Delete(int id)
        {
            var bookRestoration = context.BookRestorations.Find(id);
            if (bookRestoration == null)
            {
                return NotFound();
            }
            context.BookRestorations.Remove(bookRestoration);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
