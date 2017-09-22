using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PriceListWcfService.DataContracts
{
    /// <summary>
    ///     Flags used by the <see cref="ResponseDTOWrapper"/> class to indicate the actions that were taken on a record.
    /// </summary>
    [DataContract]
    [Flags]
    public enum RecordActionFlags
    {
        [EnumMember]
        None = 0,

        [EnumMember]
        Added = 1,

        [EnumMember]
        Updated = 2,

        [EnumMember]
        Deleted = 4,
    }

    public static class RecordActionFlagsExtensions
    {
        public static Boolean Added(this RecordActionFlags flags)
        {
            return (RecordActionFlags.Added == (flags & RecordActionFlags.Added));
        }
    } // class RecordActionFlagsExtensions

    /// <summary>
    ///     Codes that indicate errors that were encountered when processing a record.
    /// </summary>
    [DataContract]
    public enum RecordErrorCodes
    {
        [EnumMember]
        NoError = 0,

        [EnumMember]
        DuplicateCode = 100, // The Code value already exists in the table
    } // enum RecordErrorCodes

    // Wrapper object for DTO objects that includes fields to indicate if the record was added, updated, or deleted
    [DataContract]
    public class ResponseDTOWrapper<T> where T: class
    {
        #region Private data members
        /// <summary>
        ///     Flags to indicate if a record was added, updated, or deleted.
        /// </summary>
        private RecordActionFlags mActionFlags;

        /// <summary>
        ///     Processing error code
        /// </summary>
        private RecordErrorCodes mErrorCode;

        /// <summary>
        ///     Additional error information
        /// </summary>
        private String mErrorMessage;
        #endregion Private data members

        #region Public properties
        /// <summary>
        ///     Flags to indicate if a record was added, updated, or deleted.
        /// </summary>
        [DataMember]
        private RecordActionFlags ActionFlags
        {
            get { return this.mActionFlags; }
            set { this.mActionFlags = value; }
        }

        /// <summary>
        ///     Processing error code
        /// </summary>
        [DataMember]
        private RecordErrorCodes ErrorCode
        {
            get { return this.mErrorCode; }
            set { this.mErrorCode = value; }
        }

        /// <summary>
        ///     Additional error information
        /// </summary>
        [DataMember]
        private String ErrorMessage
        {
            get { return this.mErrorMessage; }
            set { this.mErrorMessage = value; }
        }
        #endregion Public properties
    } // class ResponseDTOWrapper
} // namespace PriceListWcfService.DataContracts
