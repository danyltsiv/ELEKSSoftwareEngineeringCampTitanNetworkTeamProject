using BusinessLogicTier.DataAccesLayer.Context;
using BusinessLogicTier.DataAccesLayer.Entities;
using System.Data.Entity;

namespace BusinessLogicTier.Providers
{
    /// <summary>
    /// Class MessageLogProvider.
    /// </summary>
    /// <seealso cref="BusinessLogicTier.Providers.BaseProvider" />
    public class MessageLogProvider : BaseProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageLogProvider"/> class.
        /// </summary>
        public MessageLogProvider()
            :base()
        {
        }

        /// <summary>
        /// Adds the message log.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddMessageLog(MessageLog log)
        {
            try
            {
                Context.Entry(log).State = EntityState.Added;
                Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
