using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Presentation.Entities.View;


namespace Presentation.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{

    private readonly CategoryRepository _repository;
    private readonly IMapper _mapper;
    public CategoriesController(CategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryForView>>> Get()
    {
        var categories = await _repository.GetAllAsync();
        var categoriesToReturn = _mapper.Map<List<CategoryForView>>(categories);
        return Ok(categoriesToReturn);
    }

    [HttpGet("{title}")]
    public async Task<ActionResult<CategoryForView>> Get(string title)
    {
        var category = await _repository.GetByTitleAsync(title);

        if (category == null)
            return NotFound();

        var categoryToReturn = _mapper.Map<CategoryForView>(category);

        return Ok(categoryToReturn);
    }

    [HttpPost("{title}")]
    public async Task<ActionResult> Post(string title)
    {
        var category = new Category(title);

        var categoryToReturn = _mapper.Map<CategoryForView>(category);
        _repository.Add(category);
        await _repository.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { normalizedTilte = category.NormalizedTitle }, categoryToReturn);
    }

    [HttpDelete("{title}")]
    public async Task<ActionResult> Delete(string title)
    {
        var category = await _repository.GetByTitleAsync(title);

        if (category == null)
            return NotFound();

        _repository.Delete(category);
        await _repository.SaveChangesAsync();
        return NoContent();
    }
}
