using NetcareDoctorsAPI.BLL;
using NetcareDoctorsAPI.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMvcCore(options =>
{
    options.Filters.Add(new NetcareAPIErrorHandler());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

StaticClass.InitializeAppSettings(app.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.
                       AllowAnyOrigin().
                       AllowAnyHeader().
                       WithMethods(new string[] { "Get", "Post", "Put", "Delete" }));


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
