using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        ApplicationContext _dbContext;
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        public UserController(ApplicationContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _dbContext = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public List<AppUser> GetAllregisteredUser()
        {
            List<AppUser> users = _dbContext.Users.ToList();
            return users;
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWToken");
            _signInManager.SignOutAsync();
            return StatusCode(StatusCodes.Status200OK, "Logout successfully");
        }
    }
}