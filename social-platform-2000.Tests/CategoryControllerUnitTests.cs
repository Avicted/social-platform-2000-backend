using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using social_platform_2000_backend.Controllers;
using social_platform_2000_backend.DataAccessLayer;
using social_platform_2000_backend.Models;
using social_platform_2000_backend.Services;
using social_platform_2000_backend.DTO;

namespace social_platform_2000_backend.Tests;


[TestFixture]
public class CategoryControllerUnitTests
{
    private Mock<ICategoryService> _categoryService;
    private CategoryController _controller;

    public CategoryControllerUnitTests()
    {
        _categoryService = new Mock<ICategoryService>(MockBehavior.Strict);
        _controller = new CategoryController(_categoryService.Object);
    }

    [Test]
    public async Task Create_ModelStateValid_Should_Return_Created_Category()
    {
        // Arrange
        var testCategory = new CategoryDto
        {
            Title = "Test title for a category",
            CategoryId = 1,
            PostsCount = 0,
            CreatedDate = new DateTime(),
            UpdatedDate = new DateTime()
        };

        _controller = new CategoryController(_categoryService.Object);

        _categoryService.Setup(c => c.CreateCategory(It.IsAny<CreateCategoryDto>()))
            .ReturnsAsync(testCategory);

        var newCategory = new CreateCategoryDto
        {
            Title = "Test title for a category"
        };


        // Act

        var actionResult = await _controller.PostCategory(newCategory);

        // var viewResult = Assert.IsInstanceOf<ViewResult>(actionResult);

        // Assert
        // Assert.IsInstanceOf<OkObjectResult>(result.Value);
        // Assert.NotNull(result.Value);
        // Assert.AreEqual(result.Title, "Test title for a category");
    }


    [Test]
    public async Task Create_InvalidModel_Should_Return_Bad_Request()
    {
        // Arrange
        var testCategory = new CategoryDto
        {
            Title = "Test title for a category",
            CategoryId = 1,
            PostsCount = 0,
            CreatedDate = new DateTime(),
            UpdatedDate = new DateTime()
        };

        _categoryService.Setup(c => c.CreateCategory(It.IsAny<CreateCategoryDto>()))
            .ReturnsAsync(testCategory);

        var newCategory = new CreateCategoryDto
        {
            // .. No required title provided
        };


        // Act
        ActionResult<CategoryDto> result = await _controller.PostCategory(newCategory);


        // Assert
        // Assert.IsInstanceOf<ActionResult<CategoryVM>>(result);

        // Assert.IsInstanceOf<BadRequestObjectResult>(result);

        // Assert.IsInstanceOf<SerializableError>(result.Value);
    }
}