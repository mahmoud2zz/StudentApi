using System;
using StudentApi.Models;

namespace StudentApi.Helpers
{
	public class ResponseBuilder
	{
        public static Response<T> Success<T>(string message, T? data = default)
        {
            return new Response<T>(true, message, data);
        }

        public static Response<T> Failure<T>(string message, T? data = default)
        {
            return new Response<T>(false, message, data);
        }

        public static Response<T> Build<T>(bool success, string message, T? data = default)
        {
            return new Response<T>(success, message, data);
        }
    }
}

