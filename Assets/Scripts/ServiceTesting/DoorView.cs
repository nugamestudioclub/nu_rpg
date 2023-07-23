using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NuRpg.ServiceTesting {
	public class DoorView : EntityView, IEntityView {
		private SpriteRenderer _spriteRenderer;
		
		[SerializeField]
		private DoorSpriteSheet _spriteSheet;

		public override int Id { get; set; }

		void Awake() {
			_spriteRenderer = GetComponent<SpriteRenderer>();
		}

		public override void PlayAnimation(string name) {
			if( name == "open" )
				_spriteRenderer.sprite = _spriteSheet.Opened;
			else if( name == "close" )
				_spriteRenderer.sprite = _spriteSheet.Closed;
		}

		void OnMouseDown() {
			OnSelected(new SelectionEventArgs(Id));
		}
	}
}