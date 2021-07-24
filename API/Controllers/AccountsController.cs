using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Dtos;
using API.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountsController(IMapper mapper, UserManager<IdentityUser> userManager, ITokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
        }


        public async Task<bool> UserNameExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToUpper());
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<StaffDto> staffDtos = new List<StaffDto>();
            
            var users = await _userManager.Users
                .ToListAsync();

            foreach (var user in users.ToList())
            {
                var role = await _userManager.GetRolesAsync(user);
                var staffDto = new StaffDto()
                {
                    Role = role[0],
                    UserId = user.Id,
                    Username = user.UserName
                };
                staffDtos.Add(staffDto);
            }

            staffDtos = staffDtos.OrderBy(x => x.Role).ToList();
            
            return Ok(staffDtos);
        } 
        
        [HttpPost("register")]
        public async Task<ActionResult<AccountDto>> Register([FromBody] RegistrationDto registrationDto)
        {
            // If model state is invalid
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
        
            // if username exists
            if (await UserNameExists(registrationDto.UserName))
            {
                return BadRequest("Username is taken");
            }
            
            // mapping Dto to IdentityUser
            var user = _mapper.Map<IdentityUser>(registrationDto);
            
            // adding to ASPNETUSERS table
            var result = await _userManager.CreateAsync(user, registrationDto.Password);
            
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                
                return StatusCode(500);
            }
        
            // adding to ASPNETUSERROLES table
            var roleResult = await _userManager.AddToRoleAsync(user, "Operator");
        
            if (!roleResult.Succeeded) return BadRequest(result.Errors);
        
            return new AccountDto
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AccountDto>> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByNameAsync(loginDto.UserName);

            if (user == null)
            {
                return Unauthorized("Invalid username");
            }

            if (await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                var role = await _userManager.GetRolesAsync(user);
                return new AccountDto
                {
                    UserName = user.UserName,
                    // UserRole = role[0].ToLower(), //get the name of 1st role
                    Token = await _tokenService.CreateToken(user)
                };
            }
            
            return StatusCode(401, "Credentials not found!");
        }

        [AllowAnonymous]
        [HttpDelete("{userId}")]
        public async Task<ActionResult> Delete(string userId)
        {
            // Console.WriteLine(userId);
            
            if (ModelState.IsValid)
            {
                if (userId == null)
                {
                    return BadRequest();
                }

                var user = await _userManager.FindByIdAsync(userId);

                if (user != null)
                {
                    IdentityResult result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok(new {
                                message = "Successfully deleted user"
                        });
                    }
                    else
                    {
                        return StatusCode(500);
                    }
                }
            }

            return NotFound();
        }
    }
}
