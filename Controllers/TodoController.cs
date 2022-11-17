using System;
using Microsoft.AspNetCore.Mvc;
using todo_app_.net.Services;
using todo_app_.net.Models;

namespace todo_app_.net.Controllers;

[Controller]
[Route("api/[controller]")]
public class TodoController : Controller
{
    private readonly MongoDBService _mongoDBService;
    public TodoController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<List<Todo>> Get()
    {
        return await _mongoDBService.GetAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Todo todo)
    {
        await _mongoDBService.CreateAsync(todo);
        return CreatedAtAction(nameof(Get), new { id = todo.Id }, todo);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mongoDBService.DeleteAsync(id);
        return NoContent();
    }
}