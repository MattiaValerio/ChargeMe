using ChargeMe.BackEnd.Data;
using ChargeMe.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ChargeMe.BackEnd.Controllers;

[Route("api/")]
[ApiController]
public class UserController : ControllerBase
{
    private UserManager<User> _usrmanager;
    private SignInManager<User> _signInManager;
    private DataContext _context;

    public UserController(UserManager<User> userManager, SignInManager<User> signInManager, DataContext context)
    {
        _usrmanager = userManager;
        _signInManager = signInManager;
        _context = context;
    }


    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Post(string email, string password, string city, string address, string firstName, string lastName)
    {
        try
        {
            var regUser = new User { Email = email, City = city, Address = address, UserName = email, FirstName = firstName, LastName = lastName };
            var result = await _usrmanager.CreateAsync(regUser, password);


            if (result.Succeeded)
            {
                var dbUser = _context.Users.FirstOrDefault(x=> x.Email == email);

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
