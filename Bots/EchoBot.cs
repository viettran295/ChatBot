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
        private string[] _suggestedBot = {"suggest","suggested","suggested Bot"};
        private string[] _colors = {"red", "yellow","blue"};
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            SuggestedBot SuggestBot = new SuggestedBot();
            if(_suggestedBot.Contains(turnContext.Activity.Text))
            {
                await SuggestBot.SendSuggestedActivityAsync(turnContext, cancellationToken);
            }
            else if(_colors.Contains(turnContext.Activity.Text))
            {
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
