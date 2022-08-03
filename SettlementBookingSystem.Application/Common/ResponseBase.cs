using System.Collections.Generic;

namespace SettlementBookingSystem.Application.Common;

public class ResponseBase<T>
{
    public T Data { get; set; }
    public bool Success { get; set; }
    public IEnumerable<Error> Errors { get; set; }

    public ResponseBase(T data)
    {
        Success = true;
        Data = data;
    }
    
    public ResponseBase(IEnumerable<Error> errors)
    {
        Success = false;
        Data = default(T);
        Errors = errors;
    }
    
    public ResponseBase(Error error)
    {
        Success = false;
        Data = default(T);
        Errors = new List<Error>{error};
    }
}