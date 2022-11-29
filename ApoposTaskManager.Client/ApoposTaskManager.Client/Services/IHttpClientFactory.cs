using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ApoposTaskManager.Client.Services
{
    public interface IHttpClientFactory
    {
        string Jwt
        {
            get;
            set;
        }

        HttpClient Create();
    }
}
