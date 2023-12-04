namespace Notifications.Infrastructure.Domain.Common.Exceptions;

public class FuncResult<T>
{
    public T Data { get; set; }

    public Exception? Exception {  get; set; }
    
    public bool IsSuccess => Exception is null;

    public FuncResult(T data) => Data = data;

    public FuncResult(Exception exception) => Exception = exception;
}
    