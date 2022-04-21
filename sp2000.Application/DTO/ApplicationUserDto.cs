
using sp2000.Application.Models;

namespace sp2000.Application.DTO;

public class ApplicationUserDto : BaseEntity
{
    public virtual string UserName { get; set; }
}