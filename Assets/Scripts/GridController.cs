using System;
using NuRpg.Navigation;
using UnityEngine;


namespace NuRpg {
	internal class GridController : MonoBehaviour {
		private Grid grid;

		public Grid2D<GameObject> Model { get; private set; }

		[SerializeField]
		private Vector2Int size;

		[SerializeField]
		private GameObject block;

		public void Fill() {
			grid = GetComponent<Grid>();
			Model = new(size.x, size.y);
			var random = new System.Random();
			for( int x = 0; x < Model.SizeX; ++x ) {
				for( int y = 0; y < Model.SizeY; ++y ) {
					if( random.Next(4) == 0 ) {
						var obj = Instantiate(block);
						obj.transform.position = grid.CellToWorld(new(x, y));
					}
				}
			}
		}
	}
}