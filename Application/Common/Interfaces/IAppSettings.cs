using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IAppSettings
    {
        public bool GetBool(string key, bool defaultValue = false);
        public string GetConnection(string key, string defaultValue = "");
        public int GetInt32(string key, int defaultValue = 0);
        public long GetInt64(string key, long defaultValue = 0L);
        public string GetString(string key, string defaultValue = "");
        public T Get<T>(string key = null);
        public T Get<T>(string key, T defaultValue);
     
    }
}
