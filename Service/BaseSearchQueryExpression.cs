using System;
using System.Linq;
using System.Linq.Dynamic;
using Core;

namespace Service
{
    public class BaseSearchQueryExpression<T> where T : BaseModel
    {
        public static int DefaultQueryExpression(ref IQueryable<T> data, BaseSearchQueryModel searchQuery)
        {
            var dataCount = 0;

            data = DefaultSearchQueryable(data, searchQuery);
            dataCount = data.Count();

            data = DefaultSortQueryable(data, searchQuery);
            data = DefaultPaginateQueryable(data, searchQuery);

            return dataCount;
        }

        public static IQueryable<T> DefaultSearchQueryable(IQueryable<T> data, BaseSearchQueryModel searchQuery)
        {
            var query = "";
            if (searchQuery.Search == null) return data;

            if (String.IsNullOrEmpty(searchQuery.Search.Keyword))
                return data;

            searchQuery.Search.Fields.ToList().ForEach(f =>
            {
                query = query + String.Format("{0}.ToString().ToLower().Contains(\"{1}\")", f, searchQuery.Search.Keyword.ToLower());
                query = query + " || ";
            });

            query = query.Substring(0, query.Length - 4);

            var searchExpression = query;
            return !String.IsNullOrEmpty(searchExpression) ? data.Where(searchExpression) : data;
        }

        public static IQueryable<T> DefaultSortQueryable(IQueryable<T> data, BaseSearchQueryModel searchQuery)
        {
            if (!String.IsNullOrEmpty(searchQuery.SortBy))
            {
                return searchQuery.IsSortAsc ? data.OrderBy(searchQuery.SortBy + " ASC") : data.OrderBy(searchQuery.SortBy + " DESC");
            }
            return data;
        }

        public static IQueryable<T> DefaultPaginateQueryable(IQueryable<T> data, BaseSearchQueryModel searchQuery)
        {
            var take = searchQuery.DataPerPage;
            var skip = (searchQuery.Page - 1) * take;
            return data.Skip(skip).Take(take);
        }
    }
}
