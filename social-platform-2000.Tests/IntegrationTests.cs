using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using FluentAssertions;
using Respawn;

namespace social_platform_2000.Tests;

[SetUpFixture]
public class Tests
{
    private static IConfigurationRoot _configuration = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    private static Checkpoint _checkpoint = null!;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddEnvironmentVariables();
        // .AddJsonFile("appsettings.json", true, true)

        _configuration = builder.Build();

        // var startup = new Startup(_configuration);
        var services = new ServiceCollection();

        services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
            w.EnvironmentName == "Development" &&
            w.ApplicationName == "social-platform-2000-backend"));

        services.AddLogging();

        var app = builder.Build();

        // app. startup.ConfigureServices(services);
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TheAnswerToAllQuestions()
    {
        Assert.AreEqual(42.0f, 42.0f);
    }
}