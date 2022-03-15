using System;

namespace ItechArt.Common;

public class OperationResult<TResult, TError>
    where TError : struct, Enum
{
    private readonly TResult _result;


    public bool IsSuccessful { get; }

    public TResult Result
        => !IsSuccessful
            ? throw new Exception("Result was not set")
            : _result;

    public TError? Error { get; }


    private OperationResult(bool isSucceccful, TResult result, TError? error)
    {
        IsSuccessful = isSucceccful;
        _result = result;
        Error = error;
    }


    public static OperationResult<TResult, TError> CreateSuccessful(TResult result)
    {
        return new OperationResult<TResult, TError>(true, result, null);
    }

    public static OperationResult<TResult, TError> CreateUnsuccessful(TError? error)
    {
        return new OperationResult<TResult, TError>(false, default, error);
    }
}