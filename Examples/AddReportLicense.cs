using System;
using System.Threading.Tasks;

namespace IReportsApiExamples.Examples
{
    public class AddReportLicense
    {
        /// <summary>Adds a report license for a specified user for a desired report</summary>
        /// <param name="wrapper">The ApiWrapper object to use</param>
        /// <param name="username">The username of the user</param>
        /// <param name="reportCode">The report code of the report to create a license for</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        public static async Task DoWork(
            ApiWrapper wrapper,
            string username,
            string reportCode,
            DateTime startDate,
            DateTime endDate)
        {
            await wrapper.PostReportLicenseForUserAsync(
                username,
                CreateLicenseForm(reportCode, startDate, endDate));

            Console.WriteLine($"Added report license '{reportCode}' to user '{username}'");
        }

        /// <summary>Creates a CreateLicenseForm which can be posted on the server</summary>
        /// <param name="reportCode">The report code of the report to create a license for</param>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        private static CreateUserLicenseForm CreateLicenseForm(
            string reportCode,
            DateTime startDate,
            DateTime endDate)
        {
            return new CreateUserLicenseForm
            {
                Report = reportCode,
                AllowAllAttachmentExtensions = true,

                ActiveDates = new LicenseDateRangeForm
                {
                    StartDate = startDate,
                    EndDate = endDate
                }
            };
        }
    }
}