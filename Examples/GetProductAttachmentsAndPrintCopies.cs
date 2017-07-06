using System;
using System.IO;
using System.Threading.Tasks;

namespace IReportsApiExamples.Examples
{
    public class GetProductAttachmentsAndPrintCopies
    {
        public static async Task DoWork(IReportsLibrary iReportsLibrary)
        {
            var toc = await iReportsLibrary.GetProductByCodeAsync(
                "reports",
                "CNFDDiet05",
                false,
                true);
            foreach (var attachment in toc.Attachments)
            {
                var fileModel = await iReportsLibrary.GetAttachmentAsync("reports", "CNFDDiet05", attachment.FileCode);
                File.WriteAllBytes(fileModel.Name, fileModel.Content);
            }

            foreach (var attachment in toc.PrintCopies)
            {
                var fileModel = await iReportsLibrary.GetPrintCopyAsync(
                    "reports",
                    "CNFDDiet05",
                    attachment.Extension,
                    attachment.FileCode);
                File.WriteAllBytes(fileModel.Name, fileModel.Content);
            }
        }

    }
}