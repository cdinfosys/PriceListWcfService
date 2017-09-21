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
        /// <param name="request">
        ///     Request object for the method call.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="GetSuppliersResponse"/> object that contains the results.
        /// </returns>
        [OperationContract]
        GetSuppliersResponse GetSuppliers(GetSuppliersRequest request);

        /// <summary>
        ///     Add, update, or delete suppliers
        /// </summary>
        /// <param name="supplierData">
        ///     Supplier record changes
        /// </param>
        /// <returns>
        ///     Returns a <see cref="UpdateSuppliersResponse"/> object with the result for every <param name="supplierDataIn"/> record.
        /// </returns>
        [OperationContract]
        UpdateSuppliersResponse UpdateSuppliers(UpdateSuppliersRequest supplierDataIn);
    } // interface IPriceListService
} // namespace PriceListWcfService
