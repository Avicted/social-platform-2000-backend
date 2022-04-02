using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace social_platform_2000_backend.Models;

public class BaseEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}