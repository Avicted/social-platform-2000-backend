
using sp2000.Application.Models;

namespace sp2000.Application.DTO;

public class AuthenticationResponseDto : BaseEntity
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string AccessToken { get; set; }
    public DateTime Expires { get; set; }


    public AuthenticationResponseDto(ApplicationUser user, string token, DateTime expires)
    {
        Id = user.Id;
        Username = user.Username;
        AccessToken = token;
        Expires = expires;
    }
}
