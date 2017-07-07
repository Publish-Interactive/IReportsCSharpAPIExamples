using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IReportsApiExamples.Examples
{
    public class ImportAccountsAndSubscribers
    {
        /// <summary>Reads the accounts and subscribers from a Json file and uploads them to the server</summary>
        /// <param name="wrapper">The ApiWrapper object to use</param>
        /// <param name="accountsFilePath">The path to the Json file with the accounts in</param>
        /// <param name="uersFilePath">The path to the Json file with the subscribers in</param>
        public static void DoWork(
            ApiWrapper wrapper,
            string accountsFilePath,
            string subscribersFilePath)
        {
            var accountList = getListFromJson<AccountSearchResults>(accountsFilePath);
            var subscriberList = getListFromJson<SubscriberDataModel>(subscribersFilePath);

            foreach (var account in accountList)
            {
                wrapper.PutAccountAsync(account.Name, CreateAccountForm(account));
            }

            foreach (var subscriber in subscriberList)
            {
                wrapper.PutSubscriberAsync(
                    subscriber.AccountName,
                    subscriber.Username,
                    CreateSubscriberDataForm(subscriber));
            }
        }

        /// <summary>Deserializes the desired Json file into a list with a specified type</summary>
        /// <param name="filePath">The path to the Json file to be deserialized</param>
        private static List<T> getListFromJson<T>(string filePath)
        {
            using (var streamReader = File.OpenText(filePath))
            {
                var jsonTextReader = new JsonTextReader(streamReader);
                var jsonSerializer = new JsonSerializer();

                return jsonSerializer.Deserialize<List<T>>(jsonTextReader);
            }
        }

        /// <summary>Converts a AccountSearchResults into an AccountCreateForm 
        /// so it can be uploaded to the server</summary>

        /// <param name="account">The desired AccountSearchResults object to convert</param>
        private static AccountCreateForm CreateAccountForm(AccountSearchResults account)
        {
            return new AccountCreateForm
            {
                CompanyName = account.CompanyName,
                CountryCode = account.CountryCode,
                IsEnabled = account.IsEnabled
            };
        }

        /// <summary>Converts a SubscriberDataModel into an SubscriberDataForm 
        /// so it can be uploaded to the server</summary>

        /// <param name="subscriberModel">The desired SubscriberDataModel object you wish to convert</param>
        public static SubscriberDataForm CreateSubscriberDataForm(SubscriberDataModel subscriberModel)
        {
            return new SubscriberDataForm
            {
                Name = subscriberModel.Name,
                Email = subscriberModel.Email,
                Phone = subscriberModel.Phone,
                JobTitle = subscriberModel.JobTitle,
                Department = subscriberModel.Department,
                AccountName = subscriberModel.AccountName,
                IsEnabled = subscriberModel.IsEnabled,
                Company = subscriberModel.Company,
                CountryCode = subscriberModel.CountryCode,
                LanguageCode = subscriberModel.LanguageCode,
                SendWelcomeEmail = false
            };
        }
    }
}