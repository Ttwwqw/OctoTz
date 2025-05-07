
using System;
using Naninovel;

[CommandAlias("Location")]
public class LocationTriggerCmd : Command {

	[ParameterAlias(NamelessParameterAlias)]
	public StringParameter LocationName;

	[ParameterAlias("isOpen")]
	public BooleanParameter isOpen;

	[ParameterAlias("addMark")] /// separate couple marks with ','
	public StringParameter markToAdd;

	[ParameterAlias("removeMark")] /// separate couple marks with ','
	public StringParameter markToRemove;


	public override UniTask ExecuteAsync(AsyncToken asyncToken = default) {

		var manager = Managers.GetManager<LocationsManager>();

		if (isOpen.HasValue) {
			manager.ChangeLocationStatus(LocationName.Value, isOpen.Value);
		}

		if (markToAdd.HasValue) {
			manager.ChangeLocationMark(LocationName.Value, true, markToAdd.Value.Split(',', StringSplitOptions.RemoveEmptyEntries));
		}

		if (markToRemove.HasValue) {
			manager.ChangeLocationMark(LocationName.Value, false, markToRemove.Value.Split(',', StringSplitOptions.RemoveEmptyEntries));
		}

		return new UniTask();

	}

}

/// Add init location dialog
[CommandAlias("Location_Event_EnterDialog")]
public class AddLocationDialogEventCmd : Command {

	[ParameterAlias(NamelessParameterAlias)]
	public StringParameter LocationName;

	[ParameterAlias("dialog"), RequiredParameter]
	public StringParameter dialogName;

	public override UniTask ExecuteAsync(AsyncToken asyncToken = default) {
		Managers.GetManager<LocationsManager>().AddLocationEvent(LocationName.Value, new StartDialogEvent(dialogName));
		return new UniTask();
	}

}
