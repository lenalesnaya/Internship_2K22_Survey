using System;
using System.Collections.Generic;
using System.Linq;

namespace ItechArt.Common;

public class OperationResult<TResult, TStatus>
    where TResult: class
    where TStatus: Enum
{
    private TResult _result;


    public OperationResult(TResult result,TStatus  status)
    {
        _result = result;
        Status = status;
    }

    public OperationResult(TStatus  status)
    {
        Status = status;
    }

    public OperationResult(TStatus  status, params string[] errors)
    {
        Status = status;
        AddErrors(errors);
    }


    public bool Success => Errors == null;
    
    public TResult Result
    {
        get
        {
            if (_result == null)
            {
                throw new Exception("Result was not set");
            }

            return _result;
        }
    }

    public IList<string> Errors { get; private set;}
    public TStatus Status { get; private set; }


    public void AddError(string error)
    {
        Errors ??= new List<string>();
        Errors.Add(error);
    }

    public void AddErrors(IEnumerable<string> errors)
    {
        Errors = Errors == null
            ? new List<string>(errors)
            : Errors.Concat(errors).ToList();
    }
}