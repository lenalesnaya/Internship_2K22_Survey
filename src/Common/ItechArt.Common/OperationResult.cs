using System;
using System.Collections.Generic;
using System.Linq;

namespace ItechArt.Common;

public class OperationResult<TResult, TStatus>
    where TStatus: Enum
{
    private readonly TResult _result;


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

    public bool Success => Errors == null;

    public IList<string> Errors { get; set;}

    private TStatus Status { get; }


    private OperationResult(TResult result, TStatus  status)
    {
        _result = result;
        Status = status;
    }

    private OperationResult(TStatus  status, params string[] errors)
    {
        Status = status;
        AddErrors(errors);
    }


    public static OperationResult<TResult, TStatus> SuccessResult(TResult result, TStatus status)
    {
        return new OperationResult<TResult, TStatus>(result, status);
    }

    public static OperationResult<TResult, TStatus> FailureResult(TStatus status,params string[] errors)
    {
        return new OperationResult<TResult, TStatus>(status, errors);
    }

    private void AddErrors(IEnumerable<string> errors)
    {
        Errors = Errors == null
            ? new List<string>(errors)
            : Errors.Concat(errors).ToList();
    }
}