using LibraryManagementCleanArchitecture.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementCleanArchitecture.Application.Response
{
    public class Result<T>
    {
        public T? Value { get; private set; }
        public string? Error { get; private set; }
        public bool IsSuccess => Error is null;

        private Result(T? value, string? error)
        {
            Value = value;
            Error = error;
        }

        public static Result<T> Success(T value) => new (value, null);
        public static Result<T> Failure(Error error) => new (default, error.Message);
    }
}
