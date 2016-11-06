using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class goCourt : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        SceneManager.LoadScene("Court");
    }
}
