﻿namespace sp2000.Application.DTO;

public class CreateCommentDto
{
    public string AuthorName { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int PostId { get; set; }
    public int? ParentCommentId { get; set; }
    public Guid ApplicationUserId { get; set; }
}
