#nullable enable

using NuRpg.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NuRpg.Collections {
	public class PriorityDeque<TElement, TPriority> : IPriorityDeque<TElement, TPriority> {
		private readonly List<(TElement Element, TPriority Priority)> _frontList;
		private readonly List<(TElement Element, TPriority Priority)> _backList;
		private readonly IComparer<TPriority> _comparer;
		private readonly TPriority? _defaultPriority;
		private readonly ItemList _items;

		public PriorityDeque() :
			this(capacity: 0, comparer: null, defaultPriority: default) { }

		public PriorityDeque(IComparer<TPriority>? comparer) :
			this(capacity: 0, comparer, defaultPriority: default) { }

		public PriorityDeque(TPriority defaultPriority) :
			this(capacity: 0, comparer: null, defaultPriority) { }

		public PriorityDeque(IComparer<TPriority>? comparer, TPriority? defaultPriority) {
			_frontList = new();
			_backList = new();
			_comparer = comparer ?? Comparer<TPriority>.Default;
			_defaultPriority = defaultPriority;
			_items = new(this);
		}

		public PriorityDeque(IEnumerable<(TElement Element, TPriority Priority)> items) :
			this(items, comparer: null, defaultPriority: default) { }

		public PriorityDeque(IEnumerable<(TElement Element, TPriority Priority)> items, IComparer<TPriority>? comparer) :
			this(items, comparer, defaultPriority: default) { }

		public PriorityDeque(IEnumerable<(TElement Element, TPriority Priority)> items, TPriority? defaultPriority) :
			this(items, comparer: null, defaultPriority) { }

		public PriorityDeque(IEnumerable<(TElement Element, TPriority Priority)> items, IComparer<TPriority>? comparer, TPriority? defaultPriority) {
			Exceptions.ArgumentNull.ThrowIfNull(items, nameof(items));
			_comparer = comparer ?? Comparer<TPriority>.Default;
			_backList = new(items);
			_backList.Sort(CompareByPriorityBack());
			_frontList = new(_backList.Count);
			int frontCount = _backList.Count / 2 + _backList.Count % 2;
			for( int i = frontCount - 1; i >= 0; --i )
				_frontList.Add(_backList[i]);
			for( int i = frontCount; i < _backList.Count; ++i )
				_backList[i - frontCount] = _backList[i];
			_backList.RemoveRange(frontCount, _backList.Count - frontCount);
			_defaultPriority = defaultPriority;
			_items = new(this);
		}

		public PriorityDeque(int capacity) :
		this(capacity, comparer: null, defaultPriority: default) { }

		public PriorityDeque(int capacity, IComparer<TPriority>? comparer) :
		this(capacity, comparer, defaultPriority: default) { }

		public PriorityDeque(int capacity, TPriority? defaultPriority) :
		this(capacity, comparer: null, defaultPriority) { }

		public PriorityDeque(int capacity, IComparer<TPriority>? comparer, TPriority? defaultPriority) {
			_frontList = new(capacity);
			_backList = new(capacity);
			_comparer = comparer ?? Comparer<TPriority>.Default;
			_defaultPriority = defaultPriority;
			_items = new(this);
		}

		public int Count => _frontList.Count + _backList.Count;

		public IComparer<TPriority> Comparer => _comparer;

		public TPriority? DefaultPriority => _defaultPriority;

		public ItemList Items => _items;
		IList<(TElement Element, TPriority Priority)> IPriorityDeque<TElement, TPriority>.Items => _items;
		IReadOnlyList<(TElement Element, TPriority Priority)> IReadOnlyPriorityDeque<TElement, TPriority>.Items => _items;

		public ReadOnlyPriorityDeque<TElement, TPriority> AsReadOnly() {
			return new(this);
		}

		public void Clear() {
			_frontList.Clear();
			_backList.Clear();
		}

		public TElement DequeueBack() {
			Exceptions.InvalidOperation.ThrowIfEmpty(Count);
			var (element, _) = DoDequeueBack();
			return element;
		}

		public TElement DequeueFront() {
			Exceptions.InvalidOperation.ThrowIfEmpty(Count);
			var (element, _) = DoDequeueFront();
			return element;
		}

		public void Enqueue(TElement element) {
			DoEnqueue(new(element, _defaultPriority!));
		}

		public void Enqueue(TElement element, TPriority priority) {
			DoEnqueue(new(element, priority));
		}

		public void EnqueueBack(TElement element) {
			_backList.Add(new(element, GetLowestPriority()!));
		}

		public void EnqueueFront(TElement element) {
			_frontList.Add(new(element, GetHighestPriority()!));
		}

#if NET6_0_OR_GREATER
		public void EnsureCapacity(int capacity) {
			_frontList.EnsureCapacity(capacity);
			_backList.EnsureCapacity(capacity);
		}
#endif

		public TElement PeekBack() {
			Exceptions.InvalidOperation.ThrowIfEmpty(Count);
			var (element, _) = DoPeekBack();
			return element;
		}

		public TElement PeekFront() {
			Exceptions.InvalidOperation.ThrowIfEmpty(Count);
			var (element, _) = DoPeekFront();
			return element;
		}

#if NET6_0_OR_GREATER
		public void TrimExcess() {
			_frontList.TrimExcess();
			_backList.TrimExcess();
		}
#endif

		public bool TryDequeueBack([MaybeNullWhen(false)] out TElement element, [MaybeNullWhen(false)] out TPriority priority) {
			if( Count > 0 ) {
				(element, priority) = DoDequeueBack();
				return true;
			}
			else {
				element = default;
				priority = default;
				return false;
			}
		}

		public bool TryDequeueFront([MaybeNullWhen(false)] out TElement element, [MaybeNullWhen(false)] out TPriority priority) {
			if( Count > 0 ) {
				(element, priority) = DoDequeueFront();
				return true;
			}
			else {
				element = default;
				priority = default;
				return false;
			}
		}

		public bool TryPeekBack([MaybeNullWhen(false)] out TElement element, [MaybeNullWhen(false)] out TPriority priority) {
			if( Count > 0 ) {
				(element, priority) = DoPeekBack();
				return true;
			}
			else {
				element = default;
				priority = default;
				return false;
			}
		}

		public bool TryPeekFront([MaybeNullWhen(false)] out TElement element, [MaybeNullWhen(false)] out TPriority priority) {
			if( Count > 0 ) {
				(element, priority) = DoPeekFront();
				return true;
			}
			else {
				element = default;
				priority = default;
				return false;
			}
		}

		private (List<(TElement Element, TPriority Priority)> List, int Index) BinarySearch((TElement Element, TPriority Priority) item) {
			if( _frontList.Count > 0 && _backList.Count > 0 ) {
				int comparison = _comparer.Compare(item.Priority, _backList[0].Priority);
				if( comparison < 0 ) {
					return (_frontList, _frontList.BinarySearch(item, CompareByItemFront()));
				}
				else if( comparison > 0 ) {
					return (_backList, _backList.BinarySearch(item, CompareByItemBack()));
				}
				else {
					int index = _frontList.BinarySearch(item,CompareByItemFront());
					return index >= 0
						? (_frontList, index)
						: (_backList, _backList.BinarySearch(item, CompareByItemBack()));
				}
			}
			else if( _frontList.Count > 0 ) {
				return (_frontList, _frontList.BinarySearch(item, CompareByItemFront()));
			}
			else if( _backList.Count > 0 ) {
				return (_backList, _backList.BinarySearch(item, CompareByItemBack()));
			}
			else {
				return (_frontList, -1);
			}
		}

		private Comparer<(TElement Element, TPriority Priority)> CompareByItemBack() {
			return Comparer<(TElement Element, TPriority Priority)>.Create(
				(a, b) => {
					int index = _comparer.Compare(a.Priority, b.Priority);
					if( index < 0 )
						return -1;
					else if( index == 0 && EqualityComparer<TElement>.Default.Equals(a.Element, b.Element) )
						return 0;
					else
						return 1;
				}
			);
		}

		private Comparer<(TElement Element, TPriority Priority)> CompareByItemFront() {
			return Comparer<(TElement Element, TPriority Priority)>.Create(
				(b, a) => {
					int index = _comparer.Compare(a.Priority, b.Priority);
					if( index > 0 )
						return 1;
					else if( index == 0 && EqualityComparer<TElement>.Default.Equals(a.Element, b.Element) )
						return 0;
					else
						return -1;
				}
			);
		}

		private Comparer<(TElement Element, TPriority Priority)> CompareByPriorityBack() {
			return Comparer<(TElement Element, TPriority Priority)>.Create(
				(a, b) => _comparer.Compare(a.Priority, b.Priority)
			);
		}

		private Comparer<(TElement Element, TPriority Priority)> CompareByPriorityFront() {
			return Comparer<(TElement Element, TPriority Priority)>.Create(
				(b, a) => _comparer.Compare(a.Priority, b.Priority)
			);
		}

		private int GetAbsoluteIndex(bool isFront, int relativeIndex) {
			return isFront ? _frontList.Count - relativeIndex - 1 : _frontList.Count + relativeIndex;
		}

		private (TElement Element, TPriority Priority) DoDequeueBack() {
			List<(TElement Element, TPriority Priority)> list;
			int index;
			if( _backList.Count > 0 ) {
				list = _backList;
				index = _backList.Count - 1;
			}
			else {
				list = _frontList;
				index = 0;
			}
			var item = list[index];
			list.RemoveAt(index);
			return item;
		}

		private (TElement Element, TPriority Priority) DoDequeueFront() {
			List<(TElement Element, TPriority Priority)> list;
			int index;
			if( _frontList.Count > 0 ) {
				list = _frontList;
				index = _frontList.Count - 1;
			}
			else {
				list = _backList;
				index = 0;
			}
			var item = list[index];
			list.RemoveAt(index);
			return item;
		}

		private void DoEnqueue((TElement element, TPriority priority) item) {
			var (list, index) = UpperBoundByPriority(item);
			list.Insert(index, item);
		}

		private (TElement Element, TPriority Priority) DoPeekBack() {
			if( _backList.Count > 0 )
				return _backList[^1];
			else
				return _frontList[0];
		}

		private (TElement Element, TPriority Priority) DoPeekFront() {
			if( _frontList.Count > 0 )
				return _frontList[^1];
			else
				return _backList[0];
		}

		private TPriority? GetHighestPriority() {
			if( _frontList.Count > 0 )
				return _frontList[^1].Priority;
			else if( _backList.Count > 0 )
				return _backList[0].Priority;
			else
				return _defaultPriority;
		}

		private TPriority? GetLowestPriority() {
			if( _backList.Count > 0 )
				return _backList[^1].Priority;
			else if( _frontList.Count > 0 )
				return _frontList[0].Priority;
			else
				return _defaultPriority;
		}

		private (List<(TElement Element, TPriority Priority)> List, int Index) GetRelativeLocation(int index) {
			return index < _frontList.Count
				? (_frontList, _frontList.Count - index - 1)
				: (_backList, index - _frontList.Count);
		}

		private (List<(TElement Element, TPriority Priority)> List, int Index) UpperBoundByPriority((TElement Element, TPriority Priority) item) {
			if( _frontList.Count > 0 && _backList.Count > 0 ) {
				return _comparer.Compare(item.Priority, _backList[0].Priority) < 0
					? (_frontList, _frontList.LowerBound(0, _frontList.Count, item, CompareByPriorityFront()))
					: (_backList, _backList.UpperBound(0, _backList.Count, item, CompareByPriorityBack()));
			}
			else if( _frontList.Count > 0 ) {
				int index = _frontList.LowerBound(0, _frontList.Count, item, CompareByPriorityFront());
				return index > 0 ? (_frontList, index) : (_backList, 0);
			}
			else if( _backList.Count > 0 ) {
				int index = _backList.UpperBound(0, _backList.Count, item, CompareByPriorityBack());
				return index > 0 ? (_backList, index) : (_frontList, 0);
			}
			else {
				return (_comparer.Compare(item.Priority, _defaultPriority!) <= 0 ? _frontList : _backList, 0);
			}
		}

		public class ItemList :
			IList<(TElement Element, TPriority Priority)>,
			IReadOnlyList<(TElement Element, TPriority Priority)>,
			ICollection {
			private readonly PriorityDeque<TElement, TPriority> _deque;

			public ItemList(PriorityDeque<TElement, TPriority> deque) {
				_deque = deque;
			}

			public (TElement Element, TPriority Priority) this[int index] {
				get {
					var (list, listIndex) = _deque.GetRelativeLocation(index);
					return list[listIndex];
				}
				set => Exceptions.NotSupported.ThrowIfReadOnly(true);
			}

			public int Count => _deque.Count;

			bool ICollection<(TElement Element, TPriority Priority)>.IsReadOnly => true;

			bool ICollection.IsSynchronized => false;

			object ICollection.SyncRoot => this;

			void IList<(TElement Element, TPriority Priority)>.Insert(int index, (TElement Element, TPriority Priority) item) {
				Exceptions.NotSupported.ThrowIfReadOnly(true);
			}

			void ICollection<(TElement Element, TPriority Priority)>.Add((TElement Element, TPriority Priority) item) {
				Exceptions.NotSupported.ThrowIfReadOnly(true);
			}

			void ICollection<(TElement Element, TPriority Priority)>.Clear() {
				Exceptions.NotSupported.ThrowIfReadOnly(true);
			}

			public bool Contains((TElement Element, TPriority Priority) item) {
				var (_, listIndex) = _deque.BinarySearch(item);
				return listIndex >= 0;
			}

			public void CopyTo((TElement Element, TPriority Priority)[] array, int arrayIndex) {
				Exceptions.ArgumentNull.ThrowIfNull(array, nameof(array));
				Exceptions.ArgumentOutOfRange.ThrowIfIndexOutOfRange(arrayIndex, array.Length, nameof(arrayIndex));
				Exceptions.Argument.ThrowIfCountLessThan(array.Length, arrayIndex + Count, nameof(array));
				int i = 0;
				foreach( var item in GetItems() )
					array.SetValue(item, i++);
			}

			void ICollection.CopyTo(Array array, int index) {
				Exceptions.ArgumentNull.ThrowIfNull(array, nameof(array));
				Exceptions.ArgumentOutOfRange.ThrowIfIndexOutOfRange(index, array.Length, nameof(index));
				Exceptions.Argument.ThrowIfCountLessThan(array.Length, index + Count, nameof(array));
				int i = 0;
				foreach( var item in GetItems() )
					array.SetValue(item, i++);
			}

			public IEnumerator<(TElement Element, TPriority Priority)> GetEnumerator() {
				return GetItems().GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator() {
				return GetItems().GetEnumerator();
			}

			public int IndexOf((TElement Element, TPriority Priority) item) {
				var (list, listIndex) = _deque.BinarySearch(item);
				return _deque.GetAbsoluteIndex(list == _deque._frontList, listIndex);
			}

			bool ICollection<(TElement Element, TPriority Priority)>.Remove((TElement Element, TPriority Priority) item) {
				Exceptions.NotSupported.ThrowIfReadOnly(true);
				return false;
			}

			void IList<(TElement Element, TPriority Priority)>.RemoveAt(int index) {
				Exceptions.NotSupported.ThrowIfReadOnly(true);
			}

			private IEnumerable<(TElement Element, TPriority Priority)> GetItems() {
				for( int i = _deque._frontList.Count - 1; i >= 0; --i )
					yield return _deque._frontList[i];
				for( int i = 0; i < _deque._backList.Count; ++i )
					yield return _deque._backList[i];
			}
		}
	}
}