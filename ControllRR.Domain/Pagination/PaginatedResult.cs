namespace ControllRR.Domain.PaginatedResult;


public class PaginatedResult<T>
{
    public IEnumerable<T> Data { get; set; }
    public int TotalRecords { get; set; }
    public int FilteredRecords { get; set; }
}