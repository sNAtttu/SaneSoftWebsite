using SaneSoftWebsiteBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace SaneSoftWebsiteBot.Services
{
    public static class AuthenticationService
    {
        private static readonly string AppID = "lsexY7Hw-cM.cwA.6_s.bOEZlT5x17j4Z-p6l_xGzbh-rcAVEKeQpkGDT9L2PgQ";
        private static readonly string TokenUrl = "https://directline.botframework.com/v3/directline/conversations";
        public static AuthenticationToken GetAuthenticationToken()
        {
            WebClient client = new WebClient();
            byte[] bitti = new byte[0];
            client.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + AppID);
            var response = client.UploadData(TokenUrl, bitti);
            var responseString = Encoding.UTF8.GetString(response);
            return JsonConvert.DeserializeObject<AuthenticationToken>(responseString);
        }
    }
}