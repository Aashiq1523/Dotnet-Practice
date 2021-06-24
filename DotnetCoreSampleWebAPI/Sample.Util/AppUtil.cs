using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Util
{
    public class AppUtil
    {
        public static ObjectResult IActionResultResponse(int statusCode, Object resbody)
        {
            return new ObjectResult(resbody)
            {
                Value = resbody,
                StatusCode = statusCode
            };
        }

        public static ObjectResult WrapperAppException(Exception ex)
        {
            AppException appException = null;
            if (ex is AppException)
                appException = (AppException)ex;
            else
                appException = new AppException(AppError.UNKNOWN_ERROR, ex);
            List<AppError> errors = new List<AppError>();
            int statusCode;
            if (appException.appErrors != null)
            {
                IList<AppError> appErrors = appException.appErrors;
                errors.AddRange(appErrors);
                statusCode = appErrors.First().code;
            }
            else
            {
                AppError appError = appException.appError;
                errors.Add(appError);
                statusCode = appError.code;
            }

            return new ObjectResult(errors)
            {
                Value = errors,
                StatusCode = statusCode
            };
        }

        private static AppException ToAppException(Exception ex, AppError appError)
        {
            AppException appException = null;
            if (ex is AppException)
            {
                appException = (AppException)ex;
                return appException;
            }
            return new AppException(appError);
        }

        public static void TryOut(AppError appError, Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                throw ToAppException(ex, appError);
            }
        }

        public static T TryOut<T>(AppError appError, Func<T> action)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                throw ToAppException(ex, appError);
            }
        }

        public static IActionResult PrepareIActionResult<T>(int statusCode, Func<T> action)
        {
            try
            {
                T result = action();
                return IActionResultResponse(statusCode, result);
            }
            catch (Exception ex)
            {
                return WrapperAppException(ex);
            }
        }
    }
}
