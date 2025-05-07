
using UnityEngine;

public class ActorAnchor : SceneAnchor {
    
    [field: SerializeField] public string Tag { get; private set; }
    [field: SerializeField] public Actor Actor { get; private set; }
    [field: SerializeField] public string Pose { get; private set; }

#if UNITY_EDITOR
	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, 0.25f);
		Gizmos.color = Color.white;
	}
#endif

}
