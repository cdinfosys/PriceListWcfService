using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceListWcfService
{
    namespace Interfaces
    {
        /// <summary>
        ///     Interface for data access classes.
        /// </summary>
        public interface IDataAccess : IDisposable
        {
            /// <summary>
            ///     Get the current time from the database server.
            /// </summary>
            /// <returns>
            ///     Returns the current UTC time of the database.
            /// </returns>
            DateTime GetDatabaseServerUtcTime();
        } // interface IDataAccess
    } // namespace Interfaces
} // namespace PriceListWcfService