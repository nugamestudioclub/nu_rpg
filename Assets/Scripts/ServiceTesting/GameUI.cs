using System.Collections.Generic;
using UnityEngine;

namespace NuRpg.ServiceTesting {
	public class GameUI : MonoBehaviour {
		private int _selectedId = -1;

		private readonly Dictionary<int, IUserInterface> _ui = new();


		public void Bind(int id, IUserInterface ui) {
			_ui[id] = ui;
		}

		private void View_Selected(object sender, SelectionEventArgs e) {
			var ui = _ui[e.Id];
			if( e.Id != _selectedId )
				ui.Visible = false;
		}
	}
}