using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Recordings.Queries.GetRecordings
{
    public class RecordingListVm
    {
        public IList<ConversationRecordingViewModel> recordList { get; set; } = new List<ConversationRecordingViewModel>();
    }
}
