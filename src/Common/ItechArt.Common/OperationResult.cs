using System;

namespace ItechArt.Common;

public class OperationResult<TResult, TError>
    where TError : struct, Enum
{
    private readonly TResult _result;


    public bool IsSuccessful { get; }

    public TResult Result
        => IsSuccessful
            ? _result
            : throw new Exception("Result was not set");

    public TError? Error { get; }


    private OperationResult(bool isSuccessful, TResult result, TError? error)
    {
        IsSuccessful = isSuccessful;
        _result = result;
        Error = error;
    }


    public static OperationResult<TResult, TError> CreateSuccessful(TResult result)
    {
        return new OperationResult<TResult, TError>(true, result, null);
    }

    public static OperationResult<TResult, TError> CreateUnsuccessful(TError error)
    {
        return new OperationResult<TResult, TError>(false, default, error);
    }
}