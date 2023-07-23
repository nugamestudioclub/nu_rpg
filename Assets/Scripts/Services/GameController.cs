using NuRpg.Collections;
using NuRpg.ServiceTesting;
using System.Collections.Generic;
using System.Linq;

namespace NuRpg.Services {
	public class GameController : IGameController {
		private IGameModel _model;
		
		private IGameView _view;

		public void Bind(IGameModel model, IGameView view) {
			_model = model;
			model.Creating += GameModel_Creating;
			model.Reading += GameModel_Reading;
			model.Updating += GameModel_Updating;
			model.Deleting += GameModel_Deleting;

			_view = view;
			view.Creating += GameView_Creating;
			view.Reading += GameView_Reading;
			view.Updating += GameView_Updating;
			view.Deleting += GameView_Deleting;
		}

		private void GameModel_Creating(object sender, CreateModelEventArgs e) {
			_view.Create(e.Id, e.Data);
		}

		private void GameModel_Reading(object sender, ReadModelEventArgs e) {

		}

		private void GameModel_Updating(object sender, UpdateModelEventArgs e) {

		}

		private void GameModel_Deleting(object sender, DeleteModelEventArgs e) {

		}

		private void GameView_Creating(object sender, CreateViewEventArgs e) {
			if( e.Data.TryGetValue<string>("type", out var type) && type == "ui" ) {
				var actions = _model.GetActions(e.Data.GetValue<int>("actor"), e.Id).ToList();
				var response = new Blackboard();
				response.SetValue("type", "ui");
				response.SetValue("actions", actions);
				response.SetValue("ui", e.Data.GetValue<DoorUI>("ui"));
				_view.Create(e.Id, response);
			}
		}

		private void GameView_Reading(object sender, ReadViewEventArgs e) {

		}

		private void GameView_Updating(object sender, UpdateViewEventArgs e) {

		}

		private void GameView_Deleting(object sender, DeleteViewEventArgs e) {

		}
	}
}