var AllowOrigin = "AllowAllOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowOrigin,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173",
                                              "*")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors(AllowOrigin);

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
