
using Naninovel;
using UnityEngine;

public static class NaniExtensions {


    public static void DisableAll(this ICameraManager camManager) {
        camManager.Camera.enabled = false;
        camManager.UICamera.enabled = false;
    }

    public static void EnableAll(this ICameraManager camManager) {
        camManager.Camera.enabled = true;
        camManager.UICamera.enabled = true;
    }

    public static void EnableUiOnly(this ICameraManager camManager) {
        camManager.Camera.enabled = false;
        camManager.UICamera.enabled = true;
    }


}
