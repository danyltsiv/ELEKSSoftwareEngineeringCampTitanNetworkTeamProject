using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;

namespace WebApiTier.Helpers.Paging
{
    /// <summary>
    /// Class Pagination.
    /// </summary>
    public static class Pagination
    {
        /// <summary>
        /// Applies the paging.
        /// </summary>
        /// <typeparam name="TResult">The type of the t result.</typeparam>
        /// <param name="controller">The controller.</param>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="link">The link.</param>
        /// <returns>IEnumerable&lt;TResult&gt;.</returns>
        public static IEnumerable<TResult> ApplyPaging<TResult>(this ApiController controller, 
                                                                 IQueryable<TResult> query, 
                                                                 int page, 
                                                                 int pageSize,
                                                                 string link)
        {
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var urlHelper = new UrlHelper(controller.Request);
            var prevLink = page > 0 ? urlHelper.Link(link, new { page = page - 1, pageSize = pageSize }) : "";
            var nextLink = page < totalPages - 1 ? urlHelper.Link(link, new { page = page + 1, pageSize = pageSize }) : "";

            var paginationHeader = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PrevPageLink = prevLink,
                NextPageLink = nextLink
            };

            HttpContext.Current.Response.Headers.Add("X-Pagination",
            Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));

            var queryResult = query
                .Reverse()
                .Skip(pageSize * page)
                .Take(pageSize)
                .ToList();

            return queryResult;
        }
    }
}
        