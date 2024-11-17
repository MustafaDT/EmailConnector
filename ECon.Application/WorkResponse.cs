namespace ECon.Application
{
    public class WorkResponse<T>
    {
        public bool Status { get; private set; }

        private string? _message;
        public string? Message
        {
            get => _message?.Replace("'", ""); // To avoid notification failure
            private set => _message = value;
        }

        public T? Result { get; private set; }

        public static WorkResponse<T> Success(T result, string? message)
        {
            return new WorkResponse<T>()
            {
                Status = true,
                Message = message,
                Result = result
            };
        }

        public static WorkResponse<T> Success(T result)
        {
            return new WorkResponse<T>()
            {
                Status = true,
                Message = null,
                Result = result
            };
        }

        public static WorkResponse<T> Success()
        {
            return new WorkResponse<T>()
            {
                Status = true,
                Message = null,
                Result = default(T)
            };
        }

        public static WorkResponse<T> Error(string message)
        {
            return new WorkResponse<T>()
            {
                Status = false,
                Message = message,
                Result = default(T)
            };
        }
    }
}