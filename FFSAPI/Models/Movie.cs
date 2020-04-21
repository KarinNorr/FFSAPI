using System;
using System.Collections.Generic;
namespace FFSAPI.Models
{
    public class Movie
    {
        public int Id { get; set;  }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Amount { get; set; }

        //listan används inte
        public ICollection<Trivia> Trivias { get; set; }
        
    }
}
