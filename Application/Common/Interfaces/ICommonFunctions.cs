using System.Collections.Generic;

namespace Application.Common.Interfaces
{
    public interface ICommonFunctions
    {
        bool IsDualConsentRecording(string targetPhoneNumber, Dictionary<string, string> areaPhoneCodes);
    }
}
