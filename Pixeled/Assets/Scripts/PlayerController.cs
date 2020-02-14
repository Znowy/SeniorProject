using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    Rigidbody2D rb2d;
    Animator anim;
    Vector2 offsetPlayer = new Vector2(0f, -0.148f);

    const int STATE_WALK_UP = 1;
    const int STATE_WALK_LEFT = 2;
    const int STATE_WALK_DOWN = 3;
    const int STATE_WALK_RIGHT = 4;
    const int STATE_IDLE_UP = 11;
    const int STATE_IDLE_LEFT = 12;
    const int STATE_IDLE_DOWN = 13;
    const int STATE_IDLE_RIGHT = 14;
    int currentState = 0;

    static string currentlyEquipped = "none";
    float waitTime = 0.0f;
    GameObject plowedGround;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        plowedGround = Resources.Load<GameObject>("Prefabs/PlowedGround_Dry");

        Debug.Log("Game Loaded");
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTime > 0)
            waitTime -= Time.deltaTime;

        if (currentlyEquipped == "Tool_Hoe")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (waitTime <= 0.0f)
                {
                    if (Physics2D.OverlapBoxAll(new Vector2(rb2d.position.x, rb2d.position.y + offsetPlayer.y), new Vector2(0.08f, 0.08f), 0f).Length < 2)
                    {
                        GameObject instantiatedObject = Instantiate(plowedGround, new Vector2(rb2d.position.x, rb2d.position.y + offsetPlayer.y), Quaternion.identity);
                        if (instantiatedObject.name.Contains("(Clone)"))
                            instantiatedObject.name = instantiatedObject.name.Replace("(Clone)", "");
                        Debug.Log("Player has plowed the ground!");
                        waitTime = 0.5f;
                    }
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Player is now colliding with " + collision.name);
        if (waitTime <= 0.0f)
        {
            if (collision.name.Contains("Tool") || collision.name.Contains("Seed"))
            {
                if (Input.GetKey(KeyCode.E))
                {
                    if (currentlyEquipped != "none")
                    {
                        // Drop currently equipped object before picking up another one.
                        if (Physics2D.OverlapBoxAll(new Vector2(rb2d.position.x, rb2d.position.y + offsetPlayer.y), new Vector2(0.08f, 0.08f), 0f).Length < 3)
                        {
                            GameObject instantiatedObject = Instantiate(Resources.Load<GameObject>("Prefabs/" + currentlyEquipped), new Vector2(rb2d.position.x, rb2d.position.y + offsetPlayer.y), Quaternion.identity);
                            if (instantiatedObject.name.Contains("(Clone)"))
                                instantiatedObject.name = instantiatedObject.name.Replace("(Clone)", "");
                            Debug.Log("Player has dropped " + currentlyEquipped + " on the ground!");


                            currentlyEquipped = collision.name;
                            Destroy(collision.gameObject);

                            waitTime = 0.5f;
                        }
                    }
                    else
                    {
                        Debug.Log("Player has picked up the " + collision.name);
                        currentlyEquipped = collision.name;
                        Destroy(collision.gameObject);
                        waitTime = 0.5f;
                    }
                }
            }
            else if (collision.name.Contains("PlowedGround"))
            {
                Debug.Log("This object is plowed ground!");
                PlowedGroundScript plowedGroundScript = collision.gameObject.GetComponent<PlowedGroundScript>();

                if (Input.GetKey(KeyCode.Space) && currentlyEquipped.Contains("Seed") && plowedGroundScript.currentlyPlanted == "none")
                {
                    plowedGroundScript.currentlyPlanted = currentlyEquipped;
                    collision.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Plants/" + currentlyEquipped + "_" + plowedGroundScript.saturation);
                    waitTime = 0.5f;
                }
            }
        }
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate, put physics code here
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb2d.AddForce(Vector2.up * speed);
                anim.SetInteger("state", STATE_WALK_UP);
                //Debug.Log("Current State: " + anim.GetInteger("state"));
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb2d.AddForce(Vector2.left * speed);
                anim.SetInteger("state", STATE_WALK_LEFT);
                //Debug.Log("Current State: " + anim.GetInteger("state"));
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb2d.AddForce(Vector2.down * speed);
                anim.SetInteger("state", STATE_WALK_DOWN);
                //Debug.Log("Current State: " + anim.GetInteger("state"));
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb2d.AddForce(Vector2.right * speed);
                anim.SetInteger("state", STATE_WALK_RIGHT);
                //Debug.Log("Current State: " + anim.GetInteger("state"));
            }
            rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxSpeed);
        }
        else
        {
            //Debug.Log("Current State: " + anim.GetInteger("state"));
            currentState = anim.GetInteger("state");
            if (currentState == 1 || currentState == 2 || currentState == 3 || currentState == 4)
                anim.SetInteger("state", anim.GetInteger("state") + 10);
            //Debug.Log("Current State: " + anim.GetInteger("state"));
        }
    }
}
