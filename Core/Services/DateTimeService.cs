using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class DateTimeService : IDateTime
    {

        public DateTime Now => DateTime.Now;

        // public DateTime StartTime => new();

        DateTime IDateTime.StartTime{ get; set; }

        public DateTime CreateDate { get; set; }
        public TimeZoneInfo LocalTimeZone { get; set; } = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
    }
}
