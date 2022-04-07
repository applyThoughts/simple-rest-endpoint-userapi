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
app.MapGet("/search/", ([FromServices] IUserRepository db,string? email,string? phone,string? orderBy) =>
    {
        var users = db.GetUsers();
        if (email!=null || phone!=null)
        {
            users = users.Where(u =>
                ((email != null && u.Email.Contains(email)) || (phone != null && u.Phone.Contains(phone))));

        }


        if (!string.IsNullOrEmpty(orderBy))
        {
            string direction = orderBy.ToLower().Contains("asc") ? "asc" : "desc";
            orderBy = orderBy.ToLower().Replace("asc","").Replace("desc","").Trim();
            switch (orderBy.ToLower())
            {
                case "fistname":
                    users = direction == "asc" ? users.OrderBy(u => u.FirstName) : users.OrderByDescending(u => u.FirstName);
                    break;
                case "lastname":
                    users = direction == "asc" ? users.OrderBy(u => u.LastName) : users.OrderByDescending(u => u.LastName);
                    break;
                case "age":
                    users = direction == "asc" ? users.OrderBy(u => u.Age) : users.OrderByDescending(u => u.Age);
                    break;
                default:
                    break;
            }
        }

        return users;
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
