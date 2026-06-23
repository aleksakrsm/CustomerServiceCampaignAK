namespace Application.Features.Base
{
    public class Result
    {
        public bool IsSuccess { get; protected set; }
        public string Message { get; set; }
        protected Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
        public static Result Success() => new(true);
        public static Result Failure(string message) => new(false) { Message = message };

    }

    public class Result<T> : Result
    {
        public T? Data { get; private set; }
        public string Message { get; set; }

        private Result(bool isSuccess, T? data = default)
            : base(isSuccess)
        {
            Data = data;
        }

        public static Result<T> Success(T data) => new(true, data);
        public static Result<T> Failure(string message) => new(false) { Message = message };
    }
}
