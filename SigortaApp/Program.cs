using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using SigortaApp.BL;
using SigortaApp.BL.Abstract;
using SigortaApp.BL.Concrete;
using SigortaApp.Entity.Concrete;
using SigortaApp.DAL;
using SigortaApp.DAL.Abstract;
using SigortaApp.DAL.Concrete;
using SigortaApp.DAL.EntityFramework;
using SigortaApp.DAL.Repositories;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using SigortaApp.Web;

var builder = WebApplication.CreateBuilder(args);

var provider = builder.Services.BuildServiceProvider();
var configration  = provider.GetRequiredService<IConfiguration>();


builder.Services.AddDbContext<Context>(options => options.UseSqlServer(configration.GetConnectionString("CoreDefaultDB")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));


builder.Services.AddIdentity<AppUser, AppRole>(x =>
{
    x.Password.RequireUppercase = false;
    x.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddRazorPages()
.AddNewtonsoftJson();

builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession();

builder.Services.AddMvc();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.LoginPath = "/Login/Index2/";
    });

builder.Services.ConfigureApplicationCookie(opts =>

{

    //Cookie settings

    opts.Cookie.HttpOnly = true;

    opts.ExpireTimeSpan = TimeSpan.FromMinutes(100);

    opts.AccessDeniedPath = new PathString("/Login/AccessDenied/");

    opts.LoginPath = "/Login/Index2/";

    opts.SlidingExpiration = true;

});

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IAboutRepository, EFAboutRepository>();
builder.Services.AddTransient<IAdminRepository, EFAdminRepository>();
builder.Services.AddTransient<IBlogRepository, EFBlogRepository>();
builder.Services.AddTransient<IBrandRepository, EFBrandRepository>();
builder.Services.AddTransient<ICategoryRepository, EFCategoryRepository>();
builder.Services.AddTransient<ICommentRepository, EFCommentRepository>();
builder.Services.AddTransient<IContactRepository, EFContactRepository>();
builder.Services.AddTransient<IDevicesRepository, EFDevicesRepository>();
builder.Services.AddTransient<IDevicesReleaseRepository, EFDevicesReleaseRepository>();
builder.Services.AddTransient<IMessageRepository, EFMessageRepository>();
builder.Services.AddTransient<IMessage2Repository, EFMessage2Repository>();
builder.Services.AddTransient<INewsLetterRepository, EFNewsLetterRepository>();
builder.Services.AddTransient<INotificationRepository, EFNotificationRepository>();
builder.Services.AddTransient<ITypesRepository, EFTypesRepository>();
builder.Services.AddTransient<IUnitRepository, EFUnitRepository>();
builder.Services.AddTransient<IUserRepository, EFUserRepository>();
builder.Services.AddTransient<IWriterRepository, EFWriterRepository>();
builder.Services.AddTransient<ITaskRepository, EFTaskRepository>();
builder.Services.AddTransient<ITaskHistoryRepository, EFTaskHistoryRepository>();
builder.Services.AddTransient<IPaymentTaskRepository, EFPaymentTaskRepository>();
builder.Services.AddTransient<IStatusRepository, EFStatusRepository>();
builder.Services.AddTransient<ICompanyRepository, EFCompanyRepository>();
builder.Services.AddTransient<ICityRepository, EFCityRepository>();
builder.Services.AddTransient<IFilesRepository, EFFilesRepository>();
builder.Services.AddTransient<IFileTaskRepository, EFFileTaskRepository>();
builder.Services.AddTransient<IPaymentRepository, EFPaymentRepository>();
builder.Services.AddTransient<IBankAccountRepository, EFBankAccountRepository>();
builder.Services.AddTransient<IUnitTypesRepository, EFUnitTypesRepository>();
builder.Services.AddTransient<IBankAndCaseDetailsRepository, EFBankAndCaseDetailsRepository>();
builder.Services.AddTransient<IUserRoleRepository, EFUserRoleRepository>();
builder.Services.AddTransient<ICalendarRepository, EFCalendarRepository>();


builder.Services.AddTransient<IAboutService, AboutService>();
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<IBlogService, BlogService>();
builder.Services.AddTransient<IBrandService, BrandService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IContactService, ContactService>();
builder.Services.AddTransient<IDevicesService, DevicesService>();
builder.Services.AddTransient<IDevicesReleaseService, DevicesReleaseManager>();
builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.AddTransient<IMessage2Service, Message2Service>();
builder.Services.AddTransient<INewsLetterService, NewsLetterService>();
builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddTransient<ITypesService, TypesService>();
builder.Services.AddTransient<IUnitService, UnitService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IWriterService, WriterService>();
builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<ITaskHistoryService, TaskHistoryService>();
builder.Services.AddTransient<IPaymentTaskService, PaymentTaskService>();
builder.Services.AddTransient<IStatusService, StatusService>();
builder.Services.AddTransient<ICompanyService, CompanyService>();
builder.Services.AddTransient<ICityService, CityService>();
builder.Services.AddTransient<IFilesService, FilesService>();
builder.Services.AddTransient<IFileTaskService, FileTaskService>();
builder.Services.AddTransient<IPaymentService, PaymentService>();
builder.Services.AddTransient<IBankAccountService, BankAccountService>();
builder.Services.AddTransient<IUnitTypesService, UnitTypesService>();
builder.Services.AddTransient<IBankAndCaseDetailsService, BankAndCaseDetailsService>();
builder.Services.AddTransient<IUserRoleService, UserRoleService>();
builder.Services.AddTransient<ICalendarService, CalendarService>();



builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });


//JOB Başlangıç
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    // Job'ı tanımlayın
    var jobKey = new JobKey("TestJob");
    q.AddJob<TestJob>(opts => opts.WithIdentity(jobKey));

    //q.AddTrigger(opts => opts
    //    .ForJob(jobKey)
    //    .WithIdentity("TestJob-minute-trigger")
    //    .WithDailyTimeIntervalSchedule(x => x.WithIntervalInSeconds(10)) // Saniye olarak çalışacak
    //);

    //// Trigger'ı tanımlayın
    //q.AddTrigger(opts => opts
    //    .ForJob(jobKey)
    //    .WithIdentity("TestJob-trigger")
    //    .WithDailyTimeIntervalSchedule(x => x.WithIntervalInHours(24)) // Günlük olarak çalışacak
    //);

    //q.AddTrigger(opts => opts
    //    .ForJob(jobKey)
    //    .WithIdentity("TestJob-weekly-trigger")
    //    .WithCronSchedule("0 0 0 ? * MON") // Haftalık olarak Pazartesi günleri çalışacak
    //);

    //q.AddTrigger(opts => opts
    //    .ForJob(jobKey)
    //    .WithIdentity("TestJob-monthly-trigger")
    //    .WithCronSchedule("0 0 0 1 * ?") // Aylık olarak her ayın ilk günü çalışacak
    //);
});

// Quartz.NET'i host olarak ekleyin
//builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

//JOB Bitiş

var app = builder.Build();
var environment = app.Environment;

if (environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error1", "?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.UseNotyf();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Login}/{action=Index2}/{id?}");

    endpoints.MapRazorPages();
});

try
{
    app.Run();
}
catch (Exception ex)
{
    throw;
}
