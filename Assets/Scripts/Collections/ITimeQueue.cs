using NuRpg.Units;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NuRpg.Collections {
	public interface ITimeQueue<T> {
		IList<(T Element, int Priority)> PriorityItems { get; }

		IList<(T Element, DateTimeSpan Span)> DelayedItems { get; }

		DateTime Now { get; }

		void Clear();

		void Delay(T element, TimeSpan duration);

		void DelayDays(T element, double days) {
			Delay(element, TimeSpan.FromDays(days));
		}

		void DelayHours(T element, double hours) {
			Delay(element, TimeSpan.FromHours(hours));
		}

		void DelayMilliSeconds(T element, double milliseconds) {
			Delay(element, TimeSpan.FromMilliseconds(milliseconds));
		}

		void DelayMinutes(T element, double minutes) {
			Delay(element, TimeSpan.FromMinutes(minutes));
		}

		void DelaySeconds(T element, double seconds) {
			Delay(element, TimeSpan.FromSeconds(seconds));
		}

		void DelayTicks(T element, long ticks) {
			Delay(element, TimeSpan.FromTicks(ticks));
		}

		T Dequeue();

		void Enqueue(T element);

		void Enqueue(T element, int priority);

		void EnqueueFront(T element);

		void EnqueueBack(T element);

		T PeekBack();

		T PeekFront();

		bool TryDequeueBack([MaybeNullWhen(false)] out T element);

		bool TryDequeueFront([MaybeNullWhen(false)] out T element);

		bool TryPeekBack([MaybeNullWhen(false)] out T element);

		bool TryPeekFront([MaybeNullWhen(false)] out T element);

		void Update(TimeSpan delta);

		void UpdateDays(double days) {
			Update(TimeSpan.FromDays(days));
		}

		void UpdateHours(double hours) {
			Update(TimeSpan.FromHours(hours));
		}

		public void UpdateMilliseconds(double milliseconds) {
			Update(TimeSpan.FromMilliseconds(milliseconds));
		}

		public void UpdateMinutes(double minutes) {
			Update(TimeSpan.FromMinutes(minutes));
		}

		public void UpdateSeconds(double seconds) {
			Update(TimeSpan.FromSeconds(seconds));
		}

		public void UpdateTicks(long ticks) {
			Update(TimeSpan.FromTicks(ticks));
		}
	}
}