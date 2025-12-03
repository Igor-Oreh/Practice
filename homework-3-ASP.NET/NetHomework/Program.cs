using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();
app.UseRouting();

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Response-Time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    await next();
});

app.MapControllers();

app.Run();

public class User
{
    public string Name {get; set;} = string.Empty;
    public int Age {get; set;}
}

[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    [HttpPost]
    public IActionResult CreateUser(User user)
    {
        return Ok(new { 
                Message = "Пользователь получен", 
                User = user 
            });
    }
}