using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IRecordingUtilityService
    {
        public bool FetchCdrRecording(IDateTime dateTime,IConfiguration _config, string called1, string called2, string called3, int LeadTransitId, string recordSavePath, int TimeBuffer, int TimeShift);
        public void MoveRecordingToGCS(string recordingBasePath, IConfiguration _config);
    }
}
