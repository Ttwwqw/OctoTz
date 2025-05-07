
using UnityEngine;
using System.Collections;

public class LocationPlayerEnterHandler : SceneStartInstruction {

    [SerializeField] private LocationTag _locationTag;
    [SerializeField] private LocationEnter[] _enters = new LocationEnter[0];

    [Header("[TEMP, TODO] - links")]
    [SerializeField] private GameObject _playerObject;

    public override IEnumerator Initialize() {

        if (Managers.GetManager<CrossSceneBuffer>().TryGetBufferedValue<LocationEnterInfo>((x) => x.location == _locationTag.LocationName, out var info, true)) {
            
            if (_enters.ContaitsWhere(x => x.Index == info.enterPosition, out var enter)) {

                enter.InstantiateAtAnchor(_playerObject);
                return base.Initialize();

            }

        }
        
        _enters[0].InstantiateAtAnchor(_playerObject);
        return base.Initialize();

    }

#if UNITY_EDITOR
	private void OnDrawGizmosSelected() {
        foreach (var enter in _enters) {
            Gizmos.DrawWireSphere(enter.transform.position, 0.25f);
			UnityEditor.Handles.Label(enter.transform.position, enter.Index.ToString());
        }
	}
#endif

}

[System.Serializable]
public struct LocationEnterInfo {

    public LocationEnterInfo(string location, int enterPosition) {
        this.location = location; this.enterPosition = enterPosition;
    }

    public readonly string location;
    public readonly int enterPosition;

}