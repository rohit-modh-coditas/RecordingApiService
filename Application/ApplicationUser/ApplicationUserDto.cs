using Application.Common.Mappings;
using AutoMapper;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ApplicationUser
{
    public class ApplicationUserDto:IMapFrom<AspNetUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AspNetUser, ApplicationUserDto>();
        }
    }
}
