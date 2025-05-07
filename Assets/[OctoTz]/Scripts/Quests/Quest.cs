
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Objects/Quest")]
public class Quest : ScriptableItem {

    [SerializeField, TextArea(2, 10)] string _devNotes;

    [field: SerializeField] public string QuestName { get; private set; }
    [field: SerializeField] public QuestStage[] Stages { get; private set; }

    [Serializable]
    public struct QuestStage {
        public string name;
        public string description;
    }

}
