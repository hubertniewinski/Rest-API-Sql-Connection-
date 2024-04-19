using Microsoft.AspNetCore.Mvc;
using RestAPI.Models;
using RestAPI.Services;

namespace RestAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController(IAnimalsService animalsService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IEnumerable<AnimalDto>> GetAnimalsAsync([FromQuery] string orderBy = "name", CancellationToken cancellationToken = default) 
        => await animalsService.GetAnimalsAsync(orderBy, cancellationToken);

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAnimalAsync([FromBody] CreateAnimalDto dto, CancellationToken cancellationToken)
    {
        await animalsService.CreateAnimalAsync(dto, cancellationToken);
        return Created();
    }

    [HttpPut("{idAnimal}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAnimalAsync([FromRoute] int idAnimal, [FromBody] CreateAnimalDto dto, CancellationToken cancellationToken)
    {
        await animalsService.UpdateAnimalAsync(idAnimal, dto, cancellationToken);
        return Ok();
    }

    [HttpDelete("{idAnimal}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteAnimalAsync([FromRoute] int idAnimal, CancellationToken cancellationToken)
    {
        await animalsService.DeleteAnimalAsync(idAnimal, cancellationToken);
        return NoContent();
    }
}