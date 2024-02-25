using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;
// using OfficeOpenXml;

namespace InventoryManagerWeb.Services;

public class ExcelReportService
{
    private readonly IUnitOfWork _unitOfWork;

    public ExcelReportService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}