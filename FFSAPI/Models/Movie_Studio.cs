using System;
namespace FFSAPI.Models
{
    public class Movie_Studio
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int StudioId { get; set; }
        public bool IsLent { get; set; } = true;

        public Movie Movie { get; set; }
        public Studio Studio { get; set; }
    }
}
