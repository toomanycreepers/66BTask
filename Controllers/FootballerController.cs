using Microsoft.AspNetCore.Mvc;
using WebFootballers.AppServices;
using WebFootballers.Models.DTOs;

namespace WebFootballers.Controllers
{
    public class FootballerController : Controller
    {
        private readonly FootballerService _footballerService;

        public FootballerController(FootballerService footballerService)
        {
            _footballerService = footballerService;
        }

        [HttpPost]
        public async Task<IActionResult> AddFootballer([FromForm] FootballerCreationDTO dto)
        {
            if (ModelState.IsValid)
            {
                try 
                {
                    await _footballerService.AddFootballer(dto);
                    return Ok();
                }
                catch (ArgumentException e) 
                {
                    return BadRequest(e.Message);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AlterFootballer([FromForm] FootballerDTO dto)
        {
            if (ModelState.IsValid)
            {
                try 
                {
                    await _footballerService.AlterFootballer(dto);
                    return RedirectToAction("FootballerList", "Home");
                }
                catch(ArgumentException e)
                {
                    return BadRequest(e.Message);
                }
            }
            return BadRequest();
        }
    }
}
