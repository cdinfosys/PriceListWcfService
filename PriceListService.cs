using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PriceListWcfService.DataContracts;
using PriceListWcfService.DataContracts.Supplier;
using PriceListWcfService.Interfaces;
using PriceListWcfService.Model;

namespace PriceListWcfService
{
    public class PriceListService : IPriceListService
    {
        public GetSuppliersResponse GetSuppliers()
        {
            GetSuppliersResponse responseObject = new GetSuppliersResponse()
            {
                ErrorCode = ResponseErrorCode.NoError
            };

            using (Mutex accessMutex = new Mutex(false, Utility.Utility.SupplierMutexName))
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

        public UpdateSuppliersResponse UpdateSuppliers(DateTime clientUtcTime, UpdateSuppliersRequest supplierDataIn)
        {
            UpdateSuppliersResponse response = new UpdateSuppliersResponse();

            try
            {
                using (Mutex accessMutex = new Mutex(false, Utility.Utility.SupplierMutexName))
                {
                    try
                    {
                        // Prevent other instance from accessing the table until we are done.
                        accessMutex.WaitOne();

                        Dictionary<Guid, SupplierDTO> suppliersMap = supplierDataIn.SupplierRecords.ToDictionary(u => u.UniqueIdentifier);

                        using (IDataAccess dataAccess = GetDataAccessObject())
                        {
                            DateTime serverUtcTime = dataAccess.GetDatabaseServerUtcTime();

                            foreach (SupplierDTO supplierRec in supplierDataIn.SupplierRecords)
                            {
                            }
                        }
                    }
                    finally
                    {
                        accessMutex.ReleaseMutex();
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

        #region Helper methods
        private IDataAccess GetDataAccessObject()
        {
            return new SqlServerDataAccess();
        }
        #endregion Helper methods
    }
}
