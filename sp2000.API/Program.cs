using AutoWrapper;
using Microsoft.EntityFrameworkCore;
using sp2000.Application.Services;
using Microsoft.OpenApi.Models;
using sp2000.Infrastructure;
using sp2000.Application.Interfaces;
using sp2000.API.Services;
using sp2000.Application;
using sp2000.Infrastructure.Persistance;
using sp2000.Application.Helpers;


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
builder.Services.Configure<ApplicationSettings>(
    builder.Configuration.GetSection("JWTConfig")
);

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

// @Note(Avic): Scoped services live as long as one request

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IPostsService, PostService>();
builder.Services.AddScoped<ICommentsService, CommentsService>();
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();

// builder.Services.AddControllersWithViews();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Social Platform API",
        Description = "A Dotnet Core Web API for discussing topics in a forum",
        Contact = new OpenApiContact()
        {
            Name = "Victor Anderssén",
            Url = new Uri("https://notasoftwaredevelopmentcompany.com"),
        },
        License = new OpenApiLicense()
        {
            Name = "MIT",
            Url = new Uri("https://choosealicense.com/licenses/mit/"),
        },
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
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
    IsApiOnly = true,
    UseApiProblemDetailsException = true,
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(MyAllowSpecificOrigins);
}

app.UseDefaultFiles(); // so index.html is not required
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// check Swagger authentication
app.Use(async (context, next) =>
{
    var path = context.Request.Path;
    if (path.Value.Contains("/swagger/", StringComparison.OrdinalIgnoreCase))
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            context.Response.Redirect("/login");
            return;
        }
    }

    await next();
});

app.UseEndpoints(app =>
{
    app.MapControllers();
});

// catch-all handler for HTML5 client routes - serve index.html
/* app.Run(async context =>
{
    var path = context.Request.Path.Value;
    var environment = builder.Environment;

    // Make sure Angular output was created in wwwroot
    // Running Angular in dev mode nukes output folder!
    // so it could be missing.
    if (environment.WebRootPath == null)
    {
        throw new InvalidOperationException("wwwroot folder doesn't exist. Please recompile your React Project before accessing index.html. API calls will work fine.");
    }

    context.Response.ContentType = "text/html";
    await context.Response.SendFileAsync(Path.Combine(environment.WebRootPath, "index.html"));
}); */

app.Run();