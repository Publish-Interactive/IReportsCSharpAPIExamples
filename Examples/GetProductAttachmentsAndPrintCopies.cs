using System;
using System.IO;
using System.Threading.Tasks;

namespace IReportsApiExamples.Examples
{
    public class GetProductAttachmentsAndPrintCopies
    {
        public static async Task DoWork(ApiWrapper wrapper, string productCode)
        {
            var toc = await wrapper.GetProductByCodeAsync(
                "reports",
                productCode,
                false,
                true);
            foreach (var attachment in toc.Attachments)
            {
                var fileModel = await wrapper.GetAttachmentAsync("reports", productCode, attachment.FileCode);
                File.WriteAllBytes(fileModel.Name, fileModel.Content);
            }

            foreach (var attachment in toc.PrintCopies)
            {
                var fileModel = await wrapper.GetPrintCopyAsync(
                    "reports",
                    productCode,
                    attachment.Extension,
                    attachment.FileCode);
                File.WriteAllBytes(fileModel.Name, fileModel.Content);
            }
        }

    }
}