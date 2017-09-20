using System;
using System.Collections.Generic;
using System.ServiceModel;
using PriceListWcfService.DataContracts;

namespace PriceListWcfService
{
    [ServiceContract] 
    public interface IPriceListService
    {
        /// <summary>
        ///     Get all supplier records from the database.
        /// </summary>
        /// <returns>
        ///     Returns a <see cref="GetSuppliersResponse"/> object that contains the results.
        /// </returns>
        [OperationContract]
        GetSuppliersResponse GetSuppliers();

        /// <summary>
        ///     Add, update, or delete suppliers
        /// </summary>
        /// <param name="clientUtcTime">
        ///     UTC time of the client calling the service.
        /// </param>
        /// <param name="supplierData">
        ///     Supplier record changes
        /// </param>
        /// <returns>
        ///     Returns a <see cref="UpdateSuppliersResponse"/> object with the result for every <param name="supplierDataIn"/> record.
        /// </returns>
        [OperationContract]
        UpdateSuppliersResponse UpdateSuppliers(DateTime clientUtcTime, UpdateSuppliersRequest supplierDataIn);
    } // interface IPriceListService
} // namespace PriceListWcfService
