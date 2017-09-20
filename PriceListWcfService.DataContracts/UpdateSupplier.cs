using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PriceListWcfService.DataContracts.Supplier;

namespace PriceListWcfService
{
    namespace DataContracts
    {
        /// <summary>
        ///     Payload for the UpdateSuppliers service call.
        /// </summary>
        [DataContract]
        public class UpdateSuppliersRequest
        {
            private List<SupplierDTO> mSupplierRecords;

            /// <summary>
            ///     Gets or sets a list of supplier records.
            /// </summary>
            [DataMember]
            public List<SupplierDTO> SupplierRecords
            {
                get { return mSupplierRecords; }
                set { this.mSupplierRecords = value; }
            }
        }

        /// <summary>
        ///     List entries for the <see cref="UpdateSuppliersResponse"/> object
        /// </summary>
        [DataContract]
        public class  SupplierUpdateResult : EntityUpdateResult
        {
            #region Private data members
            private Int32 mSupplierID;
            private Guid mUniqueIdentifier;
            #endregion Private data members

            #region Public properties
            [DataMember]
            public Int32 SupplierID
            {
                get { return mSupplierID; }
                set { this.mSupplierID = value; }
            }

            [DataMember]
            public Guid UniqueIdentifier
            {
                get { return mUniqueIdentifier; }
                set { this.mUniqueIdentifier = value; }
            }
            #endregion Public properties
        }

        /// <summary>
        ///     Response object for the UpdateSuppliers service call.
        /// </summary>
        [DataContract]
        public class UpdateSuppliersResponse : ResponseHeader
        {
            private IList<SupplierUpdateResult> mRecordUpdateResults;

            /// <summary>
            ///     A collection of <see cref="SupplierUpdateResult"/> objects to indicate what action was taken for every record in the request.
            /// </summary>
            public IList<SupplierUpdateResult> RecordUpdateResults
            {
                get { return this.mRecordUpdateResults; }
                set { this.mRecordUpdateResults = value; }
            }
        }
    } // namespace DataContracts
} // namespace PriceListWcfService
