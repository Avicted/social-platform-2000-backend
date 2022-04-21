using sp2000.Application.Interfaces;

namespace sp2000.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}