using sp2000.Application.Interfaces;

namespace sp2000.Application.Helpers;

public class ApplicationSettings : IApplicationSettings
{
    public string Secret { get; set; } = null!;
}
