using ClinicManagement.Backend.Business.Errors;
using ClinicManagement.Backend.Business.Results;
using System.Diagnostics.CodeAnalysis;

namespace ClinicManagement.Backend.Business.Results
{
    public class Result
    {
        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Los argumentos son invalidos", nameof(error));
            }
            IsSuccess = isSuccess;
            Error = error;
        }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }
        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error error) => new(false, error);
        
    }
}

public class Result<T> : Result
{
    private readonly T? _value;
    private Result(bool isSuccess, T? value, Error error) : base(isSuccess, error)
    {
        if (isSuccess && value is null || !isSuccess && value is not null)
        {
            throw new ArgumentException("Los argumentos son invalidos", nameof(value));

        }
        _value = value;
    }
    [NotNull]
    public T? Value => IsSuccess ? _value! : throw new InvalidOperationException("El valor de un resultado fallido no puede ser accedido");
    public static Result<T> Success(T value) => new(true, value, Error.None);
    public static new Result<T> Failure(Error error) => new(false, default, error);

}