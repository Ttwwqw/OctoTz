
using System.Linq;
using UnityEngine;
using System.Collections;

public class LocationEnterDialogHandler : SceneStartInstruction {

	[SerializeField] private LocationTag _locationTag;
	[SerializeField] private float _startDialogDelay = 2f;

    public override IEnumerator Initialize() {

		var events = (from ev in Managers.GetManager<LocationsManager>().GetLocationInfo(_locationTag.LocationName).events where ev is StartDialogEvent select ev as StartDialogEvent).ToList();

		foreach (var dialogEvent in events) {
			yield return new WaitForSeconds(_startDialogDelay);
			yield return dialogEvent.ExecuteAsync();
			Managers.GetManager<LocationsManager>().RemoveLocationEvent(_locationTag.LocationName, dialogEvent);
		}

		yield return base.Initialize();

	}

}
