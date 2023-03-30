using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ChinookContext;
using ChinookEntities;

namespace WebApp.Pages
{
    public class ViewAlbums : PageModel
    {
        public string Heading { get; set; }
        public List<Album> albums { get; set; }
        public List<Artist> artists { get; set; }
        private readonly ChinookDatabase _db;

        [BindProperty]
        public string SearchTerm { get; set; }

        public ViewAlbums(ChinookDatabase db)
        {
            _db = db;
        }

        public async Task OnGetAsync(string searchTerm)
        {
            Heading = "Chinook Music Store";

            IQueryable<Album> albumsQuery = _db.albums.Include(a => a.artists);
            IQueryable<Artist> artistsQuery = _db.artists;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                string searchTermLower = searchTerm.ToLower();
                albumsQuery = albumsQuery.Where(a => a.Title.ToLower().Contains(searchTermLower)
                                                  || a.artists.Name.ToLower().Contains(searchTermLower));
                artistsQuery = artistsQuery.Where(a => a.Name.ToLower().Contains(searchTermLower));
            }

            albums = await albumsQuery.ToListAsync();
            artists = await artistsQuery.ToListAsync();
        }


        public async Task<IActionResult> OnPostAsync(int hdnAlbumId)
        {
            Album album = await _db.albums.FirstOrDefaultAsync(aa => aa.AlbumId == hdnAlbumId);
            if (album == null) return NotFound(hdnAlbumId);
            _db.albums.Remove(album);
            await _db.SaveChangesAsync();
            return Redirect("~/Index");
        }
    }
}
