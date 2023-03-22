using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ChinookContext;
using ChinookEntities;

namespace Project.Pages
{
    public class Update : PageModel
    {
        public string Heading { get; set; }
        private readonly ChinookDatabase _db;
        private readonly ILogger<Update> _logger;
        
        public Update(ChinookDatabase db, ILogger<Update> logger)
        {
            _db = db;
            _logger = logger;
        }

        [BindProperty]
        public Album Album { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Album = await _db.albums.FindAsync(id);

            if (Album == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
{
    if (!ModelState.IsValid)
    {
        return Page();
    }

    var albumFromDb = await _db.albums.FindAsync(id);

    if (albumFromDb == null)
    {
        return NotFound();
    }

    albumFromDb.Title = Album.Title;
    albumFromDb.ArtistId = Album.ArtistId;

    try
    {
        await _db.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException ex)
    {
        _logger.LogError(ex, "Error updating album");
        throw;
    }

    return RedirectToPage("Index");
}

    }
}
