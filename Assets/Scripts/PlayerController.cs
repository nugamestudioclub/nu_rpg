using NuRpg.Navigation;
using UnityEngine;

namespace NuRrpg {
	public class PlayerController : MonoBehaviour {
		public void HandleInput(Vector2 input) {
			transform.Translate(input);
			///
		}
	}
}