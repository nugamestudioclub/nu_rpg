using System.Numerics;

namespace NuRpg.ServiceTesting {
	public class Door {
		private readonly Vector3 _position;
		private readonly Quaternion _rotation;

		public Door(Vector3 position, Quaternion rotation) {
			_position = position;
			_rotation = rotation;
		}

		public Vector3 Position => _position;

		public Quaternion Rotation => _rotation;
	}
}