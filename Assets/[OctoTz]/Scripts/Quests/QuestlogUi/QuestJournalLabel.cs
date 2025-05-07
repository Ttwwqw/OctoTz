

using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class QuestJournalLabel : MonoBehaviour {

	public Quest Quest { get; private set; }

	[SerializeField] private RectTransform _rect;
	[SerializeField] private TMP_Text _questTitle;
	[SerializeField] private TMP_Text _stagesDescription;

	public void Setup(Quest quest, QuestProgress progress) {
		Quest = quest;
		Refresh(progress);
	}

	public void Refresh(QuestProgress progress) {

		_questTitle.text = Quest.QuestName;

		List<string> stages = new List<string>();
		var stagesInfo = Quest.Stages;

		for (int i = 0; i < progress.stageNumber && i < Quest.Stages.Length; i++) {
			stages.Add(stagesInfo[i].description);
		}

		for (int i = 0; i < stages.Count - 1; i++) {
			stages[i] = string.Format("<color=grey>-{0}</color>", stages[i]);
		}

		_stagesDescription.text = string.Join('\n', stages);

		_rect.sizeDelta = new Vector2(_rect.sizeDelta.x, _questTitle.rectTransform.sizeDelta.y + _stagesDescription.rectTransform.sizeDelta.y);

	}

}
