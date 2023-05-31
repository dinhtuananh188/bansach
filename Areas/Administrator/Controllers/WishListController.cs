using Microsoft.AspNetCore.Mvc;
using Sach.Model.Models;
using Sach.Repository;

namespace BanSachCu.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class WishListController : Controller
    {
        SachCuContext context = new SachCuContext();
        private WishListRepository wishListRepo;
        public WishListController()
        {
            wishListRepo = new WishListRepository();
        }
        public IActionResult Index()
        {
            var WishLists = wishListRepo.GetAll().ToList();
            return View("Index", WishLists);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(WishList wishList)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    wishListRepo.Insert(wishList);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(wishList);



        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var wishList = wishListRepo.GetById(id);
            if (wishList == null)
            {
                return NotFound();
            }
            return View(wishList);
        }

        [HttpPost]
        public IActionResult Edit(WishList wishList)
        {
            if (ModelState.IsValid)
            {
                context.WishLists.Update(wishList);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wishList);
        }

        public IActionResult Delete(int id)
        {
            var wishList = context.WishLists.Find(id);
            if (wishList == null)
            {
                return NotFound();
            }
            context.WishLists.Remove(wishList);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
