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
            [EnumMember]
            NoError = 0,

            [EnumMember]
            ExceptionCaught,
        } // enum ResponseErrorCodes
    } // namespace DataContracts
} // namespace PriceListWcfService
