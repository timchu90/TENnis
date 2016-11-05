using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveBall : MonoBehaviour {
    private Rigidbody2D rb2d;

    private bool canJump;

    private Vector2 up, down;

    public float throwStrength;

    private float count;

    public Text countText;

    private float airTime;

    public float targetTime;

    private float startingAirTime;

    private bool swing;
   
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
        swing = false;
    }
    void Update()
    { 
        if (Input.GetKeyUp("space") && canJump == true )
        {
            canJump = false;
            count = 0;
            rb2d.AddForce(up);
        }
        if(canJump == false)
        { 
            count = count + Time.deltaTime;
            countText.text = count.ToString();
            airTime = airTime - Time.deltaTime;
            transform.Rotate(Vector3.forward * -15);
            if (Input.GetKeyDown("space") && swing == false)
            {
                swing = true;
                if (airTime > 0.3F && airTime < 0.7F)
                {
                    print("YOU WIN");
                    print(count);
                    Destroy(gameObject);
                }
                else
                {
                    print("YOU LOSE");
                    print(count);
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
            canJump = true;
            countText.text = count.ToString();
            
        }
    }
}
