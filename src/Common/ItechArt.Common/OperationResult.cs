using System;

namespace ItechArt.Common;

public class OperationResult<TResult, TError>
    where TError : Enum
{
    private readonly TError _error;
    private readonly TResult _result;

    private readonly bool _success;


    public bool Seccess => _success;

    public TResult Result
    {
        get => !_success
            ? throw new Exception("Result was not set")
            : _result;
    }

    public TError Error
    {
        get => _success
            ? throw new Exception("Successful result has no error")
            : _error;
    }


    private OperationResult(TResult result, TError error, bool seccess)
    {
        _result = result;
        _error = error;
        _success = seccess;
    }


    public static OperationResult<TResult, TError> CreateSuccessfulResult(TResult result)
    {
        return new OperationResult<TResult, TError>(result, default, true);
    }

    public static OperationResult<TResult, TError> CreateFailureResult(TError error)
    {
        return new OperationResult<TResult, TError>(default, error, false);
    }
}