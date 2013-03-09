using System;
using System.Collections.Generic;

namespace RavenDemo.Web.Models
{
    public class PagedList<T>
    {
        private readonly int _pageSize;

        public PagedList(int page, int pageSize, int totalResults, IList<T> list)
        {
            _pageSize = pageSize;
            CurrentPage = page;
            TotalResults = totalResults;
            List = list;
        }

        public int CurrentPage { get; private set; }
        public int TotalResults { get; private set; }
        public IList<T> List { get; private set; }

        public double NumberOfPages
        {
            get
            {
                double result = (double) TotalResults/_pageSize;
                return Math.Ceiling(result);
            }
        }

        
    }
}