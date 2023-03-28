namespace backend_cn.APIResult
{
    public static class APIResultExtenstions
    {
        public static ApiResultViewModel ApiResultViewModel(this IAPIResult apiResult, Enum status, string msg)
        {
            return new ApiResultViewModel
            {
                StatusCode = (int)(object)status,
                Message = msg
            };
        }

        public static ApiResultViewModel<T> ApiResultViewModel<T>(this IAPIResult apiResult, Enum status, string msg, T data)
        {
            return new ApiResultViewModel<T>
            {
                StatusCode = (int)(object)status,
                Message = msg,
                Data = data
            };
        }
    }
}
