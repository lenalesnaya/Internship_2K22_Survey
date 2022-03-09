using System;

namespace ItechArt.Common;

public class OperationResult<TResult, TError>
    where TError : Enum
{
    private readonly TResult _result;
    private readonly TError _error;


    public bool Success { get; }

    public TResult Result
        => Success
            ? throw new Exception("Result was not set")
            : _result;

    public TError Error
        => Success
            ? throw new Exception("Successful result has no error")
            : _error;


    private OperationResult(bool success, TResult result, TError error)
    {
        Success = success;
        _result = result;
        _error = error;
    }


    public static OperationResult<TResult, TError> CreateSuccessfulResult(TResult result)
    {
        return new OperationResult<TResult, TError>(true, result, default);
    }

    public static OperationResult<TResult, TError> CreateFailureResult(TError error)
    {
        return new OperationResult<TResult, TError>(false, default, error);
    }
}