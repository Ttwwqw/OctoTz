
[System.Serializable]
public struct LocationCheck {

	public string locationId;

	public string status_checkRule;
	public bool status;

	public string marks_checkRule;
	public string mark;

	public bool Check(LocationInfo location) {
		return CheckStatus(location) && CheckMarks(location);
	}

	private bool CheckStatus(LocationInfo location) {
		if (string.IsNullOrWhiteSpace(status_checkRule)) return true;

		switch (status_checkRule) {
		case "==": return location.isOpen == status;
		case "!=": return location.isOpen != status;
		}

		return false;
	}

	private bool CheckMarks(LocationInfo location) {
		if (string.IsNullOrWhiteSpace(marks_checkRule)) return true;

		switch (marks_checkRule) {
		case "==":
		case "!=":
			return location.marks.Contains(mark);
		}

		return false;
	}

}
