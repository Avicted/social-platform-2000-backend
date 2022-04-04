using social_platform_2000_backend.Models;

namespace social_platform_2000_backend.DataAccessLayer;

public static class ApplicationDbContextSeed
{
    // @Note(Avic): This is where we seed the base of the data
    public static async Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        // Seed, if necessary
        if (!context.Categories.Any())
        {
            context.Categories.Add(new Category
            {
                Title = "This is the first category title",
            });
            context.Categories.Add(new Category
            {
                Title = "This is the second category title",
            });
            context.Categories.Add(new Category
            {
                Title = "This is the third category title",
            });

            context.Posts.Add(new Post
            {
                CategoryId = 1,
                Title = "This is the title of the first post",
                Content = "The content of the first post is short and contains skands åäö ÅÄÖ 123 !?",
            });
            context.Posts.Add(new Post
            {
                CategoryId = 1,
                Title = "This is the title of the second post",
                Content = "The content of the second post is longer than the first post The content of the second post is longer than the first post The content of the second post is longer than the first post The content of the second post is longer than the first post The content of the second post is longer than the first post The content of the second post is longer than the first post.",
            });
            context.Posts.Add(new Post
            {
                CategoryId = 2,
                Title = "This is the title of the third post",
                Content = "The content of the third post is very short",
            });

            await context.SaveChangesAsync();
        }
    }
}