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
            using (Mutex accessMutex = new Mutex(false, Utility.Utility.SupplierMutexName))
            {
                try
                {
                    // Prevent other instance from accessing the table until we are done.
                    accessMutex.WaitOne();

                    return new GetSuppliersResponse
                    {
                        ErrorCode = ResponseErrorCode.ExceptionCaught,
                        ErrorDescription = "Wat kyk jy?"
/*
                        Suppliers = new List<SupplierDTO>()
                        {
                            new SupplierDTO()
                            {
                                SupplierID = 1,
                                UniqueIdentifier = Guid.NewGuid(),
                                Code = "SUPP1",
                                Descr = "Big Supplier",
                                Address = new List<String>() { "Here", "There", "Everywhere" }
                            },
                            new SupplierDTO()
                            {
                                SupplierID = 2,
                                UniqueIdentifier = Guid.NewGuid(),
                                Code = "SUPP2",
                                Descr = "Biger Supplier",
                                Address = new List<String>() { "Tom", "Dick", "Harry" }
                            },
                        }
                        */
                    };
                }
                finally
                {
                    accessMutex.ReleaseMutex();
                }
            }
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
