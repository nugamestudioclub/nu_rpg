using NuRpg.Units;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NuRpg.Collections {
	public class TimeQueue<T> : ITimeQueue<T> {
		private readonly DelayQueue<T> _delayQueue;
		private readonly PriorityQueue<T, int> _priorityQueue;

		public TimeQueue(DateTime time) {
			_delayQueue = new(time);
			_priorityQueue = new();
		}

		// TODO: Don't copy
		public IList<(T Element, DateTimeSpan Span)> DelayedItems => _delayQueue.UnorderedItems.ToList();

		// TODO: Don't copy
		public IList<(T Element, int Priority)> PriorityItems => _priorityQueue.UnorderedItems.ToList();

		public DateTime Time => _delayQueue.Time;

		public void Clear() {
			_delayQueue.Clear();
			_priorityQueue.Clear();
		}

		public void Delay(T element, TimeSpan duration) {
			_delayQueue.Enqueue(element, duration);
		}

		public T Dequeue() {
			return _priorityQueue.Dequeue();
		}

		public void Enqueue(T element) {
			_priorityQueue.Enqueue(element, priority: 0);
		}

		public void Enqueue(T element, int priority) {
			_priorityQueue.Enqueue(element, priority);
		}

		public void EnqueueBack(T element) {
			throw new NotImplementedException();
		}

		public void EnqueueFront(T element) {
			throw new NotImplementedException();
		}

		public T Peek() {
			return _priorityQueue.Peek();
		}

		public bool TryDequeue(out T element) {
			return _priorityQueue.TryDequeue(out element, out _);
		}

		public bool TryPeek(out T element) {
			return _priorityQueue.TryPeek(out element, out _);
		}

		public void Update(TimeSpan delta) {
			_delayQueue.Update(delta);
			while( _delayQueue.Ready )
				Enqueue(_delayQueue.Dequeue());
		}
	}
}