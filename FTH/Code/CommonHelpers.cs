using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FTH.Code
{
    public static class CommonHelpers
    {
        public static Task Set<T>(this Task<T> task, Expression<Func<T>> setter)
        {
            return task.ContinueWith((resultTask) =>
            {
                var parameter = Expression.Parameter(typeof(T), "value");

                var body = Expression.Assign(setter.Body, parameter);
                var lambda = Expression.Lambda<Action<T>>(body, parameter);

                lambda.Compile().Invoke(resultTask.Result);
            });
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool IsValidEmail(this string email)
        {
            if (email.IsNullOrWhiteSpace())
                return false;

            try
            {
                var e = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }

        }


    }
}
