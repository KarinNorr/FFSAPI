using System;
namespace FFSAPI.Models
{
    public class Trivia
    {
        public int Id { get; set; }
        private int rating; 
        public int Rating 
        {
            get { return rating; }
            set
            {
                if (value <= 5 && value > 0) { rating = value; }
                else throw new Exception("Unvalid rating range");
            }
        }
        public string Comment { get; set; }

        public int MovieId { get; set; }
        public int StudioId { get; set; }

        public Movie Movie { get; set; }
        public Studio Studio { get; set; }

    }
}
