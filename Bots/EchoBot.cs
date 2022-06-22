// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with EchoBot .NET Template version v4.15.2

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace EchoBot.Bots
{
    public class EchoBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            string[] trigger = {"suggest","suggested","suggested Bot"};
            if(trigger.Contains(turnContext.Activity.Text))
            {
                SuggestedBot SuggestBot = new SuggestedBot();
                await SuggestBot.OnMessageActivityAsync(turnContext, cancellationToken);
            }
            else 
            {
                var replyText = $"You said: {turnContext.Activity.Text}";
                await turnContext.SendActivityAsync(MessageFactory.Text(replyText));
            }
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome! Type 'suggest' to activate suggested Bot";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText), cancellationToken);
                }
            }
        }
    }
}
