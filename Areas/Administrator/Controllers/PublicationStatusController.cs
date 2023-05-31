using Microsoft.AspNetCore.Mvc;
using Sach.Model.Models;
using Sach.Repository;

namespace BanSachCu.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class PublicationStatusController : Controller
    {
        SachCuContext context = new SachCuContext();
        private PublicationStatusRepository publicationStatusRepo;
        public PublicationStatusController()
        {
            publicationStatusRepo = new PublicationStatusRepository();
        }
        public IActionResult Index()
        {
            var PublicationStatuses = publicationStatusRepo.GetAll().ToList();
            return View("Index", PublicationStatuses);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PublicationStatus publicationStatus)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    publicationStatusRepo.Insert(publicationStatus);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(publicationStatus);



        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var publicationStatus = publicationStatusRepo.GetById(id);
            if (publicationStatus == null)
            {
                return NotFound();
            }
            return View(publicationStatus);
        }

        [HttpPost]
        public IActionResult Edit(PublicationStatus publicationStatus)
        {
            if (ModelState.IsValid)
            {
                context.PublicationStatuses.Update(publicationStatus);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(publicationStatus);
        }

        public IActionResult Delete(int id)
        {
            var publicationStatus = context.PublicationStatuses.Find(id);
            if (publicationStatus == null)
            {
                return NotFound();
            }
            context.PublicationStatuses.Remove(publicationStatus);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
