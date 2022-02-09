using System;

namespace ItechArt.Survey.Foundation;

public class OperationResult<T, U>
    where U: Enum 
{
    public T Data { get; set; }

    public U OperationStatus { get; set; }
}