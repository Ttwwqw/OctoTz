
using System;
using Naninovel;

[CommandAlias("Quest")]
public class QuestTriggerCmd : Command {

	[ParameterAlias(NamelessParameterAlias)]
	public StringParameter QuestName;

	[ParameterAlias("setStage")]
	public IntegerParameter stageNumber;

	[ParameterAlias("setStageByName")]
	public StringParameter stageName;

	[ParameterAlias("addMark")] /// separate couple marks with ','
	public StringParameter markToAdd;

	[ParameterAlias("removeMark")] /// separate couple marks with ','
	public StringParameter markToRemove;

	public override UniTask ExecuteAsync(AsyncToken asyncToken = default) {

		var manager = Managers.GetManager<QuestsManager>();

		if (stageNumber.HasValue) {
			manager.SetQuestStage(QuestName.Value, stageNumber.Value);
		}

		if (stageName.HasValue) {
			manager.SeQuestStageByName(QuestName.Value, stageName.Value);
		}

		if (markToAdd.HasValue) {
			manager.ChangeQuestMarks(QuestName.Value, true, markToAdd.Value.Split(',', StringSplitOptions.RemoveEmptyEntries));
		}

		if (markToRemove.HasValue) {
			manager.ChangeQuestMarks(QuestName.Value, false, markToRemove.Value.Split(',', StringSplitOptions.RemoveEmptyEntries));
		}

		return new UniTask();

	}

}
