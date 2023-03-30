using System;
using System.Linq.Expressions;
using IntusWindows.Sales.Order.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IntusWindows.Sales.Order.Infrastructure.Repositories;

public class BaseContextRepository : IBaseContextRepository
{

    public readonly Context context;

    public BaseContextRepository(Context context) => this.context = context;


    public async ValueTask<T?> GetAsync<T>(Expression<Func<T, bool>>? filter = null) where T : class
    {
        return await this.context.Set<T>().FirstOrDefaultAsync(filter);
    }

    public async ValueTask<T?> GetAsync<T, I>(Expression<Func<T, I>> inclue, Expression<Func<T, bool>>? filter = null) where T : class
    {
        return await this.context.Set<T>().Include(inclue).FirstOrDefaultAsync(filter);
    }

    public async ValueTask<T?> GetAsync<T, I>(Expression<Func<T, IEnumerable<I>>> inclue, Expression<Func<T, bool>> filter = null) where T : class
    {
        return await this.context.Set<T>().Include(inclue).FirstOrDefaultAsync(filter);
    }

    public async ValueTask<List<T>> GetListAsync<T>(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null) where T : class
    {
        return await ExecuteQueryAsync(GetQuery(filter), orderBy);
    }

    public async ValueTask<List<T>> GetListAsync<T, I>(Expression<Func<T, I>> inclue, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null) where T : class
    {
        var query = GetQuery(filter);
        if (inclue is not null)
            query = query.Include(inclue);

        return await ExecuteQueryAsync(query, orderBy);
    }

    async ValueTask<List<T>> ExecuteQueryAsync<T>(IQueryable<T> query, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy)
    {
        return await (orderBy is not null ? orderBy(query).ToListAsync() : query.ToListAsync());
    }

    IQueryable<T> GetQuery<T>(Expression<Func<T, bool>>? filter) where T : class
    {
        IQueryable<T> query = this.context.Set<T>().AsQueryable();
        if (filter is not null)
            query = query.Where(filter);

        return query;
    }
}

