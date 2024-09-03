using sp2000.Application.Models;
using sp2000.Infrastructure.Persistance;

namespace sp2000.Infrastructure;

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
                Title = "This is the 6th category title",
            });
            context.Categories.Add(new Category
            {
                Title = "This is the 7th category title",
            });
            context.Categories.Add(new Category
            {
                Title = "This is the 8th category title",
            });
            context.Categories.Add(new Category
            {
                Title = "This is the 9th category title",
            });
            context.Categories.Add(new Category
            {
                Title = "This is the 10th category title",
            });
            context.Categories.Add(new Category
            {
                Title = "This is the 11th category title",
            });
            context.Categories.Add(new Category
            {
                Title = "This is the 12th category title",
            });
            context.Categories.Add(new Category
            {
                Title = "This is the 13th category title",
            });
            context.Categories.Add(new Category
            {
                Title = "This is the 14th category title",
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



            context.Comments.Add(new Comment
            {
                CommentId = 100,
                AuthorName = "Jane DoeDoeDoeDoeDoeSurname",
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In gravida auctor est, in varius augue interdum ac. Praesent pharetra mattis nisl at suscipit.",
                PostId = 1,
            });
            context.Comments.Add(new Comment
            {
                CommentId = 101,
                AuthorName = "Bob",
                Content = "Steave bob is the best painter in the world",
                PostId = 1,
                ParentCommentId = 100,
            });
            context.Comments.Add(new Comment
            {
                CommentId = 102,
                AuthorName = "Alice",
                Content = "Hello Team!",
                PostId = 1,
                ParentCommentId = 100,
            });
            context.Comments.Add(new Comment
            {
                CommentId = 103,
                AuthorName = "Bob",
                Content = "Testing 123 åäö ÅÄÖ !",
                PostId = 1,
                ParentCommentId = 102,
            });
            context.Comments.Add(new Comment
            {
                CommentId = 104,
                AuthorName = "helloteam",
                Content = "This is a new comment",
                PostId = 1,
                ParentCommentId = 103,
            });
            context.Comments.Add(new Comment
            {
                CommentId = 105,
                AuthorName = "This a longer name than usual",
                Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                PostId = 1,
                ParentCommentId = 104,
            });


            await context.SaveChangesAsync();
        }
    }
}