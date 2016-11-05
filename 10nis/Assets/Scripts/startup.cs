using UnityEngine;
using System.Collections;

public class startup : MonoBehaviour {

    void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        Screen.SetResolution(640, 480, false, 60);
    }
}
