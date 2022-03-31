using System;

namespace ItechArt.Common;

public class OperationResult<TError>
    where TError : struct, Enum
{
    public bool IsSuccessful { get; }

    public TError? Error { get; }


    protected OperationResult(bool isSuccessful, TError? error)
    {
        IsSuccessful = isSuccessful;
        Error = error;
    }


    public static OperationResult<TError> CreateSuccessful()
    {
        return new OperationResult<TError>(true,null);
    }

    public static OperationResult<TError> CreateUnsuccessful(TError error)
    {
        return new OperationResult<TError>(default, error);
    }
}