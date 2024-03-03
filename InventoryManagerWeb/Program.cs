using InventoryManagerWeb.Repositories.IRepository.IPostgresRepositories;
using InventoryManagerWeb.Repositories.PostgresDbs;
using InventoryManagerWeb.Services;
using InventoryManagerWeb.Settings;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().WriteTo.Console().WriteTo.File("logs.txt").CreateLogger();

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ItemCountService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<SearchServices>();
builder.Services.AddScoped<ExcelReportService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddDbContext<ApplicationDbContext>(Options =>
    Options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.MapControllers();

app.Run();