
using System;
using System.Collections;
using Naninovel;
using UnityEngine;

public class NaninovelManager : SceneManager {
	

	public override IEnumerator OnInitialize() {

		if (!Engine.Initialized) {
			Debug.Log("Naninovel not initialized");
			UniTask task = RuntimeInitializer.InitializeAsync();
			yield return new WaitUntil(() => task.IsCompleted);
			Debug.Log("Naninovel is initialized now");
		}

		yield return base.OnInitialize();

	}

	public void StartNaninovelScript(Script script, string label = null, Action<Script> endCallback = null) {
		StartNaninovelScript(script.name, label, endCallback);
	}

	public void StartNaninovelScript(string script, string label = null, Action<Script> endCallback = null) {

		Managers.GetManager<TooltipsManager>().Visable = false;
		Managers.GetManager<InputManager>().DisableActionMap("Player","UI");

		Engine.GetService<ICameraManager>().EnableAll();
		Engine.GetService<IScriptPlayer>().PreloadAndPlayAsync(script, label);

		EndRouteCmd.EndedOnce += DialogEnded;
		EndRouteCmd.EndedOnce += endCallback;

	}

	public async UniTask StartNaninovelScriptAsync(Script script, string label = null) {
		await StartNaninovelScriptAsync(script.name, label);
	}

	public async UniTask StartNaninovelScriptAsync(string script, string label = null) {

		Managers.GetManager<TooltipsManager>().Visable = false;
		Managers.GetManager<InputManager>().DisableActionMap("Player", "UI");

		await Engine.GetService<IScriptPlayer>().PreloadAndPlayAsync(script, label);
		Engine.GetService<ICameraManager>().EnableAll();

		bool awaitTrigger = false;
		EndRouteCmd.EndedOnce += DialogEnded;

		await UniTask.WaitUntil(() => awaitTrigger, PlayerLoopTiming.PostLateUpdate);

	}

	private void DialogEnded(Script script) {

		Managers.GetManager<TooltipsManager>().Visable = true;
		Managers.GetManager<InputManager>().EnableActionMap("Player", "UI");

		Engine.GetService<ICameraManager>().DisableAll();

	}

}
