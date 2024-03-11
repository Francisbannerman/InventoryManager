using SendGrid;
using SendGrid.Helpers.Mail;
using Quartz;
using Quartz.Impl;

namespace InventoryManagerWeb.Services;

public class EmailService
{
    private readonly ExcelReportService _excelReport;
    private readonly string apiKey = "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";
    public EmailService(ExcelReportService excelReport)
    {
        _excelReport = excelReport;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("francisbannerman@ymail.com", "Francis");
        var to = new EmailAddress(toEmail);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, body, body);

        //TODO
        //Confirm if attachment is with the mail
        var attachment = new Attachment
        {
            Content = Convert.ToBase64String(await _excelReport.DownloadSummary()),
            Filename = "inventoryReport.xlsx",
            Type = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            Disposition = "attachment",
            ContentId = "excelAttachment"
        };
        msg.AddAttachment(attachment);
        
        var response = await client.SendEmailAsync(msg);
        if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted)
        {
            throw new Exception($"Failed to send email. Status: {response.StatusCode}");
        }
    }
}