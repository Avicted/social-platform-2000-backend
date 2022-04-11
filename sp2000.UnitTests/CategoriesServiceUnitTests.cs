using System;
using sp2000.Application.DTO;
using System.Collections.Generic;

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

    /*[Fact]
    public async void GetCategories_Returns_Categories()
    {
        // Arrange
        var wrapper = A.Fake<IRepositoryWrapper>();
        var mapper = A.Fake<IMapper>();

        var sut = new CategoriesService(wrapper, mapper);
        List<CategoryDto> fakeCategories = GetFakeCategories();

        A.CallTo(() => wrapper.Category.GetAllGategoriesAsync()).Returns(fakeCategories.AsQueryable());

        // Act
        var result = await sut.GetCategories(null);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<List<CategoryDto>>(result);
        Assert.Equal(fakeCategories.Count, result.Count);
    }*/

    /*[Fact]
    public async void GetCategories_Returns_An_Empty_List_Of_Categories()
    {
        // Arrange
        var wrapper = A.Fake<IRepositoryWrapper>();
        var mapper = A.Fake<IMapper>();

        var sut = new CategoriesService(wrapper, mapper);
        List<CategoryDto> fakeCategories = GetFakeCategories();

        A.CallTo(() => wrapper.Category.GetAllGategoriesAsync()).Returns(new List<CategoryDto>());

        // Act
        var result = await sut.GetCategories(null);

        // Assert
        Assert.Equal(result, new List<CategoryDto>());
    }*/

    /*[Fact]
    public async void CreateCategory_Returns_The_Created_Category()
    {
        // Arrange
        var newCategory = new CreateCategoryDto()
        {
            Title = "This is a title for a new category",
        };

        var fakeCategory = new Category()
        {
            CategoryId = 1,
            Title = "Category title here åäö ÅÄÖ",
            CreatedDate = DateTime.Today,
            UpdatedDate = DateTime.Today,
        };

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "CategoriesDatabase")
            .Options;
        
        // Insert seed data into the database using one instance of the context
        using (var context = new ApplicationDbContext(options))
        {
            context.Categories.Add(fakeCategory);
            context.SaveChanges();
        }

        // Use a clean instance of the context to run the test
        using (var context = new ApplicationDbContext(options))
        {
            var categoriesRepository = new CategoriesRepository(context);
            var wrapper = new Mock<IRepositoryWrapper>();

            // categoriesRepository.CreateCategory
            wrapper.Setup(w => w.Category.CreateCategory(It.IsAny<Category>()));

            // List<Movies> movies == movieRepository.GetAll()

            // Assert.Equal(3, movies.Count);
            var mapper = A.Fake<IMapper>();
            var sut = new CategoriesService(wrapper.Object, mapper);

            // Act
            var result = await sut.CreateCategory(newCategory);

            // Assert
            Assert.Equal(newCategory.Title, result.Title);
        }
    }*/
}
