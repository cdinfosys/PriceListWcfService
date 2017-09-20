using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using PriceListWcfService.Interfaces;
using System.Configuration;
using System.Data;

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
