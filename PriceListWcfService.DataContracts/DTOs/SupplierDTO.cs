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
            ///     Storage for database Suppliers.Supplier records
            /// </summary>
            [DataContract]
            public class SupplierDTO : ICloneable
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

                public Object Clone()
                {
                    return new SupplierDTO()
                    {
                        mSupplierID = this.mSupplierID,
                        mUniqueIdentifier = this.mUniqueIdentifier,
                        mCode = this.mCode,
                        mDescr = this.mDescr,
                        mAddress = this.mAddress
                    };
                }
            } // class SupplierDTO

            /// <summary>
            ///     Supplier DTO with some additional fields for internal use
            /// </summary>
            public class ExtSupplierDTO : SupplierDTO
            {
                private DateTime mLastUpdateTime;
                private Int32 mSystemUserID;

                public DateTime LastUpdateTime
                {
                    get { return this.mLastUpdateTime; }
                    set { this.mLastUpdateTime = value; }
                }

                public Int32 SystemUserID
                {
                    get { return this.mSystemUserID; }
                    set { this.mSystemUserID = value; }
                }
            }
        } // namespace Supplier
    } // namespace DataContracts
} // namespace PriceListWcfService
