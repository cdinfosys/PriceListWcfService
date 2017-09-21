using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PriceListWcfService.DataContracts;
using PriceListWcfService.DataContracts.Supplier;
using PriceListWcfService.Helpers;
using PriceListWcfService.Interfaces;
using PriceListWcfService.Model;
using PriceListWcfService.Utilities;

namespace PriceListWcfService
{
    public class PriceListService : IPriceListService
    {
        /// <summary>
        ///     Request a list of suppliers from the server.
        /// </summary>
        /// <param name="request">
        ///     Request parameters.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="GetSuppliersResponse"/> object with the results of the operation
        /// </returns>
        public GetSuppliersResponse GetSuppliers(GetSuppliersRequest request)
        {
            GetSuppliersResponse responseObject = new GetSuppliersResponse()
            {
                ErrorCode = ResponseErrorCode.NoError
            };

            using (Mutex accessMutex = new Mutex(false, Utilities.Utility.SupplierMutexName))
            {
                try
                {
                    // Prevent other instance from accessing the table until we are done.
                    accessMutex.WaitOne();

                    using (IDataAccess dataAccess = GetDataAccessObject())
                    {
                        responseObject.Suppliers = new List<SupplierDTO>(dataAccess.GetSuppliers());
                    }
                }
                catch (Exception ex)
                {
                    responseObject.ErrorCode = ResponseErrorCode.ExceptionCaught;
                    responseObject.ErrorDescription = ex.Message;
                }
                finally
                {
                    accessMutex.ReleaseMutex();
                }
            }

            return responseObject;
        }

        public UpdateSuppliersResponse UpdateSuppliers(UpdateSuppliersRequest supplierDataIn)
        {
            UpdateSuppliersResponse response = new UpdateSuppliersResponse();

            try
            {
                using (IDataAccess dataAccess = GetDataAccessObject())
                {
                    // Immediately get the time on the database server so that is will be close to the time when the request was sent.
                    DateTime serverUtcTime = dataAccess.GetDatabaseServerUtcTime();

                    IOrderedEnumerable<SupplierDTO> updatedSuppliers = supplierDataIn.SupplierRecords.OrderBy(u => u.UniqueIdentifier);
                    using (Mutex accessMutex = new Mutex(false, Utilities.Utility.SupplierMutexName))
                    {
                        IEnumerable<Guid> recordIDs = updatedSuppliers.TransformElements(u => u.UniqueIdentifier);

                        // Prevent other instance from accessing the table until we are done.
                        accessMutex.WaitOne();
                        try
                        {
                            IOrderedEnumerable<ExtSupplierDTO> storedSuppliers = dataAccess.GetExistingSupplierRecords(recordIDs).OrderBy(u => u.UniqueIdentifier);

                            SuppliersMergeParameter mergeParameter = new SuppliersMergeParameter
                            (
                                dataAccess,
                                1 // TODO replace with user ID from security module
                            );

                            List<ExtSupplierDTO> updatedList = new List<ExtSupplierDTO>
                            (
                                Utility.MergeLists<ExtSupplierDTO, SupplierDTO, ExtSupplierDTO>
                                (
                                    storedSuppliers,
                                    updatedSuppliers,
                                    (s, u) => { return s.UniqueIdentifier.CompareTo(u.UniqueIdentifier); },
                                    SupplierRecordAdded,
                                    SupplierRecordDeleted,
                                    SupplierRecordUpdated,
                                    mergeParameter
                                )
                            );
                        }
                        finally
                        {
                            accessMutex.ReleaseMutex();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorCode = ResponseErrorCode.ExceptionCaught;
                response.ErrorDescription = ex.ToString();
            }

            return response;
        }

        private ExtSupplierDTO SupplierRecordAdded(SupplierDTO newRecord, Object additionalData)
        {
            SuppliersMergeParameter mergeParam = additionalData as SuppliersMergeParameter;

            ExtSupplierDTO supplierDetail = newRecord.Clone() as ExtSupplierDTO;
            supplierDetail.LastUpdateTime = DateTime.Now;
            supplierDetail.SystemUserID = mergeParam.SystemUserID;

            mergeParam.DataAccessObject.AddSupplier(supplierDetail);

            return supplierDetail;
        }

        private void SupplierRecordDeleted(ExtSupplierDTO deletedRecord, Object additionalData)
        {
            // Not handling deletion of supplier records at this time.
        }

        private ExtSupplierDTO SupplierRecordUpdated(ExtSupplierDTO originalData, SupplierDTO updatedData, Object additionalData)
        {
            SuppliersMergeParameter mergeParam = additionalData as SuppliersMergeParameter;

            ExtSupplierDTO supplierDetail = updatedData.Clone() as ExtSupplierDTO;
            supplierDetail.LastUpdateTime = DateTime.Now;
            supplierDetail.SystemUserID = mergeParam.SystemUserID;

            mergeParam.DataAccessObject.UpdateSupplier(supplierDetail);
            return supplierDetail;
        }

        #region Helper methods
        /// <summary>
        ///     Get the object with which the data store can be accessed.
        /// </summary>
        /// <returns>
        ///     Returns an instance of an object that implements the <see cref="IDataAccess"/> interface.
        /// </returns>
        private IDataAccess GetDataAccessObject()
        {
            return new SqlServerDataAccess();
        }

        /// <summary>
        ///     Subtracts the client time from the server time
        /// </summary>
        /// <param name="serverTime">
        ///     UTC time on the server
        /// </param>
        /// <param name="clientTime">
        ///     UTC time on the client.
        /// </param>
        /// <returns>
        ///     Returns the difference between the <paramref name="serverTime"/> and the <paramref name="clientTime"/>.
        /// </returns>
        private TimeSpan GetServerClientTimeDifference(DateTime serverTime, DateTime clientTime)
        {
            return serverTime.Subtract(clientTime);
        }

        /// <summary>
        ///     Calculates a time value adjusted for the time difference between the client machine and the server.
        /// </summary>
        /// <param name="utcTime">
        ///     Time value to adjust.
        /// </param>
        /// <param name="adjustment">
        ///     Time difference between the client and the server (see <seealso cref="GetServerClientTimeDifference"/>).
        /// </param>
        /// <returns>
        ///     Returns an adjusted time value
        /// </returns>
        private DateTime AdjustClientUtcTime(DateTime utcTime, TimeSpan adjustment)
        {
            return utcTime.Add(adjustment);
        }
        #endregion Helper methods
    }
}
