using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Results;

public class Result
{
    public bool IsSuccess { get; set; }
    public string? Error { get; set; }

    protected Result(bool success, string? error)
    {
        IsSuccess = success;
        Error = error;

    }
    public static Result Success()
        => new Result(true, null);

    public static Result Failure(string error)
        => new Result(false, error);
}

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(T value) : base(true, null)
    {
        Value = value;
    }

    private Result(string error) : base(false, error)
    {
    }

    public static Result<T> Success(T value)
        => new(value);

    public static new Result<T> Failure(string error)
        => new(error);
}
