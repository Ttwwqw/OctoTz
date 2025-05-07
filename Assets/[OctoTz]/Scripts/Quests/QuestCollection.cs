
using UnityEngine;

[CreateAssetMenu(fileName = "QuestCollection", menuName = "Scriptable Objects/QuestCollection")]
public class QuestCollection : ScriptableCollection<Quest> {





#if UNITY_EDITOR
    [ContextMenu("Catch all items in project")]
    private void CatchAllDataItems() {
        CatchAllItemsAndSaveAsset();
    }
#endif

}
