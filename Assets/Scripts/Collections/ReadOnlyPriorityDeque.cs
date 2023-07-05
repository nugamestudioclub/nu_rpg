#nullable enable

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace NuRpg.Collections {
	public class ReadOnlyPriorityDeque<TElement, TPriority> :
		IReadOnlyPriorityDeque<TElement, TPriority> {
		private readonly IPriorityDeque<TElement, TPriority> _deque;
		private readonly ReadOnlyCollection<(TElement Element, TPriority Priority)> _items;

		public ReadOnlyPriorityDeque(IPriorityDeque<TElement, TPriority> deque) {
			Exceptions.ArgumentNull.ThrowIfNull(deque, nameof(deque));
			_deque = deque;
			_items = deque.Items is ReadOnlyCollection<(TElement Element, TPriority Priority)> readOnly
				? readOnly
				: new ReadOnlyCollection<(TElement Element, TPriority Priority)>(deque.Items);
		}

		public int Count => _deque.Count;

		public IComparer<TPriority> Comparer => _deque.Comparer;

		public TPriority? DefaultPriority => _deque.DefaultPriority;

		public ReadOnlyCollection<(TElement Element, TPriority Priority)> Items => _items;

		IReadOnlyList<(TElement Element, TPriority Priority)> IReadOnlyPriorityDeque<TElement, TPriority>.Items => _items;

		public TElement PeekBack() {
			return _deque.PeekBack();
		}

		public TElement PeekFront() {
			return _deque.PeekFront();
		}

		public bool TryPeekBack([MaybeNullWhen(false)] out TElement element, [MaybeNullWhen(false)] out TPriority priority) {
			return _deque.TryPeekBack(out element, out priority);
		}

		public bool TryPeekFront([MaybeNullWhen(false)] out TElement element, [MaybeNullWhen(false)] out TPriority priority) {
			return _deque.TryPeekFront(out element, out priority);
		}
	}
}