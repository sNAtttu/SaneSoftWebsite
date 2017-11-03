using SaneSoftWebsiteBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using SaneSoftWebsiteBot.Services;
using System.Net;
using Microsoft.Bot.Connector;
using System.Text;
using System.Web.Http.Controllers;
using System.Threading.Tasks;
using SaneSoftWebsiteBot.Helpers;
using System.Web.Http.Cors;

namespace SaneSoftWebsiteBot.Controllers
{
    [Route("api/commands")]
    public class WebsiteMessageController : ApiController
    {
        private readonly static string BotId = "9974177d-01c4-45cd-a919-8ad3b2a66428";
        private readonly static string SubscriptionKey = "f89c151033584055bbf97d5d415dfb07";

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public WebsiteMessage Post([FromBody]WebsiteMessage message)
        {
            var luisResponse = GetLuisResponse(message);
            WebsiteMessage messageToReturn = CreateAnswerBasedOnLuis(luisResponse);
            messageToReturn.Command = message.Command;
            return messageToReturn;
        }

        private WebsiteMessage CreateAnswerBasedOnLuis(LuisResponseMessage luisResponse)
        {
            WebsiteMessage messageToReturn = new WebsiteMessage();
            switch (luisResponse.TopScoringIntent.Intent.ToLower())
            {
                case "email":
                    messageToReturn.Answer = "Email address: sanesofta@gmail.com";
                    break;
                case "phone":
                    messageToReturn.Answer = "SaneSoftware does not have phone capability.";
                    break;
                case "address":
                    messageToReturn.Answer = "Headquarters: Tiimalasintie 1, Finland, Espoo";
                    break;
                case "businessid":
                    messageToReturn.Answer = "Company Id (Y-tunnus): 2693596-3";
                    break;
                case "sql inject":
                    messageToReturn.Answer = "Joakim, are you trying SQL Injection on my website?";
                    break;
                case "technology":
                    messageToReturn.Answer = $"Sane software is specialized in .NET, C#, AR / VR projects. This site has been done using React <3 ASP.NET Core";
                    break;
                case "version":
                    messageToReturn.Answer = $"Current version: 'Early access pre-alpha stage 0.0.0.1'";
                    break;
                case "time":
                    messageToReturn.Answer = $"Server time is {DateTime.Now.ToString()}. The Bot doesn't care about your timezone :)";
                    break;
                case "help":
                    messageToReturn.Answer = string.Format("Available options: {0}\n\n",
                        string.Join("\n\n", Constants.CommandOptions));
                    break;
                case "none":
                    messageToReturn.Answer = $"I'm the awesome bot who works for Sane Software. Though I'm quite smart I don't know what you mean. \n\n Detected intent: " + luisResponse.TopScoringIntent.Intent;
                    break;
                default:
                    messageToReturn.Answer = "I guess you have no intentions or the bot is internal server error (500)";
                    break;
            }
            return messageToReturn;
        }

        private static LuisResponseMessage GetLuisResponse(WebsiteMessage message)
        {
            var luisUrl = $"https://westeurope.api.cognitive.microsoft.com/luis/v2.0/apps/{BotId}?subscription-key={SubscriptionKey}&verbose=true&timezoneOffset=0&q=";
            var url = luisUrl + message.Command;
            WebClient client = new WebClient();
            var intentData = client.DownloadData(url);
            var responseString = Encoding.UTF8.GetString(intentData);
            return JsonConvert.DeserializeObject<LuisResponseMessage>(responseString);
        }
    }
}