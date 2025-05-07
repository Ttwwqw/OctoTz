
using UnityEngine;
using UnityEngine.Events;

public sealed class QuestStatusRuleHandler : RuleHandler {
	
	[SerializeField] private QuestCheck[] _rules;

	[Header("Optional")]
	[SerializeField] private bool _autoCheck;
	[field: SerializeField] public UnityEvent<QuestStatusRuleHandler> OnPositive { get; private set; }
	[field: SerializeField] public UnityEvent<QuestStatusRuleHandler> OnNegative { get; private set; }

	private void Awake() {
		if (_autoCheck) {
			CheckRule();
		}
	}

	public override bool CheckRule() {

		if (_rules == null || _rules.Length <= 0) {
			OnPositive?.Invoke(this);
			return true;
		}

		var questManager = Managers.GetManager<QuestsManager>();

		foreach (var r in _rules) {

			questManager.GetQuestProgress(r.quest.Id).Print();

			if (!r.Check(questManager.GetQuestProgress(r.quest.Id))) {
				OnNegative?.Invoke(this);
				return false;
			}
		}

		OnPositive?.Invoke(this);
		return true;

	}

}
