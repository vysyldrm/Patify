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
    public class PhotoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhotoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Photo
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Photos.Include(p => p.Advert);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Photo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photos = await _context.Photos
                .Include(p => p.Advert)
                .FirstOrDefaultAsync(m => m.PhotosId == id);
            if (photos == null)
            {
                return NotFound();
            }

            return View(photos);
        }

        // GET: Photo/Create
        public IActionResult Create()
        {
            ViewData["AdvertId"] = new SelectList(_context.Advert, "AdvertId", "Description");
            return View();
        }

        // POST: Photo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhotosId,AdvertId")] Photos photos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(photos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdvertId"] = new SelectList(_context.Advert, "AdvertId", "Description", photos.AdvertId);
            return View(photos);
        }

        // GET: Photo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photos = await _context.Photos.FindAsync(id);
            if (photos == null)
            {
                return NotFound();
            }
            ViewData["AdvertId"] = new SelectList(_context.Advert, "AdvertId", "Description", photos.AdvertId);
            return View(photos);
        }

        // POST: Photo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhotosId,AdvertId")] Photos photos)
        {
            if (id != photos.PhotosId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(photos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotosExists(photos.PhotosId))
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
            ViewData["AdvertId"] = new SelectList(_context.Advert, "AdvertId", "Description", photos.AdvertId);
            return View(photos);
        }

        // GET: Photo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photos = await _context.Photos
                .Include(p => p.Advert)
                .FirstOrDefaultAsync(m => m.PhotosId == id);
            if (photos == null)
            {
                return NotFound();
            }

            return View(photos);
        }

        // POST: Photo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var photos = await _context.Photos.FindAsync(id);
            _context.Photos.Remove(photos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhotosExists(int id)
        {
            return _context.Photos.Any(e => e.PhotosId == id);
        }
    }
}
