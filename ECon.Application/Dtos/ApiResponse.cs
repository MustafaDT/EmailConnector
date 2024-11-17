namespace ECon.Application.Dtos;

public class ApiResponse
{
    private ApiResponse() { }

    public int SystemRequestId { get; set; }
    public bool Succeeded { get; set; }
    public ErrorDetail? Error { get; set; }
    public object? Result { get; set; }

    public class ErrorDetail
    {
        public string? ErrorMessageEn { get; set; }
        public string? ErrorMessageAr { get; set; }
        public string? ErrorCode { get; set; }
    }

    public static ApiResponse Success(int systemRequestId, object result)
    {
        var apiResponse = new ApiResponse
        {
            Succeeded = true,
            SystemRequestId = systemRequestId,
            Result = result
        };

        return apiResponse;
    }

    public static ApiResponse Failure(int systemRequestId, string errorMessageEn, string errorMessageAr,string errorCode)
    {
        var apiResponse = new ApiResponse
        {
            Succeeded = false,
            SystemRequestId = systemRequestId,
            Error = new ErrorDetail
            {
                ErrorMessageEn = errorMessageEn,
                ErrorMessageAr = errorMessageAr,
                ErrorCode = errorCode
            }
        };

        return apiResponse;
    }
}