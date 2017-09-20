using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceListWcfService
{
    namespace Utility
    {
        public static class Utility
        {
            /// <summary>
            ///     Gets the name of the mutex to use when accessing the Supplier.Supplier table.
            /// </summary>
            public static String SupplierMutexName => "SUPPLIER.SUPPLIER_CB61F9BAD4AE4573B961EE0B69464534";
        }
    } // namespace Utility
} // namespace PriceListWcfService