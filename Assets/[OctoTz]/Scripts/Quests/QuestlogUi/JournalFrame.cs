
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(RectTransform))]
public class JournalFrame : FrameBase {

	[SerializeField] private GameObject _emptyLabel;
	[SerializeField] private QuestJournalLabel _labelPrefab;
	[SerializeField] private RectTransform _labelsParent;

	private QuestsManager _questsManager = null;
	private List<QuestProgress> _questsProgress = null;
	private List<QuestJournalLabel> _activeLabels = null;
	private MonoPull<QuestJournalLabel> _pull = null;

	public override void Open() {

		if (_questsManager == null) {

			_activeLabels = new();
			_pull = new MonoPull<QuestJournalLabel>(_labelPrefab, _labelsParent, false);

			_questsManager = Managers.GetManager<QuestsManager>();
			_questsProgress = _questsManager.GetAllSavedQuests();
			_questsManager.QuestUpdated += OnQuestUpdated;

			foreach (var q in _questsProgress) {
				OnQuestUpdated(q);
			}

			_emptyLabel.SetActive(_activeLabels.Count <= 0);

		}

		base.Open();

	}

	private void OnDestroy() {
		if (_questsManager != null) {
			_questsManager.QuestUpdated -= OnQuestUpdated;
		}
	}

	private void OnQuestUpdated(QuestProgress progress) {

		if (_activeLabels.ContaitsWhere(x => x.Quest.Id == progress.quest, out var label)) {

			if (progress.status.IsOneOf(QuestStatus.DontStarted, QuestStatus.Completed)) {
				_activeLabels.Remove(label);
				_pull.ReturnItem(label);
				_emptyLabel.SetActive(_activeLabels.Count <= 0);
			} else {
				label.Refresh(progress);
			}
			
		} else {

			CreateQuestLabel(progress);

		}

	}

	private void CreateQuestLabel(QuestProgress progress) {

		if (progress.status.IsOneOf(QuestStatus.DontStarted, QuestStatus.Completed)) {
			return;
		}

		_activeLabels.Add(_pull.GetItem());
		_activeLabels[^1].Setup(_questsManager.GetQuestInfo(progress.quest), progress);
		_activeLabels[^1].transform.SetAsLastSibling();
		_activeLabels[^1].gameObject.SetActive(true);

	}

}


