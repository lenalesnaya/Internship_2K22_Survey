using System;

namespace ItechArt.Common;

public class OperationResult<TResult, TStatusError>
    where TStatusError: Enum
{
    private readonly TResult _result;
    private readonly TStatusError _status;


    public TResult Result
    {
        get
        {
            if (Success == false)
            {
                throw new Exception("Result was not set");
            }

            return _result;
        }
    }

    public bool Success => Result != null;


    private OperationResult(TResult result)
    {
        _result = result;
    }

    private OperationResult(TStatusError  status)
    {
        _status = status;
    }


    public static OperationResult<TResult, TStatusError> GetSuccessResult(TResult result)
    {
        return new OperationResult<TResult, TStatusError>(result);
    }

    public static OperationResult<TResult, TStatusError> GetFailureResult(TStatusError status)
    {
        return new OperationResult<TResult, TStatusError>(status);
    }
}