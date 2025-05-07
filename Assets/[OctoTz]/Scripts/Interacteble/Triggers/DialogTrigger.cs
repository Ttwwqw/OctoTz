
using Naninovel;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {

    [SerializeField] private Script _naninovelScript;
	[SerializeField] private string _scriptName;

    public async void StartNaninovelDialog() {

		if (_naninovelScript != null) {
			await Managers.GetManager<NaninovelManager>().StartNaninovelScriptAsync(_naninovelScript);
		} else {
			await Managers.GetManager<NaninovelManager>().StartNaninovelScriptAsync(_scriptName);
		}

	}

}
