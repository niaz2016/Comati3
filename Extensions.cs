using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Comati3
{
    public static class Extensions
    {
        public static DbSet<T?> WhereActive<T>(this DbSet<T?> source)
        where T : class
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            // Build the expression tree for the filter
            var parameter = Expression.Parameter(typeof(T), "item");
            var activeProperty = Expression.Property(parameter, "Active");
            var deletedProperty = Expression.Property(parameter, "Deleted");

            var activeCondition = Expression.Equal(activeProperty, Expression.Constant(true));
            var deletedCondition = Expression.Equal(deletedProperty, Expression.Constant(false));

            var andCondition = Expression.AndAlso(activeCondition, deletedCondition);
            var lambda = Expression.Lambda<Func<T, bool>>(andCondition, parameter);

            return (DbSet<T?>)source.Where(lambda);
        }
    }
}
