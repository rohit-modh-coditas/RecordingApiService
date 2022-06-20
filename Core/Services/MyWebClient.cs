using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class MyWebCient : WebClient
    {
        private readonly IConfiguration _config;
        public MyWebCient(IConfiguration config) {
            this._config = config;
        }
        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest w = base.GetWebRequest(uri);
            w.Timeout = Convert.ToInt32(_config.GetValue<string>("CASRecordingRequestTimeOut"));
            return w;
        }
    }
}
