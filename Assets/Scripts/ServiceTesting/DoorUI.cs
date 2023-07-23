using UnityEngine;

namespace NuRpg.ServiceTesting {
	public abstract class EntityUI : MonoBehaviour, IUserInterface {
		public abstract bool Visible { get; set; }
		public abstract System.Numerics.Vector3 Position { get; set; }
	}

	public class DoorUI : EntityUI {
		// private RectTransform _rectTransform;

		[SerializeField]
		DoorUIOption _option;

		private bool _visible;

		void Awake() {
			// _rectTransform =GetComponentInParent<RectTransform>();
			Visible = false;
		}

		public override bool Visible {
			get => _visible;
			set {
				_visible = value;
				gameObject.SetActive(value);
			}
		}

		public void Add(string action, int status) {
			var option = Instantiate(_option, transform);
			option.Load(action, status);
		}

		public override System.Numerics.Vector3 Position {
			get => new(transform.position.x, transform.position.y, transform.position.z);
			set => transform.position = new(value.X, value.Y + 1.5f, value.Z);
			/*
			get => new(
					transform.position.x * _rectTransform.localScale.x,
					transform.position.y * _rectTransform.localScale.y,
					transform.position.z * _rectTransform.localScale.z
				);
			set {
				transform.position = new(
					value.X / _rectTransform.localScale.x,
					(value.Y + 1) / _rectTransform.localScale.y,
					value.Z / _rectTransform.localScale.z
				);
			}
			*/
		}
	}
}