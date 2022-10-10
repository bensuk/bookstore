using bookstore.Data;
using bookstore.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<BookstoreDbContex>();
builder.Services.AddTransient<IPublishersRespository, PublishersRespository>();
builder.Services.AddTransient<IAuthorsRepository, AuthorsRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();