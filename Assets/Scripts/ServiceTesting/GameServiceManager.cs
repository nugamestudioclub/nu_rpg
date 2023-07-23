using NuRpg.Services;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

namespace NuRpg.ServiceTesting {
	public class GameServiceManager : MonoBehaviour {
		private readonly IGameModel _model = new Game();

		[SerializeField]
		private GameView _view;

		private readonly IGameController _controller = new GameController();

		private bool _isLoaded;

		public static GameServiceManager Instance { get; private set; }

		[SerializeField]
		public DoorUI _doorUI;

		public DoorUI DoorUI => _doorUI;

		void Awake() {
			Instance = this;
			_controller.Bind(_model, _view);
		}

		void Update() {
			if( Input.GetKeyDown(KeyCode.Z) && !_isLoaded ) {
				_model.Load();
				_isLoaded = true;
			}
		}
	}
}