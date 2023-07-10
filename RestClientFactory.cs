using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace TestAPI
{
    public class RestClientFactory
    {
        private const string BaseUrl = "https://automationexercise.com/api";
        
        public static RestClient CreateClient(){
            RestClient client = new RestClient(BaseUrl);
            return client;
        }
    }
}
