using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NuRpg.Collections {
	public interface IPriorityDeque<TElement, TPriority> :
		IReadOnlyPriorityDeque<TElement, TPriority> {
		new IList<(TElement Element, TPriority Priority)> Items { get; }
		TElement DequeueBack();
		TElement DequeueFront();
		void Enqueue(TElement element);
		void Enqueue(TElement element, TPriority priority);
		void EnqueueBack(TElement element);
		void EnqueueFront(TElement element);
		bool TryDequeueBack([MaybeNullWhen(false)] out TElement element, [MaybeNullWhen(false)] out TPriority priority);
		bool TryDequeueFront([MaybeNullWhen(false)] out TElement element, [MaybeNullWhen(false)] out TPriority priority);
	}
}