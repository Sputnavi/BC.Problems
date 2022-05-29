using BC.Problems.Models;
using BC.Problems.Repositories.Extensions.Utils;
using System.Linq.Dynamic.Core;

namespace BC.Problems.Repositories.Extensions;

public static class ProblemRepositoryExtensions
{
    public static IQueryable<Problem> Search(this IQueryable<Problem> problems, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return problems;
        }

        var lowerCaseTerm = searchTerm.Trim().ToLower();

        return problems.Where(c => c.BicycleSerialNumber.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<Problem> Sort(this IQueryable<Problem> problems, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
        {
            return problems.OrderBy(c => c.DateCreated);
        }

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Problem>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
        {
            return problems.OrderBy(c => c.DateCreated);
        }

        return problems.OrderBy(orderQuery);
    }
}
