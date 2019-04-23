using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bangazon.Data;
using Bangazon.Models;
using Bangazon.Models.ProductViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;


//Authors: Brittany Ramos-Janeway, Hannah Neal, Asia Carter

namespace Bangazon.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        // GET: Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Product.Include(p => p.ProductType).Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }
//---------------------------------------------------------------------------------------------------------------------

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductType)
                .Include(p => p.User)
                //NOTE HN: The ".Include" methods tell the context to load the Product.ProductType and Product.User properties.
                .FirstOrDefaultAsync(m => m.ProductId == id);
            //NOTE HN: The ".FirstOrDefaultAsync" method retrieves a single product that matches the Id.
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        // HN: This is the end of the details view; To update the quantity of items shown, a product needs to be added to a valid order via the "add to order" button. This also involves a valid user and a "shopping cart" (which represents the open order).

//---------------------------------------------------------------------------------------------------------------------

        // GET: Products/Create


        // When a user chooses to add a product to sell this method directs the user to the correct form view

        public IActionResult Create()
        {
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "ProductTypeId", "Label");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // When a user fills in all required fields they are then redirected to the details view of the newly created product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,DateCreated,Description,Title,Price,Quantity,UserId,City,ImagePath,ProductTypeId")] Product product)
        {

            //the User and UserId fields must be disregarded in order to determine if the model state is valid
            ModelState.Remove("User");
            ModelState.Remove("UserId");

            //the user is instead obtained by the current authorized user
            var user = await GetCurrentUserAsync();

            if (ModelState.IsValid)
            {
                //the user id is declaired using the asyc method above and established once model state is determined
                product.UserId = user.Id;
                _context.Add(product);
                await _context.SaveChangesAsync();
                //the routing occurs here instead of in the view because the product id must be created before the redirect occurs
                return RedirectToAction("Details", new { id = product.ProductId});

            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "ProductTypeId", "Label", product.ProductTypeId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", product.UserId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "ProductTypeId", "Label", product.ProductTypeId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", product.UserId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,DateCreated,Description,Title,Price,Quantity,UserId,City,ImagePath,ProductTypeId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductTypeId"] = new SelectList(_context.ProductType, "ProductTypeId", "Label", product.ProductTypeId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", product.UserId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductType)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
       
    }

}
