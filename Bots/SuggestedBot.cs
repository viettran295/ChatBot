using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace EchoBot.Bots
{
    public class SuggestedBot : EchoBot
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var text = turnContext.Activity.Text.ToLowerInvariant();
            string respondText = ProcessInput(text);
            await turnContext.SendActivityAsync(respondText);
        }
        public async Task SendSuggestedActivityAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = MessageFactory.Text("What is your favorite color?");
            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                {
                    new CardAction(){Title = "Red", Type = ActionTypes.ImBack, Value = "red", Image = "https://via.placeholder.com/20/FF0000?text=R", ImageAltText = "R" },
                    new CardAction(){Title = "Yellow", Type = ActionTypes.ImBack, Value = "yellow", Image = "https://via.placeholder.com/20/FFFF00?text=Y", ImageAltText = "Y" },
                    new CardAction(){Title = "Blue", Type = ActionTypes.ImBack, Value = "blue",  Image = "https://via.placeholder.com/20/0000FF?text=B", ImageAltText = "B" },
                }
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }
        private static string ProcessInput(string text)
        {
            const string reply = "is the best color";
            switch (text)
            {
                case "red": return $"Red {reply}";
                case "yellow": return $"Yellow {reply}";
                case "blue": return $"Blue {reply}";
                default: return "Pls choose ur color";
            }
        }
    }
}