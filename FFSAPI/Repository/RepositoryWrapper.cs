using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using FFSAPI.Contracts;
using FFSAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FFSAPI.Repository
{
    public class RepositoryWrapper
    {

        //refernser till de generiska interfacen
        //behöver jag 
        private readonly IRepositoryBase<Studio> studioRepository;
        private readonly IRepositoryBase<Movie> movieRepository;
        private readonly IRepositoryBase<Trivia> triviaRepository;
        private readonly IRepositoryBase<Movie_Studio> movie_studioRepository;

        public RepositoryWrapper(IRepositoryBase<Studio> studioRepository, IRepositoryBase<Movie> movieRepository,
            IRepositoryBase<Trivia> triviaRepository,IRepositoryBase<Movie_Studio> movie_studioRepository)
        {
            this.studioRepository = studioRepository;
            this.movieRepository = movieRepository;
            this.triviaRepository = triviaRepository;
            this.movie_studioRepository = movie_studioRepository;

        }

        //-------------------
        //METODER FÖR MOVIE:
        //-------------------

        public async Task<Movie> GetMovieById(int id)
        {
            return await movieRepository.GetById(id);
        }

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await movieRepository.FindAll();
        }

        //create
        public async Task CreateMovie(Movie movie)
        {
           if (MovieContainsTitle(movie))
            {
                await movieRepository.Create(movie);
                await movieRepository.Save();
            }
           //else return throw new notimplementedexception
        }
        
        //update amount for Movie
        public async Task UpdateAmount (int id, int newAmount)
        {
            var movie = await movieRepository.GetById(id);
            //if (movie == null) /*{ throw new NotImplementedException(); }*/
            movie.Amount = newAmount;
            //movieRepository.Update(movie);
            await movieRepository.Save();
        }


        //add bool to check if Movie is available to rent
        private  async Task<bool> IsAvailable(int id)
        {
            var movie = await GetMovieById(id);
            if (movie == null) { return false; }
            var rentedMovies = await movie_studioRepository.FindByCondition(m => m.IsLent == true && m.MovieId == id);
            List<Movie_Studio> asList = new List<Movie_Studio>();
            foreach (var item in rentedMovies)
            {
                asList.Add(item);
            }
            var numberOfRentedMovies = asList.Count();
            var maxAvailable = movie.Amount;
            if (numberOfRentedMovies < maxAvailable) { return true; }
            else return false;
        }

        //bool to check if the data is ok
        public bool MovieContainsTitle(Movie movie)
        {
            if (movie.Title != null) { return true; }
            else return false;
        }


        //-------------------
        //METODER FÖR STUDIO:
        //-------------------

        public async Task<Studio> GetStudioById(int id)
        {
            return await studioRepository.GetById(id);
        }

        public async Task<IEnumerable<Studio>> GetAllStudios()
        {
            return await studioRepository.FindAll();
        }

        public async Task CreateStudio(Studio studio)
        {
            await studioRepository.Create(studio);
            await studioRepository.Save();
        }

        //update with exception handler
        public async Task DeleteStudio(int id)
        {
            await studioRepository.Delete(id);
            await studioRepository.Save();
        }

        public async Task UpdateStudio(int id, string name, string location)
        {
            var studio = await studioRepository.GetById(id);
            studio.Name = name;
            studio.Location = location;
            studioRepository.Update(studio);
            await studioRepository.Save();
        }

        //create objekt till Movie_Studio

        //lägga till film för uthyrning

        //Is Movie available? kontrollerar tillräckligt med filmer att hyra ut

        //public async Task<ActionResult<Movie_Studio>> CreateNewRental(Movie_Studio movietoberented)
        //{
        //    var movie =  movieRepository.FindByCondition(m => m.Id == movietoberented.Id);
        //    //var movie = await _context.Movies.FindAsync(movietoberented.MovieId);
        //    if (movie == null) { return FoundResult(); }

        //    //var rentedMovies = _context.MoviesInStudios.Where(m => m.IsLent == true && m.MovieId == movietoberented.MovieId);

        //    //var numberOfRentedMovie = rentedMovies.Count();
        //    //var maxAmount = movie.Amount;

        //    //if (numberOfRentedMovie < maxAmount)
        //    //{
        //    //    _context.MoviesInStudios.Add(movietoberented);
        //    //    await _context.SaveChangesAsync();
        //    //    return (CreatedAtAction(nameof(GetMovieInRent), new { id = movietoberented.Id }, movietoberented));
        //    //}

        //    else { return BadRequest(); }

        //     movie_studioRepository.Save();

        //}




        //logiken för att 


        //var ska jag spara till databasen, från mitt repository?
        //sätt i så fall denna metod där
        //public void Save()
        //{

        //}
    }
}
