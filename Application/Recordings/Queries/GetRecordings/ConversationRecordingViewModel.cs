using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Recordings.Queries.GetRecordings
{
    public class ConversationRecordingViewModel:IMapFrom<TCall>
    {
        public DateTime? LeadCatchTime { get; set; }
        public string ContactTel1 { get; set; }
        public DateTime? CallSendTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string ContactTel2 { get; set; }
        public string ContactTel3 { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string campaignName { get; set; }
        public string companyName { get; set; }
        public string disposition { get; set; }
        public DateTime createDate { get; set; }
        public DateTime? hotTransferTime { get; set; }
        public int LeadTransitId { get; set; }
        public Nullable<int> PrimaryNumberIndex { get; set; }
        public Nullable<int> TalkTime { get; set; }
        public string pitcherCompanyName { get; set; }
        public bool? IsSFTP { get; set; }
        public bool? IsDualConsent { get; set; }
    }
}
