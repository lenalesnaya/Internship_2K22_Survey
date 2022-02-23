using System;

namespace ItechArt.Common;

public class OperationResult<TResult, TError>
    where TResult : class
    where TError : Enum
{
    private readonly TError _error;
    private readonly TResult _result;

    private readonly bool _success;


    public TResult Result
    {
        get
        {
            if (!_success)
            {
                throw new InvalidOperationException("Result was not set");
            }

            return _result;
        }
    }

    public TError Error
    {
        get
        {
            if (_success)
            {
                throw new InvalidOperationException("Successful result has no error");
            }

            return _error;
        }
    }


    public bool Success
    {
        get
        {
            return _success;
        }
    }


    private OperationResult(TResult result = null, TError error = default)
    {
        _result = result;
        _success = result != null;
        _error = error;
    }


    public static OperationResult<TResult, TError> CreateSuccessfulResult(TResult result)
    {
        return new OperationResult<TResult, TError>(result);
    }

    public static OperationResult<TResult, TError> CreateFailureResult(TError error)
    {
        return new OperationResult<TResult, TError>(null, error);
    }
}