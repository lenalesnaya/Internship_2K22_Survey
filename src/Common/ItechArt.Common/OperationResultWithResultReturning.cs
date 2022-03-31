using System;

namespace ItechArt.Common;

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
}
