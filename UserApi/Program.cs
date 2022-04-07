using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Helper;
using UserApi.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddDbContext<UserDataContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MigrateDatabase();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/user/{id}", ([FromServices] IUserRepository db, int id) =>
{
    return db.GetUserById(id);
});


app.MapGet("/users", ([FromServices] IUserRepository db) =>
    {
        return db.GetUsers();
    }
);

app.MapPut("/user/{id}", ([FromServices] IUserRepository db, User user) =>
{
    return db.PutUser(user);
});

app.MapPost("/user", ([FromServices] IUserRepository db, User user) =>
{
    return db.AddUser(user);
});
app.MapDelete("/user/{id}", ([FromServices] IUserRepository db, int id) =>
{
    if (db.DeleteUser(id))
    {
        return Results.Ok();
    }
    return Results.NotFound();
});
app.Run();
