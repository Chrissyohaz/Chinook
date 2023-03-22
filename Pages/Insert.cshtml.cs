using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ChinookContext;
using ChinookEntities;

namespace Project.Pages
{
    public class Insert : PageModel
    {
        public string Heading { get; set; }

        public void OnGet()
        {
            Heading = "Chinook Albums - Insert";

        }
        public IActionResult OnPost()
        {
            Album insAlbum = new Album() {
                Title = Request.Form["albTitle"],
                ArtistId = int.Parse(Request.Form["artID"]),
            };
            ChinookDatabase db = new ChinookDatabase();
            db.albums.Add(insAlbum);
            db.SaveChanges();
            return Redirect("~/Index");
        }
    }
}