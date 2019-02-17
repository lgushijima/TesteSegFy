using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SegFy
{
    public class Grid
    {
        public class Request
        {
            public int pageSize { get; set; }
            public int pageNumber { get; set; }
            public string sortColumn { get; set; }
            public string sortOrder { get; set; }
            public string search { get; set; }
        }

        public class Result<T>
        {
            public int total { get; set; }
            public List<T> list { get; set; }
            public bool error { get; set; }
            public int errorCode { get; set; }
            public string message { get; set; }

            public void setSuccess()
            {
                this.error = false;
                this.errorCode = 0;
                this.message = null;
            }
            public void setError(string message, int errorCode = 500)
            {
                this.error = true;
                this.errorCode = errorCode;
                this.message = message;
            }
        }
    }



    public static class ExtensionMethods
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, string order)
        {
            var asc = order == "asc" ? true : false;
            string methodName = asc ? "OrderBy" : "OrderByDescending";

            var type = typeof(T);
            string[] args = propertyName.Split('.');
            var property = type.GetProperty(args.First());
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = args.Aggregate(parameter, (Expression parent, string path) => Expression.Property(parent, path));

            var propertyType = property.PropertyType;
            if (args.Length > 1)
            {
                propertyType = Type.GetType(type.Namespace + "." + args[args.Length - 2]).GetProperty(args.Last()).PropertyType;
            }

            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { type, propertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }

        public static IQueryable<T> Like<T>(this IQueryable<T> source, string propertyName, string keyword)
        {
            var type = typeof(T);
            string[] args = propertyName.Split('.');
            var property = type.GetProperty(args.First());
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = args.Aggregate(parameter, (Expression parent, string path) => Expression.Property(parent, path));

            var constant = Expression.Constant("%" + keyword + "%");
            var like = typeof(SqlMethods).GetMethod("Like", new Type[] { typeof(string), typeof(string) });
            MethodCallExpression methodExp = Expression.Call(null, like, propertyAccess, constant);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(methodExp, parameter);
            return source.Where(lambda);
        }

        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> entities, string propertyName, string order)
        {
            if (!entities.Any() || string.IsNullOrEmpty(propertyName))
                return entities;

            var propertyInfo = entities.First().GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (order.ToLower() == "asc")
                return entities.OrderBy(e => propertyInfo.GetValue(e, null));
            else
                return entities.OrderByDescending(e => propertyInfo.GetValue(e, null));
        }
    }
}