
using UnityEngine;

public class ActorObject : Interacteble {
	
	[SerializeField] private Actor _actorSet;

	private ActorInfo _actorInfo;
	private ActorsManager _actorsManager;

	private void Awake() {
		Managers.SubscribeOnManagerCreated<ActorsManager>((m) => {
			_actorsManager = m as ActorsManager;
			_actorsManager.ActorUpdated += OnActorStateChanged;
			OnActorStateChanged(_actorsManager.GetActorInfo(_actorSet.Id));
		});
	}

	private void OnDestroy() {
		Managers.GetManager<ActorsManager>().ActorUpdated -= OnActorStateChanged;
	}

	/// [TODO] - runtime handlers
	private void OnActorStateChanged(ActorInfo info) {

		if (info.actorId == _actorSet.Id) {
			_actorInfo = info;

			if (_actorInfo.currentLocation != Scenes.Current.name) {
				Debug.Log(string.Format("Location: {0} Scene: {1}", _actorInfo.currentLocation, Scenes.Current.name));
				gameObject.SetActive(false);
			}

		}

	}

	public override void Activate() {

		if (_actorInfo.avaibleDialogs.Count > 0) {

			var dialog = _actorInfo.avaibleDialogs[0];

			if (!dialog.repeatable) {
				_actorsManager.RemoveDialogOption(_actorInfo.actorId, dialog.dialogName);
			}

			Managers.GetManager<NaninovelManager>().StartNaninovelScript(dialog.dialogName, dialog.startlabel);

		} else {

			if (_actorInfo.speeches.Count > 0) {

				Managers.GetManager<TooltipsManager>().ShowTooltipHint(this, _actorInfo.speeches[Random.Range(0, _actorInfo.speeches.Count)]);

			} else {

				Managers.GetManager<TooltipsManager>().ShowTooltipHint(this, "...");

			}

		}

		base.Activate();

	}

}
