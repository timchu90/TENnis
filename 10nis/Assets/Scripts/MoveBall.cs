using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveBall : MonoBehaviour {
    private Rigidbody2D rb2d;

    private bool canJump;

    private Vector2 up, down;

    public float throwStrength;

    private float count;

    public Text countText, winText;

    private float airTime;

    public float targetTime;

    private float startingAirTime;

    private bool swing;

    Animator anim;

    public GameObject player;

    void Reset()
    {
        SceneManager.LoadScene("Court");
    }
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        up = new Vector2(0, throwStrength);
        down = new Vector2(0, throwStrength * -1);
        canJump = true;
        count = 0;
        countText.text = count.ToString();
        airTime = targetTime + 0.5F;
        startingAirTime = airTime;
        swing = true;
        winText.text = "";
        anim = player.GetComponent<Animator>();
    }
    void Update()
    { 
        if (Input.GetKeyUp("space") && canJump == true )
        {
            anim.SetTrigger("Throw");
            canJump = false;
            count = 0;
            rb2d.AddForce(up);
            swing = false;
        }
        if(canJump == false)
        { 
            if(swing == false)
            {
                count = count + Time.deltaTime;
                countText.text = count.ToString();
            }
            airTime = airTime - Time.deltaTime;
            transform.Rotate(Vector3.forward * -15);
            if (Input.GetKeyDown("space") && swing == false)
            {
                anim.SetTrigger("Swing");
                swing = true;
                if (airTime > 0.3F && airTime < 0.7F)
                {
                    GameObject.Find("Ball").transform.localScale = new Vector3(0, 0, 0);
                    Invoke("Reset", 3); 
                    winText.text = "YOU WIN!";
                    
                }
                else
                {
                    winText.text = "YOU LOSE...";
                    Invoke("Reset", 3);
                }
            }
        }
        if(airTime < 0)
        {
            rb2d.AddForce(down);
            airTime = startingAirTime;
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.name == "Background")
        {
            //canJump = true;
            //countText.text = count.ToString();
            winText.text = "YOU LOSE...";
            Invoke("Reset", 3);
        }
    }
}
