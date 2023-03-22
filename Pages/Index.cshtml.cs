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
        public void OnGet()
        {

            Heading = "Chinook Music Store";

            ChinookDatabase db = new ChinookDatabase();
            albums = db.albums.Include(a => a.artists).ToList();
            artists = db.artists.ToList();

        }



        public IActionResult OnPost(int hdnAlbumId)
        {
            ChinookDatabase db = new ChinookDatabase();
            var album = db.albums.Where(aa => aa.AlbumId == hdnAlbumId).FirstOrDefault(); if (album == null) return NotFound(hdnAlbumId); album.Title = $"*{album.Title}*"; db.SaveChanges(); return Redirect("~/Index");
        }


    }
}
