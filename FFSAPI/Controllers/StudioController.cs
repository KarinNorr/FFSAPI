using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FFSAPI.Models;
using FFSAPI.Repository;

namespace FFSAPI.Controllers
{
    [Route("api/studios")]
    [ApiController]
    public class StudioController : ControllerBase
    {
        private readonly RepositoryWrapper _wrapper; 
        private readonly MyDbContext _context;

        public StudioController(RepositoryWrapper wrapper)
        {
            _wrapper = wrapper;
        }

        //GET: api/studios/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Studio>> GetStudio(int id)
        {
            var studio = await _wrapper.GetStudioById(id);
            if (studio == null) { return NotFound(); }
            return studio;
        }

        //POST: api/Studios
        [HttpPost]
        public async Task PostStudio(Studio studio)
        {
            await _wrapper.CreateStudio(studio);
        }

        //DELETE: api/studios/id
        [HttpDelete("{id}")]
        public async Task DeleteStudio(int id)
        {
            await _wrapper.DeleteStudio(id);
        }

        //PUT: api/studios/id
        [HttpPut("{id}/newname/{name}/newlocation/{location}")]
        public async Task PutStudio(int id, string newname, string newlocation)
        {
            await _wrapper.UpdateStudio(id, newname, newlocation);
        }
    }
}
