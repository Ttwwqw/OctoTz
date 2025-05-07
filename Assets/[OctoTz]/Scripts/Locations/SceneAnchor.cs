
using UnityEngine;

public class SceneAnchor : MonoBehaviour {

    [field: SerializeField] public Transform Point { get; private set; }

    public virtual void ClampObject(Transform obj) {
        obj.SetPositionAndRotation(GetPosition(), Point.rotation);
    }

    public virtual T InstantiateAtAnchor<T>(T obj) where T : Object {
        return Instantiate(obj, GetPosition(), Point.rotation);
    }

    protected virtual Vector3 GetPosition() {
        if (Physics.Raycast(Point.position + Vector3.up * 0.2f, Vector3.down, out var hit)) {
            return hit.point;
        } else {
            return Point.position;
        }
    }

}
