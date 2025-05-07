
using UnityEngine;

[CreateAssetMenu(fileName = "Actor", menuName = "Scriptable Objects/Actor")]
public class Actor : ScriptableItem {

    [field: SerializeField] public string ActorName { get; private set; }
    [field: SerializeField] public ActorObject SceneObject { get; private set; }
    [field: SerializeField] public ActorInfo DefaultSettings { get; private set; }

}
