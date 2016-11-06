using UnityEngine;
using System.Collections;

public class DataHolder : MonoBehaviour {

    public int winNum, lossNum;
    public static DataHolder holder;

    void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
        winNum = 0;
        lossNum = 0;
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        Screen.SetResolution(640, 480, false, 60);

    }

}
