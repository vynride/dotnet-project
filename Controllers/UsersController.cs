using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: api/users
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<User>> GetAll()
    {
        return Ok(_userService.GetAll());
    }

    // GET: api/users/1
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<User> GetById(int id)
    {
        var user = _userService.GetById(id);
        return user is null ? NotFound(new { message = $"User {id} was not found." }) : Ok(user);
    }

    // POST: api/users
    [HttpPost]
    [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public ActionResult<User> Create([FromBody] User user)
    {
        if (_userService.EmailExists(user.Email))
            return Conflict(new { message = "A user with this email already exists." });

        var created = _userService.Create(user);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/users/1
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult Update(int id, [FromBody] User user)
    {
        if (_userService.GetById(id) is null)
            return NotFound(new { message = $"User {id} was not found." });

        if (_userService.EmailExists(user.Email, id))
            return Conflict(new { message = "Another user already uses this email." });

        return _userService.Update(id, user) ? NoContent() : NotFound();
    }

    // DELETE: api/users/1
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        return _userService.Delete(id)
            ? NoContent()
            : NotFound(new { message = $"User {id} was not found." });
    }
}
