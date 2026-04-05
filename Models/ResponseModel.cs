namespace mvc_backend_1.Models;

public class ResponseModel<T>
{
    public string? Message { get; set; }
    public required string Status { get; set; }
    public T? Data { get; set; }
}
