﻿using Application.ApplicationUser.Queries.GetToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        bool ValidateToken();
        string CreateJwtSecurityToken(UserAuthQuery userAuth);
    }
}
