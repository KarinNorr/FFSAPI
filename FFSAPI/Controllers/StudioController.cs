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
    [Route("api/studios")]
    [ApiController]
    public class StudioController : ControllerBase
    {
        private readonly MyDbContext _context;

        public StudioController(MyDbContext context)
        {
            _context = context;

        }

        //hämtar en studio med specifikt id
        [HttpGet("{id}")]
        public async Task<ActionResult<Studio>> GetStudio(int id)
        {
            var studio = await _context.Studios.FindAsync(id);
            if (studio == null)
            {
                return NotFound();
            }
            return studio;
        }

        //lägger till en ny studio
        [HttpPost]
        public async Task<ActionResult<Studio>> PostStudio(Studio studio)
        {
            _context.Studios.Add(studio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudio), new { id = studio.Id }, studio);
        }

        //byter namn och ort på en studio, tar in id, nytt namn och ny ort
        [HttpPut("{id}/newname/{name}/newlocation/{location}")]
        public async Task<IActionResult> PutStudio(int id, string name, string location)
        {
            var studioToChange = await _context.Studios.FindAsync(id);
            if (studioToChange == null) { return NotFound(); }

            studioToChange.Location = location;
            studioToChange.Name = name;
            _context.Studios.Update(studioToChange);
            await _context.SaveChangesAsync();

            return NoContent();
         }

        //tar bort en studio
        [HttpDelete("{id}")]
        public async Task<ActionResult<Studio>> DeleteStudio(int id)
        {
            var studioToDelete = await _context.Studios.FindAsync(id);
            if (studioToDelete == null) { return NotFound(); }

            _context.Studios.Remove(studioToDelete);
            await _context.SaveChangesAsync();
            return studioToDelete;
        }
    }
}
