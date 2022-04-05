using sp2000.Models;

namespace Infrastructure;

public static class ApplicationDbContextSeed
{
    // @Note(Avic): This is where we seed the base of the data
    public static async Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        // Seed, if necessary
        if (!context.Categories.Any())
        {
            var category_1 = context.Categories.Add(new Category
            {
                Title = "This is the first category title",
            }).Entity;

            var category_2 = context.Categories.Add(new Category
            {
                Title = "This is the second category title",
            }).Entity;

            var category_3 = context.Categories.Add(new Category
            {
                Title = "This is the third category title",
            }).Entity;

            context.Categories.Add(new Category
            {
                Title = "This is the fourth category title",
            });
            context.Categories.Add(new Category
            {
                Title = "This is the fifth category title",
            });
            context.Categories.Add(new Category
            {
                Title = "This is the sixth category title",
            });
            context.Categories.Add(new Category
            {
                Title = "This is the seventh category title",
            });
            context.Categories.Add(new Category
            {
                Title = "This is the eighth category title",
            });
            context.Categories.Add(new Category
            {
                Title = "This is the nineth category title",
            });
            context.Categories.Add(new Category
            {
                Title = "This is the tenth category title",
            });

            await context.SaveChangesAsync();


            context.Posts.Add(new Post
            {
                CategoryId = category_1.CategoryId,
                Title = "This is the title of the first post",
                Content = "The content of the first post is short and contains skands åäö ÅÄÖ 123 !?",
            });
            context.Posts.Add(new Post
            {
                CategoryId = category_1.CategoryId,
                Title = "This is the title of the second post",
                Content = "The content of the second post is longer than the first post The content of the second post is longer than the first post The content of the second post is longer than the first post The content of the second post is longer than the first post The content of the second post is longer than the first post The content of the second post is longer than the first post.",
            });
            context.Posts.Add(new Post
            {
                CategoryId = category_2.CategoryId,
                Title = "This is the title of the third post",
                Content = "The content of the third post is very short",
            });
            context.Posts.Add(new Post
            {
                CategoryId = category_3.CategoryId,
                Title = "This is the test title",
                Content = "The content here is quite short",
            });

            await context.SaveChangesAsync();
        }
    }
}