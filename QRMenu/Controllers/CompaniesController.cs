using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QRMenu.Data;
using QRMenu.Models;

namespace QRMenu.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public CompaniesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Application
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.Companies!.Include(c => c.State);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Application/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .Include(c => c.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Application/Create
        public IActionResult Create()
        {
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name");
            return View();
        }

        // POST: Application/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PostalCode,Address,Phone,EMail,TaxNumber,WebAddress,StateId")] Company company)
        {
            company.RegisterDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name", company.StateId);
            return View(company);
        }

        // GET: Application/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name", company.StateId);
            return View(company);
        }

        // POST: Application/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PostalCode,Address,Phone,EMail,RegisterDate,TaxNumber,WebAddress,StateId")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
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
            ViewData["StateId"] = new SelectList(_context.Set<State>(), "Id", "Name", company.StateId);
            return View(company);
        }

        // GET: Application/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .Include(c => c.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Companies == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Company'  is null.");
            }
            var company = _context.Companies.Find(id);
            if (company != null)
            {
                company.StateId = 0;
                _context.Companies.Update(company);
                IQueryable<Restaurant> restaurants = _context.Restaurants.Where(r => r.CompanyId == id);
                foreach(Restaurant restaurant in restaurants)
                {
                    restaurant.StateId = 0;
                    _context.Restaurants.Update(restaurant);
                    IQueryable<Category> categories = _context.Categories.Where(c => c.RestaurantId == restaurant.Id);
                    foreach(Category category in categories)
                    {
                        category.StateId = 0;
                        _context.Categories.Update(category);
                        IQueryable<Food> foods = _context.Foods.Where(f => f.CategoryId == category.Id);
                        foreach(Food food in foods)
                        {
                            food.StateId = 0;
                            _context.Foods.Update(food);
                        }
                    }
                }
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
          return (_context.Companies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
