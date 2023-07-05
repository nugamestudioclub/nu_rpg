#nullable enable

using System.Collections.Generic;
using System.Linq;

namespace NuRpg.Collections {
	public static class Extensions {
		public static IEnumerable<T> EnumerateMoreThanOnce<T>(this IEnumerable<T> items) {
			Exceptions.ArgumentNull.ThrowIfNull(items, nameof(items));
			return items is ICollection<T> || items is IReadOnlyCollection<T>
				? items
				: items.ToList();
		}

		public static int EstimateCapacity<T>(this IEnumerable<T> items) {
			if( items is ICollection<T> collection )
				return collection.Count;
			else if( items is IReadOnlyCollection<T> readOnlyCollection )
				return readOnlyCollection.Count;
			else
				return 0;
		}

		public static int LowerBound<T>(this IList<T> list, T item) {
			return list.LowerBound(0, list.Count, item, null);
		}

		public static int LowerBound<T>(this IList<T> list, T item, IComparer<T>? comparer) {
			return list.LowerBound(0, list.Count, item, comparer);
		}

		public static int LowerBound<T>(this IList<T> list, int startIndex, int count, T item, IComparer<T>? comparer) {
			Exceptions.ArgumentOutOfRange.ThrowIfNegative(startIndex, nameof(startIndex));
			Exceptions.ArgumentOutOfRange.ThrowIfNegative(count, nameof(count));
			Exceptions.ArgumentOutOfRange.ThrowIfInvalidRange(startIndex, count, list.Count);
			comparer ??= Comparer<T>.Default;
			int stopIndex = startIndex + count;
			while( startIndex < stopIndex ) {
				int middle = startIndex + (stopIndex - startIndex) / 2;
				if( comparer.Compare(list[middle], item) < 0 )
					startIndex = middle + 1;
				else
					stopIndex = middle;
			}
			return startIndex;
		}

		public static int UpperBound<T>(this IList<T> list, T item) {
			return list.UpperBound(0, list.Count, item, null);
		}

		public static int UpperBound<T>(this IList<T> list, T item, IComparer<T>? comparer) {
			return list.UpperBound(0, list.Count, item, comparer);
		}


		public static int UpperBound<T>(this IList<T> list, int startIndex, int count, T item, IComparer<T>? comparer) {
			Exceptions.ArgumentOutOfRange.ThrowIfNegative(startIndex, nameof(startIndex));
			Exceptions.ArgumentOutOfRange.ThrowIfNegative(count, nameof(count));
			Exceptions.ArgumentOutOfRange.ThrowIfInvalidRange(startIndex, count, list.Count);
			comparer ??= Comparer<T>.Default;
			int stopIndex = startIndex + count;
			while( startIndex < stopIndex ) {
				int midpointIndex = startIndex + (stopIndex - startIndex) / 2;
				if( comparer.Compare(item, list[midpointIndex]) < 0 )
					stopIndex = midpointIndex;
				else
					startIndex = midpointIndex + 1;
			}
			return startIndex;
		}
	}
}