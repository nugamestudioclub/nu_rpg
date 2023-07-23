using NuRpg.Collections;
using NuRpg.Services;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace NuRpg.ServiceTesting {
	public class Game : IGameModel {
		private readonly List<object> _objects = new();

		public event EventHandler<CreateModelEventArgs> Creating;
		public event EventHandler<ReadModelEventArgs> Reading;
		public event EventHandler<UpdateModelEventArgs> Updating;
		public event EventHandler<DeleteModelEventArgs> Deleting;

		public IEnumerable<(string Action, int Status)> GetActions(int actor, int target) {
			if( _objects[target] is Door ) {
				yield return ("Open", 1);
				yield return ("Attack", 1);
				yield return ("Pick Lock", 0);
			}
		}

		public void Load() {
			int id = _objects.Count;
			var position = new Vector3(4f, 2f, 0f);
			var door = new Door(position, Quaternion.Identity);

			var data = new Blackboard();
			data.SetValue("resource", "door");
			data.SetValue("position", position);

			OnCreate(new(id, data));
			_objects.Add(door);
		}

		protected virtual void OnCreate(CreateModelEventArgs e) {
			Creating?.Invoke(this, e);
		}

		protected virtual void OnRead(ReadModelEventArgs e) {
			Reading?.Invoke(this, e);
		}

		protected virtual void OnUpdate(UpdateModelEventArgs e) {
			Updating?.Invoke(this, e);
		}

		protected virtual void OnDelete(DeleteModelEventArgs e) {
			Deleting?.Invoke(this, e);
		}
	}
}