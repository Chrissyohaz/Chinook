using System;
using System.Collections.Generic;


namespace ChinookEntities
{
    public class Album
    {
        public int AlbumId { get; set; } // Primary Key from chinook table albums
        public string Title { get; set; }
        public int ArtistId { get; set; }

        // Navigation Property
        public Artist artists { get; set;}
    }

    public class Artist {
        public int ArtistId { get; set;} // Primary Key from chinook table albums
        public string Name { get; set;}

        // Navigation property and assumes one artist can have many albums
        public ICollection<Album> albums { get; set;}
    }

}