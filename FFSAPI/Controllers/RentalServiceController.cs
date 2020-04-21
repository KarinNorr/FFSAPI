using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FFSAPI.Models;


namespace FFSAPI.Controllers
{
    [Route("api/rentalservice")]
    [ApiController]
    public class RentalServiceController : ControllerBase
    {
        private readonly MyDbContext _context;

        public RentalServiceController(MyDbContext context)
        {
            _context = context;
        }

        //hämtar en utlåning med specifikt id
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie_Studio>> GetMovieInRent(int id)
        {
            var movieInRent = await _context.MoviesInStudios.FindAsync(id);
            if (movieInRent == null) { return NotFound(); }

            return movieInRent;
        }

        //lägger till film för uthyrning
        //kontrollerar tillräckligt med filmer att hyra ut
        [HttpPost]
        public async Task<ActionResult<Movie_Studio>> PostRentedMovie(Movie_Studio movietoberented)
        {
            var movie = await _context.Movies.FindAsync(movietoberented.MovieId);
            if (movie == null) { return NotFound(); }
           
            var rentedMovies = _context.MoviesInStudios.Where(m => m.IsLent == true && m.MovieId == movietoberented.MovieId);

            var numberOfRentedMovie = rentedMovies.Count();
            var maxAmount = movie.Amount;

            if (numberOfRentedMovie < maxAmount)
            {
                _context.MoviesInStudios.Add(movietoberented);
                await _context.SaveChangesAsync();
                return (CreatedAtAction(nameof(GetMovieInRent), new { id = movietoberented.Id }, movietoberented));
            }

            else { return BadRequest(); }

        }

        //returnerar film, isLent = false
        [HttpPut("{rentId}/returnMovie")]
        public async Task<IActionResult> PutToReturnMovie(int rentId)
        {
            var lendedMovie = await _context.MoviesInStudios.FindAsync(rentId);
            if (lendedMovie == null) { return NotFound(); }
            lendedMovie.IsLent = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //hämtar alla filmer med specifik filmstudio 
        [HttpGet("{studioId}/getrentedmovies")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetRentedMovies(int studioId)
        {
            var studio = await _context.Studios.FindAsync(studioId);
            if (studio == null) { return NotFound(); }

            var rentedMovies = await _context.MoviesInStudios.Where(m => m.IsLent == true && m.StudioId == studio.Id).ToListAsync();
            var movieResults = new List<Movie>();

            foreach (var rentedMovie in rentedMovies)
            {
                var movie = _context.Movies.Where(m => m.Id == rentedMovie.MovieId);
                movieResults.AddRange(movie);
            }

            return movieResults;
        }
        //returnerar etikett xml
        [HttpGet("etikett/{rentId}/{movieId}/")]
        [Produces("application/xml")]
        public async Task<ActionResult<EtikettData>> GetEtkett(int rentId, int movieId)
        {
            var rentedMovie = await _context.MoviesInStudios.FindAsync(rentId);
            if (rentedMovie == null || rentedMovie.IsLent == false || rentedMovie.MovieId != movieId) { return NotFound(); }
            
            var movie = await _context.Movies.FindAsync(movieId);
            if (movie == null) { return NotFound(); }

            var studio = await _context.Studios.FindAsync(rentedMovie.StudioId);

            return new EtikettData { Filmnamn = movie.Title, Ort = studio.Location }; 
        }

        private ActionResult<Movie_Studio> NotImplementedException()
        {
            throw new NotImplementedException();
        }
    }
}

