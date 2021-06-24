using System;
using System.Collections.Generic;

namespace Sample.Util
{
    public class AppException : Exception
    {
        public AppError appError { get; }
        public IList<AppError> appErrors { get; }

        public AppException() { }
        public AppException(AppError appError, string message) : base(appError.ToString() + ", " + message) { }
        public AppException(AppError appError) : base(appError.ToString()) => this.appError = appError;
        public AppException(IList<AppError> appErrors) => this.appErrors = appErrors;
        public AppException(string message) : base(message) { }
        public AppException(string message, Exception exception) : base(message, exception) { }
        public AppException(AppError appError, Exception exception) : base()
        {
            this.appError = appError;
        }
        public AppException(AppError appError, string message, Exception exception) : base()
        {
            this.appError = appError;
            this.appError.message = message;
            this.appError.key = exception.Message;
        }
        public AppException(IList<AppError> appErrors, string message, Exception exception) : base()
        {
            this.appErrors = appErrors;
            this.appError.message = message;
            this.appError.key = exception.Message;
        }
    }
}
