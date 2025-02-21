namespace Backend.Application.Common
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }

        private Result(bool isSuccess, string? error = null) 
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new Result(true);
        public static Result Failure(string error) => new Result(false, error);

    }
}
