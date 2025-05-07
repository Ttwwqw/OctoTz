
using UnityEngine;
using System.Collections;

public class LocationActorsSpawnHandler : SceneStartInstruction {

	[SerializeField] private LocationTag _locationTag;
	[SerializeField] private ActorAnchorsCollection[] _anchors = new ActorAnchorsCollection[0];

	public override IEnumerator Initialize() {

		var actorsManager = Managers.GetManager<ActorsManager>();

		/// [TODO] - poses ect.
		foreach (var collection in _anchors) {

			if (actorsManager.GetActorInfo(collection.Actor).currentLocation == _locationTag.LocationName) {
				collection.Anchors.Random().InstantiateAtAnchor(collection.Actor.SceneObject);
			}

		}

		return base.Initialize();

	}

}
