using System;

namespace NuRpg.Units {
	public readonly struct ElapsedTime<T> :
		IEquatable<ElapsedTime<T>>
		where T : IComparable<T>, IEquatable<T> {
		private readonly T _start;
		private readonly T _duration;

		public ElapsedTime(T start, T duration) {
			ThrowIfTimeNegative(start, nameof(start));
			ThrowIfTimeNegative(duration, nameof(duration));
			_start = start;
			_duration = duration;
		}

		public T Start => _start;

		public T Duration => _duration;

		public T End {
			get {
				dynamic startTime = _start;
				dynamic delay = _duration;
				return startTime + delay;
			}
		}

		public static bool operator ==(ElapsedTime<T> left, ElapsedTime<T> right) {
			return left.Equals(right);
		}

		public static bool operator !=(ElapsedTime<T> left, ElapsedTime<T> ight) {
			return !left.Equals(ight);
		}

		public bool Equals(ElapsedTime<T> other) {
			return _start.Equals(other.Start) && _duration.Equals(other.Duration);
		}

		public override bool Equals(object other) {
			if( other is ElapsedTime<T> elapsedTime )
				return Equals(elapsedTime);
			else
				return false;
		}

		public override int GetHashCode() {
			return _start.GetHashCode() ^ _duration.GetHashCode();
		}

		private static void ThrowIfTimeNegative(T time, string paramName) {
			if( time.CompareTo(default) < 0 )
				throw new ArgumentOutOfRangeException(paramName);
		}
	}
}