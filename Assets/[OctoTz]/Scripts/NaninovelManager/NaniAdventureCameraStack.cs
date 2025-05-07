
using Naninovel;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Camera))]
public class NaniAdventureCameraStack : MonoBehaviour {

	private void Awake() {
		if (Engine.Initialized) {
			AddNaniCameraToStack();
		} else {
			Engine.OnInitializationFinished += AddNaniCameraToStack;
		}
	}

	private void AddNaniCameraToStack() {

		Engine.OnInitializationFinished -= AddNaniCameraToStack;

		var cameraData = GetComponent<Camera>().GetUniversalAdditionalCameraData();
		
		var naniCamera = Engine.GetService<ICameraManager>().Camera;
		var naniCameraUI = Engine.GetService<ICameraManager>().UICamera;

		naniCamera.GetUniversalAdditionalCameraData().renderType = CameraRenderType.Overlay;

		cameraData.cameraStack.Add(naniCamera);
		cameraData.cameraStack.Add(naniCameraUI);

	}


}
