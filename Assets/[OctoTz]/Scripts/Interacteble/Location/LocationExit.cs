
using UnityEngine;

public class LocationExit : Interacteble {

	[Header("Left empty if want use as interactive decoration")]
	[SerializeField] private string _nextLocation;
	[SerializeField] private int _enterSceneIndex;

	[Header("Tooltop hint if player cant move to location")]
	[SerializeField] private string[] _errorMessages = new string[0];

	public override void Activate() {

		if (!string.IsNullOrWhiteSpace(_nextLocation) && Managers.GetManager<LocationsManager>().GetLocationInfo(_nextLocation).isOpen) {

			Managers.GetManager<CrossSceneBuffer>().AddValue(_nextLocation, new LocationEnterInfo(_nextLocation, _enterSceneIndex));
			Scenes.LoadScene(_nextLocation);

			base.Activate();

		} else {

			if (_errorMessages.Length > 0) {

				Managers.GetManager<TooltipsManager>().ShowTooltipHint(this, _errorMessages[Random.Range(0, _errorMessages.Length)]);

			} else {

				Managers.GetManager<TooltipsManager>().ShowTooltipHint(this, "Closed");
			
			}

		}

	}

}
