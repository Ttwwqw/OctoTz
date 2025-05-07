
using UnityEngine;

public class CallFrame : MonoBehaviour {
	
	public void OpenNotAwaibleFunction() {
		Managers.GetManager<GameFrames>().Open<NotAwaible>();
	}

	public void OpenSettingsFrame() {
		Managers.GetManager<GameFrames>().Open<SettingsFrame>();
	}

	public void OpenFastTravelMenu() {
		Managers.GetManager<GameFrames>().Open<FastTravelFrame>();
	}

	public void OpenHelpMainMenu() {
		OpenNotAwaibleFunction();
	}

}
