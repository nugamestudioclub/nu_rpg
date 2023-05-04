using System;
using System.Collections.Generic;
using UnityEngine;

namespace NuRpg.Navigation {
	public class RectGridNavigator2D : IPathFinder<Vector2Int> {
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