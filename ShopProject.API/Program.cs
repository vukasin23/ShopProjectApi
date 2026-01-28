var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<ShopProjectContext>(x => );

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();





//Komande za git 

//Git status, git add ., git commit -m "poruka", git push 