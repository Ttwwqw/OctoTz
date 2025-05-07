
using UnityEngine;

[CreateAssetMenu(fileName = "ActorsCollection", menuName = "Scriptable Objects/ActorsCollection")]
public class ActorsCollection : ScriptableCollection<Actor> {





#if UNITY_EDITOR
    [ContextMenu("Catch all items in project")]
    private void CatchAllDataItems() {
        CatchAllItemsAndSaveAsset();
    }
#endif

}
