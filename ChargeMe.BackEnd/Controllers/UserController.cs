using ChargeMe.BackEnd.Data;
using ChargeMe.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

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


    //[HttpPost]
    //[Route("register")]
    //public async Task<ActionResult> Register(string email, string password, string city, string address, string firstName, string lastName)
    //{
    //    try
    //    {
    //        var regUser = new User { Email = email, City = city, Address = address, UserName = email, FirstName = firstName, LastName = lastName };
    //        var result = await _usrmanager.CreateAsync(regUser, password);


    //        if (result.Succeeded)
    //        {
    //            var dbUser = _context.Users.FirstOrDefault(x=> x.Email == email);

    //            return Ok(dbUser);
    //        }

    //        return BadRequest("Impossibile creare l'utente");
    //    }
    //    catch (Exception e)
    //    {
    //        return BadRequest(e.Message);
    //    }
    //}

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> Register([FromBody]UserDto json)
    {
        try
        {
            Console.WriteLine(json);

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


public class UserDto
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public string city { get; set; }
    public string address { get; set; }
}