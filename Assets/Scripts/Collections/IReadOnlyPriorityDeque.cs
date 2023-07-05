#nullable enable

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NuRpg.Collections {
	public interface IReadOnlyPriorityDeque<TElement, TPriority> {
		int Count { get; }
		IComparer<TPriority> Comparer { get; }
		TPriority? DefaultPriority { get; }
		IReadOnlyList<(TElement Element, TPriority Priority)> Items { get; }
		TElement PeekBack();
		TElement PeekFront();
		bool TryPeekBack([MaybeNullWhen(false)] out TElement element, [MaybeNullWhen(false)] out TPriority priority);
		bool TryPeekFront([MaybeNullWhen(false)] out TElement element, [MaybeNullWhen(false)] out TPriority priority);
	}
}