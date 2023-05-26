using NuRrpg;
using UnityEngine;

namespace NuRpg {
	public class GameManager : MonoBehaviour {
		public static GameManager Instance { get; private set; }

		[SerializeField]
		private GridController gridController;

		[SerializeField]
		private PlayerController playerController;

		void Awake() {
			if( Instance == null ) {
				Instance = this;
				DontDestroyOnLoad(this);
				Initialize();
			}
			else {
				Destroy(this);
			}
		}

		private void Initialize() {
			gridController.Fill();
		}

		void Update() {
			HandleInput();
		}

		private void HandleInput() {
			Vector2 input = Vector2.zero;

			if( Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) ) {
				input = Vector2.up;
			}
			else if( Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) ) {
				input = Vector2.left;
			}
			else if( Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) ) {
				input = Vector2.down;
			}
			else if( Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) ) {
				input = Vector2.right;
			}

			playerController.HandleInput(input);
		}
	}
}