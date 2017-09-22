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
                protected Int32 mSupplierID;
                protected Guid mUniqueIdentifier;
                protected String mCode;
                protected String mDescr;
                protected IList<String> mAddress;
                protected Boolean mIsRetired;
                protected DateTime mLastUpdateTime;

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

                [DataMember]
                protected Boolean IsRetired
                {
                    get { return this.mIsRetired; }
                    set { this.mIsRetired = value; }
                }

                public DateTime LastUpdateTime
                {
                    get { return this.mLastUpdateTime; }
                    set { this.mLastUpdateTime = value; }
                }

                public Object Clone()
                {
                    return new SupplierDTO()
                    {
                        mSupplierID = this.mSupplierID,
                        mUniqueIdentifier = this.mUniqueIdentifier,
                        mCode = this.mCode,
                        mDescr = this.mDescr,
                        mAddress = this.mAddress,
                        mLastUpdateTime = this.mLastUpdateTime
                    };
                }
            } // class SupplierDTO

            /// <summary>
            ///     Supplier DTO with some additional fields for internal use
            /// </summary>
            public class ExtSupplierDTO : SupplierDTO, ICloneable
            {
                protected Int32 mSystemUserID;
                protected Int32 mResultCode;

                public Int32 SystemUserID
                {
                    get { return this.mSystemUserID; }
                    set { this.mSystemUserID = value; }
                }

                public Int32 ResultCode
                {
                    get { return this.mResultCode; }
                    set { this.mResultCode = value; }
                }

                public new Object Clone()
                {
                    return new ExtSupplierDTO()
                    {
                        mSupplierID = this.mSupplierID,
                        mUniqueIdentifier = this.mUniqueIdentifier,
                        mCode = this.mCode,
                        mDescr = this.mDescr,
                        mAddress = this.mAddress,
                        mSystemUserID = this.mSystemUserID,
                        mResultCode = this.mResultCode
                    };
                }
            }
        } // namespace Supplier
    } // namespace DataContracts
} // namespace PriceListWcfService
