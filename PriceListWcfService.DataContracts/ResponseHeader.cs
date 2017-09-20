using System;
using System.Runtime.Serialization;

namespace PriceListWcfService
{
    namespace DataContracts
    {
        [DataContract]
        public abstract class ResponseHeader
        {
            #region Private data members
            /// <summary>
            ///     Error code
            /// </summary>
            private ResponseErrorCode mErrorCode = ResponseErrorCode.NoError;

            /// <summary>
            ///     Additional error information
            /// </summary>
            private String mErrorDescription = String.Empty;
            #endregion // Private data members

            #region Public properties
            /// <summary>
            ///     Gets or sets an error code
            /// </summary>
            [DataMember]
            public ResponseErrorCode ErrorCode
            {
                get { return this.mErrorCode; }
                set { this.mErrorCode = value; }
            }

            /// <summary>
            ///     Gets or sets the error description
            /// </summary>
            [DataMember]
            public String ErrorDescription
            {
                get { return this.mErrorDescription; }
                set { this.mErrorDescription = value; }
            }
            #endregion Public properties
        } // class ResponseHeader
    }  // namespace DataContracts
} // namespace PriceListWcfService
