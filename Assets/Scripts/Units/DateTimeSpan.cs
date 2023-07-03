using System;

namespace NuRpg.Units {
	public readonly struct DateTimeSpan :
		IEquatable<DateTimeSpan> {
		private readonly DateTime _start;
		private readonly DateTime _end;
		
		public DateTimeSpan(DateTime start, DateTime end) {
			_start = start;
			_end = end;
		}

		public DateTimeSpan(DateTime start, TimeSpan duration) {
			_start = start;
			_end = start + duration;
		}

		public DateTime Start => _start;

		public DateTime End => _end;

		public TimeSpan Elapsed => _end - _start;

		public static bool operator ==(DateTimeSpan left, DateTimeSpan right) {
			return left.Equals(right);
		}

		public static bool operator !=(DateTimeSpan left, DateTimeSpan right) {
			return !left.Equals(right);
		}

		public bool Equals(DateTimeSpan other) {
			return _start == other.Start && _end == other.End;
		}

		public override bool Equals(object obj) {
			return obj is DateTimeSpan other && Equals(other);
		}

		public override int GetHashCode() {
			return _start.GetHashCode() ^ _end.GetHashCode();
		}
	}
}