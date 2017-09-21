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
            Int32 systemUserID
        )
        {
            this.mDataAccessObject = dataAccessObject;
            this.mSystemUserID = systemUserID;
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

        #endregion // Public accessor methods
    } // class SuppliersMergeParameter
} // namespace PriceListWcfService.Helpers
