using DirectDebitSubmissionNightyJob.Boundary.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace DirectDebitSubmissionNightyJob.Extension
{
    public static class PagedResultEFCoreExtensions
    {
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize)
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = query.Count()
            };

            var pageCount = (double) result.TotalCount / pageSize;
            result.TotalPages = (int) Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }

        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize)
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = await query.CountAsync().ConfigureAwait(false)
            };

            var pageCount = (double) result.TotalCount / pageSize;
            result.TotalPages = (int) Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync().ConfigureAwait(false);

            return result;
        }

        public static PagedResult<TDestination> GetPaged<T, TDestination>(this IQueryable<T> query, IMapper mapper, int page, int pageSize) where TDestination : class
        {
            var result = new PagedResult<TDestination>
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = query.Count()
            };

            var pageCount = (double) result.TotalCount / pageSize;
            result.TotalPages = (int) Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip)
                                  .Take(pageSize)
                                  .ProjectTo<TDestination>(mapper?.ConfigurationProvider)
                                  .ToList();
            return result;
        }

        public static async Task<PagedResult<TDestination>> GetPagedAsync<TSource, TDestination>(this IQueryable<TSource> query, IMapper mapper, int page, int pageSize) where TDestination : class
        {
            var result = new PagedResult<TDestination>
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = await query.CountAsync().ConfigureAwait(false)
            };

            var pageCount = (double) result.TotalCount / pageSize;
            result.TotalPages = (int) Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;

            result.Results = await query.Skip(skip)
                                        .Take(pageSize)
                                        .ProjectTo<TDestination>(mapper?.ConfigurationProvider)
                                        .ToListAsync().ConfigureAwait(false);
            return result;
        }
    }
}
