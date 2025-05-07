
using Naninovel;
using Naninovel.UI;

/// [TODO] - end callbacks
public abstract class MiniGameFrame : CustomUI {
	public abstract bool IsRunning { get; protected set; }
	public abstract void StartGame();
	public abstract void StopGame();
}

[CommandAlias("Mini-game")]
public class MiniGameCmd : Command {

	[ParameterAlias(NamelessParameterAlias), RequiredParameter]
	public StringParameter MiniGameFrameName;

	public override async UniTask ExecuteAsync(AsyncToken asyncToken = default) {

		MiniGameFrame miniGameFrame = Engine.GetService<IUIManager>().GetUI(MiniGameFrameName.Value) as MiniGameFrame;
		miniGameFrame.Show();
		miniGameFrame.StartGame();

		await UniTask.WaitUntil(() => !miniGameFrame.IsRunning);

		await Engine.GetService<IUIManager>().GetUI(MiniGameFrameName.Value).ChangeVisibilityAsync(false);

	}

}
