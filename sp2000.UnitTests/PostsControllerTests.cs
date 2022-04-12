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
using sp2000.Application.Interfaces;

namespace sp2000.UnitTests;

public class PostsControllerTests
{
    private static List<PostDto> GetFakePosts()
    {
        return new List<PostDto> {
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

    [Fact]
    public async void GetPostById_Should_Return_Post()
    {
        // Arrage
        var fakePost = new PostDto()
        {
            PostId = 1,
            Title = "This is a post title",
            CategoryId = 1,
            Content = "This is the post content 123 åäö",
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
        };

        var postsService = A.Fake<IPostsService>();
        var commentsService = A.Fake<ICommentsService>();

        A.CallTo(() => postsService.GetPostByID(1)).Returns(Task.FromResult(fakePost));

        var controller = new PostsController(postsService, commentsService);

        // Act
        var actionResult = await controller.GetPostByID(1);

        // Assert
        var okResult = (CustomApiResponse)actionResult;
        Assert.NotNull(okResult);

        var result = okResult.Result as PostDto;
        Assert.Equal(fakePost, result);
    }

    [Fact]
    public async void GetPostById_Should_Return_NotFound()
    {
        // Arrange
        var postsService = A.Fake<IPostsService>();
        var commentsService = A.Fake<ICommentsService>();

        A.CallTo(() => postsService.GetPostByID(1)).Returns(Task.FromResult<PostDto?>(null));

        var controller = new PostsController(postsService, commentsService);

        // Act
        var actionResult = await controller.GetPostByID(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(actionResult);
    }

    [Fact]
    public async void PutPost_Should_Return_Updated_Post()
    {
        // Arrage
        var updatePost = new UpdatePostDto()
        {
            CategoryId = 1,
            Title = "This is the new title of post id 1",
            Content = "This is the new content of the first post åäö 123"
        };

        var categoriesService = A.Fake<ICategoriesService>();
        var postsService = A.Fake<IPostsService>();
        var commentsService = A.Fake<ICommentsService>();

        var fakePost = GetFakePosts().First();
        fakePost.CategoryId = updatePost.CategoryId;
        fakePost.Title = updatePost.Title;
        fakePost.Content = updatePost.Content;

        A.CallTo(() => postsService.UpdatePost(1, updatePost)).Returns(Task.FromResult(fakePost));

        var controller = new PostsController(postsService, commentsService);

        // Act
        var actionResult = await controller.PutPost(1, updatePost);

        // Assert
        Assert.NotNull(actionResult);

        var okResult = (CustomApiResponse)actionResult;
        var result = okResult.Result as PostDto;

        var obj1Str = JsonConvert.SerializeObject(fakePost);
        var obj2Str = JsonConvert.SerializeObject(result);
        Assert.Equal(obj1Str, obj2Str);
    }

    [Fact]
    public async void PutPost_Should_Return_NotFound()
    {
        // Arrage
        var updatePost = new UpdatePostDto()
        {
            CategoryId = 1,
            Title = "This is the new title of post id 1",
            Content = "This is the new content of the first post åäö 123"
        };

        var categoriesService = A.Fake<ICategoriesService>();
        var postsService = A.Fake<IPostsService>();
        var commentsService = A.Fake<ICommentsService>();


        A.CallTo(() => postsService.UpdatePost(2000, updatePost)).Returns(Task.FromResult<PostDto?>(null));

        var controller = new PostsController(postsService, commentsService);

        // Act
        var actionResult = await controller.PutPost(2000, updatePost);

        // Assert
        Assert.IsType<NotFoundObjectResult>(actionResult);
    }

    [Fact]
    public async void CreatePost_Should_Return_Created_Post()
    {
        // Arrange
        var postsService = A.Fake<IPostsService>();
        var commentsService = A.Fake<ICommentsService>();

        var newPost = new CreatePostDto()
        {
            CategoryId = 1,
            Title = "This is the post title for the first post",
            Content = "The content is a text field with the actual forum post content.",
        };

        var fakeCreatedPost = GetFakePosts().First();
        fakeCreatedPost.CategoryId = newPost.CategoryId;
        fakeCreatedPost.Title = newPost.Title;
        fakeCreatedPost.Content = newPost.Content;

        A.CallTo(() => postsService.CreatePost(newPost)).Returns(Task.FromResult(fakeCreatedPost));

        var controller = new PostsController(postsService, commentsService);

        // Act
        var actionResult = await controller.CreatePost(newPost);

        // Assert
        Assert.NotNull(actionResult);

        var okResult = actionResult as CustomApiResponse;
        var result = okResult?.Result as PostDto;

        var obj1Str = JsonConvert.SerializeObject(fakeCreatedPost);
        var obj2Str = JsonConvert.SerializeObject(result);
        Assert.Equal(obj1Str, obj2Str);
    }

    [Fact]
    public async void CreatePost_Returns_BadRequest_On_Invalid_Model()
    {
        // Arrange
        var postsService = A.Fake<IPostsService>();
        var commentsService = A.Fake<ICommentsService>();

        var newPost = new CreatePostDto()
        {
            // Invalid model test!
            // CategoryId = 1,
            // Title = "This is the post title for the first post",
            // Content = "The content is a text field with the actual forum post content.",
        };

        var fakeCreatedPost = GetFakePosts().First();

        A.CallTo(() => postsService.CreatePost(newPost)).Returns(Task.FromResult(fakeCreatedPost));

        var controller = new PostsController(postsService, commentsService);

        // Act
        var actionResult = await controller.CreatePost(newPost);

        // Assert
        Assert.NotNull(actionResult);
        Assert.IsType<BadRequestObjectResult>(actionResult);
    }

    [Fact]
    public async void DeletePost_Returns_Ok_NoContent()
    {
        // Arrange
        var postsService = A.Fake<IPostsService>();
        var commentsService = A.Fake<ICommentsService>();

        A.CallTo(() => postsService.DeletePost(It.IsAny<int>())).Returns(Task.FromResult(true));

        var controller = new PostsController(postsService, commentsService);

        // Act
        var actionResult = await controller.DeletePost(1);

        // Assert
        Assert.NotNull(actionResult);
        Assert.IsType<NotFoundObjectResult>(actionResult);
    }

    [Fact]
    public async void DeletePost_Returns_NotFound()
    {
        // Arrange
        var postsService = A.Fake<IPostsService>();
        var commentsService = A.Fake<ICommentsService>();

        A.CallTo(() => postsService.DeletePost(It.IsAny<int>())).Returns(Task.FromResult(false));

        var controller = new PostsController(postsService, commentsService);

        // Act
        var actionResult = await controller.DeletePost(1);

        // Assert
        Assert.NotNull(actionResult);
        Assert.IsType<NotFoundObjectResult>(actionResult);
    }
}
