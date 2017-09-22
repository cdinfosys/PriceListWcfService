using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace PriceListWcfService
{
    namespace Utilities
    {
        public static class Utility
        {
            #region Class members
            /// <summary>
            ///     Instance of the logger class.
            /// </summary>
            private static Logger mLogger;
            #endregion // Class members

            #region Construction
            static Utility()
            {
                mLogger = LogManager.GetCurrentClassLogger();
            }
            #endregion Construction

            #region Public properties
            /// <summary>
            ///     Gets the instance of the Logger.
            /// </summary>
            public static Logger EventLogger => Utility.mLogger;

            /// <summary>
            ///     Gets the name of the mutex to use when accessing the Supplier.Supplier table.
            /// </summary>
            public static String SupplierMutexName => "SUPPLIER.SUPPLIER_CB61F9BAD4AE4573B961EE0B69464534";

            /// <summary>
            ///     Merges an existing collection of records and an updated collection of records and returns a merged collection.
            /// </summary>
            /// <typeparam name="TElement1">
            ///     Type of records in the original collection
            /// </typeparam>
            /// <typeparam name="TElement2">
            ///     Type of records in the revised collection
            /// </typeparam>
            /// <typeparam name="TResult">
            ///     Data type of the records in the resulting collection.
            /// </typeparam>
            /// <param name="originalCollection">
            ///     Original ordered collection of items.
            /// </param>
            /// <param name="revisedCollection">
            ///     New ordered collection of items.
            /// </param>
            /// <param name="compareKeysCallback">
            ///     Callback function to compare the record from the original collection to the record of the revised collection.
            /// </param>
            /// <param name="recAddedCallback">
            ///     Called if a record appears in the revised collection but is missing from the original collection.
            /// </param>
            /// <param name="recDeletedCallback">
            ///     Called if a record appears in the original collection but is missing from the revised collection.
            /// </param>
            /// <param name="recsMatchedCallback">
            ///     Called if a record exists in the original and revised collection.
            /// </param>
            /// <param name="userData">
            ///     Additional data that is passed to the callback methods.
            /// </param>
            /// <returns>
            ///     Returns a merged list of items.
            /// </returns>
            public static IEnumerable<TResult> MergeLists<TElement1, TElement2, TResult>
            (
                IOrderedEnumerable<TElement1> originalCollection, 
                IOrderedEnumerable<TElement2> revisedCollection,
                Func<TElement1, TElement2, Int32> compareKeysCallback,
                Func<TElement2, Object, TResult> recAddedCallback,
                Action<TElement1, Object> recDeletedCallback,
                Func<TElement1, TElement2, Object, TResult> recsMatchedCallback,
                Object userData
            ) where TElement1 : class
              where TElement2 : class
            {
                List<TResult> result = new List<TResult>();

                IEnumerator<TElement1> originalListIterator = originalCollection.GetEnumerator();
                IEnumerator<TElement2> revisedListIterator = revisedCollection.GetEnumerator();

                TElement1 originalRec = originalListIterator.MoveNext() ? originalListIterator.Current : null;
                TElement2 revisedRec = revisedListIterator.MoveNext() ? revisedListIterator.Current : null;
                do
                {
                    if ((originalRec != null) && (revisedRec != null))
                    {
                        Int32 compareResult = compareKeysCallback(originalRec, revisedRec);
                        if (compareResult < 0)
                        {
                            recDeletedCallback(originalRec, userData);
                            originalRec = originalListIterator.MoveNext() ? originalListIterator.Current : null;
                        }
                        else if (compareResult > 0)
                        {
                            result.Add(recAddedCallback(revisedRec, userData));
                            revisedRec = revisedListIterator.MoveNext() ? revisedListIterator.Current : null;
                        }
                        else
                        {
                            result.Add(recsMatchedCallback(originalRec, revisedRec, userData));
                            originalRec = originalListIterator.MoveNext() ? originalListIterator.Current : null;
                            revisedRec = revisedListIterator.MoveNext() ? revisedListIterator.Current : null;
                        }
                    }
                    else if (originalRec != null)
                    {
                        recDeletedCallback(originalRec, userData);
                        originalRec = originalListIterator.MoveNext() ? originalListIterator.Current : null;
                    }
                    else if (revisedRec != null)
                    {
                        result.Add(recAddedCallback(revisedRec, userData));
                        revisedRec = revisedListIterator.MoveNext() ? revisedListIterator.Current : null;
                    }
                } while ((originalRec != null) || (revisedRec != null));

                return result;
            }

            /// <summary>
            ///     Iterates through the collection in <paramref name="sourceList"/> and calls the function object in <paramref name="func"/>
            ///     to produce a new list of <typeparamref name="TResult"/>.
            /// </summary>
            /// <typeparam name="TSource">
            ///     Type of elements in the source collection.
            /// </typeparam>
            /// <typeparam name="TResult">
            ///     Type of elements in the resulting collection.
            /// </typeparam>
            /// <param name="sourceList">
            ///     Source collection.
            /// </param>
            /// <param name="func">
            ///     Delegate to convert items from the source collection into items for the resulting collection.
            /// </param>
            /// <returns>
            ///     Returns a collection of type <typeparamref name="TResult"/>.
            /// </returns>
            public static IEnumerable<TResult> TransformElements<TSource, TResult>
            (
                this IEnumerable<TSource> sourceList, 
                Func<TSource, TResult> func
            )
            {
                List<TResult> result = new List<TResult>();
                foreach (TSource sourceRec in sourceList)
                {
                    result.Add(func(sourceRec));
                }
                return result;
            }
            #endregion Public properties
        } // class Utility
    } // namespace Utility
} // namespace PriceListWcfService