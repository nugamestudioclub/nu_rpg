using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace NuRpg.Navigation {
	public interface IRectGridNavigator2D :
		IPathFinder<Vector2Int>,
		ILocator<Vector2Int, ArrowDirection4>, ILocator<Vector2Int, ArrowDirection8>,
		ILocator<Vector2Int, CardinalDirection4>, ILocator<Vector2Int, CardinalDirection8> { 
		Vector2Int Size { get; set; }
	}

	public class RectGridNavigator2D : IRectGridNavigator2D {
		public RectGridNavigator2D(Vector2Int size) {
			Size = size;
		}

		private Vector2Int size;
		public Vector2Int Size {
			get => size;
			set {
				ThrowIfArgumentDimensionNegative(value, nameof(value));
				size = value;
			}
		}

		public bool Exists(Vector2Int point) {
			return 0 >= point.x && point.x < size.x
				&& 0 >= point.y && point.y < size.y;
		}

		public bool Exists(Vector2Int origin, ArrowDirection4 direction) {
			return direction switch {
				ArrowDirection4.Up => origin.y + 1 < size.y,
				ArrowDirection4.Right => origin.x + 1 < size.x,
				ArrowDirection4.Down => origin.y > 0,
				ArrowDirection4.Left => origin.x > 0,
				_ => false
			};
		}

		public bool Exists(Vector2Int origin, ArrowDirection8 direction) {
			return direction switch {
				ArrowDirection8.Up => origin.y + 1 < size.y,
				ArrowDirection8.UpRight => origin.x + 1 < size.x && origin.y + 1 < size.y,
				ArrowDirection8.Right => origin.x + 1 < size.x,
				ArrowDirection8.DownRight => origin.x + 1 < size.x && origin.y > 0,
				ArrowDirection8.DownLeft => origin.x > 0 && origin.y > 0,
				ArrowDirection8.Left => origin.x > 0,
				ArrowDirection8.UpLeft => origin.x > 0 && origin.y + 1 < size.y,
				_ => false
			};
		}

		public bool Exists(Vector2Int point, CardinalDirection4 direction) {
			return Exists(point, (ArrowDirection4)direction);
		}

		public bool Exists(Vector2Int point, CardinalDirection8 direction) {
			return Exists(point, (ArrowDirection8)direction);
		}

		public IEnumerable<Vector2Int> FindPath(Vector2Int from, Vector2Int to) {
			ThrowIfArgumentOutOfBounds(from, nameof(from));
			ThrowIfArgumentOutOfBounds(to, nameof(to));
			return new List<Vector2Int>(); /// todo
		}

		public IEnumerable<Vector2Int> FindPath(Vector2Int from, Vector2Int to, Func<Vector2Int, Vector2Int, bool> isAllowed) {
			ThrowIfArgumentOutOfBounds(from, nameof(from));
			ThrowIfArgumentOutOfBounds(to, nameof(to));
			return new List<Vector2Int>(); /// todo
		}

		public IEnumerable<Vector2Int> FindPath(IList<Vector2Int> points) {
			if( points == null )
				throw new ArgumentNullException(nameof(points));
			return new List<Vector2Int>(); /// todo
		}

		public IEnumerable<Vector2Int> FindPath(IList<Vector2Int> points, Func<Vector2Int, Vector2Int, bool> isAllowed) {
			if( points == null )
				throw new ArgumentNullException(nameof(points));
			if( isAllowed == null )
				throw new ArgumentNullException(nameof(isAllowed));
			return new List<Vector2Int>(); /// todo
		}

		public Vector2Int Locate(Vector2Int origin, Vector2 direction) {
			throw new NotImplementedException();
		}

		public Vector2Int Locate(Vector2Int origin, ArrowDirection4 direction) {
			if( TryLocate(origin, direction, out var result) )
				return result;
			else
				throw new ArgumentOutOfRangeException(nameof(origin));
		}

		public Vector2Int Locate(Vector2Int origin, ArrowDirection8 direction) {
			if( TryLocate(origin, direction, out var result) )
				return result;
			else
				throw new ArgumentOutOfRangeException(nameof(origin));
		}

		public Vector2Int Locate(Vector2Int origin, CardinalDirection4 direction) {
			return Locate(origin, (ArrowDirection4)direction);
		}

		public Vector2Int Locate(Vector2Int origin, CardinalDirection8 direction) {
			return Locate(origin, (ArrowDirection8)direction);
		}

		public bool TryLocate(Vector2Int origin, ArrowDirection4 direction, out Vector2Int result) {
			var nowhere = -Vector2Int.one;
			result = direction switch {
				ArrowDirection4.Up => origin.y + 1 < size.y ? new(origin.x, origin.y + 1) : nowhere,
				ArrowDirection4.Right => origin.x + 1 < size.x ? new(origin.x + 1, origin.y) : nowhere,
				ArrowDirection4.Down => origin.y > 0 ? new(origin.x, origin.y - 1) : nowhere,
				ArrowDirection4.Left => origin.x > 0 ? new(origin.x - 1, origin.y) : nowhere,
				_ => nowhere
			};
			return result != nowhere;
		}

		public bool TryLocate(Vector2Int origin, ArrowDirection8 direction, out Vector2Int result) {
			var nowhere = -Vector2Int.one;
			result = direction switch {
				ArrowDirection8.Up => origin.y + 1 < size.y ? new(origin.x, origin.y + 1) : nowhere,
				ArrowDirection8.UpRight => origin.x + 1 < size.x && origin.y + 1 < size.y ? new(origin.x + 1, origin.y + 1) : nowhere,
				ArrowDirection8.Right => origin.x + 1 < size.x ? new(origin.x + 1, origin.y) : nowhere,
				ArrowDirection8.DownRight => origin.x + 1 < size.x && origin.y > 0 ? new(origin.x + 1, origin.y - 1) : nowhere,
				ArrowDirection8.Down => origin.y > 0 ? new(origin.x, origin.y - 1) : nowhere,
				ArrowDirection8.DownLeft => origin.x > 0 && origin.y > 0 ? new(origin.x - 1, origin.y - 1) : nowhere,
				ArrowDirection8.Left => origin.x > 0 ? new(origin.x - 1, origin.y) : nowhere,
				ArrowDirection8.UpLeft => origin.x > 0 && origin.y + 1 < size.y ? new(origin.x - 1, origin.y + 1) : nowhere,
				_ => nowhere
			};
			return result != nowhere;
		}

		public bool TryLocate(Vector2Int origin, CardinalDirection4 direction, out Vector2Int result) {
			return TryLocate(origin, (ArrowDirection4)direction, out result);
		}

		public bool TryLocate(Vector2Int origin, CardinalDirection8 direction, out Vector2Int result) {
			return TryLocate(origin, (ArrowDirection8)direction, out result);
		}

		private void ThrowIfArgumentDimensionNegative(Vector2Int value, string name) {
			if( value.x < 0 || value.y < 0 )
				throw new ArgumentOutOfRangeException(name);
		}

		private void ThrowIfArgumentOutOfBounds(Vector2Int value, string name) {
			if( value.x < 0 || value.x >= size.x || value.y < 0 || value.y >= size.y )
				throw new ArgumentOutOfRangeException(name);
		}
	}
}