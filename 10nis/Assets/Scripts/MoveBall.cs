using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveBall : MonoBehaviour {
    private Rigidbody2D rb2d;
    private bool canJump, falling;
    private Vector2 up, down;
    public float throwStrength;
    private float count;
    private int intCount;
    public Text countText, winText;
    private float airTime;
    private float targetTime;
    private float startingAirTime;
    private bool swing;
    Animator anim;
    public GameObject player;
    private GameObject LevelManager;

    void Reset()
    {
        SceneManager.LoadScene("Court");
    }
    void Start()
    {
        //load up leve manager
        LevelManager = GameObject.Find("GameManager");
        //initiate starting time
        if (LevelManager.GetComponent<DataHolder>().winNum < 4)
        {
            targetTime = 3;
        }
        else if (LevelManager.GetComponent<DataHolder>().winNum < 8)
        {
            targetTime = 5;
        }
        else if (LevelManager.GetComponent<DataHolder>().winNum < 12)
        {
            targetTime = 8;
        }
        else
        {
            targetTime = 10;
        }
        //actual airtime
        airTime = targetTime + 0.5F;
        startingAirTime = airTime;

        //booleans for game state
        falling = false;
        canJump = true;
        swing = true;

        rb2d = GetComponent<Rigidbody2D>();
        up = new Vector2(0, throwStrength);
        down = new Vector2(0, throwStrength * -1);
        
        //counter
        count = 0;
        countText.text = count.ToString();
        
        winText.text = "";
        anim = player.GetComponent<Animator>();

        print("Wins:"+LevelManager.GetComponent<DataHolder>().winNum);
        print("Losses:" + LevelManager.GetComponent<DataHolder>().lossNum);
        
    }
    void Update()
    { 
        //in game.

        //start throw
        if (Input.GetKeyUp("space") && canJump == true )
        {
            anim.SetTrigger("Throw");
            canJump = false;
            count = 0;
            rb2d.AddForce(up);
            swing = false;
        }
        //ball in air
        if(canJump == false)
        { 
            //count before swing
            if(swing == false)
            {
                count = count + Time.deltaTime ;
                intCount = Mathf.RoundToInt(count / (targetTime / 10));
                countText.text = intCount.ToString();
            }
            //decrease airtime and rotate ball
            airTime = airTime - Time.deltaTime;
            transform.Rotate(Vector3.forward * -15);

            //swing
            if (Input.GetKeyDown("space") && swing == false)
            {
                anim.SetTrigger("Swing");
                swing = true;
                //if swing in correct range
                if (airTime > 0.3F && airTime < 0.7F)
                {
                    GameObject.Find("Ball").transform.localScale = new Vector3(0, 0, 0);
                    Invoke("Reset", 3); 
                    winText.text = "YOU WIN!";
                    LevelManager.GetComponent<DataHolder>().winNum += 1;
                    print("win");
                    print(count);
                    print(airTime);
                }
                //swing missed
                else
                {
                    winText.text = "YOU LOSE...";
                    Invoke("Reset", 3);
                    LevelManager.GetComponent<DataHolder>().lossNum += 1;
                    print("lose");
                    print(count);
                    print(airTime);
                    airTime = startingAirTime;
                }
            }
        }

        //if airtime goes a little bfore target, ball comes back down
        if(airTime < 0.5F && falling == false)
        {
            rb2d.AddForce(down);
            falling = true;
        }
        
    }

    //also loses on collision with ground
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.name == "Background")
        {
            //only lose if has not swung already. 
            //prevent two losses
            if(swing == false)
            {
                swing = true;
                winText.text = "YOU LOSE...";
                print("did not swing lose");
                Invoke("Reset", 3);
                LevelManager.GetComponent<DataHolder>().lossNum += 1;
                airTime = startingAirTime;
            }
        }
    }
}
