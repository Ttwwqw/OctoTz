
using UnityEngine;

public class ActorAnchorsCollection : MonoBehaviour {

	[field: SerializeField] public Actor Actor { get; private set; }
	[field: SerializeField] public ActorAnchor[] Anchors { get; private set; } = new ActorAnchor[0];

#if UNITY_EDITOR
	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		foreach (var anchor in Anchors) {
			Gizmos.DrawWireSphere(anchor.transform.position, 0.25f);
		}
		Gizmos.color = Color.white;
	}
#endif
}
