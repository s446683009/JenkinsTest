namespace Identity.Application.Dtos;

public class PageResult<T>
{
    public IEnumerable<T> List { get; set; }
    public int Total { get; set; }
    public int Page { get; set; }
    public int Rows { get; set; }
}