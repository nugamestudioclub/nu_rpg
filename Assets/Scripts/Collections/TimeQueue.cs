using NuRpg.Units;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace NuRpg.Collections {
	public class TimeQueue<T> : ITimeQueue<T> {
		private readonly DelayQueue<T> _delayQueue;
		private readonly ReadOnlyCollection<(T Element, DateTimeSpan Span)> _delayedItems;
		private readonly PriorityDeque<T, int> _priorityQueue;
		private readonly ReadOnlyCollection<(T Element, int Priority)> _priorityItems;

		public TimeQueue(DateTime time) {
			_delayQueue = new(time);
			_priorityQueue = new();
			_priorityItems = new(_priorityQueue.Items);
		}

		public ReadOnlyCollection<(T Element, DateTimeSpan Span)> DelayedItems => _delayedItems;
		IList<(T Element, DateTimeSpan Span)> ITimeQueue<T>.DelayedItems => _delayedItems;

		public ReadOnlyCollection<(T Element, int Priority)> PriorityItems => _priorityItems;
		IList<(T Element, int Priority)> ITimeQueue<T>.PriorityItems => _priorityItems;

		public DateTime Now => _delayQueue.Now;

		public void Clear() {
			_delayQueue.Clear();
			_priorityQueue.Clear();
		}

		public void Delay(T element, TimeSpan duration) {
			_delayQueue.Enqueue(element, duration);
		}

		public T Dequeue() {
			return _priorityQueue.DequeueFront();
		}

		public void Enqueue(T element) {
			_priorityQueue.Enqueue(element);
		}

		public void Enqueue(T element, int priority) {
			_priorityQueue.Enqueue(element, priority);
		}

		public void EnqueueBack(T element) {
			_priorityQueue.EnqueueBack(element);
		}

		public void EnqueueFront(T element) {
			_priorityQueue.EnqueueFront(element);
		}

		public T PeekFront() {
			return _priorityQueue.PeekFront();
		}

		public T PeekBack() {
			return _priorityQueue.PeekFront();
		}

		public bool TryDequeueBack([MaybeNullWhen(false)] out T element) {
			return _priorityQueue.TryDequeueBack(out element, out _);
		}

		public bool TryDequeueFront([MaybeNullWhen(false)] out T element) {
			return _priorityQueue.TryDequeueFront(out element, out _);
		}

		public bool TryPeekBack([MaybeNullWhen(false)] out T element) {
			return _priorityQueue.TryPeekBack(out element, out _);
		}

		public bool TryPeekFront([MaybeNullWhen(false)] out T element) {
			return _priorityQueue.TryPeekFront(out element, out _);
		}

		public void Update(TimeSpan delta) {
			_delayQueue.Update(delta);
			while( _delayQueue.Ready )
				Enqueue(_delayQueue.Dequeue());
		}
	}
}