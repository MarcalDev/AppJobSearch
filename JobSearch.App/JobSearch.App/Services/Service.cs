using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace JobSearch.App.Services
{
    public abstract class Service
    {
        protected HttpClient _client;
        protected string BaseApiUrl = "http://localhost:46781";


        public Service()
        {
            _client = new HttpClient();
        }
    }
}
