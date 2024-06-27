using ChargeMe.BackEnd.Data;
using ChargeMe.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using ChargeMe.Shared.Dtos;

namespace ChargeMe.BackEnd.Controllers;

[Route("api/")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _usrmanager;
    private SignInManager<User> _signInManager;
    private readonly DataContext _context;

    public UserController(UserManager<User> userManager, SignInManager<User> signInManager, DataContext context)
    {
        _usrmanager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> Register([FromBody]UserDto json)
    {
        try
        {
            var userdto = json;
            
            var regUser = new User { Email = userdto.email, City = userdto.city, Address = userdto.address, UserName = userdto.email, FirstName = userdto.firstName, LastName = userdto.lastName };
            var result = await _usrmanager.CreateAsync(regUser, userdto.password);


            if (result.Succeeded)
            {
                var dbUser = _context.Users.FirstOrDefault(x => x.Email == userdto.email);

                return Ok(dbUser);
            }

            return BadRequest("Impossibile creare l'utente");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}