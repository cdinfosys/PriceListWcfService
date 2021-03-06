﻿using System;
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

            /// <summary>
            ///     Retrieve the Suppliers.Supplier records with known identifiers.
            /// </summary>
            /// <param name="supplierIdentifiers">
            ///     Collection of unique identifiers
            /// </param>
            /// <returns>
            ///     Returns a list of SupplierDTO records
            /// </returns>
            IEnumerable<ExtSupplierDTO> GetExistingSupplierRecords(IEnumerable<Guid> supplierIdentifiers);

            /// <summary>
            ///     Update an existing Suppliers.Supplier record.
            /// </summary>
            /// <param name="updateRec">
            ///     Updated record values.
            /// </param>
            ExtSupplierDTO UpdateSupplier(ExtSupplierDTO updateRec);

            /// <summary>
            ///     Mark a Suppliers.Supplier record as inactive
            /// </summary>
            /// <param name="systemUserID">
            ///     User ID of the user updating the record.
            /// </param>
            /// <param name="recordUpdatedTime">
            ///     UTC time when the record was updated.
            /// </param>
            /// <param name="supplierID">
            ///     ID of the record to update.
            /// </param>
            /// <param name="isRetired">
            ///     Flag to indicate the retired state of the supplier
            /// </param>
            ExtSupplierDTO RetireSupplier(Int32 systemUserID, DateTime recordUpdatedTime, Int32 supplierID, Boolean isRetired);

            /// <summary>
            ///     Add a new record to Suppliers.Supplier
            /// </summary>
            /// <param name="supplierRec">
            ///     New supplier details.
            /// </param>
            ExtSupplierDTO AddSupplier(ExtSupplierDTO supplierRec);

            /// <summary>
            ///     Convert a collection of strings into a delimited string for storage in the database.
            /// </summary>
            /// <param name="stringCollection">
            ///     Strings to concatenate
            /// </param>
            /// <returns>
            ///     Returns the concatenated strings.
            /// </returns>
            String StringCollectionToString(IList<String> stringCollection);

            /// <summary>
            ///     Breaks a delimited string into a collection of separate strings.
            /// </summary>
            /// <param name="inputString">
            ///     Delimited string.
            /// </param>
            /// <returns>
            ///     Returns a collection of strings.
            /// </returns>
            IList<String> StringToStringCollection(String inputString);
        } // interface IDataAccess
    } // namespace Interfaces
} // namespace PriceListWcfService