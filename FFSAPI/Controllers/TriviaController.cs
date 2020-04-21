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
    [Route("api/trivias")]
    [ApiController]
    public class TriviaController : ControllerBase
    {
        private readonly MyDbContext _context;

        public TriviaController(MyDbContext context)
        {
            _context = context;
        }

        //hämtar en trivia med ett specifikt id
        [HttpGet("{id}")]
        public async Task<ActionResult<Trivia>> GetTrivia(int id)
        {
            var trivia = await _context.Trivias.FindAsync(id);
            if (trivia == null) { return NotFound(); }

            return trivia;
        }

        //lägger upp en trivia
        [HttpPost]
        public async Task<ActionResult<Trivia>> PostTrivia(Trivia trivia)
        {
            _context.Trivias.Add(trivia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTrivia), new { id = trivia.Id }, trivia);
        }


        //rapportera in betyg till Trivia
        [HttpPut("{id}/{rating}")]
        public async Task<IActionResult> PutTrivia(int id, int rating)
        {
            var trivia = await _context.Trivias.FindAsync(id);
            if (trivia == null) { return NotFound(); }

            trivia.Rating = rating;
            await _context.SaveChangesAsync();
            return NoContent();

        }

        //tar bort omdöme från Trivia
        [HttpPut("{id}/deletecomment")]
        public async Task<IActionResult> DeleteCommentInTrivia(int id)
        {
            var trivia = await _context.Trivias.FindAsync(id);
            if (trivia == null) { return NotFound(); }

            trivia.Comment = "Bortagen kommentar";
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private IActionResult InvalidOperationException()
        {
            throw new NotImplementedException();
        }

        private IActionResult NotImplementedException()
        {
            throw new NotImplementedException();
        }
    }
}
