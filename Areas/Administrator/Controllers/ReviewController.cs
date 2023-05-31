
using Microsoft.AspNetCore.Mvc;
using Sach.Model.Models;
using Sach.Repository;

namespace BanSachCu.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class ReviewController : Controller
    {
        SachCuContext context = new SachCuContext();
        private ReviewRepository reviewRepo;
        public ReviewController()
        {
            reviewRepo = new ReviewRepository();
        }
        public IActionResult Index()
        {
            var reviews = reviewRepo.GetAll().ToList();
            return View("Index", reviews);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Review review)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    reviewRepo.Insert(review);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(review);



        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var review = reviewRepo.GetById(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        [HttpPost]
        public IActionResult Edit(Review review)
        {
            if (ModelState.IsValid)
            {
                context.Reviews.Update(review);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(review);
        }

        public IActionResult Delete(int id)
        {
            var review = context.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }
            context.Reviews.Remove(review);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
