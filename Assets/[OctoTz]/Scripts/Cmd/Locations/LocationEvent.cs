


using System;
using Naninovel;

[System.Serializable]
public abstract class LocationEvent {
	public abstract UniTask ExecuteAsync();
}

[System.Serializable]
public class StartDialogEvent : LocationEvent {

	private string _scriptName;

	public StartDialogEvent(string dialogName) {
		_scriptName = dialogName;
	}

	public override UniTask ExecuteAsync() {
		return Managers.GetManager<NaninovelManager>().StartNaninovelScriptAsync(_scriptName);
	}

}
