﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        ClaimsPrincipal? ClaimsPrincipal { get; }
       string ? AuthKey{ get; }
    }

}
