namespace backend.Domain.Specifications;

public class Pagination<T> where T : class
{
    
    public IReadOnlyCollection<T> Data { get; set; }
    public int Count { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; }

    public Pagination()
    {
    }

    public Pagination(int pageIndex, int pageSize, int count, IReadOnlyCollection<T> data)
    {
        PageIndex = pageIndex;
        PageSize =  pageSize;
        Count = count;
        Data = data;
    }
}