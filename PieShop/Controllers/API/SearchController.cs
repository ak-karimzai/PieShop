using Microsoft.AspNetCore.Mvc;
using PieShop.Models;

namespace PieShop.Controllers.API;

[Route("api/search")]
[ApiController]
public class SearchController : ControllerBase
{
    private readonly IPieRepository _pieRepository;

    public SearchController(IPieRepository pieRepository)
    {
        _pieRepository = pieRepository;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var allPies = _pieRepository.AllPies;
        return Ok(allPies);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var pie = _pieRepository.GetPieById(id);
        
        if (pie == null)
            return NotFound();

        return Ok(pie);
    }

    [HttpPost]
    public IActionResult SearchPies([FromBody] string searchQuery)
    {
        IEnumerable<Pie> pies = new List<Pie>();
        
        if (!string.IsNullOrEmpty(searchQuery))
        {
            pies = _pieRepository
                .SearchPies(searchQuery);
        }

        return new JsonResult(pies);
    }
}