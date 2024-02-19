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
        public IActionResult AddFootballer([FromForm] FootballerCreationDTO dto)
        {
            if (ModelState.IsValid)
            {
                try 
                {
                    _footballerService.AddFootballer(dto);
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
        public IActionResult AlterFootballer([FromForm] FootballerDTO dto)
        {
            if (ModelState.IsValid)
            {
                try 
                {
                    _footballerService.AlterFootballer(dto);
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
