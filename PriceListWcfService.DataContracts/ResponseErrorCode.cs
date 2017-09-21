using System.Runtime.Serialization;

namespace PriceListWcfService
{
    namespace DataContracts
    {
        /// <summary>
        ///     Enumerated respose error codes used by <see cref="ResponseHeader"/> derivatives.
        /// </summary>
        [DataContract]
        public enum ResponseErrorCode
        {
            /// <summary>
            ///     The operation was successful.
            /// </summary>
            [EnumMember]
            NoError = 0,

            /// <summary>
            ///     An exception was caught
            /// </summary>
            [EnumMember]
            ExceptionCaught = 100,

            /// <summary>
            ///     The user is not logged in.
            /// </summary>
            [EnumMember]
            NotLoggedIn = 200,

            /// <summary>
            ///     The user session expired
            /// </summary>
            [EnumMember]
            SessionExpired = 300,
        } // enum ResponseErrorCodes
    } // namespace DataContracts
} // namespace PriceListWcfService
