using SigortaApp.MinimalAPii;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbCoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddScoped<ITaskService, TaskService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


var users = app.MapGroup("/task");

users.MapGet("/", async (ITaskService _taskService) =>
{
    var result = new CommonResponse();
    try {
        var data = await _taskService.GetAllUsers();
        result.Code = 200;
        result.Message = "İş Listesi Başarılı";
        result.Data = data;
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        result.Code = 400;
        result.Message = $"Başarısız";
        result.Data = ex.Message;
        return Results.BadRequest(result);
    }
});
users.MapGet("/{id}", async (ITaskService _taskService, int id) =>
{
    var result = new CommonResponse();
    try {
        var data = await CheckUser(id, _taskService);
        if (data == null)
        {
            result.Code = StatusCodes.Status404NotFound;
            result.Message = "Task Bulunamadı.";
            result.Data = MessageNotFound(id);
            return Results.NotFound(result);
        }
        result.Code = 200;
        result.Message = "İş detayları Başarılı";
        result.Data = data;
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        result.Code = 400;
        result.Message = $"Başarısız.";
        result.Data = ex.Message;
        return Results.BadRequest(result);
    }    
});
users.MapPost("/", async (ITaskService _taskService, [FromBody] SigortaApp.MinimalAPii.Task model) =>
{
    var result = new CommonResponse();
    try {
        var data = await _taskService.CreateUser(model);
        result.Code = 200;
        result.Message = $"İş Başarılı bir şekilde eklendi {model.Id}";
        result.Data = data;
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        result.Code = 400;
        result.Message = $"Başarısız";
        result.Data = ex.Message;
        return Results.BadRequest(result);
    }    
});
users.MapPut("/", async (ITaskService _taskService, [FromBody] SigortaApp.MinimalAPii.Task model) =>
{
    var result = new CommonResponse();
    try {
        var checkUser = await CheckUser(model.Id, _taskService);
        if (checkUser == null)
        {
            result.Code = StatusCodes.Status404NotFound;
            result.Message = "İş bulunamadı.";
            result.Data = MessageNotFound(model.Id);
            return Results.NotFound(result);
        }
        var data = await _taskService.UpdateUser(model);
        result.Code = 200;
        result.Message = $"Güncelleme Başarılı {model.Id}";
        result.Data = data;
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        result.Code = 400;
        result.Message = $"Güncelleme Başarısız";
        result.Data = ex.Message;
        return Results.BadRequest(result);
    }    
});
users.MapDelete("/{id}", async (ITaskService _taskService, int id) =>
{
    var result = new CommonResponse();
    try {
        var checkUser = await CheckUser(id, _taskService);
        if (checkUser == null)
        {
            result.Code = StatusCodes.Status404NotFound;
            result.Message = "İş Bulunamadı.";
            result.Data = MessageNotFound(id);
            return Results.NotFound(result);
        }
        var data = await _taskService.DeleteUser(id);
        result.Code = 200;
        result.Message = $"İş Silindi.";
        result.Data = data;
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        result.Code = 400;
        result.Message = $"Başarısız";
        result.Data = ex.Message;
        return Results.BadRequest(result);
    }    
});
static async Task<SigortaApp.MinimalAPii.Task?> CheckUser(int id, ITaskService _taskService)
{
    var data = await _taskService.GetUserDetail(id);
    return data;
}
static string MessageNotFound(int id)
{
    return $"User with id {id} not found";
}


app.Run();
