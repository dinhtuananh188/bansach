using BanSachCu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sach.Model.Models;
using Sach.Repository;
using System.Diagnostics;

namespace BanSachCu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ProductCategoryRepository productCategoryRepo;
        private ProductRepository productRepo;
        private ReviewRepository reviewRepo;
        private CustomerRepository customerRepo;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            productCategoryRepo = new ProductCategoryRepository();
            reviewRepo = new ReviewRepository();
            customerRepo = new CustomerRepository();
            productRepo = new ProductRepository();
        }

        public IActionResult Index()
        {
            var TopCatagory = productCategoryRepo.GetAll().OrderByDescending(p=>p.Id).ToList();
            var popularProducts = productRepo.GetAll().OrderByDescending(p=>p.ViewCount).ToList();
            var tupleModel = new Tuple<List<ProductCategory>, List<Product>>(TopCatagory, popularProducts);
            return View(tupleModel);
        }
        public IActionResult Details(int id)
        {
            var product = productRepo.GetById(id);
            var categoryproduct = productCategoryRepo.GetAll().Where(p => p.Id == product.CategoryId).ToList();
            var review = reviewRepo.GetAll().OrderByDescending(p => p.Id).ToList();
            var cusreview = customerRepo.GetAll().OrderByDescending(p => p.Id).ToList();
            var tupleModel = new Tuple<Product, List<ProductCategory>,List<Review>,List<Customer>>(product, categoryproduct, review, cusreview);
            return View(tupleModel);
        }
        public IActionResult Contact()
        {
            return View();
        }
        
        public IActionResult Shop()
        {
            var TopCatagory = productCategoryRepo.GetAll().OrderByDescending(p => p.Id).ToList();
            var popularProducts = productRepo.GetAll().OrderByDescending(p => p.ViewCount).ToList();
            var tupleModel = new Tuple<List<ProductCategory>, List<Product>>(TopCatagory, popularProducts);
            return View(tupleModel);
        }
        public IActionResult ShopCategory(int id)
        {
            var productCategory = productCategoryRepo.GetById(id);
            var TopCatagory = productCategoryRepo.GetAll().OrderByDescending(p => p.Id).ToList();
            var popularProducts = productRepo.GetAll().OrderByDescending(p => p.Id).ToList();
            var tupleModel = new Tuple<ProductCategory,List<ProductCategory>, List<Product>>(productCategory,TopCatagory, popularProducts);
            return View(tupleModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}