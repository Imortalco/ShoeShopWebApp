using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoeShopWebApp.Data;
using ShoeShopWebApp.Models;

namespace ShoeShopWebApp.Controllers
{
    public class ShoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoesController(ApplicationDbContext context)
        {
            _context = context;
        }
        

        // GET: Shoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Shoe.ToListAsync());
        }

        // GET: Shoes/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: Shoes/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchModel)
        {
            return View("Index",await _context.Shoe.Where(w=>w.Model.Contains(SearchModel)).ToListAsync());
        }

        public IActionResult Detail(int id)
        {
            var shoe = _context.Shoe.Find(id);
            shoe.Reviews=_context.Review.Where(w => w.ShoeId == id).ToList();
            ViewBag.shoe = shoe;
            var review = new Review()
            {
                ShoeId = shoe.Id
            };
            return View("Detail",review);
        }

        public ActionResult SetReview(Review review, double rating)
        {
            review.PostDate = DateTime.Now;
            review.Rating = rating;
            review.UserName = User.Identity.Name;

            _context.Review.Add(review);
            _context.SaveChanges();
            return RedirectToAction("Detail", "Shoes", new { id = review.ShoeId });
        }

        // GET: Shoes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            
            if (id == null)
            {
                return NotFound();
            }
            var shoe = await _context.Shoe
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (shoe == null)
            {
                return NotFound();
            }

            return View(shoe);
        }


        // GET: Shoes/Create
        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model,Description,Price,Photo")] Shoe shoe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shoe);
        }

        // GET: Shoes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoe = await _context.Shoe.FindAsync(id);
            if (shoe == null)
            {
                return NotFound();
            }
            return View(shoe);
        }

        // POST: Shoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,Description,Price,Photo")] Shoe shoe)
        {
            if (id != shoe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoeExists(shoe.Id))
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
            return View(shoe);
        }

        // GET: Shoes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoe = await _context.Shoe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoe == null)
            {
                return NotFound();
            }

            return View(shoe);
        }

        // POST: Shoes/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoe = await _context.Shoe.FindAsync(id);
            _context.Shoe.Remove(shoe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoeExists(int id)
        {
            return _context.Shoe.Any(e => e.Id == id);
        }
    }
}
