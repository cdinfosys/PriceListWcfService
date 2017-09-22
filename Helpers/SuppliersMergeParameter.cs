using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriceListWcfService.Interfaces;

namespace PriceListWcfService.Helpers
{
    /// <summary>
    ///     Objects of this class are passed to the <see cref="Utilities.Utility.MergeList" method/>
    /// </summary>
    internal class SuppliersMergeParameter
    {
        #region Private data members
        /// <summary>
        ///     Object to access the data store.
        /// </summary>
        private readonly IDataAccess mDataAccessObject;

        /// <summary>
        ///     Storage for the <see cref="SystemUserId"/> property.
        /// </summary>
        private readonly Int32 mSystemUserID;

        /// <summary>
        ///     Time difference between the client making a call to the server and the time on the server.
        /// </summary>
        private TimeSpan mClientServerTimeDifference;
        #endregion // Private data members

        #region Construction
        /// <summary>
        ///     Construct a parameter object
        /// </summary>
        /// <param name="dataAccessObject">
        ///     Object to access the data store.
        /// </param>
        public SuppliersMergeParameter
        (
            IDataAccess dataAccessObject,
            Int32 systemUserID,
            TimeSpan clientServerTimeDifference
        )
        {
            this.mDataAccessObject = dataAccessObject;
            this.mSystemUserID = systemUserID;
            this.mClientServerTimeDifference = clientServerTimeDifference;
        }
        #endregion // Construction

        #region Public accessor methods
        /// <summary>
        ///     Gets the object to access the data store.
        /// </summary>
        public IDataAccess DataAccessObject => mDataAccessObject;

        /// <summary>
        ///     Gets the user id of the user performing the operation.
        /// </summary>
        public Int32 SystemUserID => mSystemUserID;

        /// <summary>
        ///     Gets the time difference calculated between the time on the client machine and the time on the server.
        /// </summary>
        public TimeSpan ClientServerTimeDifference => this.mClientServerTimeDifference;
        #endregion // Public accessor methods
    } // class SuppliersMergeParameter
} // namespace PriceListWcfService.Helpers
