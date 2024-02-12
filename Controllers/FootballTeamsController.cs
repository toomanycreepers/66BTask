using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebFootballers.Data;
using WebFootballers.Models.Entities;

namespace WebFootballers.Controllers
{
    [Route("Football/Teams")]
    public class FootballTeamsController : Controller
    {
        private WebFootballersDbContext db = new WebFootballersDbContext();

        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> GetRelevantTeams()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = db.FootballTeams.Where(p => p.Name.Contains(term)).Select(p => p.Name).ToList();
                return Ok(names);
            }
            catch
            {
                return BadRequest();
            }
        }
    
    }
}
