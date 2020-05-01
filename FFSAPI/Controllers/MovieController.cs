using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FFSAPI.Models;
using FFSAPI.Contracts;
using FFSAPI.Repository;

namespace FFSAPI.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly RepositoryWrapper _wrapper;

        public MovieController(RepositoryWrapper wrapper)
        {
            _wrapper = wrapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _wrapper.GetMovieById(id);
            return movie;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            var movies = await _wrapper.GetAllMovies();
            return Ok(movies);
        }

        //POST: api/Movies
        [HttpPost]
        public async Task PostMovie(Movie movie)
        {
            await _wrapper.CreateMovie(movie);
        }

        //PUT: api/movies/5/newamount/10
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //Todo: Fix exception handling 
        [HttpPut("{id}/newamount/{newAmount}")]
        public void PutNewAmount(int id, int newAmount)
        {
             _wrapper.UpdateAmount(id, newAmount);
        }

        //private bool MovieExists(int id)
        //{
        //    return _context.Movies.Any(e => e.Id == id);
        //}
    }
}
