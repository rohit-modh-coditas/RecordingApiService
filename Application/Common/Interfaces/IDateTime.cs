using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IDateTime
    {
        DateTime Now { get; }
        DateTime StartTime { get; set; }
        DateTime CreateDate { get; set; }
        TimeZoneInfo LocalTimeZone { get; set; }
    }
}
