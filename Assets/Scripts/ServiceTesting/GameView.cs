using NuRpg.Collections;
using NuRpg.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace NuRpg.ServiceTesting {
	public class GameView : MonoBehaviour, IGameView {
		[SerializeField]
		private DoorView _door;

		private readonly Dictionary<int, IEntityView> _views = new();

		private int _currentSelection = -1;

		public event EventHandler<CreateViewEventArgs> Creating;
		public event EventHandler<ReadViewEventArgs> Reading;
		public event EventHandler<UpdateViewEventArgs> Updating;
		public event EventHandler<DeleteViewEventArgs> Deleting;

		public void Create(int id, IReadOnlyBlackboard data) {
			if( data.TryGetValue<string>("resource", out var resource) && resource == "door" ) {
				var door = Instantiate(_door);
				var position = data.GetValue<System.Numerics.Vector3>("position");
				door.transform.position = new Vector3(position.X, position.Y, position.Z);
				_views[id] = door;
				door.Selected += EntityView_Selected;
			}
			else if( data.TryGetValue<string>("type", out var type) && type == "ui" ) {
				var doorUI = data.GetValue<DoorUI>("ui");
				var actions = data.GetValue<List<(string Action, int Status)>>("actions");
				foreach( var (action, status) in actions )
					doorUI.Add(action, status);
				var doorView = (DoorView)_views[id];
				doorUI.Position = new System.Numerics.Vector3(
					doorView.transform.position.x,
					doorView.transform.position.y,
					doorView.transform.position.z
				);
				doorUI.Visible = true;
			}
		}

		protected virtual void OnCreate(CreateViewEventArgs e) {
			Creating?.Invoke(this, e);
		}

		protected virtual void OnRead(ReadViewEventArgs e) {
			Reading?.Invoke(this, e);
		}

		protected virtual void OnUpdate(UpdateViewEventArgs e) {
			Updating?.Invoke(this, e);
		}

		protected virtual void OnDelete(DeleteViewEventArgs e) {
			Deleting?.Invoke(this, e);
		}

		private void EntityView_Selected(object sender, SelectionEventArgs e) {
			int selection = e.Id;
			if( selection == _currentSelection )
				return;
			if( _views[selection] is DoorView doorView ) {
				var data = new Blackboard();
				data.SetValue("type", "ui");
				data.SetValue("actor", int.MaxValue);
				data.SetValue("ui", GameServiceManager.Instance.DoorUI);
				data.SetValue("view", selection);
				var args = new CreateViewEventArgs(e.Id, data);
				OnCreate(args);
			}
			_currentSelection = selection;
		}
	}
}