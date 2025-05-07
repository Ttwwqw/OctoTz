
using Naninovel;
using UnityEngine;

public interface IStringValidator {
	bool Validate(string value);
}

public struct DefaultStringValidator : IStringValidator {
	public bool Validate(string value) {
		return !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);
	}
}

[CommandAlias("InputField")]
public class InputFieldCmd : Command {

	[ParameterAlias(NamelessParameterAlias), RequiredParameter]
	public StringParameter CustomPropertyNameToWrite;

	[ParameterAlias("placeholder"), RequiredParameter]
	public StringParameter PlaceholderText;

	public override async UniTask ExecuteAsync(AsyncToken asyncToken = default) {

		string newValue = string.Empty;

		InputFieldUI frame = Engine.GetService<IUIManager>().GetUI("InputFieldUI") as InputFieldUI;
		frame.Show();
		frame.Setup(PlaceholderText.Value, (result) => { newValue = result; }, new DefaultStringValidator());

		await UniTask.WaitUntil(() => !string.IsNullOrEmpty(newValue), PlayerLoopTiming.PostLateUpdate);

		Engine.GetService<ICustomVariableManager>().SetVariableValue(CustomPropertyNameToWrite.Value, newValue);
		await Engine.GetService<IStateManager>().SaveGlobalAsync();

		await Engine.GetService<IUIManager>().GetUI("InputFieldUI").ChangeVisibilityAsync(false);
		
	}

}
