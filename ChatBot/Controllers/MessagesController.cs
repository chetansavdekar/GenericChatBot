﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using TogetherChatBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System.Configuration;
using System.Web;
using StackExchange.Redis;

namespace TogetherChatBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {      
     
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            IDatabase cache = CacheManager.Connection.GetDatabase();
            // Simple get of data types from the cache
            string initiatePOCFor = cache.StringGet("InitiatePOCFor");
            //int key2 = (int)cache.StringGet("key2");

            //string initiatePOCFor = ConfigurationManager.AppSettings["InitiatePOCFor"].ToString();          

            if (activity.Type == ActivityTypes.Message)
            {
                if (initiatePOCFor.Equals("T"))
                {
                    await InitiateTogetherActivity(activity);
                }
                else if (initiatePOCFor.Equals("M"))
                {
                    await InitiateMorrisonActivity(activity);
                }

            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private static async Task InitiateTogetherActivity(Activity activity)
        {
            string[] strArray = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

            if ((activity.Text.ToLower().Equals("hi")) || activity.Text.ToLower().Equals("hello"))
            {
                Activity reply = activity.CreateReply("Hello there! How can I help you today?");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else if (activity.Text.ToLower().Contains("ensure") || activity.Text.ToLower().Contains("call"))
            {
                Activity reply = activity.CreateReply("Noted! Is there anything else I can help you with?");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else if ((activity.Text.ToLower().Contains("nope")) || activity.Text.ToLower().Contains("ok") || (activity.Text.ToLower().Contains("thanks")) || (activity.Text.ToLower().Contains("thank")) || (activity.Text.ToLower().Contains("nothing")))
            {
                Activity reply = activity.CreateReply("Ok. Have a good day!");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else if (activity.Text.ToLower().Contains("complaint") || activity.Text.ToLower().Contains("notice") || activity.Text.ToLower().Contains("arrears") || activity.Text.ToLower().Contains("credit") || activity.Text.ToLower().Contains("rating"))
            {
                if (activity.Text.ToLower().Contains("complaint"))
                {
                    Activity reply = activity.CreateReply("Please tell me. How can i help you?");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                else if (activity.Text.ToLower().Contains("notice") || activity.Text.ToLower().Contains("arrears") || activity.Text.ToLower().Contains("credit"))
                {
                    if (activity.Text.ToLower().Contains("receive"))
                    {
                        Activity reply = activity.CreateReply("We will need to discuss your account in order to answer your query. Find out how to contact us to discuss your account at https://togethermoney.com/get-in-touch/personal-lending/.");
                        await connector.Conversations.ReplyToActivityAsync(reply);
                    }
                    else if (activity.Text.ToLower().Contains("affect") || activity.Text.ToLower().Contains("impact") || activity.Text.ToLower().Contains("credit") || activity.Text.ToLower().Contains("rating"))
                    {
                        Activity reply = activity.CreateReply("No, the Notice of Arrears letter simply details any outstanding instalments on your mortgage/loan account, and in itself has no impact on your credit rating. However, having late or missed payments on your mortgage/loan account will affect your credit rating.");
                        await connector.Conversations.ReplyToActivityAsync(reply);
                    }
                }

            }

            //else if (activity.Text.ToLower().Contains("apply") || activity.Text.ToLower().Contains("email") || activity.Text.ToLower().Contains("phone") || activity.Text.ToLower().Contains("number") || activity.Text.ToLower().Contains("call") || activity.Text.ToLower().Contains("name"))
            //{
            //    // await Conversation.SendAsync(activity, () => new ApplicationDialog());
            //    await Conversation.SendAsync(activity, MakeAccountDialog);
            //}
            else //if (activity.Text.ToLower().Contains("loan") || activity.Text.ToLower().Contains("mortgage") || activity.Text.ToLower().Contains("bridging") || strArray.Any(activity.Text.Equals))
            {
                await Conversation.SendAsync(activity, MakeLoanDialog);
            }
        }

        private static async Task InitiateMorrisonActivity(Activity activity)
        {
            string[] strArray = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

            if ((activity.Text.ToLower().Equals("hi")) || activity.Text.ToLower().Equals("hello"))
            {
                Activity reply = activity.CreateReply("Hello there! How can I help you today?");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else if (activity.Text.ToLower().Contains("ingredients") || activity.Text.ToLower().Contains("pudding"))
            {
                Activity reply = activity.CreateReply("Sure. Can I have your name please?");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else if (activity.Text.ToLower().Contains("chetan") || activity.Text.ToLower().Contains("srinivas"))
            {
                Activity reply = activity.CreateReply("Thanks " + activity.Text.ToString() + ". Please find below the ingredients required. 140g plain flour,4 eggs,200 ml milk, sunflower oil for 8 large puds. Would you like to add the items to your cart?");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else if ((activity.Text.ToLower().Equals("y")) || (activity.Text.ToLower().Contains("yes")) || activity.Text.ToLower().Contains("please"))
            {
                Activity reply = activity.CreateReply("Items added to your cart. Please login to checkout. Please rate your experience with us here. (1 - 10) 1 - lowest, 10 - highest ");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else if (strArray.Any(activity.Text.Equals))
            {
                int rating = Convert.ToInt32(activity.Text);
                if (rating >= 7)
                {
                    Activity reply = activity.CreateReply("Thanks for your rating!");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                else if (rating < 7)
                {
                    Activity reply = activity.CreateReply("Thanks for your rating! Please tell us to improve ourselves.");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }

            }
            else if ((activity.Text.ToLower().Contains("thanks")) || (activity.Text.ToLower().Contains("thank")) || (activity.Text.ToLower().Contains("ok")))
            {
                Activity reply = activity.CreateReply("Ok. Have a good day!");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }

        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }


        //internal static IDialog<Account> MakeRootDialog()
        //{
        //    return Chain.From(() => FormDialog.FromForm(Account.BuildForm));

        //}

        internal static IDialog<Loans> MakeLoanDialog()
        {
            return Chain.From(() => FormDialog.FromForm(Loans.BuildForm));
        }

        //internal static IDialog<Account> MakeAccountDialog()
        //{
        //    return Chain.From(() => FormDialog.FromForm(Account.BuildForm));
        //}

    }
}