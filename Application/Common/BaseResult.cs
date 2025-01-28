using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common;
public class BaseResult<TData> : BaseResult
{
    public TData? Data { get; set; }
    public static BaseResult<TData> Success
        (TData? data,
        string? Message = null)
    {
        return new BaseResult<TData>()
        {
            Data = data,
            Message = Message,
            IsSuccess = true
        };
    }
    public static BaseResult<TData> Fail
        (
    string? Message = null, ResultType Type=ResultType.Client)
    {
        return new BaseResult<TData>()
        {

            Message = Message,
            IsSuccess = false,
            Type = Type
        };
    }


    public static BaseResult<TData> Fail
       (ResultType Type=ResultType.System
   )
    {
        return new BaseResult<TData>()
        {

            Message = string.Empty,
            IsSuccess = false, Type = Type

        };
    }
}
public class BaseResult
{
    public ResultType Type { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public static BaseResult Success(string? Message = null)
    {
        return new BaseResult()
        {

            Message = Message,
            IsSuccess = true
        };
    }
    public static BaseResult Fail(
        ResultType Type = ResultType.System)
    {
        return new BaseResult()
        {
            IsSuccess = false,
            Type = Type
        };
    }
    public static BaseResult Fail(string? Message = null,
        ResultType Type=ResultType.Client)
    {
        return new BaseResult()
        {
            Message = Message,
            IsSuccess = false,
            Type = Type
        };
    }
}