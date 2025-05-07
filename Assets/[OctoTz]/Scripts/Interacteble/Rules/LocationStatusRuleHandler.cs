
using UnityEngine;

public class LocationStatusRuleHandler : RuleHandler {

	[SerializeField] private LocationCheck[] _rules;

	public override bool CheckRule() {

		if (_rules == null || _rules.Length <= 0) {
			return true;
		}

		var locationsManager = Managers.GetManager<LocationsManager>();

		foreach (var r in _rules) {
			if (!r.Check(locationsManager.GetLocationInfo(r.locationId))) {
				return false;
			}
		}

		return true;

	}

}

