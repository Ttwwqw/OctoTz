
using System;
using Naninovel;

[CommandAlias("ActorInfo")]
public class ActorInfoCmdEditing : Command {

	[ParameterAlias(NamelessParameterAlias)]
	public StringParameter ActorName;

	[ParameterAlias("location")]
	public StringParameter location;

	[ParameterAlias("speeches")] /// separete couple speeches witn ','
	public StringParameter speeches;

	[ParameterAlias("addMark")] /// separate couple marks with ','
	public StringParameter markToAdd;

	[ParameterAlias("removeMark")] /// separate couple marks with ','
	public StringParameter markToRemove;

	public override UniTask ExecuteAsync(AsyncToken asyncToken = default) {

		var actorsManager = Managers.GetManager<ActorsManager>();

		if (location.HasValue) {
			actorsManager.SetCurrentLocation(ActorName.Value, location.Value);
		}

		if (speeches.HasValue) {
			actorsManager.ReplaceSpeeches(ActorName.Value, speeches.Value.Split(',', StringSplitOptions.RemoveEmptyEntries));
		}

		if (markToAdd.HasValue) {
			actorsManager.ChangeMarks(ActorName.Value, true, markToAdd.Value.Split(',', StringSplitOptions.RemoveEmptyEntries));
		}

		if (markToRemove.HasValue) {
			actorsManager.ChangeMarks(ActorName.Value, false, markToRemove.Value.Split(',', StringSplitOptions.RemoveEmptyEntries));
		}

		return new UniTask();

	}

}

[CommandAlias("ActorDialog")]
public class ActorInfoCmdDialogsEditing : Command {

	[ParameterAlias(NamelessParameterAlias)]
	public StringParameter ActorName;

	[ParameterAlias("addDialogOption")]
	public StringParameter dialogToAdd;

	[ParameterAlias("removeDialogOption")]
	public StringParameter dialogToRemove;

	[ParameterAlias("startLabel")]
	public StringParameter label;

	[ParameterAlias("repeatable")]
	public BooleanParameter repeatable;

	public override UniTask ExecuteAsync(AsyncToken asyncToken = default) {

		var actorsManager = Managers.GetManager<ActorsManager>();

		if (dialogToRemove.HasValue) {
			actorsManager.RemoveDialogOption(ActorName.Value, dialogToRemove.Value);
		}

		if(dialogToAdd.HasValue) {
			actorsManager.AddDialogOption(ActorName.Value,
				new DialogOption() {
					dialogName = dialogToAdd.Value,
					startlabel = label.HasValue ? label.Value : string.Empty,
					repeatable = repeatable.HasValue && repeatable.Value
				});
		}

		return new UniTask();

	}

}
