using Xunit;
using Moq;
using System;
using FakeItEasy;
using sp2000.Application.DTO;
using sp2000.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using sp2000.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using sp2000.Interfaces;
using Microsoft.EntityFrameworkCore;
using sp2000.Models;
using AutoMapper;

namespace sp2000.UnitTests;
public class CategoriesServiceUnitTests
{

    private static List<CategoryDto> GetFakeCategories()
    {
        return new List<CategoryDto> {
            new CategoryDto() {
                CategoryId = 1,
                CreatedDate = DateTime.Today,
                UpdatedDate = DateTime.Today,
                PostsCount = 2,
                Title = "Test title 1"
            },
            new CategoryDto() {
                CategoryId = 2,
                CreatedDate = DateTime.Today,
                UpdatedDate = DateTime.Today,
                PostsCount = 0,
                Title = "Test title 2"
            },
            new CategoryDto() {
                CategoryId = 3,
                CreatedDate = DateTime.Today,
                UpdatedDate = DateTime.Today,
                PostsCount = 0,
                Title = "Test title 3"
            }
        };
    }

    [Fact]
    public async void GetCategories_Returns_Categories()
    {
        // Arrange
        var wrapper = A.Fake<IRepositoryWrapper>();
        var mapper = A.Fake<IMapper>();

        var service = new CategoriesService(wrapper, mapper);
        List<CategoryDto> fakeCategories = GetFakeCategories();

        A.CallTo(() => wrapper.Category.GetAllGategoriesAsync()).Returns(fakeCategories.AsQueryable());

        // Act
        var result = await service.GetCategories(null);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<List<CategoryDto>>(result);
    }

    [Fact]
    public async void GetCategories_Returns_An_Empty_List_Of_Categories()
    {
        // Arrange
        var wrapper = A.Fake<IRepositoryWrapper>();
        var mapper = A.Fake<IMapper>();

        var service = new CategoriesService(wrapper, mapper);
        List<CategoryDto> fakeCategories = GetFakeCategories();

        A.CallTo(() => wrapper.Category.GetAllGategoriesAsync()).Returns(new List<CategoryDto>());

        // Act
        var result = await service.GetCategories(null);

        // Assert
        Assert.Equal(result, new List<CategoryDto>());
    }
}
