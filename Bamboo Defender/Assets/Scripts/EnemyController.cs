using System.Collections;
using Pathfinding;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EnemyController : MonoBehaviour
{
    public float maxHealth = 10, health = 10;
    public Image healthBar;
    public float speedGainAfterFight = 0.25f;
    public float trappedTime = 4.0f;
    public float windLastTime = 3.0f;
    public float baitLastTime = 10f;
    public float ThunderDmg = 2.0f;
    public GameObject[] itemsDrop;
    public GameObject SpawnPlace;
    
    public GameObject fight;
    public float dropRate = 0.3f;
    
    private Rigidbody2D rb;
    private Transform spawnTransform;
    
    private bool isshake;
    private Vector2 moveDirection = Vector2.down;

    public Transform target;
    public Transform bait_target;
    public shake cameraShake;
   
    public float movespeed = 0.75f;
    public float nextWPdist = .5f;
    public GameObject floatingDMG;
    public float nextTeleportTimer = 2.0f;

    Path path;
    int currentWP = 0;
    bool reachedEnd = false;
    Seeker seeker;

    private SpriteRenderer enemySprite;
    public Sprite rabbitLeft;
    public Sprite rabbitRight;
    public Sprite rabbitBack;
    public Sprite rabbitFront;
    public Sprite lionLeft;
    public Sprite lionRight;
    public Sprite lionBack;
    public Sprite lionFront;
    public Animator animator;

    private bool isFirstChangeDir = false;
    private bool isSecondChangeDir = false;
    private Vector2 lastDir = Vector2.zero;
    private Vector2 lastDir1 = Vector2.zero;

    public GameObject RabbitDie, LionDie;
    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        spawnTransform = Instantiate(SpawnPlace, rb.transform.position, Quaternion.identity).transform;
        enemySprite = GetComponent<SpriteRenderer>();

    }

    void Start()
    {
        InvokeRepeating("Pathwork", 0f, .5f);
        target = GameObject.FindGameObjectWithTag("Base").transform;
        isshake = false;
        cameraShake = GameObject.Find("Main Camera").GetComponent<shake>();
    }

    void Pathwork()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, PathComplete);
    }


    void PathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWP = 0;
        }
    }

    private void Update()
    {
        if (nextTeleportTimer < 2.0f)
        {
            nextTeleportTimer += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        EnemyAI();
    }

    void EnemyAI()
    {
        if (path == null) return;
        if (currentWP >= path.vectorPath.Count)
        {
            reachedEnd = true;
            return;
        }
        else
        {
            reachedEnd = false;
        }

        Vector2 dir = ((Vector2)path.vectorPath[currentWP] - rb.position).normalized;

        rb.transform.Translate(dir * movespeed * Time.deltaTime);   // move in uniform speed

        
        if (lastDir != dir)
        {
            if (isFirstChangeDir)
            {
                if (isSecondChangeDir)
                {
                    isFirstChangeDir = false;
                    isSecondChangeDir = false;
                    if (lastDir == lastDir1 && lastDir== dir)
                    {
                        if (gameObject.name.StartsWith("Rabbit"))
                        {
                            if (dir.x < -0.5f)
                                enemySprite.sprite = rabbitLeft;
                            if (dir.x > 0.5f)
                                enemySprite.sprite = rabbitRight;
                            if (dir.y < -0.5f)
                                enemySprite.sprite = rabbitFront;
                            if (dir.y > 0.5f)
                                enemySprite.sprite = rabbitBack;
                        }
                        else if (gameObject.name.StartsWith("Lion"))
                        {
                            if (dir.x < -0.5f)
                                enemySprite.sprite = lionLeft;
                            if (dir.x > 0.5f)
                                enemySprite.sprite = lionRight;
                            if (dir.y < -0.5f)
                                enemySprite.sprite = lionFront;
                        }
                    }
                }
                else
                {
                    lastDir1 = dir;
                    isSecondChangeDir = true;
                }
            }
            else
            {
                lastDir = dir;
                isFirstChangeDir = true;
            }
        }
        else
        {
            if (gameObject.name.StartsWith("Rabbit"))
            {
                if (dir.x < -0.5f)
                    enemySprite.sprite = rabbitLeft;
                if (dir.x > 0.5f)
                    enemySprite.sprite = rabbitRight;
                if (dir.y < -0.5f)
                    enemySprite.sprite = rabbitFront;
                if (dir.y > 0.5f)
                    enemySprite.sprite = rabbitBack;
            }
            else if (gameObject.name.StartsWith("Lion"))
            {
                if (dir.x < -0.5f)
                    enemySprite.sprite = lionLeft;
                if (dir.x > 0.5f)
                    enemySprite.sprite = lionRight;
                if (dir.y < -0.5f)
                    enemySprite.sprite = lionFront;
            }
        }
        


        //Vector2 f = dir * speed * Time.deltaTime; // move by force
        //rb.AddForce(f);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWP]);
        if (distance < nextWPdist)
        {
            currentWP++;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.collider.CompareTag("Defense"))
        {
            TakeDamage(1);
            
            var go = Instantiate(floatingDMG, transform.position, Quaternion.identity, transform);
            go.GetComponent<TextMeshPro>().text = "-10";
        }
        if (other.collider.CompareTag("Enemy"))
        // hard to control which one will take damage first, so I send both of them to levelManager and get infos back at same time
        {
            if (rb.GetComponent<SpriteRenderer>().color.a > other.collider.GetComponent<SpriteRenderer>().color.a)
            {
                StartCoroutine(StopMovingForSecond(.5f));
                float h1 = GetHealth();
                float h2 = other.gameObject.GetComponent<EnemyController>().GetHealth();

                GameObject fightSmoke = Instantiate(fight, rb.transform.position, Quaternion.identity);
                //shake fightShake = fightSmoke.GetComponent<shake>();

                //StartCoroutine(fightShake.Shake(.4f, .2f));
                Destroy(fightSmoke, 2.0f);

                if (h1 > h2)
                {
                    rb.gameObject.GetComponent<EnemyController>().SpeedUp(speedGainAfterFight);
                }
                else
                {
                    other.gameObject.GetComponent<EnemyController>().SpeedUp(speedGainAfterFight);
                }
               
                TakeDamage(h2);
                other.gameObject.GetComponent<EnemyController>().TakeDamage(h1);

                FindObjectOfType<AudioManager>().Play("FightSFX");
            }



            //Vector3 pos = rb.transform.position;

            //if (rb.GetComponent<SpriteRenderer>().color.a != other.collider.GetComponent<SpriteRenderer>().color.a)  
            //    // ****** only the higher transparent one will call this function, remember to change it for new enemy prefab ******
            //{
            //    StartCoroutine(StopMovingForSecond(.5f));
            //    float h1 = GetHealth();
            //    float h2 = other.gameObject.GetComponent<EnemyController>().GetHealth();
            //    if (h1 == h2)   // h1 = h2 = 0
            //    {
            //        if (isshake == false)
            //        {
            //            isshake = true;

            //            GameObject fS = Instantiate(fight, pos, Quaternion.identity);

            //            shake fightShake = fS.GetComponent<shake>();

            //            StartCoroutine(fightShake.Shake(.4f, .4f));
            //            Destroy(fS, .5f);
            //            //Destroy(fightShake);
            //        }
            //        TakeDamage(h2);
            //        other.gameObject.GetComponent<EnemyController>().TakeDamage(h1);
            //    }
            //    else if (h1 > h2)   // h1 = 0, h2 -= h1, speed of h2 += speedGainAfterFight
            //    {
            //        TakeDamage(h2);
            //        other.gameObject.GetComponent<EnemyController>().TakeDamage(h1);
            //        other.gameObject.GetComponent<EnemyController>().SpeedUp(speedGainAfterFight);

            //        if (isshake == false)
            //        {
            //            isshake = true;

            //            shake fightShake;

            //            GameObject fS = Instantiate(fight, rb.transform.position, Quaternion.identity);
            //            fS.transform.parent = rb.gameObject.transform;
            //            fightShake = fS.GetComponent<shake>();

            //            StartCoroutine(fightShake.Shake(.4f, .4f));
            //            Destroy(fS, .5f);
            //            Destroy(fightShake, .6f);
            //        }

            //    }
            //    //else    // h1 -= h2, h2 = 0, speed of h1 += speedGainAfterFight
            //    //{
            //    //    if (isshake == false)
            //    //    {
            //    //        isshake = true;
            //    //        StartCoroutine(StopMovingForSecond(.5f));
            //    //        shake fightShake;

            //    //        GameObject fS = Instantiate(fight, gameObject.transform.position, Quaternion.identity);

            //    //        fightShake = fS.GetComponent<shake>();

            //    //        StartCoroutine(fightShake.Shake(.4f, .2f));
            //    //        Destroy(fS, .5f);
            //    //        Destroy(fightShake);
            //    //    }
            //    //    rb.gameObject.GetComponent<EnemyController>().SpeedUp(speedGainAfterFight);
            //    //    TakeDamage(h2);
            //    //    other.gameObject.GetComponent<EnemyController>().TakeDamage(h2);
            //    //}
            //    isshake = false;
            //    FindObjectOfType<AudioManager>().Play("EnemyFightSFX");
            //}
        }

        else if (other.collider.CompareTag("Player"))   // Player will be sent back home when collides enemy.
        {
            StartCoroutine(cameraShake.Shake(.3f, .2f)); //shake camera if enemy and player meet for .3 second

            other.collider.GetComponent<PlayerController>().FlashBackBase();
        }
    }

    IEnumerator FightDelay()
    {
        float temp = movespeed;
        movespeed = 0;
        yield return new WaitForSeconds(.5f);
        movespeed = temp;
        yield return null;
    }

    public void SpeedUp(float v)
    {
        movespeed += v;
    }

    public void TakeDamage(float h)
    {
        health -= h;
        if (health < 0)
        {
            health = 0;
        }

        healthBar.fillAmount = health / maxHealth;
        if (health == 0)
        {
            if (Random.Range(0f,1f) <= dropRate)
            {
                var pickUp = Instantiate(itemsDrop[Random.Range(0, itemsDrop.Length)], gameObject.transform.position, Quaternion.identity);
            }

            if (rb.name[0] == 'R')
            {
                GameObject go = Instantiate(RabbitDie, rb.transform.position, Quaternion.identity);
                Destroy(go, 3);
            }
            else
            {
                GameObject go = Instantiate(LionDie, rb.transform.position, Quaternion.identity);
                Destroy(go, 3);
            }

            Destroy(gameObject);
            LevelManager.instance.KillEnemy(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Base"))   // lose when enemy reach the base
        {
            LevelManager.instance.AttackBase();
            Destroy(gameObject);
            LevelManager.instance.KillEnemy(1);
        }

        if (other.CompareTag("Trap"))
        {
            rb.mass += 100;
            Destroy(other.gameObject);
            StartCoroutine(StopMovingForSecond(trappedTime));
            Invoke("ResumeMass", trappedTime);

            if (rb.gameObject.name[0] == 'L')
            {
                FindObjectOfType<AudioManager>().Play("LionHurtSFX");
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("rabbitHurtSFX");
            }
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
                Debug.Log("This enemy has been teleported within 2 seconds.");
                return;
            }
            else
            {
                GameObject g1 = GameObject.FindGameObjectsWithTag("Portal")[0];
                GameObject g2 = GameObject.FindGameObjectsWithTag("Portal")[1];
                transform.position = other.gameObject == g1 ? g2.transform.position : g1.transform.position;
                nextTeleportTimer = 0;
                FindObjectOfType<AudioManager>().Play("TeleportSFX");
            }
        }
    }

    void ResumeMass()
    {
        rb.mass -= 100;
    }

    IEnumerator StopMovingForSecond(float t)
    {
        float temp = movespeed;
        movespeed = 0;
        yield return new WaitForSeconds(t);
        movespeed = temp;
        
    }

    public float GetHealth()
    {
        return health;
    }

    public void SufferWind()
    {
        StartCoroutine(ChangeTarget());
    }

    IEnumerator ChangeTarget()
    {
        target = spawnTransform;
        yield return new WaitForSeconds(windLastTime);
        target = GameObject.FindGameObjectWithTag("Base").transform;
    }


    public void SufferThunder()
    {
        TakeDamage(ThunderDmg);
    }

    public void SufferBait()
    {
        StartCoroutine(Baited());
 
    }

    IEnumerator Baited()
    {
        GameObject bait = GameObject.FindGameObjectWithTag("bait_enemy");
        target = bait.transform;
        yield return new WaitForSeconds(baitLastTime);
        target = GameObject.FindGameObjectWithTag("Base").transform;
        Destroy(bait);
    }
}
