using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PriceListWcfService.DataContracts
{
    [DataContract]
    public abstract class RequestHeader
    {
        #region Private data members
        /// <summary>
        ///     UTC time of the client machine at the time of the request.
        /// </summary>
        private DateTime mClientUtcTime;

        /// <summary>
        ///     Session token with which the user can be identified.
        /// </summary>
        private String mSessionToken;
        #endregion Private data members

        #region Public properties
        /// <summary>
        ///     Gets or sets the UTC time on the client machine at the time of the request.
        /// </summary>
        [DataMember]
        public DateTime ClientUtcTime
        {
            get { return this.mClientUtcTime; }
            set { this.mClientUtcTime = value; }
        }

        /// <summary>
        ///     Gets or sets the session token used to identify the user.
        /// </summary>
        [DataMember]
        public String SessionToken
        {
            get { return this.mSessionToken; }
            set { this.mSessionToken = value; }
        }
        #endregion // Public properties
    } // class RequestHeader
} // namespace PriceListWcfService.DataContracts
