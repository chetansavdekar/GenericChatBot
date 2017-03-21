using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TogetherChatBot.Models
{

    public enum PuddingTypes
    {
        YorkshirePudding,
        WhitePudding,
        BreadAndButterPudding,
        StickyToffeePudding
    };

    [Serializable]
    public class Pudding
    {        
        [Prompt("What kind of {&} you are looking for? Please choose any one of these.  {||}", ChoiceFormat = "{1}")]
        public PuddingTypes? PuddingType;

        [Prompt("Sure. Can I have your name please?")]
        public string Name;

        public static IForm<Pudding> BuildForm()
        {

            return new FormBuilder<Pudding>()
                    .Field(nameof(Pudding.Name))
                    .Message("Thanks {Name}.")
                    .Field(nameof(Pudding.PuddingType))
                    .Confirm("Please find the required ingredients for {PuddingType}: 140g plain flour,4 eggs,200 ml milk, sunflower oil for 8 large puds. Would you like to add the items to your cart?  Please confirm by Yes/No.")
                    //.Field(nameof(Loans.LoanType))
                    //.Confirm("{LoanType} Details: We offer loans up to 1 million pounds in this category. LTV can be up to 70 %. Rates from 0.65 % a month up to 50 % LTV. Rates from 0.75 % a month up to 70 % LTV. Available for 12 months. Our standard personal bridging loan can be secured against a single property as a first charge. Would you like to apply? Please confirm by Yes/No.")
                    ////.Field(nameof(Loans.ApplyConfirm))
                    //.Field(nameof(Loans.Name))
                    //.Field(nameof(Loans.PhoneNumber))
                    //.Field(nameof(Loans.EmailAddress))
                    //.Confirm("Thanks for the information. Please confirm the following: Your Full Name: {Name}, Phone Number : {PhoneNumber}, Email Address: {EmailAddress}. Please confirm by Yes/No. ")
                    ////.Field(nameof(Loans.Confirm))
                    //.Message("Thanks for the confirmation {Name}. Your reference number is 128723612Aadf. Do expect a call within the next 48 hours.")
                  
                    .Build();
        }
    }
}