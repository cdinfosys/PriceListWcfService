using System;
using System.Runtime.Serialization;

namespace PriceListWcfService.DataContracts
{
    namespace Product
    {
        /// <summary>
        ///     Storage for database Products.Product records
        /// </summary>
        [DataContract]
        public class ProductDTO
        {
            #region Private data members
            private Int32 mProductID;
            private Guid mUniqueID;
            private String mCode;
            private String mDescr;
            private Int32 mRetired;
            #endregion Private data members

            #region Public properties
            public Int32 ProductID
            { 
                get { return this.mProductID; } 
                set { this.mProductID = value; } 
            }

            [DataMember]
            public Guid UniqueID
            { 
                get { return this.mUniqueID; } 
                set { this.mUniqueID = value; }
            }

            [DataMember]
            public String Code
            {
                get { return this.mCode; }
                set { this.mCode = value; }
            }

            [DataMember]
            public String Descr
            {
                get { return this.mDescr; }
                set { this.mDescr = value; }
            }

            [DataMember]
            public Int32 Retired
            {
                get { return this.mRetired; } 
                set { this.mRetired = value; }
            }
            #endregion Public properties
        } // class ProductDTO
    } // namespace Product
} // namespace PriceListWcfService.DataContracts
