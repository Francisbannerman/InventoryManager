using InventoryManagerWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagerWeb.Controllers;


[ApiController]
[Route("Service")]
public class ServiceController : ControllerBase
{
    private readonly ExcelReportService _excelReport;
    private readonly EmailService _emailService;
    public ServiceController(ExcelReportService excelReport, EmailService emailService)
    {
        _excelReport = excelReport;
        _emailService = emailService;
    }

    [HttpGet("Report")]
    public async Task<IActionResult> DownloadInventoryReport()
    {
        var file = await  _excelReport.DownloadSummary();
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "inventoryReport.xlsx");
        
        System.IO.File.WriteAllBytes(filePath, file);
        return PhysicalFile(filePath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "inventoryReport.xlsx");
    }
    
    [HttpPost("SendMail")]
    public async Task<IActionResult> SendEmail()
    {
        try
        {
            await _emailService.SendEmailAsync("francisbannerman@gmail.com",
                "Main Subject", "Kindly find the attached file.");
            return Ok("Email sent successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Failed to send email");
        }
    }
}