using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IReportsApiExamples.Examples
{
    public class MoveUserToAnotherAccount
    {
        /// <summary>Moves a specified user to a different account. if the user already exists,
        /// it moves it to the desired account, if not, then it creates a new account with the 
        /// specified name and creates the new user inside it.</summary>

        /// <param name="wrapper">The ApiWrapper object to use</param>
        /// <param name="uername">The username of the user to move</param>
        /// <param name="newAccountName">The name of the account to move the user across to, or create</param>
        /// <param name="companyName">The company name of the new account</param>
        /// <param name="countryCode">The country code of the new account</param>
        /// <param name="email">The email address of the new user</param>
        public static async Task DoWork(ApiWrapper wrapper,
            string username,
            string newAccountName,
            string companyName,
            string countryCode,
            string email)
        {
            SubscriberDataModel subscriberModel = await GetSubscriber(wrapper,
                username,
                email,
                companyName,
                countryCode,
                newAccountName);

            await wrapper.PutSubscriberAsync(subscriberModel.AccountName,
                username,
                CreateSubscriberDataForm(subscriberModel, newAccountName));

            Console.WriteLine($"Put '{username}' in '{newAccountName}' account");

        }

        /// <summary>Gets the subscriber object of the desired user. if the user doesnt exist,
        /// it creates a new SubscriberDataModel object with the specified email address and account name.
        /// it loops through all the accounts, and all the subscribers in each account to find the desired user</summary>

        /// <param name="wrapper">The ApiWrapper object to use</param>
        /// <param name="uername">The username of the user to move</param>
        /// <param name="newAccountName">The name of the account to move the user across to, or create</param>
        /// <param name="companyName">The company name of the new account</param>
        /// <param name="countryCode">The country code of the new account</param>
        /// <param name="email">The email address of the new user</param>
        private static async Task<SubscriberDataModel> GetSubscriber(ApiWrapper wrapper,
            string username,
            string newAccountName,
            string companyName,
            string countryCode,
            string email)
        {
            foreach (AccountSearchResults accountResult in await wrapper.GetAllAvailableAccountsAsync())
            {
                foreach (SubscriberDataModel subscriber in await wrapper.GetSubscribersAsync(accountResult.Name))
                {
                    if (subscriber.Username == username)
                    {
                        Console.WriteLine($"User '{username}' exists");
                        return subscriber;
                    }
                }
            }

            await CreateAccount(wrapper, companyName, countryCode, newAccountName);

            Console.WriteLine($"User '{username}' does not exist so created new account called '{newAccountName}'");

            return new SubscriberDataModel
            {
                Email = email,
                AccountName = newAccountName,
                IsEnabled = true
            };
        }

        /// <summary>Creates a new account with the desired company name and country code</summary>

        /// <param name="wrapper">The ApiWrapper object to use</param>
        /// <param name="newAccountName">The name of the account to move the user across to, or create</param>
        /// <param name="companyName">The company name of the new account</param>
        /// <param name="countryCode">The country code of the new account</param>
        private static async Task CreateAccount(ApiWrapper wrapper,
            string newAccountName,
            string companyName,
            string countryCode)
        {
            AccountCreateForm accountForm = new AccountCreateForm
            {
                CompanyName = companyName,
                CountryCode = countryCode
            };

            await wrapper.PutAccountAsync(newAccountName, accountForm);
        }

        /// <summary>Creates a SubscriberDataForm object so it can be put on the server. It takes all the
        /// values from the SubscriberModel and copies them to the new SubscriberDataForm</summary>

        /// <param name="subscriberModel">The SubscriberDataModel of the desired user to copy the values from</param>
        /// <param name="newAccountName">The account name of the account the desired user should be moved to</param>
        private static SubscriberDataForm CreateSubscriberDataForm(SubscriberDataModel subscriberModel,
            string newAccountName)
        {
            return new SubscriberDataForm
            {
                Name = subscriberModel.Name,
                Email = subscriberModel.Email,
                Phone = subscriberModel.Phone,
                JobTitle = subscriberModel.JobTitle,
                Department = subscriberModel.Department,
                AccountName = newAccountName,
                IsEnabled = subscriberModel.IsEnabled,
                Company = subscriberModel.Company,
                CountryCode = subscriberModel.CountryCode,
                LanguageCode = subscriberModel.LanguageCode,
                SendWelcomeEmail = false
            };
        }
    }
}