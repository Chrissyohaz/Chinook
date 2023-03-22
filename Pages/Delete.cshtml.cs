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
    public class Delete : PageModel
{
    private readonly ChinookDatabase _db;
    
    public Delete(ChinookDatabase db)
    {
        _db = db;
    }
    
    public IActionResult OnPost()
    {
        Album delAlbum = _db.albums.Single(ai => ai.AlbumId == int.Parse(Request.Form["hdnAlbumId"]));
        _db.albums.Remove(delAlbum);
        _db.SaveChanges();
        return Redirect("~/Index");
    }
}

}