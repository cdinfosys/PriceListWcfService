using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using PriceListWcfService.DataContracts.Supplier;

namespace PriceListWcfService
{
    namespace DataContracts
    {
        /// <summary>
        ///     Response object for the <see cref="IPriceListService.GetSuppliers()" service call./> 
        /// </summary>
        [DataContract]
        public class GetSuppliersResponse : ResponseHeader
        {/*
            /// <summary>
            ///     Storage for the <see cref="Suppliers"/> property
            /// </summary>
            private List<SupplierDTO> mSuppliers;

            /// <summary>
            ///     Collection of supplier detail objects.
            /// </summary>
            [DataMember]
            public List<SupplierDTO> Suppliers
            {
                get
                {
                    return this.mSuppliers;
                }
                set
                {
                    this.mSuppliers = value;
                }
            }*/
        }
    } // namespace DataContracts
} // namespace PriceListWcfService
