namespace Slay.Utilities.Extensions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public static class ExpressionsExtensions
    {
        public static Expression<Func<TSource, bool>> Between<TSource, TKey>(Expression<Func<TSource, TKey>> keySelector, TKey low, TKey high) where TKey : IComparable<TKey>
        {
            Expression lowerBound = Expression.GreaterThanOrEqual(keySelector.Body, Expression.Constant(low));

            Expression upperBound = Expression.LessThanOrEqual(keySelector.Body, Expression.Constant(high));

            Expression and = Expression.AndAlso(lowerBound, upperBound);

            Expression<Func<TSource, bool>> lambda = Expression.Lambda<Func<TSource, bool>>(and, keySelector.Parameters);

            return lambda;
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            if (second == null)
            {
                return first;
            }

            if (first == null)
            {
                return second;
            }

            return first.Compose(second, Expression.And);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            if (second == null)
            {
                return first;
            }

            if (first == null)
            {
                return second;
            }

            return first.Compose(second, Expression.Or);
        }

        public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            if (second == null)
            {
                return first;
            }

            if (first == null)
            {
                return second;
            }

            return first.Compose(second, Expression.OrElse);
        }

        private static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }
    }
}
