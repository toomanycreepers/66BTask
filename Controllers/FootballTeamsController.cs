using Microsoft.AspNetCore.Mvc;
using WebFootballers.AppServices;

namespace WebFootballers.Controllers
{
    [Route("Football/Teams")]
    public class FootballTeamsController : Controller
    {
        private readonly FootballTeamService _service;

        public FootballTeamsController(FootballTeamService footballTeamsService)
        {
            _service = footballTeamsService;
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<string>>> GetRelevantTeams()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var names = await _service.GetRelevantTeamNames(term);
                return Ok(names);
            }
            catch(ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    
    }
}
