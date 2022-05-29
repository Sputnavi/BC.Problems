using BC.Problems.Models;
using BC.Problems.Repositories.Extensions.Utils;
using System.Linq.Dynamic.Core;

namespace BC.Problems.Repositories.Extensions;

public static class BicycleRepositoryExtensions
{
    public static IQueryable<Bicycle> Search(this IQueryable<Bicycle> bicycles, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return bicycles;
        }

        var lowerCaseTerm = searchTerm.Trim().ToLower();

        return bicycles.Where(c => c.SerialNumber.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<Bicycle> Sort(this IQueryable<Bicycle> bicycles, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
        {
            return bicycles.OrderBy(c => c.SerialNumber);
        }

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Bicycle>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
        {
            return bicycles.OrderBy(c => c.SerialNumber);
        }

        return bicycles.OrderBy(orderQuery);
    }
}
