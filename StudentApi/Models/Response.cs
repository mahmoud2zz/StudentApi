﻿using System;
namespace StudentApi.Models;

public class Response<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public Response(bool success, string message, T? data = default)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public Response() { }
}