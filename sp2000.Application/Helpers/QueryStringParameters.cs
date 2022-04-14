namespace sp2000.Application.Helpers;

public abstract class QueryStringParameters
{
    public const int MaxPageSize = 25;
    private int _pageSize = 1;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value > MaxPageSize) ? MaxPageSize: value;
        }
    }
    public int PageNumber { get; set; } = 1;
}

