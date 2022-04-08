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

namespace sp2000.UnitTests
{
    public class CategoriesControllerTests
    {
        [Fact]
        public async void GetCategories_Returns_A_List_Of_Categories()
        {
            // Arrange
            var fakePosts = new List<PostDto> { 
                new PostDto()
                {
                    PostId = 1,
                    Title = "This is the post title for the first post",
                    CategoryId = 1,
                    Content = "The content is a text field with the actual forum post content.",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new PostDto()
                {
                    PostId = 2,
                    Title = "This is the post title for the second post",
                    CategoryId = 2,
                    Content = "The content is a text field with the actual forum post content.",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            };

            var fakeCategories = new List<CategoryDto> {
                new CategoryDto() {
                    CategoryId = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    PostsCount = 2,
                    Title = "Test title 1"
                },
                new CategoryDto() {
                    CategoryId = 2,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    PostsCount = 0,
                    Title = "Test title 2"
                },
                new CategoryDto() {
                    CategoryId = 3,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    PostsCount = 0,
                    Title = "Test title 3"
                }
            };
            
            // A.CollectionOfDummy<CategoryDto>(count).AsEnumerable();

            var categoriesService= A.Fake<ICategoriesService>();
            A.CallTo(() => categoriesService.GetCategories(null)).Returns(Task.FromResult(fakeCategories));

            var postsService= A.Fake<IPostsService>();
            var controller = new CategoriesController(categoriesService, postsService);


            // Act
            var actionResult = await controller.GetCategories(null);

            
            // Assert
            var okResult = (OkObjectResult)actionResult;

            Assert.NotNull(okResult);

            var result = okResult.Value as List<CategoryDto>;

            Assert.Equal(fakeCategories.Count, result?.Count);
        }
    }
}