using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace EchoBot.Bots
{
    public class StateManagementBot : ActivityHandler
    {
        private BotState _conversationState;
        private BotState _userState;
        
        public StateManagementBot(ConversationState conversationState, UserState userState)
        {
            _conversationState = conversationState;
            _userState = userState;
        }

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            await base.OnTurnAsync(turnContext, cancellationToken);

            // Save any state changes that might have occurred during the turn.
            await _conversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            await _userState.SaveChangesAsync(turnContext, false, cancellationToken);
        }

       protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome State Bot";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText), cancellationToken);
                }
            }
        }
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var conversationStateAccessors = _conversationState.CreateProperty<ConversationData>(nameof(ConversationData));
            var conversationState = conversationStateAccessors.GetAsync(turnContext, () => new ConversationData());

            var userStateAccessors = _userState.CreateProperty<UserProfile>(nameof(UserProfile));
            var userState = userStateAccessors.GetAsync(turnContext, () => new UserProfile());

            if(string.IsNullOrEmpty(UserProfile.Name))
            {
                if(ConversationData.PromtedUserForName)
                {
                    UserProfile.Name = turnContext.Activity.Text?.Trim();
                    await turnContext.SendActivityAsync($"Welcome {UserProfile.Name}");
                    ConversationData.PromtedUserForName = false;
                } 
                else
                {
                    await turnContext.SendActivityAsync("what is ur namne?");
                    ConversationData.PromtedUserForName = true;
                }
            }
            else
            {
                var messageTimeOffset = (DateTimeOffset)turnContext.Activity.Timestamp;
                var localMessageTime = messageTimeOffset.ToLocalTime();

                ConversationData.TimeStamp = localMessageTime.ToString();
                // ConversationData.ChannelID = turnContext.Activity.ChannelID.ToString();

                await turnContext.SendActivityAsync($"user name: {UserProfile.Name}");
                await turnContext.SendActivityAsync($"local time: {ConversationData.TimeStamp}");
                // await turnContext.Activity.SendActivityAsync($"channel id: {ConversationData.ChannelID}");
            }
        }
    }
}