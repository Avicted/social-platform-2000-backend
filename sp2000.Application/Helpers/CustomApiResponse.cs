
namespace sp2000.Application.Helpers;

public class CustomApiResponse
{
    public int Code { get; set; }
    public string Message { get; set; }
    public object Result { get; set; }
    public Pagination Pagination { get; set; }

    public CustomApiResponse(
        object result = null!,
        string message = "",
        int statusCode = 200,
        Pagination pagination = null!)
    {
        this.Code = statusCode;
        this.Message = message == string.Empty ? "Success" : message;
        this.Result = result;
        this.Pagination = pagination;
    }

    public CustomApiResponse(
        object? result = null,
        Pagination pagination = null!)
    {
        this.Code = 200;
        this.Message = "Success";
        this.Result = result!;
        this.Pagination = pagination;
    }

    public CustomApiResponse(object result)
    {
        this.Code = 200;
        this.Message = string.Empty;
        this.Result = result!;
        this.Pagination = null!;
    }
}

public class Pagination
{
    public int TotalItemsCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}