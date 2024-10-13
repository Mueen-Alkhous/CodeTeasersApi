using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubmissionsController : ControllerBase
{
    // GET: api/<SubmissionsController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<SubmissionsController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<SubmissionsController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<SubmissionsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<SubmissionsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
