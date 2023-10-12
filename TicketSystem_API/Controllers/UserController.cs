using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketSystem_API.Models;
using TicketSystem_API.services;
using Newtonsoft.Json;

namespace TicketSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] User newUser)
        {
            try
            {
                var registeredUser = await userService.RegisterUser(newUser);
                // Create a JSON object (an anonymous object in this example)
                var jsonObject = new
                {
                   registrationMessage="Registration successful."
                };

                // Serialize the JSON object to a JSON string
                var jsonResult = JsonConvert.SerializeObject(jsonObject);
                return Ok(jsonResult);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await userService.AuthenticateUser(loginRequest.email, loginRequest.password);

            if (user != null)
            {
                // User is authenticated; you can return user details or a token here
                return Ok(user);
            }

            return Unauthorized("Invalid credentials");
        }

        // Implement Register and Authenticate actions as previously shown

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] User updatedUser)
        {
            try
            {
                var user = await userService.UpdateUser(id, updatedUser);
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deleted = await userService.DeleteUser(id);

            if (deleted)
            {
                return NoContent();
            }

            return NotFound("User not found");
        }
        [HttpPut("isActive")]
        public async Task<IActionResult> UpdateIsActive(string nic, [FromBody] IsActivate body)
        {
            try
            {
                var user = await userService.UpdateUserIsActive(nic, body.isActivate);
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}