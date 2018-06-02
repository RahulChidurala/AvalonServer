using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;

namespace AvalonRestApi.Controllers.Mock
{
    public class MockLoginController : ApiController
    {

        // Login using JSON     
        public LoginResponse Post([FromBody]dynamic value)
        {
            var response = new LoginResponse();
            if(value == null)
            {
                response.success = false;
                response.message = "No parameters";

                return response;
                //return JsonConvert.SerializeObject(response);
                //return "{ 'success': false, 'exception': 'No parameters!' }";
            }

            //var valueDict = (IDictionary<string, Object>) value;
            //if(valueDict.ContainsKey("username") && valueDict.ContainsKey("password"))
            //{
            //    return "{ 'success': false, 'exception':'Invalid parameters!' }";
            //}

            var username = value.username.Value;
            var password = value.password.Value;

            if (username == "rahul" && password == "password")
            {
                response.success = true;
                return response;
                //return JsonConvert.SerializeObject(response);                
                //return "{ 'success': true }";
            }

            response.success = false;
            response.message = "Invalid credentials!";
            return response;
            //return JsonConvert.SerializeObject(response);
            //return "{ 'success': false, 'exception':'Invalid credentials' }";
        }
    }

    public struct LoginResponse
    {
        public bool success;
        public string message;
    }
}