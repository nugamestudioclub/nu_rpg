#if UNITY_2017_1_OR_NEWER

namespace NuRpg.Environment {
	public static class Unity {
		public static UnityEngine.Vector3 AsUnity(this System.Numerics.Vector3 vector) {
			return new(vector.X, vector.Y, vector.Z);
		}

		public static System.Numerics.Vector3 AsSystem(this UnityEngine.Vector3 vector) {
			return new(vector.x, vector.y, vector.z);
		}
	}
}
#endif