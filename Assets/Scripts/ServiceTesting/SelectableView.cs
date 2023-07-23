using System;
using System.Numerics;
using UnityEngine;

namespace NuRpg.ServiceTesting {
	public abstract class EntityView : MonoBehaviour, IEntityView {
		public abstract int Id { get; set; }

		public event EventHandler<SelectionEventArgs> Selected;

		public abstract void PlayAnimation(string name);

		protected virtual void OnSelected(SelectionEventArgs e) {
			Selected?.Invoke(this, e);
		}
	}
}