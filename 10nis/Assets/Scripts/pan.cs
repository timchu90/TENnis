using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class pan : MonoBehaviour {

    private Vector3 startMarker;
    private Vector3 endMarker;
    public float speed;
    private float startTime;
    private float journeyLength;
    private bool started;
    public GameObject logo;
    void Start()
    {
        started = false;
        startMarker = new Vector3(-5.2F, 0, -10);
        endMarker = new Vector3(0, 0, -10);
        journeyLength = Vector3.Distance(startMarker, endMarker);
    }
    void Update()
    {
        //on space press
        if (Input.GetKeyDown("space") && started == false)
        {
            //begin transition, remove prompt
            started = true;
            startTime = Time.time;
            GameObject.Find("StartText").transform.localScale = new Vector3(0, 0, 0);
        }
        if (started == true)
        {
            //camera pan
            //fade logo
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
            logo.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1-fracJourney);
            if (fracJourney >= 1)
            {
                SceneManager.LoadScene("Court");
            }
        }
    }
}
