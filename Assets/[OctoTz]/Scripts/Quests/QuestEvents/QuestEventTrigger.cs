
using UnityEngine;

public class QuestEventTrigger : MonoBehaviour {

    [SerializeField] private Quest _quest;

    [SerializeField] private bool _changeStage;
    [SerializeField] private int _stage;
    [SerializeField] private string _stageName;

    [SerializeField] private string[] _marksToAdd;
    [SerializeField] private string[] _marksToRemove;

    public void ChangeQuestStatus() {

        var qTrigger = new QuestTriggerCmd() { QuestName = _quest.Id };

        if (_changeStage) {
            if (!string.IsNullOrEmpty(_stageName)) {
                qTrigger.stageName.Value = _stageName;
            } else {
                qTrigger.stageNumber.Value = _stage;
            }
        }

        if (_marksToAdd != null && _marksToAdd.Length > 0) {

            qTrigger.markToAdd.Value = string.Join(',', _marksToAdd);

        }

        if (_marksToRemove != null && _marksToRemove.Length > 0) {

            qTrigger.markToRemove.Value = string.Join(',', _marksToRemove);

        }

        qTrigger.ExecuteAsync();

    }

}
