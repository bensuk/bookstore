using bookstore.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookstoreDbContex>();


var app = builder.Build();

app.Run();