using Application.ApplicationUser;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Identity
{
    //public class IdentityService : IIdentityService
    //{
    //    private readonly UserManager<ApplicationUser> _userManager;
    //    private readonly IMapper _mapper;
    //    private readonly IStoreDbContext _context;

    //    public IdentityService(UserManager<ApplicationUser> userManager, IMapper mapper, IStoreDbContext context)
    //    {
    //        _userManager = userManager;
    //        _mapper = mapper;
    //        _context = context;
    //    }

    //    public async Task<string> GetUserNameAsync(string userId)
    //    {
    //        var user = await _context.AspNetUsers.FirstAsync(u => u.Email == userId);

    //        if (user == null)
    //        {
    //            throw new UnauthorizedAccessException();
    //        }

    //        return user.UserName;
    //    }

    //    public async Task<ApplicationUserDto> CheckUserPassword(string email, string password)
    //    {
    //       // ApplicationUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
    //        var user = await _context.AspNetUsers.FirstAsync(u => u.Email == email);

    //        if (user != null &&  user.PasswordHash.Equals(password))
    //        {
    //            return _mapper.Map<ApplicationUserDto>(user);
    //        }

    //        return null;
    //    }

    //    public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
    //    {
    //        var user = new ApplicationUser
    //        {
    //            UserName = userName,
    //            Email = userName,
    //        };

    //        // var result = await _userManager.CreateAsync(user, password);
    //        return (Result.Success(), user.Id);
    //        // return (result.ToApplicationResult(), user.Id);
    //    }

    //    public async Task<bool> UserIsInRole(string userId, string role)
    //    {
    //        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

    //        return await _userManager.IsInRoleAsync(user, role);
    //    }

    //    public async Task<Result> DeleteUserAsync(string userId)
    //    {
    //        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

    //        if (user != null)
    //        {
    //            //return await DeleteUserAsync(user);
    //        }

    //        return Result.Success();
    //    }

    //    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    //    {
    //        //var result = await _userManager.DeleteAsync(user);

    //        //return result.ToApplicationResult();
    //        return Result.Success();
    //    }
    //}
}
