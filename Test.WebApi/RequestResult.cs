using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Test.WebApi
{
    public class BaseResult 
    {
        public bool InternalServerError { get; set; }
        public HttpStatusCode Status { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class RequestResult<T> : BaseResult
        where T : class, new()
    {
        public T Model { get; set; }

        public RequestResult()
        {
            Status = HttpStatusCode.OK;
        }

        public RequestResult(HttpStatusCode status)
        {
            Status = status;
        }

        public RequestResult(string errorMessage)
        {
            InternalServerError = true;
            ErrorMessage = errorMessage;
        }

        public RequestResult(HttpStatusCode status, string errorMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
        }

        public RequestResult(T model)
        {
            Status = HttpStatusCode.OK;
            Model = model;
        }
    }
}