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

namespace sp2000.UnitTests
{
    public class CategoriesControllerTests
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
        public async void GetCategories_Returns_A_List_Of_Categories()
        {
            // Arrange
            var fakePosts = GetFakePosts();
            var fakeCategories = GetFakeCategories();

            var categoriesService = A.Fake<ICategoriesService>();
            A.CallTo(() => categoriesService.GetCategories(null)).Returns(Task.FromResult(fakeCategories));

            var postsService = A.Fake<IPostsService>();
            var controller = new CategoriesController(categoriesService, postsService);

            // Act
            var actionResult = await controller.GetCategories(null);

            // Assert
            var okResult = (OkObjectResult)actionResult;
            Assert.NotNull(okResult);

            var result = okResult.Value as List<CategoryDto>;
            Assert.Equal(fakeCategories.Count, result?.Count);
        }

        [Fact]
        public async void GetCategories_Returns_NotFound()
        {
            // Arrange
            var fakePosts = GetFakePosts();
            var fakeCategories = new List<CategoryDto>();

            var categoriesService = A.Fake<ICategoriesService>();
            A.CallTo(() => categoriesService.GetCategories(null)).Returns(Task.FromResult(fakeCategories));

            var postsService = A.Fake<IPostsService>();
            var controller = new CategoriesController(categoriesService, postsService);

            // Act
            var actionResult = await controller.GetCategories(null);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

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
            var okResult = (OkObjectResult)actionResult;
            Assert.NotNull(okResult);

            var result = okResult.Value as CategoryDto;
            Assert.Equal(fakeCategory, result);
        }

        [Fact]
        public async void GetCategoryById_Returns_NotFound()
        {
            // Arrage
            var categoriesService = A.Fake<ICategoriesService>();
            A.CallTo(() => categoriesService.GetCategoryByID(1337)).Returns(Task.FromResult<CategoryDto?>(null));

            var postsService = A.Fake<IPostsService>();
            var controller = new CategoriesController(categoriesService, postsService);

            // Act
            var actionResult = await controller.GetCategoryByID(1337);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void GetPostsInCategory_Returns_Posts()
        {
            // Arrage
            var categoriesService = A.Fake<ICategoriesService>();
            var postsService = A.Fake<IPostsService>();
            A.CallTo(() => postsService.GetPostsInCategory(2, null)).Returns(Task.FromResult(GetFakePosts()));

            var controller = new CategoriesController(categoriesService, postsService);

            // Act
            var actionResult = await controller.GetPostsInCategory(2, null);

            // Assert
            Assert.NotNull(actionResult);

            var okResult = (OkObjectResult)actionResult;
            var result = okResult.Value as List<PostDto>;

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
            A.CallTo(() => postsService.GetPostsInCategory(2, null)).Returns(Task.FromResult(GetFakePosts()));

            var controller = new CategoriesController(categoriesService, postsService);

            // Act
            var actionResult = await controller.GetPostsInCategory(2, null);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
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
            var actionResult = controller.PutCategory(1, updateCategory);

            // Assert
            Assert.NotNull(actionResult);

            var result = actionResult.Result as CategoryDto;

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

            var fakeCategory = GetFakeCategories().First();
            fakeCategory.Title = updateCategory.Title;

            A.CallTo(() => categoriesService.UpdateCategory(1, updateCategory)).Returns(Task.FromResult<CategoryDto?>(null));

            var controller = new CategoriesController(categoriesService, postsService);

            // Act
            var actionResult = controller.PutCategory(1, updateCategory);

            // Assert
            Assert.Null(actionResult);
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async void PostCategory_Returns_Created_Category()
        {

        }

        [Fact]
        public async void PostCategory_Returns_BadRequest_On_Invalid_Model()
        {

        }

        [Fact]
        public async void DeleteCategory_Returns_Ok_NoContent()
        {

        }

        [Fact]
        public async void DeleteCategory_Returns_NotFound()
        {

        }
    }
}