using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Application.Models
{
    public class PageList<T>
    {
        public PageList(List<T> items, int page, int pageSize, int totalCount)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public List<T>? Items { get; set; }
        public int Page { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public bool HasPreviousPage => Page > 1;
        public bool HasNextPage => (Page * PageSize) < TotalCount;

        public static async Task<PageList<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
        {
            var totalCount = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PageList<T>(items, page, pageSize, totalCount);
        }
    }
}
