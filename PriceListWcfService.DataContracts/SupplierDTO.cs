using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PriceListWcfService
{
    namespace DataContracts
    {
        namespace Supplier
        {
            /// <summary>
            ///     Storage for database Supplier.Supplier records
            /// </summary>
            [DataContract]
            public class SupplierDTO
            {
                private Int32 mSupplierID;
                private Guid mUniqueIdentifier;
                private String mCode;
                private String mDescr;
                private IList<String> mAddress;

                [DataMember]
                public Int32 SupplierID 
                {
                    get { return  this.mSupplierID; }
                    set { this.mSupplierID = value; }
                }

                [DataMember]
                public Guid UniqueIdentifier
                {
                    get { return this.mUniqueIdentifier; }
                    set { this.mUniqueIdentifier = value; }
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
                public IList<String> Address
                {
                    get { return this.mAddress; }
                    set { this.mAddress = value; }
                }
            }
        } // namespace Supplier
    } // namespace DataContracts
} // namespace PriceListWcfService
