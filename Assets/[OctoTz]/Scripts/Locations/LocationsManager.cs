
using System;
using System.Collections.Generic;

[Serializable]
public struct LocationInfo {
    public string location;
    public bool isOpen;
    public List<string> marks;
	public List<LocationEvent> events;
}

public class LocationsManager : Manager {

	public LocationsManager(ICollectionsLoader loader) {
		
	}

	public event Action<LocationInfo> LocationUpdated;

	private List<LocationInfo> _locationsStatus = new List<LocationInfo>();

	public void ChangeLocationStatus(string location, bool isOpen) {

		var locationInfo = LoadLocationStatus(location);
		locationInfo.isOpen = isOpen;

		SaveLocationStatus(locationInfo);

	}

	public void ChangeLocationMark(string location, bool add, params string[] marks) {

		LocationInfo l = LoadLocationStatus(location);

		if (add) {
			l.marks.AddWithoutDoubles(marks);
		} else{
			l.marks.RemoveAllWhere((x) => x.IsOneOf(marks));
		}

		SaveLocationStatus(l);

	}

	public void AddLocationEvent(string location, LocationEvent lEvent) {

		var locationInfo = LoadLocationStatus(location);
		locationInfo.events.AddWithoutDoubles(lEvent);

		SaveLocationStatus(locationInfo);

	}

	public void RemoveLocationEvent(string location, LocationEvent lEvent) {

		var locationInfo = LoadLocationStatus(location);
		locationInfo.events.Remove(lEvent);

		SaveLocationStatus(locationInfo);

	}

	public LocationInfo GetLocationInfo(string location) {
		return LoadLocationStatus(location);
	}


	#region Save\load
	// TODO - save brige?
	private LocationInfo LoadLocationStatus(string location) {

		if (_locationsStatus.ContaitsWhere(x => x.location == location, out var q)) {
			return q;
		}

		_locationsStatus.Add(new LocationInfo() { location = location, isOpen = false, marks = new(), events = new() });
		return _locationsStatus[^1];

	}

	private void SaveLocationStatus(LocationInfo locationInfo) {

		_locationsStatus[_locationsStatus.GetIndexOf((x) => x.location == locationInfo.location)] = locationInfo;
		LocationUpdated?.Invoke(locationInfo);

	}

	#endregion

}
