var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(); // dodanie swaggera

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // swagger api
    app.UseSwaggerUI(); // ui dla swaggera
}

app.UseHttpsRedirection();

app.Run();