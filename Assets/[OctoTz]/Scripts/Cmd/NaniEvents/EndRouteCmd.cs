
using System;
using Naninovel;
using System.Collections.Generic;

[CommandAlias("EndRoute")]
public class EndRouteCmd : Command {

	public static event Action<Script> Ended;
	/// <summary>
	/// Cleaning all subscribers after each call
	/// </summary>
	public static event Action<Script> EndedOnce;

	public override UniTask ExecuteAsync(AsyncToken asyncToken = default) {

		var script = Engine.GetService<IScriptPlayer>().PlayedScript;

		Ended?.Invoke(script);
		EndedOnce?.Invoke(script);

		EndedOnce = null;
		return new UniTask();

	}

}
