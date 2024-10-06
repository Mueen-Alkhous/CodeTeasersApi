using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace Presentation.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{

    private readonly CategoryRepository _repository;
    public CategoriesController(CategoryRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Category>>> Get()
    {
        var categories = await _repository.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Category>> Get(Guid id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
            return NotFound();
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] string title)
    {
        var category = new Category { Title = title };
        _repository.AddCategory(category);
        await _repository.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = category.Id }, category);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
            return NotFound();
        _repository.DeleteCategory(category);
        await _repository.SaveChangesAsync();
        return NoContent();
    }
}
