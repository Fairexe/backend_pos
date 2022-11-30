﻿namespace backend_cn.ViewModels
{
    public class ApiResultViewModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

    }

    public class ApiResultViewModel<T> : ApiResultViewModel
    {
        public T Data { get; set; }
    }
}
