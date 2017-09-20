using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceListWcfService.DataContracts.Supplier;

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

            /// <summary>
            ///     Get a list of suppliers from the database
            /// </summary>
            /// <returns>
            ///     Returns a collection of <see cref="SupplierDTO"/> objects.
            /// </returns>
            IEnumerable<SupplierDTO> GetSuppliers();
        } // interface IDataAccess
    } // namespace Interfaces
} // namespace PriceListWcfService