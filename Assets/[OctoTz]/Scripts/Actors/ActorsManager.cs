
using System;
using System.Linq;
using System.Collections.Generic;

[Serializable]
public struct ActorInfo {
	public string actorId;
	public string currentLocation;
	public int friendship;
	public List<string> marks;
	public List<string> speeches;
	public List<DialogOption> avaibleDialogs;
}

[Serializable]
public struct DialogOption {
	public string dialogName;
	public bool repeatable;
	public string startlabel;
}

public class ActorsManager : Manager {

	public ActorsManager(ICollectionsLoader loader) {
		_actorsCollection = loader.Load<ActorsCollection>();
	}

	public event Action<ActorInfo> ActorUpdated;

	private ActorsCollection _actorsCollection;
	private List<ActorInfo> _loadedActors = new List<ActorInfo>();

	public ActorInfo GetActorInfo(string actor) {
		return LoadActor(actor);
	}

	public ActorInfo GetActorInfo(Actor actor) {
		return LoadActor(actor.Id);
	}

	public ActorObject GetActorObject(string actor) {
		LoadActor(actor);
		return _actorsCollection.GetItem(actor)?.SceneObject;
	}

	public void SetCurrentLocation(string actor, string location) {
		ActorInfo a = LoadActor(actor);
		a.currentLocation = location;
		SaveActor(a);
	}

	public void AddDialogOption(string actor, DialogOption dialog) {
		ActorInfo a = LoadActor(actor);
		if (!a.avaibleDialogs.ContaitsWhere(x => x.dialogName == dialog.dialogName)) {
			a.avaibleDialogs.Add(dialog);
			SaveActor(a);
		}
	}

	public void RemoveDialogOption(string actor, string dialogs) {
		ActorInfo a = LoadActor(actor);
		a.avaibleDialogs.RemoveAllWhere((x) => x.dialogName == dialogs);
		SaveActor(a);
	}

	public void ChangeMarks(string actor, bool add, params string[] marks) {
		ActorInfo a = LoadActor(actor);
		if (add) {
			a.marks.AddWithoutDoubles(marks);
		} else {
			a.marks.RemoveAllWhere((x) => x.IsOneOf(marks));
		}
		SaveActor(a);
	}

	public void ReplaceSpeeches(string actor, params string[] speeches) {
		ActorInfo a = LoadActor(actor);
		a.speeches = speeches.ToList();
		SaveActor(a);
	}

	#region Save\load
	// TODO - save brige?
	private ActorInfo LoadActor(string actor) {

		if (_loadedActors.ContaitsWhere(x => x.actorId == actor, out var q)) {
			return q;
		}

		_loadedActors.Add(Copy(_actorsCollection.GetItem(actor).DefaultSettings));
		return _loadedActors[^1];

		/// [TODO] Editor issue
		ActorInfo Copy(ActorInfo original) {

			return new ActorInfo() {
				actorId = original.actorId,
				currentLocation = original.currentLocation,
				friendship = original.friendship,
				marks = new(original.marks),
				speeches = new(original.speeches),
				avaibleDialogs = new(original.avaibleDialogs)
			};

		}

	}

	private void SaveActor(ActorInfo actorInfo) {

		_loadedActors[_loadedActors.GetIndexOf((x) => x.actorId == actorInfo.actorId)] = actorInfo;
		ActorUpdated?.Invoke(actorInfo);

	}

	#endregion

}
