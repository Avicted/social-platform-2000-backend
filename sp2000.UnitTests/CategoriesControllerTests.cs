using Xunit;
using Moq;
using System;
using FakeItEasy;
using sp2000.Application.DTO;
using sp2000.Application.Helpers;
using sp2000.Application.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using sp2000.Application.Interfaces;
using sp2000.Application.Models;
using AutoWrapper.Wrappers;
using sp2000.API.Controllers;

namespace sp2000.UnitTests;

public class CategoriesControllerTests
{
    private static PagedList<PostDto> GetFakePosts()
    {
        return new PagedList<PostDto> {
            new PostDto()
            {
                PostId = 1,
                Title = "This is the post title for the first post",
                CategoryId = 1,
                Content = "The content is a text field with the actual forum post content.",
                CreatedDate = DateTime.Today,
                UpdatedDate = DateTime.Today
            },
            new PostDto()
            {
                PostId = 2,
                Title = "This is the post title for the second post",
                CategoryId = 2,
                Content = "The content is a text field with the actual forum post content.",
                CreatedDate = DateTime.Today,
                UpdatedDate = DateTime.Today
            }
        };
    }

    private static PagedList<CategoryDto> GetFakeCategories()
    {
        return new PagedList<CategoryDto> {
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

    /*[Fact]
    public async void GetCategories_Returns_A_List_Of_Categories()
    {
        // Arrange
        var fakePosts = GetFakePosts();
        var fakeCategories = GetFakeCategories();

        var categoriesService = A.Fake<ICategoriesService>();
        var postsService = A.Fake<IPostsService>();

        A.CallTo(() => categoriesService.GetCategories(null)).Returns(Task.FromResult(fakeCategories));

        var controller = new CategoriesController(categoriesService, postsService);

        // Act
        var actionResult = await controller.GetCategories(null);

        // Assert
        var okResult = (CustomApiResponse)actionResult;
        Assert.NotNull(okResult);

        var result = okResult.Result as List<CategoryDto>;
        Assert.Equal(fakeCategories.Count, result?.Count);
    }

    [Fact]
    public async void GetCategories_Returns_NotFound()
    {
        // Arrange
        var fakePosts = GetFakePosts();
        var fakeCategories = new List<CategoryDto>();

        var categoriesService = A.Fake<ICategoriesService>();
        var postsService = A.Fake<IPostsService>();

        A.CallTo(() => categoriesService.GetCategories(It.IsAny<int>())).Returns(Task.FromResult(fakeCategories));

        var controller = new CategoriesController(categoriesService, postsService);

        // Act
        var actionResult = await controller.GetCategories(null);

        // Assert
        Assert.IsType<NotFoundResult>(actionResult);
    }*/

    [Fact]
    public async void GetCategoryById_Returns_A_Category()
    {
        // Arrage
        var fakeCategory = new CategoryDto()
        {
            CategoryId = 1337,
            Title = "This is a category title",
            PostsCount = 0,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
        };

        var categoriesService = A.Fake<ICategoriesService>();
        A.CallTo(() => categoriesService.GetCategoryByID(1337)).Returns(Task.FromResult(fakeCategory));

        var postsService = A.Fake<IPostsService>();
        var controller = new CategoriesController(categoriesService, postsService);

        // Act
        var actionResult = await controller.GetCategoryByID(1337);

        // Assert
        var okResult = (CustomApiResponse)actionResult;
        Assert.NotNull(okResult);

        var result = okResult.Result as CategoryDto;
        Assert.Equal(fakeCategory, result);
    }

    [Fact]
    public async void GetCategoryById_Returns_NotFound()
    {
        // Arrage
        var categoriesService = A.Fake<ICategoriesService>();
        A.CallTo(() => categoriesService.GetCategoryByID(2000)).Returns(Task.FromResult<CategoryDto?>(null));

        var postsService = A.Fake<IPostsService>();
        var controller = new CategoriesController(categoriesService, postsService);

        // Act
        var actionResult = await controller.GetCategoryByID(2000);

        // Assert
        Assert.IsType<CustomApiResponse>(actionResult);
        Assert.Equal<int>(404, actionResult.Code);
    }

    [Fact]
    public async void GetPostsInCategory_Returns_Posts()
    {
        // Arrage
        var categoriesService = A.Fake<ICategoriesService>();
        var postsService = A.Fake<IPostsService>();

        PostParameters postParameters = new PostParameters
        {
            PageNumber = 1,
            PageSize = 10,
        };

        A.CallTo(() => postsService.GetPostsInCategory(It.IsAny<PostParameters>(), It.IsAny<int>())).Returns(
            Task.FromResult(GetFakePosts())
        );

        var controller = new CategoriesController(categoriesService, postsService);

        // Act
        var actionResult = await controller.GetPostsInCategory(postParameters, 1);

        // Assert
        Assert.NotNull(actionResult);

        var okResult = (CustomApiResponse)actionResult;
        var result = okResult.Result as PagedList<PostDto>;

        PostDto? fakePost = GetFakePosts().Find(p => p.PostId == 1);

        // @Note(Avic): Serialize the C# objects to JSON strings and compare them
        var obj1Str = JsonConvert.SerializeObject(fakePost);
        var obj2Str = JsonConvert.SerializeObject(result?.Find(p => p.PostId == 1));
        Assert.Equal(obj1Str, obj2Str);
    }

    [Fact]
    public async void GetPostsInCategory_Returns_NotFound()
    {
        // Arrage
        var categoriesService = A.Fake<ICategoriesService>();
        var postsService = A.Fake<IPostsService>();
        var fakeCategories = new List<PostDto>();

        PostParameters postParameters = new PostParameters
        {
            PageNumber = 1,
            PageSize = 10,
        };

        A.CallTo(() => postsService.GetPostsInCategory(It.IsAny<PostParameters>(), 2000)).Returns(
            Task.FromResult<PagedList<PostDto>>(new PagedList<PostDto>(new List<PostDto>(), 0, 0, 0))
        );

        var controller = new CategoriesController(categoriesService, postsService);

        // Act
        var actionResult = await controller.GetPostsInCategory(postParameters, 2000);

        // Assert
        Assert.Equal<int>(404, actionResult.Code);
        Assert.IsType<CustomApiResponse>(actionResult);
    }

    [Fact]
    public async void PutCategory_Returns_UpdatedCategory()
    {
        // Arrage
        var updateCategory = new UpdateCategoryDto()
        {
            Title = "This is a new title for category 1"
        };

        var categoriesService = A.Fake<ICategoriesService>();
        var postsService = A.Fake<IPostsService>();

        var fakeCategory = GetFakeCategories().First();
        fakeCategory.Title = updateCategory.Title;

        A.CallTo(() => categoriesService.UpdateCategory(1, updateCategory)).Returns(Task.FromResult(fakeCategory));

        var controller = new CategoriesController(categoriesService, postsService);

        // Act
        var actionResult = await controller.PutCategory(1, updateCategory);

        // Assert
        Assert.NotNull(actionResult);

        var okResult = (CustomApiResponse)actionResult;
        var result = okResult.Result as CategoryDto;

        var obj1Str = JsonConvert.SerializeObject(fakeCategory);
        var obj2Str = JsonConvert.SerializeObject(result);
        Assert.Equal(obj1Str, obj2Str);
    }

    [Fact]
    public async void PutCategory_Returns_NotFound()
    {
        // Arrage
        var updateCategory = new UpdateCategoryDto()
        {
            Title = "This is a new title for category 1"
        };

        var categoriesService = A.Fake<ICategoriesService>();
        var postsService = A.Fake<IPostsService>();

        A.CallTo(() => categoriesService.UpdateCategory(2000, updateCategory)).Returns(Task.FromResult<CategoryDto?>(null));

        var controller = new CategoriesController(categoriesService, postsService);

        // Act
        var actionResult = await controller.PutCategory(2000, updateCategory);

        // Assert
        Assert.Equal<int>(404, actionResult.Code);
        Assert.IsType<CustomApiResponse>(actionResult);
    }

    [Fact]
    public async void CreateCategory_Returns_Created_Category()
    {
        // Arrange
        var categoriesService = A.Fake<ICategoriesService>();
        var postsService = A.Fake<IPostsService>();

        var newCategory = new CreateCategoryDto()
        {
            Title = "This is the title of a new category"
        };

        var fakeCategories = GetFakeCategories();
        var createdCategory = fakeCategories.First();
        createdCategory.Title = newCategory.Title;

        A.CallTo(() => categoriesService.CreateCategory(newCategory)).Returns(Task.FromResult(createdCategory));

        var controller = new CategoriesController(categoriesService, postsService);

        // Act
        var actionResult = await controller.CreateCategory(newCategory);

        // Assert
        Assert.NotNull(actionResult);

        var okResult = actionResult as CustomApiResponse;
        var result = okResult?.Result as CategoryDto;

        var obj1Str = JsonConvert.SerializeObject(createdCategory);
        var obj2Str = JsonConvert.SerializeObject(result);
        Assert.Equal(obj1Str, obj2Str);
    }

    [Fact]
    public async void CreateCategory_Returns_BadRequest_On_Invalid_Model()
    {
        // Arrange
        var categoriesService = A.Fake<ICategoriesService>();
        var postsService = A.Fake<IPostsService>();

        var newCategory = new CreateCategoryDto()
        {
            // Title is missing here, the model is invalid
        };

        var fakeCategories = GetFakeCategories();
        var createdCategory = fakeCategories.First();
        createdCategory.Title = newCategory.Title;

        A.CallTo(() => categoriesService.CreateCategory(It.IsAny<CreateCategoryDto>())).Returns(Task.FromResult(createdCategory));

        var controller = new CategoriesController(categoriesService, postsService);

        // Act
        var actionResult = await Assert.ThrowsAsync<ApiProblemDetailsException>(async () => await controller.CreateCategory(newCategory));

        // Assert
        Assert.NotNull(actionResult);
        Assert.IsType<ApiProblemDetailsException>(actionResult);
    }

    [Fact]
    public async void DeleteCategory_Returns_Ok_NoContent()
    {
        // Arrange
        var categoriesService = A.Fake<ICategoriesService>();
        var postsService = A.Fake<IPostsService>();

        A.CallTo(() => categoriesService.DeleteCategory(It.IsAny<int>())).Returns(Task.FromResult(true));

        var controller = new CategoriesController(categoriesService, postsService);

        // Act
        var actionResult = await controller.DeleteCategory(1);

        // Assert
        Assert.NotNull(actionResult);
        Assert.IsType<CustomApiResponse>(actionResult);
    }

    [Fact]
    public async void DeleteCategory_Returns_NotFound()
    {
        // Arrange
        var categoriesService = A.Fake<ICategoriesService>();
        var postsService = A.Fake<IPostsService>();

        A.CallTo(() => categoriesService.DeleteCategory(It.IsAny<int>())).Returns(Task.FromResult(false));

        var controller = new CategoriesController(categoriesService, postsService);

        // Act
        var actionResult = await controller.DeleteCategory(1);

        // Assert
        Assert.NotNull(actionResult);
        Assert.IsType<CustomApiResponse>(actionResult);
    }
}
