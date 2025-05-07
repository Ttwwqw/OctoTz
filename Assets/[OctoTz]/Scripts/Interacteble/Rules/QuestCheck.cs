
[System.Serializable]
public struct QuestCheck {

	public Quest quest;

	public string status_checkRule;
	public QuestStatus status;

	public string stage_checkRule;
	public int stage;

	public string marks_checkRule;
	public string mark;

	public bool Check(QuestProgress progress) {
		return CheckStatus(progress) && CheckStage(progress) && CheckMarks(progress);
	}

	private bool CheckStatus(QuestProgress progress) {
		if (string.IsNullOrWhiteSpace(status_checkRule)) return true;
		
		switch (status_checkRule) {
		case "==": return progress.status == status;
		case "!=": return progress.status != status;
		}

		return false;
	}

	private bool CheckStage(QuestProgress progress) {
		if (string.IsNullOrWhiteSpace(stage_checkRule)) return true;

		switch (stage_checkRule) {
		case "==": return progress.stageNumber == stage;
		case "!=": return progress.stageNumber != stage;
		case ">": return progress.stageNumber > stage;
		case ">=": return progress.stageNumber >= stage;
		case "<": return progress.stageNumber < stage;
		case "=<": return progress.stageNumber <= stage;
		}

		return false;
	}

	private bool CheckMarks(QuestProgress progress) {
		if (string.IsNullOrWhiteSpace(marks_checkRule)) return true;

		switch (marks_checkRule) {
		case "==":
		case "!=":
			return progress.marks.Contains(mark);
		}

		return false;
	}

}




/* ------ Do not want create custom editor for this now ------
 
	private enum Field {
		Status, Marks, StageNumber
	}

	private enum CompareRule {
		IsEquals, IsNotEquals, IsSmaller, IsBigger
	}
	[Serializable]
	private abstract class FieldChecker {
		public Field field;
		public abstract bool Check(QuestProgress progress);
	}
	[Serializable]
	private class QuestStatusField : FieldChecker {
		public bool isQueals;
		public QuestStatus value;
		public override bool Check(QuestProgress progress) {
			return (value == progress.status) == isQueals;
		}
	}

	[Serializable]
	private struct QuestCheck {

		public string questId;
		
		public List<FieldChecker> fields;

		public bool Check(QuestProgress progress) {
			
			foreach (var f in fields) {
				if (!f.Check(progress)) {
					return false;
				}
			}

			return true;

		}

	}

*/
