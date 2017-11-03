using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SaneSoftWebsiteBot.Dialogs
{
    [LuisModel("9974177d-01c4-45cd-a919-8ad3b2a66428", "f89c151033584055bbf97d5d415dfb07", domain: "westeurope.api.cognitive.microsoft.com")]
    [Serializable]
    public class WebsiteBotDialog : LuisDialog<object>
    {

        [LuisIntent("Help")]
        public async Task ProvideHelp(IDialogContext context, LuisResult result)
        {
            List<string> options = new List<string>
            {
                "Phone",
                "BusinessId",
                "Joke"
            };
            await context.PostAsync(string.Format("Examples of options: \n\n {0}", string.Join("\n\n", options)));
        }

        [LuisIntent("Time")]
        public async Task ProvideTime(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Time on the server: {DateTime.Now.ToString()} (because the bot doesn't care where you are)");
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {

            string message = $"I'm the awesome bot who works for Sane Software. Though I'm quite smart I don't know what you mean. \n\n Detected intent: " + string.Join(", ", result.Intents.Select(i => i.Intent));
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
    }
}