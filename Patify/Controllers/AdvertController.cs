﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Patify.Data;
using Patify.Models;

namespace Patify.Controllers
{
    public class AdvertController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdvertController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Advert
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Advert.Include(a => a.Category).Include(a => a.City);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Advert/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advert = await _context.Advert
                .Include(a => a.Category)
                .Include(a => a.City)
                .FirstOrDefaultAsync(m => m.AdvertId == id);
            if (advert == null)
            {
                return NotFound();
            }

            return View(advert);
        }

        // GET: Advert/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId");
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Id");
            return View();
        }

        // POST: Advert/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdvertId,Header,Description,AnimalName,AnimalAge,CategoryId,CityId,PublishDate,AnimalRace,Gender,SizeOfAnimal,FromWho")] Advert advert)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advert);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId", advert.CategoryId);
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Id", advert.CityId);
            return View(advert);
        }

        // GET: Advert/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advert = await _context.Advert.FindAsync(id);
            if (advert == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId", advert.CategoryId);
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Id", advert.CityId);
            return View(advert);
        }

        // POST: Advert/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdvertId,Header,Description,AnimalName,AnimalAge,CategoryId,CityId,PublishDate,AnimalRace,Gender,SizeOfAnimal,FromWho")] Advert advert)
        {
            if (id != advert.AdvertId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertExists(advert.AdvertId))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId", advert.CategoryId);
            ViewData["CityId"] = new SelectList(_context.City, "Id", "Id", advert.CityId);
            return View(advert);
        }

        // GET: Advert/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advert = await _context.Advert
                .Include(a => a.Category)
                .Include(a => a.City)
                .FirstOrDefaultAsync(m => m.AdvertId == id);
            if (advert == null)
            {
                return NotFound();
            }

            return View(advert);
        }

        // POST: Advert/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advert = await _context.Advert.FindAsync(id);
            _context.Advert.Remove(advert);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertExists(int id)
        {
            return _context.Advert.Any(e => e.AdvertId == id);
        }
    }
}
