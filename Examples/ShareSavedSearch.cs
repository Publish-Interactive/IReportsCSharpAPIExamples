using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace IReportsApiExamples.Examples
{
    public class ShareSavedSearch
    {
        /// <summary>Searches through all of a specific user's saved searches for any search with the desired search terms.
        /// If one already exists, it makes it shared. If not, it creates a new saved search with the desired search terms
        /// and sets the title as the search term and makes it shared</summary>

        /// <param name="iReportsLibrary">The IReportsLibrary object to use</param>
        /// <param name="username">The username of the desired user</param>
        /// <param name="searchTerms">The desired search terms to search for in all the saved searches</param>
        public static async Task DoWork(
            IReportsLibrary iReportsLibrary,
            string username,
            string searchTerms)
        {
            List<SavedSearchListModel> savedSearchesList = await iReportsLibrary.GetSavedSearchesAsync(username);

            SavedSearchModel savedSearchModel = await FindSavedSearch(
                iReportsLibrary,
                savedSearchesList,
                username,
                searchTerms);

            if (savedSearchModel == null)
            {
                await iReportsLibrary.PostSavedSearchAsync(
                    username,
                    CreateNewSavedSearchWithSpecifiedSearchTerms(searchTerms));

                Console.WriteLine(
                    $@"Created new Saved Search with a title of '{searchTerms}'
                    which is shared and has the specified search terms");

                return;
            }

            savedSearchModel.IsShared = true;

            PutSavedSearch(iReportsLibrary, username, savedSearchModel);

            Console.WriteLine("Saved Search already exists. It is now shared.");
        }

        /// <summary>Loops through all the saved searches of the user and returns whether a saved search already
        /// exists with the specified search terms</summary>

        /// <param name="iReportsLibrary">The IReportsLibrary object to use</param>
        /// <param name="savedSearchesList">The list of saved searches from the user</param>
        /// <param name="username">The username of the user</param>        
        /// <param name="specifiedSearchTerms">The search terms to look for in the saved searches</param>
        private static async Task<SavedSearchModel> FindSavedSearch(
            IReportsLibrary iReportsLibrary,
            List<SavedSearchListModel> savedSearchesList,
            string username,
            string specifiedSearchTerms)
        {
            foreach (var model in savedSearchesList)
            {
                SavedSearchModel savedSearch = await iReportsLibrary.GetSavedSearchAsync(
                    username, model.Id);

                if (savedSearch.SearchParameters.Terms.Equals(specifiedSearchTerms))
                {
                    return savedSearch;
                }
            }

            return null;
        }

        /// <summary>Deletes the old saved search which isn't shared and posts the new shared saved search.
        /// All the data for the old saved search is retained</summary>

        /// <param name="iReportsLibrary">The IReportsLibrary object to use</param>
        /// <param name="username">The username of the user</param>        
        /// <param name="oldSavedSearch">The old saved search to delete and retrieve the data from</param>
        private static async void PutSavedSearch(
            IReportsLibrary iReportsLibrary,
            string username,
            SavedSearchModel oldSavedSearch)
        {
            await iReportsLibrary.DeleteSavedSearchAsync(username, oldSavedSearch.Id);
            await iReportsLibrary.PostSavedSearchAsync(username, CreateNewSavedSearch(oldSavedSearch));
        }

        /// <summary>Creates a new save search which retains all the data from the old saved search</summary>
        /// <param name="oldSavedSearch">The saved search to copy the data from</param>
        private static AddSavedSearchForm CreateNewSavedSearch(SavedSearchModel oldSavedSearch)
        {
            AlertFrequency alertFrequency = AlertFrequency.Never;

            switch (oldSavedSearch.AlertFrequency)
            {
                case AlertFrequency.Never:
                    alertFrequency = AlertFrequency.Never;
                    break;

                case AlertFrequency.Daily:
                    alertFrequency = AlertFrequency.Daily;
                    break;

                case AlertFrequency.Weekly:
                    alertFrequency = AlertFrequency.Weekly;
                    break;
            }

            return new AddSavedSearchForm
            {
                Title = oldSavedSearch.Title,
                IsPinnedToHomepage = oldSavedSearch.IsPinnedToHomepage,
                IsShared = oldSavedSearch.IsShared,
                AlertFrequency = alertFrequency,
                SearchParameters = oldSavedSearch.SearchParameters
            };
        }

        /// <summary>Creates a new saved search with the desired search terms</summary>
        /// <param name="searchTerms">The desired search terms to apply to the new saved search</param>
        private static AddSavedSearchForm CreateNewSavedSearchWithSpecifiedSearchTerms(string searchTerms)
        {
            ProductSearchParametersForm searchParameters = new ProductSearchParametersForm()
            {
                Terms = searchTerms
            };

            return new AddSavedSearchForm
            {
                Title = searchTerms,
                IsShared = true,
                SearchParameters = searchParameters
            };
        }
    }
}