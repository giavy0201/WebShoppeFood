using System.Linq.Expressions;

namespace DAL.Generic
{
    public class SortByColumn
    {
        public static IQueryable<T> Sort<T>(IQueryable<T> list, string propertyName, bool isAsc)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            MemberExpression property = Expression.Property(parameter, propertyName);
            LambdaExpression lambda = Expression.Lambda(property, parameter);

            string methodName = isAsc ? "OrderBy" : "OrderByDescending";

            Type[] types = { list.ElementType, property.Type };
            Expression[] args = { list.Expression, Expression.Quote(lambda) };
            MethodCallExpression methodCallExpression = Expression.Call(typeof(Queryable), methodName, types, args);

            return list.Provider.CreateQuery<T>(methodCallExpression);
        }
    }
}
