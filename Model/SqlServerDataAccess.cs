using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using PriceListWcfService.Interfaces;
using System.Configuration;
using System.Data;
using PriceListWcfService.DataContracts.Supplier;

namespace PriceListWcfService
{
    namespace Model
    {
        /// <summary>
        ///     Data access implementation for SQL Server
        /// </summary>
        public class SqlServerDataAccess : IDataAccess
        {
            #region Private data members
            /// <summary>
            ///     Shared connection object.
            /// </summary>
            private SqlConnection mSharedConnectionObject;
            #endregion Private data members

            #region IDataAccess implementation
            /// <summary>
            ///     Get the current time from the database server.
            /// </summary>
            /// <returns>
            ///     Returns the current UTC time of the database.
            /// </returns>
            public DateTime GetDatabaseServerUtcTime()
            {
                using (SqlConnection connection = CreateConnection())
                {
                    using (SqlCommand dbCommand = connection.CreateCommand())
                    {
                        dbCommand.CommandText = "SELECT Support.GetDatabaseUtcTime() ";
                        dbCommand.CommandType = CommandType.Text;
                        DateTime result = Convert.ToDateTime(dbCommand.ExecuteScalar()); 
                        return result;
                    }
                }
            }

            /// <summary>
            ///     Get a list of suppliers from the database Suppliers.Supplier table
            /// </summary>
            /// <returns>
            ///     Returns a collection of <see cref="SupplierDTO"/> objects.
            /// </returns>
            public IEnumerable<SupplierDTO> GetSuppliers()
            {
                List<SupplierDTO> result = new List<SupplierDTO>();

                using (SqlConnection connection = CreateConnection())
                {
                    using (SqlCommand dbCommand = connection.CreateCommand())
                    {
                        dbCommand.CommandText = "[Suppliers].[GetSuppliers]";
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            Int32 colIndexSupplierID = dataReader.GetOrdinal("SupplierID");
                            Int32 colIndexUniqueID = dataReader.GetOrdinal("UniqueID");
                            Int32 colIndexCode = dataReader.GetOrdinal("Code");
                            Int32 colIndexDescr = dataReader.GetOrdinal("Descr");
                            Int32 colIndexAddressDetail = dataReader.GetOrdinal("AddressDetail");

                            while (dataReader.Read())
                            {
                                SupplierDTO addRec = new SupplierDTO()
                                {
                                    SupplierID = dataReader.GetInt32(colIndexSupplierID),
                                    UniqueIdentifier = dataReader.GetGuid(colIndexUniqueID),
                                    Code = dataReader.GetString(colIndexCode),
                                    Descr = dataReader.GetString(colIndexDescr),
                                    Address = StringToStringCollection(dataReader.GetString(colIndexAddressDetail))
                                };

                                result.Add(addRec);
                            }
                        }
                    }
                }

                return result;
            }

            /// <summary>
            ///     Update an existing Suppliers.Supplier record.
            /// </summary>
            /// <param name="updateRec">
            ///     Updated record values.
            /// </param>
            /// <returns>
            ///     Returns 1 if a previous record was found and the data was not updated. Returns 2 if the data was updated.
            /// </returns>
            /// <remarks>
            ///     When the return code is 1 the <paramref name="updateRec"/> is updated with the current values in the database.
            /// </remarks>
            public Int32 UpdateSupplier(ExtSupplierDTO updateRec)
            {
                using (SqlConnection connection = CreateConnection())
                {
                    using (SqlCommand dbCommand = connection.CreateCommand())
                    {
                        dbCommand.CommandText = "[Suppliers].[UpdateSupplier]";
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.AddWithValue("@code", updateRec.Code);
                        dbCommand.Parameters.AddWithValue("@descr", updateRec.Descr);
                        String address = StringCollectionToString(updateRec.Address);
                        if (String.IsNullOrWhiteSpace(address))
                        {
                            SqlParameter nullParam = new SqlParameter("@addressDetail", SqlDbType.VarChar, 200)
                            {
                                Direction = ParameterDirection.Input,
                                Value = DBNull.Value
                            };
                            dbCommand.Parameters.Add(nullParam);
                        }
                        else
                        {
                            dbCommand.Parameters.AddWithValue("@addressDetail", address);
                        }
                        dbCommand.Parameters.AddWithValue("@recordUpdatedTime", updateRec.LastUpdateTime);
                        dbCommand.Parameters.AddWithValue("@systemUserID", updateRec.SystemUserID);

                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            Int32 colIndexActionCode = dataReader.GetOrdinal("ActionCode");
                            Int32 colIndexSupplierID = dataReader.GetOrdinal("SupplierID");
                            Int32 colIndexUniqueID = dataReader.GetOrdinal("UniqueID");
                            Int32 colIndexCode = dataReader.GetOrdinal("Code");
                            Int32 colIndexDescr = dataReader.GetOrdinal("Descr");
                            Int32 colIndexAddressDetail = dataReader.GetOrdinal("AddressDetail");
                            Int32 colIndexLastUpdateTime = dataReader.GetOrdinal("LastUpdateTime");
                            Int32 colIndexSystemUserID = dataReader.GetOrdinal("colIndexSystemUserID");

                            dataReader.Read();
                            updateRec.SupplierID = dataReader.GetInt32(colIndexSupplierID);
                            updateRec.UniqueIdentifier = dataReader.GetGuid(colIndexUniqueID);
                            updateRec.Code = dataReader.GetString(colIndexCode);
                            updateRec.Descr = dataReader.GetString(colIndexDescr);
                            updateRec.Address = dataReader.IsDBNull(colIndexAddressDetail) ? new List<String>() : new List<String>(StringToStringCollection(dataReader.GetString(colIndexAddressDetail)));
                            updateRec.LastUpdateTime = dataReader.GetDateTime(colIndexLastUpdateTime);
                            updateRec.SystemUserID = dataReader.GetInt32(colIndexSystemUserID);

                            return dataReader.GetInt32(colIndexActionCode);
                        }
                    }
                }
            }

            /// <summary>
            ///     Delete a Suppliers.Supplier record
            /// </summary>
            /// <param name="supplierID">
            ///     ID of the record to delete.
            /// </param>
            public void DeleteSupplier(Int32 supplierID)
            {
                // Cannot delete suppliers at present.
            }

            /// <summary>
            ///     Add a new record to Suppliers.Supplier
            /// </summary>
            /// <param name="supplierRec">
            ///     New supplier details.
            /// </param>
            public void AddSupplier(ExtSupplierDTO supplierRec)
            {
                using (SqlConnection connection = CreateConnection())
                {
                    using (SqlCommand dbCommand = connection.CreateCommand())
                    {
                        dbCommand.CommandText = "[Suppliers].[AddSupplier]";
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.AddWithValue("@code", supplierRec.Code);
                        dbCommand.Parameters.AddWithValue("@descr", supplierRec.Descr);
                        dbCommand.Parameters.AddWithValue("@addressDetail", StringCollectionToString(supplierRec.Address));
                        dbCommand.Parameters.AddWithValue("@systemUserID", supplierRec.SystemUserID);

                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            int colIndexSupplierID = dataReader.GetOrdinal("SupplierID");
                            int colIndexUniqueID = dataReader.GetOrdinal("UniqueID");

                            dataReader.Read();
                            supplierRec.SupplierID = dataReader.GetInt32(colIndexSupplierID);
                            supplierRec.UniqueIdentifier = dataReader.GetGuid(colIndexUniqueID);
                        }
                    }
                }
            }

            /// <summary>
            ///     Retrieve the Suppliers.Supplier records with known identifiers.
            /// </summary>
            /// <param name="supplierIdentifiers">
            ///     Collection of unique identifiers
            /// </param>
            /// <returns>
            ///     Returns a list of SupplierDTO records
            /// </returns>
            public IEnumerable<ExtSupplierDTO> GetExistingSupplierRecords(IEnumerable<Guid> supplierIdentifiers)
            {
                List<ExtSupplierDTO> result = new List<ExtSupplierDTO>();

                using (SqlConnection connection = CreateConnection())
                {
                    using (SqlCommand dbCommand = connection.CreateCommand())
                    {
                        DataTable idList = new DataTable("GuidTable", "Shared");
                        idList.Columns.Add("identifier", typeof(Guid));
                        idList.BeginLoadData();
                        try
                        {
                            foreach (Guid id in supplierIdentifiers)
                            {
                                idList.Rows.Add(id);
                            }
                        }
                        finally
                        {
                            idList.EndLoadData();
                        }

                        dbCommand.CommandText = "[Suppliers].[GetSuppliersWithIdentifiers]";
                        dbCommand.CommandType = CommandType.StoredProcedure;
                        dbCommand.Parameters.AddWithValue("@identifiersList", idList);

                        using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                        {
                            Int32 colIndexSupplierID = dataReader.GetOrdinal("SupplierID");
                            Int32 colIndexUniqueID = dataReader.GetOrdinal("UniqueID");
                            Int32 colIndexCode = dataReader.GetOrdinal("Code");
                            Int32 colIndexDescr = dataReader.GetOrdinal("Descr");
                            Int32 colIndexAddressDetail = dataReader.GetOrdinal("AddressDetail");
                            Int32 colIndexLastUpdateTime = dataReader.GetOrdinal("LastUpdateTime");
                            Int32 colIndexSystemUserID = dataReader.GetOrdinal("SystemUserID");

                            while (dataReader.Read())
                            {
                                ExtSupplierDTO addRec = new ExtSupplierDTO()
                                {
                                    SupplierID = dataReader.GetInt32(colIndexSupplierID),
                                    UniqueIdentifier = dataReader.GetGuid(colIndexUniqueID),
                                    Code = dataReader.GetString(colIndexCode),
                                    Descr = dataReader.GetString(colIndexDescr),
                                    Address = StringToStringCollection(dataReader.GetString(colIndexAddressDetail)),
                                    LastUpdateTime = dataReader.GetDateTime(colIndexLastUpdateTime),
                                    SystemUserID = dataReader.GetInt32(colIndexSystemUserID)
                                };

                                result.Add(addRec);
                            }
                        }
                    }
                }

                return result;
            }

            #endregion IDataAccess implementation

            #region IDisposable implementation
            /// <summary>
            ///     Housekeeping
            /// </summary>
            public void Dispose()
            {
                mSharedConnectionObject?.Dispose();
                this.mSharedConnectionObject = null;
            }
            #endregion IDisposable implementation

            /// <summary>
            ///     Convert a collection of strings into a delimited string for storage in the database.
            /// </summary>
            /// <param name="stringCollection">
            ///     Strings to concatenate
            /// </param>
            /// <returns>
            ///     Returns the concatenated strings.
            /// </returns>
            public String StringCollectionToString(IList<String> stringCollection)
            {
                // Remove all sequences that we want to use as our delimiter
                List<String> cleanedStrings = new List<String>();
                foreach (String str in stringCollection)
                {
                    cleanedStrings.Add(str.Replace("<\t]", "?"));
                }

                // Return a delimited string.
                return String.Join("<\t]", cleanedStrings);
            }

            /// <summary>
            ///     Breaks a delimited string into a collection of separate strings.
            /// </summary>
            /// <param name="inputString">
            ///     Delimited string.
            /// </param>
            /// <returns>
            ///     Returns a collection of strings.
            /// </returns>
            public IList<String> StringToStringCollection(String inputString)
            {
                return inputString.Split(new String[] {  "<\t]" }, StringSplitOptions.None);
            }

            #region Helper methods
            /// <summary>
            ///     Create a new connection to the database or re-use a previous connection.
            /// </summary>
            /// <returns>
            ///     Returns a <c>SqlConnection</c> object.
            /// </returns>
            private SqlConnection CreateConnection()
            {
                if (this.mSharedConnectionObject != null)
                {
                    if (this.mSharedConnectionObject.State != ConnectionState.Open)
                    {
                        this.mSharedConnectionObject.Dispose();
                        this.mSharedConnectionObject = null;
                    }
                }

                if (this.mSharedConnectionObject == null)
                {
                    SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnectionString"].ConnectionString);
                    dbConnection.Open();
                    this.mSharedConnectionObject = dbConnection;
                }

                return this.mSharedConnectionObject;
            }
            #endregion Helper methods
        } // class SqlServerDataAccess
    } // namespace Model
} // namespace PriceListWcfService
