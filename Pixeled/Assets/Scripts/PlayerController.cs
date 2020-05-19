using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private string savePath;

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

    protected int money;
    private string _currentlyEquipped;
    public string currentlyEquipped
    {
        get
        {
            return _currentlyEquipped;
        }
        set
        {
            _currentlyEquipped = value;
            if (_currentlyEquipped == "none")
            {
                equippedItemScript.EmptyEquippedItem();
            }
            else
            {
                equippedItemScript.UpdateEquippedItem(value);
            }
        }
    }

    bool isColliding;
    Collider2D collidingObject;
    GameObject plowedGround;
    MoneyScript moneyScript;
    EquippedItemScript equippedItemScript;
    PauseMenuScript pauseMenuScript;

    // Start is called before the first frame update
    void Start()
    {
        savePath = Application.dataPath + "/savedata.json";

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isColliding = false;
        plowedGround = Resources.Load<GameObject>("Prefabs/PlowedGround_Dry");
        moneyScript = GameObject.Find("MoneyAmount").GetComponent<MoneyScript>();
        equippedItemScript = GameObject.Find("ItemBackground").GetComponent<EquippedItemScript>();
        pauseMenuScript = GameObject.Find("GameUI").GetComponent<PauseMenuScript>();

        currentlyEquipped = "none";

        money = 100;

        equippedItemScript.EmptyEquippedItem();

        LoadData();
        Debug.Log("Game Loaded");

        moneyScript.UpdateMoneyText(money);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuScript.isGamePaused = !pauseMenuScript.isGamePaused;
        }
        if (!pauseMenuScript.isGamePaused)
        {
            if (isColliding)
            {
                if (collidingObject.name.Contains("Tool") || collidingObject.name.Contains("Seed") || collidingObject.name.Contains("Crop"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (currentlyEquipped != "none")
                        {
                            // Drop currently equipped object before picking up another one.
                            if (Physics2D.OverlapBoxAll(new Vector2(rb2d.position.x, rb2d.position.y + offsetPlayer.y), new Vector2(0.08f, 0.08f), 0f).Length < 3)
                            {
                                CreateObject(Resources.Load<GameObject>("Prefabs/" + currentlyEquipped));
                                Debug.Log("Player has dropped " + currentlyEquipped + " on the ground!");

                                currentlyEquipped = collidingObject.name;
                                Destroy(collidingObject.gameObject);
                            }
                        }
                        else
                        {
                            Debug.Log("Player has picked up the " + collidingObject.name);
                            currentlyEquipped = collidingObject.name;
                            Destroy(collidingObject.gameObject);
                        }
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (currentlyEquipped == "Tool_Hoe")
                    {
                        if (Physics2D.OverlapBoxAll(new Vector2(rb2d.position.x, rb2d.position.y + offsetPlayer.y), new Vector2(0.08f, 0.08f), 0f).Length < 2)
                        {
                            CreateObject(plowedGround);
                            Debug.Log("Player has plowed the ground!");
                        }
                    }
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    if (currentlyEquipped != "none")
                    {
                        if (Physics2D.OverlapBoxAll(new Vector2(rb2d.position.x, rb2d.position.y + offsetPlayer.y), new Vector2(0.08f, 0.08f), 0f).Length < 2)
                        {
                            CreateObject(Resources.Load<GameObject>("Prefabs/" + currentlyEquipped));
                            currentlyEquipped = "none";
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Player is now colliding with " + collision.name);
        isColliding = true;
        collidingObject = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isColliding = false;
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

    private void CreateObject(GameObject gameObj)
    {
        GameObject instantiatedObject = Instantiate(gameObj, new Vector2(rb2d.position.x, rb2d.position.y + offsetPlayer.y), Quaternion.identity);
        if (instantiatedObject.name.Contains("(Clone)"))
            instantiatedObject.name = instantiatedObject.name.Replace("(Clone)", "");
    }

    public void AddMoney(int AmountOfMoney)
    {
        money += AmountOfMoney;
        moneyScript.UpdateMoneyText(money);
    }

    private void SaveData()
    {
        SaveModel saveModel = new SaveModel();
        saveModel._money = money;

        string jsonData = JsonUtility.ToJson(saveModel);
        File.WriteAllText(savePath, jsonData);
        Debug.Log("JSON: " + jsonData);
    }

    private void LoadData()
    {
        if (File.Exists(savePath))
        {
            SaveModel saveModel = JsonUtility.FromJson<SaveModel>(File.ReadAllText(savePath));
            money = saveModel._money;
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}

public class SaveModel
{
    public int _money;
}