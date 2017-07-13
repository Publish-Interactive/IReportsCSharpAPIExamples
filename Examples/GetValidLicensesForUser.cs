using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace IReportsApiExamples.Examples
{
    public class GetValidLicensesForUser
    {
        /// <summary>Gets all the category and reports licenses for a specified user and determines whether
        /// they are valid for the whole duration inbetween the two dates</summary>
        /// <param name="wrapper">The ApiWrapper object to use</param>
        /// <param name="username">The username of the desired user to get the licenses from</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        public static async Task DoWork(
            ApiWrapper wrapper,
            string username,
            System.DateTime startDate,
            System.DateTime endDate)
        {
            var categoryLicensesQuery = (await wrapper.GetCategoryLicensesForUser(username, 0, null));
            var reportsLicensesQuery = (await wrapper.GetReportLicensesForUserAsync(username, 0, null));

            PrintValidLicenses<CategoryLicenseModel>(GetValidLicenses<CategoryLicenseModel>(
                                categoryLicensesQuery.Licenses,
                                startDate,
                                endDate).ToList());

            PrintValidLicenses<ReportLicenseModel>(GetValidLicenses<ReportLicenseModel>(
                                reportsLicensesQuery.Licenses,
                                startDate,
                                endDate).ToList());
        }

        /// <summary>Prints how many and which licenses are valid</summary>
        /// <T>CategoryLicenseModel or ReportLicenseModel</T>
        /// <param name="validLicenseList">The list of valid licenses to loop through</param>
        private static void PrintValidLicenses<T>(List<T> validLicenseList) where T : LicenseModel
        {
            var type = typeof(T) == typeof(CategoryLicenseModel) ? "Category" : "Report";

            Console.WriteLine($"{validLicenseList.Count} {type} Licenses are valid:");

            if (typeof(T) == typeof(CategoryLicenseModel))
            {
                validLicenseList.ForEach(l =>
                    Console.WriteLine($"'{l.Category}' license is valid between the two dates"));
            }
            else if (typeof(T) == typeof(ReportLicenseModel))
            {
                validLicenseList.ForEach(l =>
                    Console.WriteLine($"'{l.Report}' license is valid between the two dates"));
            }
        }

        /// <summary>Creates a list of valid licenses by looping through all the licenses the desired user
        /// has and determining whether they are valid between the two specified dates</summary>
        /// <T>CategoryLicenseModel or ReportLicenseModel</T>
        /// <param name="licenses">The list of licenses to loop through</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        private static IEnumerable<T> GetValidLicenses<T>(
            List<T> licenses,
            System.DateTime startDate,
            System.DateTime endDate)
        where T : LicenseModel
        {
            foreach (var license in licenses)
            {
                if (license.ActiveDates != null)
                {
                    if (license.ActiveDates.StartDate.Value.CompareTo(startDate) < 0 &&
                        license.ActiveDates.EndDate.Value.CompareTo(endDate) > 0)
                    {
                        yield return license;
                    }
                }
            }
        }
    }
}