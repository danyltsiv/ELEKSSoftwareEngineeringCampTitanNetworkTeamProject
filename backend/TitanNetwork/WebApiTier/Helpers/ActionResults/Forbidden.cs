using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApiTier.Helpers.ActionResults
{
    /// <summary>
    /// Class Forbidden.
    /// </summary>
    /// <seealso cref="System.Web.Http.IHttpActionResult" />
    public class Forbidden : IHttpActionResult
    {
        /// <summary>
        /// The _value
        /// </summary>
        string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Forbidden"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Forbidden(string value)
        {
            _value = value;
        }

        /// <summary>
        /// Creates an <see cref="T:System.Net.Http.HttpResponseMessage" /> asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that, when completed, contains the <see cref="T:System.Net.Http.HttpResponseMessage" />.</returns>
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.Forbidden,
                Content = new StringContent(_value)
            };
            return Task.FromResult(response);
        }
    }
}