using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed = 1.0f;
    public float dashSpeed = 1.5f;
    public float dashTime = 3.0f;
    public GameObject wall, trap, bait, portal;
    public GameObject wallHolder;
    //public GameObject fakeWall, redCross
    public int numOfWall = 3, numOfTrap = 100; // test
    public int wallCapacity = 3;
    //public float playerOffset = 0.85f;  // half player width 0.35 + half wall width 0.5
    public float setWallCoolDown = 1.0f, setTrapCoolDown = 1.0f, dashCoolDown = 5.0f, setPortalCoolDown = 30.0f;
    public float wallExistTime = 10.0f;
    public float trapExistTime = 10.0f;
    //public float baitExistTime = 10f;
    public float portalExistTime = 20.0f;
    public Vector2 basePos;
    public float moveSpeedPenalty = 1.5f;
    //public float itemSpeed = 0.5f, itemExistTime = 1.0f;

    private Rigidbody2D rb;
    private Vector2 faceDirection = Vector2.up;
    private float wallTimer = 2.0f;
    private float trapTimer = 6.0f;
    private float dashTimer = 2.0f;
    private float portalTimer = 30.0f;
    private Vector3 originalPos;
    //private Vector3 wallPos;
   // private bool isFakeWallSet = false;
    //private bool isFakeWallPossible = true;
    //private GameObject fakeWallInstance;

    public Image J_cooldown;
    public Image K_cooldown;
    public Image Space_cooldown;
    public Image L_cooldown;
    public Image baitImage;
    public Image windImage;
    public Image thunderImage;
    public Image J_frame;
    public Image K_frame;
    public Image L_frame;
    public Image U_frame;
    public Image I_frame;
    public Image O_frame;
    public GameObject WE;
    public GameObject TE;
    public GameObject CenterHolder;
    private SpriteRenderer display;
    public Sprite pandaFront;
    public Sprite pandaBack;
    public Sprite pandaLeft;
    public Sprite pandaRight;
    public Text wallNum;

    public int level;
    private bool hasBait = false;
    private bool hasThunder = false;
    private bool hasWind = false;
    private bool isFirstPortal = true;

    public float nextTeleportTimer = 2.0f;
    private int teleporterCount;
    public Text teleNum;
    public TextMeshProUGUI timerText;
    public float levelTimer;
    public string playerName;
    public float clearTime;

    public string[] leaderName = new string[10];
    public int[] leaderLevel = new int[10];
    public float[] leaderTime = new float[10];
    public Animator animator;

    public GameObject portalImage;
    private bool isPortalImageSet = false;
    private bool isHintShown = false;

    private void Awake()
    {
        instance = this;

        rb = GetComponent<Rigidbody2D>();
        originalPos = rb.transform.position;
        basePos = GameObject.FindGameObjectWithTag("Base").transform.position;

        display = GetComponent<SpriteRenderer>();
        //playerName = GameManager.instance.playerName;
    }
    private void Start()
    {
        PlayerPrefs.SetString("Level", SceneManager.GetActiveScene().name);
        playerName = GameManager.instance.playerName;
    }

    void FixedUpdate()
    {
        Move();
        levelTimer += Time.deltaTime;
        timerText.text = levelTimer.ToString("0.0");
    }

    void Update()
    {

        if (levelTimer >= 2 && !isHintShown)
        {
            if (PlayerPrefs.GetString("Level")[6] == '6')
            {
                isHintShown = true;
                portalImage.SetActive(true);
                isPortalImageSet = true;
                Time.timeScale = 0.01f;
            }
        }

        if (PlayerPrefs.GetString("Level")[6] == '6' && isPortalImageSet)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                portalImage.SetActive(false);
                isPortalImageSet = false;
                Time.timeScale = 1;
            }
        }


        Dash();
        NewSetWall();
        SetTrap();
        
        UseThunder();
        UseWind();

        if (nextTeleportTimer < 2.0f)
        {
            nextTeleportTimer += Time.deltaTime;
        }

        if (PlayerPrefs.GetString("Level")[6] < '6' && PlayerPrefs.GetString("Level").Length == 7)
        {
            return;
        }

        SetBait();
        SetPortal();
        
    }

    void Move()
    {
        bool isMoving = false;

        if (Input.GetKey(KeyCode.W))
        {
            rb.transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
            animator.SetInteger("direction", 2);    // 2:up, -2:down, 1:right, -1:left
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
            animator.SetInteger("direction", -2);
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
            animator.SetInteger("direction", -1);
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            animator.SetInteger("direction", 1);
            isMoving = true;
        }

        if (!isMoving) animator.SetInteger("direction", 0);
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        //if (Input.GetAxis("Horizontal") < 0)
        //    display.sprite = pandaLeft;
        //if (Input.GetAxis("Horizontal") > 0)
        //    display.sprite = pandaRight;
        //if (Input.GetAxis("Vertical") < 0)
        //    display.sprite = pandaFront;
        //if (Input.GetAxis("Vertical") > 0)
        //    display.sprite = pandaBack;

        //transform.position = transform.position + movement * Time.deltaTime * moveSpeed;
        animator.SetInteger("teleport", 0);
    }

    void Dash()
    {
        //if (dashTimer <= dashCoolDown)
        //{
        //    dashTimer += Time.deltaTime;
        //    Space_cooldown.fillAmount = dashTimer / dashCoolDown;
        //    return;
        //}

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    StartCoroutine(DashInHalfSecond());
        //    dashTimer = 0;
        //}
    }

    //IEnumerator DashInHalfSecond()
    //{
    //    float temp = moveSpeed;
    //    moveSpeed = dashSpeed;
    //    yield return new WaitForSeconds(dashTime);
    //    moveSpeed = temp;
    //}

    //void SetWall()
    //{
    //    if (PlayerPrefs.GetString("Level")[6] == '2')
    //    {
    //        return;
    //    }
    //    if (wallTimer <= setWallCoolDown)
    //    {
    //        wallTimer += Time.deltaTime;
    //        J_cooldown.fillAmount = wallTimer / setWallCoolDown;
    //        return;
    //    }
        
    //    // Option 1. set the wall immediately in front of player
    //    //if (Input.GetKeyDown(KeyCode.Space) && numOfWall > 0 && timer >= setWallTimeInterval && !isFakeWallSet)
    //    //{
    //    //    Vector3 playerPos = rb.transform.position;
    //    //    GameObject go = Instantiate(wall, playerPos + (Vector3)(faceDirection * playerOffset), Quaternion.identity);
    //    //    numOfWall--;
    //    //    timer = 0;
    //    //}

    //    // Option 2. set a fake wall with no collider around player, next space will set the wall in fake wall position
    //    //Vector3 playerPos = rb.transform.position;
    //    //wallPos = playerPos + (Vector3)faceDirection * playerOffset;

    //    //if (Input.GetKeyDown(KeyCode.J) && numOfWall > 0 && wallTimer >= setWallCoolDown)
    //    //{
    //    //    if (!isFakeWallSet) // set fake wall
    //    //    {
    //    //        fakeWallInstance = Instantiate(fakeWall, wallPos, Quaternion.identity);
    //    //        fakeWallInstance.transform.parent = rb.gameObject.transform;  //  follow player
    //    //    }
    //    //    else
    //    //    {
    //    //        if (!isFakeWallPossible) //  fake wall is overlapped with wall or enemies
    //    //        {
    //    //            GameObject go = Instantiate(redCross, wallPos, Quaternion.identity);
    //    //            Destroy(go, 0.5f);
    //    //            Destroy(rb.transform.GetChild(1).gameObject);   // test and see whether we need this line (Do we need keep the fake wall when redcross appears?)

    //    //            FindObjectOfType<AudioManager>().Play("ErrorSFX");
    //    //        }
    //    //        else
    //    //        {
    //    //            Instantiate(wall, wallPos, Quaternion.identity);
    //    //            Destroy(rb.transform.GetChild(1).gameObject);
    //    //            numOfWall--;
    //    //            wallTimer = 0;

    //    //            FindObjectOfType<AudioManager>().Play("SetWallSFX");
    //    //        }
    //    //    }
    //    //    isFakeWallSet = !isFakeWallSet;
    //    //}

    //    //if (isFakeWallSet)
    //    //{
    //    //    fakeWallInstance.transform.position = wallPos;
    //    //}

    //    // Option 3. Set the wall under player with trigger, when player leave the trigger, the fake wall changes to real wall with collider.
    //    // *** If option 2 fails, I will finish here. ***

    //    if (Input.GetKeyDown(KeyCode.J) && numOfWall > 0 && wallTimer >= setWallCoolDown)
    //    {
    //        Instantiate(wall, rb.transform.position, Quaternion.identity);
    //        wallTimer = 0;
    //        FindObjectOfType<AudioManager>().Play("SetWallSFX");
    //    }

    //    AstarPath.active.Scan();
    //}

    void NewSetWall()
    {
        if (PlayerPrefs.GetString("Level")[6] == '2')
        {
            return;
        }

        if (numOfWall < wallCapacity)
        {
            wallTimer += Time.deltaTime;
            J_cooldown.fillAmount = wallTimer / setWallCoolDown;
            if (J_cooldown.fillAmount >= 1.0f)
            {
                wallTimer = 0;
                numOfWall++;
                wallNum.text = numOfWall.ToString();
                J_frame.color = Color.white;
                J_cooldown.color = Color.white;
            }
        }

        if (numOfWall == 0)
        {
            J_frame.color = new Color(120,0,0,255);
            J_cooldown.color = Color.gray; ;
        }

        if (Input.GetKeyDown(KeyCode.J) && numOfWall > 0)
        {
            if (wallHolder.transform.childCount >= wallCapacity)
            {
                Destroy(wallHolder.transform.GetChild(0).gameObject);
                AstarPath.active.Scan();
            }

            GameObject go = Instantiate(wall, rb.transform.position, Quaternion.identity);
            go.transform.parent = wallHolder.transform;
            numOfWall--;
            wallNum.text = numOfWall.ToString();

            FindObjectOfType<AudioManager>().Play("SetWallSFX");
            AstarPath.active.Scan();
        }
    }

    void SetTrap()
    {
        //if (PlayerPrefs.GetString("Level")[6] == '1')
        //{
        //    return;
        //}
        if (trapTimer <= setTrapCoolDown)
        {
            trapTimer += Time.deltaTime;
            K_cooldown.fillAmount = trapTimer / setTrapCoolDown;
            K_cooldown.color = Color.grey;
            K_frame.color = new Color(120, 0, 0, 255);
            return;
        }

        else
        {
            K_cooldown.color = Color.white;
            K_frame.color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.K) && numOfTrap > 0 && trapTimer >= setTrapCoolDown)
        {
            Vector3 playerPos = rb.transform.position;

            GameObject go = Instantiate(trap, playerPos, Quaternion.identity);
            numOfTrap--;
            Destroy(go, trapExistTime);
            trapTimer = 0;
            FindObjectOfType<AudioManager>().Play("SetTrapSFX");
        }
    }

    void SetBait()
    {
        if (Input.GetKeyDown(KeyCode.U) && hasBait == true)
        {
            Vector3 playerPos = rb.transform.position;
            GameObject b = Instantiate(bait, playerPos, Quaternion.identity);
            hasBait = false;
            baitImage.color = Color.gray;
            U_frame.color = new Color(120, 0, 0, 255);
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemy.GetComponent<EnemyController>().SufferBait();
            }
            //Destroy(b, baitExistTime);
            FindObjectOfType<AudioManager>().Play("DropBaitSFX");
        }
    }

    void SetPortal()
    {
        //if (PlayerPrefs.GetString("Level")[6] == '1' || PlayerPrefs.GetString("Level")[6] == '2')
        //{
        //    return;
        //}
        if (portalTimer <= setPortalCoolDown)
        {
            portalTimer += Time.deltaTime;
            L_cooldown.fillAmount = portalTimer / setPortalCoolDown;
            L_cooldown.color = Color.gray;
            L_frame.color = new Color(120, 0, 0, 255);
            return;
        }
        else
        {
            L_cooldown.fillAmount = 1;
            L_cooldown.color = Color.white;
            L_frame.color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.L) && portalTimer >= setPortalCoolDown)
        {
            Vector3 playerPos = rb.transform.position;
            Instantiate(portal, playerPos, Quaternion.identity);
            teleporterCount = 3;
                
            if (!isFirstPortal)
            {
                portalTimer = 0;
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("Portal"))
                {
                    Destroy(g, portalExistTime);
                }
                isFirstPortal = true;
            }
            else
            {
                isFirstPortal = false;
            }
            
            FindObjectOfType<AudioManager>().Play("SetPortalSFX");
        }
    }

    void UseThunder()
    {
        if (Input.GetKey(KeyCode.I) && hasThunder)
        {
            hasThunder = false;
            thunderImage.color = Color.gray;
            I_frame.color = new Color(120,0,0,255);
            GameObject ThunderE = Instantiate(TE, CenterHolder.transform.position, Quaternion.identity);
            Time.timeScale = .4f;
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemy.GetComponent<EnemyController>().SufferThunder();
            }
            StartCoroutine(DestroyWind(ThunderE));
            FindObjectOfType<AudioManager>().Play("SetThunderSFX");
        }
    }

    void UseWind()
    {
        if (Input.GetKey(KeyCode.O) && hasWind)
        {
            hasWind = false;
            windImage.color = Color.gray;
            O_frame.color = new Color(120, 0, 0, 255);
            GameObject WindE = Instantiate(WE, rb.transform.position, Quaternion.identity);
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemy.GetComponent<EnemyController>().SufferWind();
            }

            StartCoroutine(DestroyWind(WindE));
            FindObjectOfType<AudioManager>().Play("windSFX");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.CompareTag("Speedup"))
        //{
        //    moveSpeed += itemSpeed;
        //    Destroy(other.gameObject);
        //}
        if (other.CompareTag("Wind"))
        {
            hasWind = true;
            windImage.color = Color.white;
            O_frame.color = Color.white;
            //GameObject WindE = Instantiate(WE, rb.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            //foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            //{
            //    enemy.GetComponent<EnemyController>().SufferWind();
            //}

            //StartCoroutine(DestroyWind(WindE));
        }


        if (other.CompareTag("Thunder"))
        {
            hasThunder = true;
            thunderImage.color = Color.white;
            I_frame.color = Color.white;
            //    GameObject ThunderE = Instantiate(TE, CenterHolder.transform.position, Quaternion.identity);
            //    Time.timeScale = .4f;
            Destroy(other.gameObject);
            //    foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            //    {
            //        enemy.GetComponent<EnemyController>().SufferThunder();
            //    }
            //    StartCoroutine(DestroyWind(ThunderE));
        }

        if (other.CompareTag("bait_player"))
        {        
            Destroy(other.gameObject);
            hasBait = true;
            baitImage.color = Color.white;
            U_frame.color = Color.white;
        }

        if (other.CompareTag("Portal"))
        {
            if (GameObject.FindGameObjectsWithTag("Portal").Length == 1)
            {
                Debug.Log("Only 1 portal in the map.");
                return;
            }

            if (nextTeleportTimer < 2.0f)
            {
                Debug.Log("Player has been teleported within 2 seconds.");
                return;
            }
            else
            {
                GameObject g1 = GameObject.FindGameObjectsWithTag("Portal")[0];
                GameObject g2 = GameObject.FindGameObjectsWithTag("Portal")[1];
                animator.SetInteger("teleport", 1);
                transform.position = other.gameObject == g1 ? g2.transform.position : g1.transform.position;
                nextTeleportTimer = 0;
                //teleporterCount--;
                //teleNum.text = teleporterCount.ToString();
                
                FindObjectOfType<AudioManager>().Play("TeleportSFX");
            }
            //if(teleporterCount == 0)
            //{
            //    foreach (GameObject portal in GameObject.FindGameObjectsWithTag("Portal"))
            //    {
            //        Destroy(portal);
            //    }
            //}
        }
    }

    //public void SetFakeWallPossible(bool isPossible)
    //{
    //    isFakeWallPossible = isPossible;
    //}

    public void FlashBackBase()
    {
        FindObjectOfType<AudioManager>().Play("PlayerFlashBackSFX");

        Vector3 pos = GameObject.FindGameObjectWithTag("Base").transform.position;

        rb.transform.position = pos; // flash back

        // option 1: player is able to move when flashing
        // option 2: player is not able to move when flashing. By changing the moveSpeedPenalty.

        moveSpeed -= moveSpeedPenalty;
        StartCoroutine(FlashInSecond());
    }

    IEnumerator FlashInSecond()
    {
        rb.gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.25f);
        rb.gameObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.25f);
        rb.gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.25f);
        rb.gameObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.25f);
        moveSpeed += moveSpeedPenalty;
    }
    IEnumerator DestroyWind(GameObject g)
    {


        yield return new WaitForSeconds(1f);

        Destroy(g);
        Time.timeScale = 1f;
    }


    public void SavePlayer()
    {
        clearTime = levelTimer;
        level = SceneManager.GetActiveScene().buildIndex + 1;

        UpdateLeaderBoard();

        SaveAndLoad.Save(this);
    }

    void UpdateLeaderBoard()
    {
        leaderLevel = GameManager.instance.leaderLevel;
        leaderName = GameManager.instance.leaderName;
        leaderTime = GameManager.instance.leaderTime;
        clearTime = levelTimer; 

        for (int i = 0; i < 10; i++)
        {
            if (leaderLevel[i] < level || (leaderLevel[i] == level && leaderTime[i] > clearTime))
            {
                //replace from 10 to i
                for (int j = 9; j > i; j--)
                {
                    leaderName[j] = leaderName[j - 1];
                    leaderTime[j] = leaderTime[j - 1];
                    leaderLevel[j] = leaderLevel[j - 1];
                }
                leaderName[i] = playerName;
                leaderTime[i] = clearTime;
                leaderLevel[i] = level - 1;   // cleared level, not playing level.
                break;
            }
        }
        GameManager.instance.leaderName = leaderName;
        GameManager.instance.leaderTime = leaderTime;
        GameManager.instance.leaderLevel = leaderLevel;
    }
}
