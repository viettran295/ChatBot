using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace EchoBot.Bots
{
    public class SuggestedBot : ActivityHandler
    {
    public async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
                string replyText = "This is suggested Bot";
                await turnContext.SendActivityAsync(MessageFactory.Text(replyText));
        }
    }
}