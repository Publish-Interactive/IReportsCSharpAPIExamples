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
        public static async Task DoWork(IReportsLibrary iReportsLibrary, string username
                                        , string searchTerms)
        {
            ObservableCollection<SavedSearchListModel> savedSearchesList
                        = await iReportsLibrary.GetSavedSearchesAsync(username);

            if (!await SavedSearchExistsAsync(iReportsLibrary, savedSearchesList, username, searchTerms))
            {
                Console.WriteLine(
                    $@"Created new Saved Search with a title of '{searchTerms}' 
                    which is shared and has the specified search terms");
            }
        }

        /// <summary>Loops through all the saved searches of the user and returns whether a saved search already
        /// exists with the specified search terms</summary>

        /// <param name="iReportsLibrary">The IReportsLibrary object to use</param>
        /// <param name="savedSearchesList">The list of saved searches from the user</param>
        /// <param name="username">The username of the user</param>        
        /// <param name="specifiedSearchTerms">The search terms to look for in the saved searches</param>
        private static async Task<bool> SavedSearchExistsAsync(
            IReportsLibrary iReportsLibrary, ObservableCollection<SavedSearchListModel> savedSearchesList
            , string username, string specifiedSearchTerms)
        {
            for (int i = 0; i < savedSearchesList.Count; i++)
            {
                SavedSearchModel savedSearch = await iReportsLibrary.GetSavedSearchAsync(
                    username, savedSearchesList[i].Id);

                if (savedSearch.SearchParameters.Terms.Equals(specifiedSearchTerms))
                {
                    savedSearch.IsShared = true;

                    PutSavedSearchAsync(iReportsLibrary, username, savedSearch);

                    Console.WriteLine("Saved Search already exists. It is now shared.");

                    return true;
                }
            }

            await iReportsLibrary.PostSavedSearchAsync(
                username, CreateNewSavedSearchWithSpecifiedSearchTerms(specifiedSearchTerms));

            return false;
        }

        /// <summary>Deletes the old saved search which isn't shared and posts the new shared saved search.
        /// All the data for the old saved search is retained</summary>

        /// <param name="iReportsLibrary">The IReportsLibrary object to use</param>
        /// <param name="username">The username of the user</param>        
        /// <param name="oldSavedSearch">The old saved search to delete and retrieve the data from</param>
        private static async void PutSavedSearchAsync(
            IReportsLibrary iReportsLibrary, string username, SavedSearchModel oldSavedSearch)
        {
            await iReportsLibrary.DeleteSavedSearchAsync(username, oldSavedSearch.Id);
            await iReportsLibrary.PostSavedSearchAsync(username, CreateNewSavedSearch(oldSavedSearch));
        }

        /// <summary>Creates a new save search which retains all the data from the old saved search</summary>
        /// <param name="oldSavedSearch">The saved search to copy the data from</param>
        private static AddSavedSearchForm CreateNewSavedSearch(SavedSearchModel oldSavedSearch)
        {
            AddSavedSearchFormAlertFrequency alertFrequency = AddSavedSearchFormAlertFrequency.Never;

            switch (oldSavedSearch.AlertFrequency)
            {
                case SavedSearchModelAlertFrequency.Never:
                    alertFrequency = AddSavedSearchFormAlertFrequency.Never;
                    break;

                case SavedSearchModelAlertFrequency.Daily:
                    alertFrequency = AddSavedSearchFormAlertFrequency.Daily;
                    break;

                case SavedSearchModelAlertFrequency.Weekly:
                    alertFrequency = AddSavedSearchFormAlertFrequency.Weekly;
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
                SearchParameters = searchParameters
            };
        }
    }
}