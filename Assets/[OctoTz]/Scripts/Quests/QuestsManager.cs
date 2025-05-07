

using System;
using System.Collections.Generic;

public enum QuestStatus {
	DontStarted, InProgress, Completed, Failed
}

[Serializable]
public struct QuestProgress {
	public string quest;
	public int stageNumber;
	public QuestStatus status;
	public List<string> marks;

	public void Print() {
		UnityEngine.Debug.Log(string.Format("Name: {0} Stage: {1} Status: {2} Marks: {3}", quest, stageNumber, status, string.Join(",", marks)));
	}

}

public class QuestsManager : Manager {

	public QuestsManager(ICollectionsLoader loader) {
		_questsCollection = loader.Load<QuestCollection>();
	}

	public event Action<QuestProgress> QuestUpdated;

	private QuestCollection _questsCollection;
	private List<QuestProgress> _questsProgress = new List<QuestProgress>();

	public Quest GetQuestInfo(string quest) {
		return _questsCollection.GetItem(quest);
	}
	public QuestProgress GetQuestProgress(Quest quest) {
		return LoadQuestStatus(quest.Id);
	}
	public QuestProgress GetQuestProgress(string quest) {
		return LoadQuestStatus(quest);
	}
	public List<QuestProgress> GetAllSavedQuests() {
		return new List<QuestProgress>(_questsProgress);
	}

	public void SeQuestStageByName(string quest, string stageName) {
		var q = GetQuestInfo(quest);
		SetQuestStage(quest, q.Stages.GetIndexOf(x => x.name == stageName));
	}

	public void SetQuestStage(string quest, int questStage) {

		var q = LoadQuestStatus(quest);

		q.stageNumber = questStage;

		if (questStage == 0) {

			q.status = QuestStatus.DontStarted;

		} if (_questsCollection.GetItem(quest).Stages.Length < questStage) {

			q.status = QuestStatus.Completed;

		} else {

			q.status = QuestStatus.InProgress;

		}

		SaveQuestStatus(q);

	}

	public void ChangeQuestMarks(string quest, bool add, params string[] marks) {

		var questProgress = LoadQuestStatus(quest);

		if (add) {
			questProgress.marks.AddWithoutDoubles(marks);
		} else {
			questProgress.marks.RemoveAllWhere((x) => x.IsOneOf(marks));
		}

		SaveQuestStatus(questProgress);

	}

	

	#region Save\load
	// TODO - save brige?
	private QuestProgress LoadQuestStatus(string quest) {

		if (_questsProgress.ContaitsWhere(x => x.quest == quest, out var q)) {
			return q;
		}

		_questsProgress.Add(new QuestProgress() { quest = quest, stageNumber = 0, status = QuestStatus.DontStarted, marks = new() });
		return _questsProgress[^1];

	}

	private void SaveQuestStatus(QuestProgress progress) {

		_questsProgress[_questsProgress.GetIndexOf((x) => x.quest == progress.quest)] = progress;
		QuestUpdated?.Invoke(progress);

	}

	#endregion

}
