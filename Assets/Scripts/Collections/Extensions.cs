using System.Collections.Generic;
using System.Linq;

namespace NuRpg.Collections {
	public static class Extensions {
		public static IEnumerable<T> EnumerateMoreThanOnce<T>(this IEnumerable<T> items) {
			Exceptions.ArgumentNull.ThrowIfNull(items, nameof(items));
			return items is IList<T> || items is IReadOnlyList<T>
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
	}
}