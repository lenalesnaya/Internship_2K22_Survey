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
        return new OperationResult<TError>(true, null);
    }

    public static OperationResult<TError> CreateUnsuccessful(TError error)
    {
        return new OperationResult<TError>(false, error);
    }
}



public class OperationResult<TResult, TError> : OperationResult<TError>
    where TError : struct, Enum
{
    private readonly TResult _result;


    public TResult Result
        => IsSuccessful
            ? _result
            : throw new Exception("Result was not set");


    private OperationResult(bool isSuccessful, TResult result, TError? error)
        : base(isSuccessful, error)
    {
        _result = result;
    }


    public static OperationResult<TResult, TError> CreateSuccessful(TResult result)
    {
        return new OperationResult<TResult, TError>(true, result, null);
    }

    public new static OperationResult<TResult, TError> CreateUnsuccessful(TError error)
    {
        return new OperationResult<TResult, TError>(false,default, error);
    }
}