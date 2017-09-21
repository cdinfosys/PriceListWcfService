using System;
using System.Runtime.Serialization;

namespace PriceListWcfService.DataContracts
{
    namespace Linking
    {
        [DataContract]
        public class SupplierProductDTO
        {
            #region Private data members
            private Int32 mSupplierProductID;
            private Int32 mSupplierID;
            private Int32 mProductID;
            #endregion // Private data members

            #region Public properties
            [DataMember]
            public Int32 SupplierProductID
            {
                get{ return this.mSupplierProductID; }
                set{ this.mSupplierProductID = value;}
            }

            [DataMember]
            public Int32 SupplierID
            {
                get{ return this.mSupplierID; }
                set{ this.mSupplierID = value;}
            }

            [DataMember]
            public Int32 ProductID
            {
                get{ return this.mProductID; }
                set{ this.mProductID = value;}
            }
            #endregion Public properties
        } // class SupplierProductDTO
    } // namespace Linking
} // namespace PriceListWcfService.DataContracts
