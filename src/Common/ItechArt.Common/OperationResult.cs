using System;
using System.Collections.Generic;
using System.Linq;

namespace ItechArt.Common;

public class OperationResult<TResult, TStatus>
    where TStatus: Enum
{
    private readonly TResult _result;
    private readonly TStatus _status;


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

    private OperationResult(TStatus  status)
    {
        _status = status;
    }


    public static OperationResult<TResult, TStatus> SuccessResult(TResult result)
    {
        return new OperationResult<TResult, TStatus>(result);
    }

    public static OperationResult<TResult, TStatus> FailureResult(TStatus status)
    {
        return new OperationResult<TResult, TStatus>(status);
    }
}