using System;
using System.Runtime.Serialization;

namespace PriceListWcfService
{
    namespace DataContracts
    {
        /// <summary>
        ///     Standard result object for methods that update entity records.
        /// </summary>
        [DataContract]
        public abstract class EntityUpdateResult
        {
            #region Private data members
            /// <summary>
            ///     Flag to indicate that the record was added to the database.
            /// </summary>
            private Boolean mAdded = false;

            /// <summary>
            ///     Flag to indicate that the existing database record was updated.
            /// </summary>
            private Boolean mUpdated = false;

            /// <summary>
            ///     Flag to indicate that the existing database record was deleted.
            /// </summary>
            private Boolean mDeleted = false;

            /// <summary>
            ///     Error code for the record.
            /// </summary>
            private Int32 mErrorCode = 0;

            /// <summary>
            ///     Error message for the record.
            /// </summary>
            private String mErrorMessage = String.Empty;
            #endregion Private data members

            #region Public properties
            [DataMember]
            public Boolean Added
            {
                get { return this.mAdded; }
                set { this.mAdded = value; }
            }

            [DataMember]
            public Boolean Deleted
            {
                get { return this.mDeleted; }
                set { this.mDeleted = value; }
            }

            [DataMember]
            public Boolean Updated
            {
                get { return this.mUpdated; }
                set { this.mUpdated = value; }
            }

            [DataMember]
            public Int32 MErrorCode
            {
                get { return mErrorCode; }
                set { this.mErrorCode = value; }
            }

            [DataMember]
            public String MErrorMessage
            {
                get { return mErrorMessage; }
                set { this.mErrorMessage = value; }
            }
            #endregion Public properties
        }
    } // namespace DataContracts
} // namespace PriceListWcfService
