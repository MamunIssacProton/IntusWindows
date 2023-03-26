using System.Linq.Expressions;

namespace IntusWindows.Sales.Order.Infrastructure.Interfaces;

public interface IBaseContextRepository : IBaseRepository
{
    Task<T?> GetAsync<T>(Expression<Func<T, bool>>? filter = null) where T : class;

    Task<T?> GetAsync<T, I>(Expression<Func<T, I>> inclue,
                            Expression<Func<T, bool>>? filter = null)
                            where T : class;

    Task<T?> GetAsync<T, I>(Expression<Func<T, IEnumerable<I>>> inclue,
                            Expression<Func<T, bool>> filter = null)
                            where T : class;


    Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>>? filter = null,
                                 Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
                                 where T : class;

    Task<List<T>> GetListAsync<T, I>(Expression<Func<T, I>> inclue,
                                     Expression<Func<T, bool>>? filter = null,
                                     Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
                                     where T : class;


}

