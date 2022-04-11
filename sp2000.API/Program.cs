using AutoWrapper;
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using sp2000.Services;
using Microsoft.OpenApi.Models;
using sp2000.Infrastructure;
using sp2000.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// @Note(Avic): Allow React to connect
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
    builder =>
    {
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);

// @Note(Avic): Scoped services live as long as one request
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IPostsService, PostService>();
builder.Services.AddScoped<ICommentsService, CommentsService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Social Platform API",
        Description = "An ASP.NET Core Web API for discussing topics in a forum",
    });
});


var app = builder.Build();


// Seed the database with fake data
// ---------------------------------------------------------------------

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
        await ApplicationDbContextSeed.SeedSampleDataAsync(context);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        logger.LogError(ex, "An error occurred while migrating or seeding the database.");

        throw;
    }
}
// ---------------------------------------------------------------------


// @Note(Avic): Formats the REST API responses from the controllers so that
// errors and 200 OK results can easily be distiguished in the React client
// https://github.com/proudmonkey/AutoWrapper
app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
{
    ShowApiVersion = true,
    ShowStatusCode = true,
    UseCustomSchema = true,
    UseApiProblemDetailsException = true
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(MyAllowSpecificOrigins);
}

app.UseAuthorization();

app.MapControllers();

app.Run();
