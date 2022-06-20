﻿using Application.Common.Notifications.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}
