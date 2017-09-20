using System;
using System.Collections.Generic;
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
            #region IDataAccess implementation
            /// <summary>
            ///     Get the current time from the database server.
            /// </summary>
            /// <returns>
            ///     Returns the current UTC time of the database.
            /// </returns>
            DateTime IDataAccess.GetDatabaseServerUtcTime()
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
                                    Address = dataReader.GetString(colIndexAddressDetail).Split('\n')
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
            }
            #endregion IDisposable implementation

            #region Helper methods
            /// <summary>
            ///     Create a new connection to the database.
            /// </summary>
            /// <returns>
            ///     Returns a <c>SqlConnection</c> object.
            /// </returns>
            private SqlConnection CreateConnection()
            {
                SqlConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlServerConnectionString"].ConnectionString);
                dbConnection.Open();
                return dbConnection;
            }
            #endregion Helper methods
        } // class SqlServerDataAccess
    } // namespace Model
} // namespace PriceListWcfService
