using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        public PaginatedList(List<T> items, int pageIndex, int pageSize)
            :base(items)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling((double)items.Count/pageSize);
        }

        public bool HasPreviousPage 
        { 
            get => PageIndex > 1;
        }

        public bool HasNextPage 
        { 
            get => PageIndex < TotalPages;
        }

        public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex=1, int pageSize=9)
        {
            var items = source.Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, pageIndex, pageSize);
        } 
    }
}