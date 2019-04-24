using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bangazon.Models;
using Bangazon.Data;
using Microsoft.EntityFrameworkCore;
using Bangazon.Models.ProductViewModels;

namespace Bangazon.Controllers
{
    public class HomeController : Controller
    {
//-----------------------------------------------------------------------------------------------
//NOTE HN: 
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
//-----------------------------------------------------------------------------------------------
//NOTE HN: fleshed out the Index() to include 20 products:
        public IActionResult Index()
        {
            List<Product> products = _context.Product
                //.Include(p => p.ProductType)
                //.Include(p => p.Title)
                //.Include(p => p.DateCreated)
                .OrderByDescending(p => p.DateCreated)
                .Take(20)
                .ToList();

            ProductListViewModel viewModel = new ProductListViewModel();
            //return View(await applicationDbContext.ToListAsync());

            viewModel.Products = products;

            return View(viewModel);
        }

//-----------------------------------------------------------------------------------------------

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
