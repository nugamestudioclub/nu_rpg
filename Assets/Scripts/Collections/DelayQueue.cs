using NuRpg.Units;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace NuRpg.Collections {
	public class DelayQueue<T> {
		private DateTime _now;
		private readonly PriorityDeque<T, DateTimeSpan> _queue;
		private readonly ReadOnlyCollection<(T Element, DateTimeSpan Span)> _items;

		public DelayQueue(DateTime time) {
			_now = time;
			_queue = new(CreateComparer());
			_items = new(_queue.Items);
		}

		public DelayQueue(DateTime time, IEnumerable<(T Element, DateTimeSpan Span)> items) {
			_now = time;
			_queue = new(items, CreateComparer());
			_items = new(_queue.Items);
		}

		public DelayQueue(DateTime time, int capacity) {
			_now = time;
			_queue = new(capacity, CreateComparer());
			_items = new(_queue.Items);
		}

		public int Count => _queue.Count;

		public bool Ready => _queue.TryPeekFront(out _, out var span) && span.End <= _now;

		public DateTime Now => _now;

		public ReadOnlyCollection<(T Element, DateTimeSpan Span)> Items => _items;

		public void Clear() {
			_queue.Clear();
		}

		public T Dequeue() {
			if( TryDequeue(out var element, out _) ) {
				return element;
			}
			else {
				ThrowNotReady();
				return default;
			}
		}

		public void Enqueue(T element, DateTimeSpan span) {
			_queue.Enqueue(element, span);
		}
		
		public void Enqueue(T element, TimeSpan duration) {
			Enqueue(element, new DateTimeSpan(_now, _now + duration));
		}

		public T Peek() {
			if( TryPeek(out var element, out _) ) {
				return element;
			}
			else {
				ThrowNotReady();
				return default;
			}
		}

		public bool TryDequeue([MaybeNullWhen(false)] out T element, out DateTimeSpan span) {
			if( _queue.TryDequeueFront(out element, out span) ) {
				return true;
			}
			else {
				element = default;
				span = default;
				return false;
			}
		}

		public bool TryPeek([MaybeNullWhen(false)] out T result, out DateTimeSpan span) {
			if( _queue.TryPeekFront(out result, out span) && span.End <= _now ) {
				return true;
			}
			else {
				result = default;
				span = default;
				return false;
			}
		}

		public void Update(TimeSpan delta) {
			_now += delta;
		}

		private static IComparer<DateTimeSpan> CreateComparer() {
			return Comparer<DateTimeSpan>.Create((a, b) => a.End.CompareTo(b.End));
		}

		[DoesNotReturn]
		private static void ThrowNotReady() {
			throw new InvalidOperationException("The queue is not ready for this operation.");
		}

		/*
		private readonly List<(T Element, DateTimeSpan Span)?> _nodes;

		private static int FindLeftChild(int index) {
			return (2 * index) + 1;
		}

		private static int FindParent(int index) {
			return (index - 1) / 2;
		}

		private static int FindRightChild(int index) {
			return (2 * index) + 2;
		}

		private int CompareAt(int index1, int index2) {
			return _nodes[index1].Value.Span.End.CompareTo(_nodes[index2].Value.Span.End);
		}

		private int FindMin(int parentIndex) {
			int leftChildIndex = FindLeftChild(parentIndex);
			int rightChildIndex = FindRightChild(parentIndex);
			if( _nodes[leftChildIndex].HasValue && _nodes[rightChildIndex].HasValue )
				return FindMin(FindMin(leftChildIndex, rightChildIndex), parentIndex);
			else if( _nodes[leftChildIndex].HasValue )
				return FindMin(leftChildIndex, parentIndex);
			else if( _nodes[rightChildIndex].HasValue )
				return FindMin(rightChildIndex, parentIndex);
			else
				return parentIndex;
		}

		private int FindMin(int index1, int index2) {
			return CompareAt(index1, index2) < 0 ? index1 : index2;
		}

		private void PullUp(int index) {
			int parentIndex = FindParent(index);
			if( CompareAt(index, parentIndex) < 0) {
				Swap(index, parentIndex);
				PullUp(parentIndex);
			}
		}

		private void PushDown(int index) {
			int minIndex = FindMin(index);
			if( index != minIndex ) {
				Swap(index, minIndex);
				PushDown(minIndex);
			}
		}


		private void Swap(int index1, int index2) {
			(_nodes[index1], _nodes[index2]) = (_nodes[index1], _nodes[index2]);
		}
		*/

	}
}