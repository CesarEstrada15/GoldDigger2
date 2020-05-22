using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crawlerMovement : MonoBehaviour
{
    private GameObject target;
    public GameObject projectile;
    public GameObject obsidianshield;
    public GameObject greenGoo;
    public GameObject purpleGoo;
    public GameObject orangeGoo;
    public GameObject area;
    public moveExcavator playerScript;
    private bool paused;
    public AudioClip moveAttackSound;
    public AudioClip shootSound;
    public AudioClip deathSound;
    public GameObject eggFull;
    public GameObject eggBroken;
    [SerializeField] public ParticleSystem explo;
    [SerializeField] public GameObject greenCrawler;
    [SerializeField] public GameObject purpleCrawler;
    [SerializeField] public GameObject orangeCrawler;
    [SerializeField] public GameObject greenEgg;
    [SerializeField] public GameObject purpleEgg;
    [SerializeField] public GameObject orangeEgg;
    [SerializeField] public healthbarController healthbar;
   

    private int health;
    private int maxHealth;
    private float speed;
    float SavedTime = 0;
    float DelayTime = 1.3f;
    float DelayTime2 = 1.0f;
    float DelayTime3 = 0.8f;

    //For green Boss movement

    private float speedAttack = 0.01f;
    
    private bool resting = false;
    public bool greenRange = false;
    public bool greenArena = false;
    public bool orangeArena = false;
    public bool orangeRange = false;
    public int callInFightOnce = 1;

    //FOR purple boss

    private bool inFight = false;
    private float movemenetSpeed;
    private Vector2[] pathList = new Vector2[8];
    
    private Vector2 currentVect;
    private int currentPoint;
    private bool pathing = true;
    private bool spawning = true;
    private bool inFight2 = false;


    // Start is called before the first frame update
    void Start()
    {
        //moveAttackSound = Resources.Load<AudioClip>("Resources/BossScreech");
        pathList[0] = new Vector2(52.5f, -50f);
        pathList[1] = new Vector2(38.6f, -46.5f);
        pathList[2] = new Vector2(27f, -39f);
        pathList[3] = new Vector2(24.5f, -13f);
        pathList[4] = new Vector2(46.6f, -6f);
        pathList[5] = new Vector2(88f, -6f);
        pathList[6] = new Vector2(88f, -42f);
        pathList[7] = new Vector2(66.4f, -42.2f);
        
        target = GameObject.Find("player");
        healthbar = transform.Find("HealthBar").GetComponent<healthbarController>();
        playerScript = target.GetComponent<moveExcavator>();
        if (this.gameObject.tag == "greenCrawler")
        {
            maxHealth = 50;
            health = 50;
            speed = 0.05f;
        } else if (this.gameObject.tag == "purpleCrawler")
        {
            health = 30;
            maxHealth = 30;
            speed = .075f;
        } else if (this.gameObject.tag == "orangeCrawler")
        {
            health = 80;
            maxHealth = 80;
            speed = .085f;
        } else if (this.gameObject.tag == "LeftBoss")
        {
            maxHealth = 500;
            health = 500;
        } else if(this.gameObject.tag == "rightBoss")
        {
            maxHealth = 300;
            health = 300;
            currentVect = pathList[0];
            currentPoint = 0;
        } else if(this.gameObject.tag == "finalBoss")
        {
            maxHealth = 750;
            health = 750;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Arena: " + orangeArena);
        Debug.Log("Range: " + orangeRange);
        // Debug.Log(greenArena);
        //Debug.Log(greenRange);
        //Green Boss
        if (target != null && target.GetComponent<moveExcavator>().paused != true) 
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
           // transform.LookAt(target.transform.position);
            if(this.gameObject.tag == "LeftBoss")
            {
                
                float distanceToPlayer = (transform.position - target.transform.position).magnitude;
                if(distanceToPlayer <= 15 && greenArena == true)
                {
                    if (callInFightOnce == 1)
                    {
                       // target.GetComponent<moveExcavator>().inFight = true;
                       // target.GetComponent<moveExcavator>().inFightChange();

                        callInFightOnce = 0;
                    }
                    OnTriggerStay();
                    
                    if (greenRange == true && resting == false)
                    {
                        //transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 0.01f);
                        StartCoroutine("moveAttackEnum", 5);
                    }
                }
                else
                {
                    if (callInFightOnce == 0)
                    {

                       // target.GetComponent<moveExcavator>().inFight = false;
                       // target.GetComponent<moveExcavator>().outFight();
                        callInFightOnce = 1;
                    }
                }
                
            }
            //Purple Boss
            if(this.gameObject.tag == "rightBoss")
            {
                
                float distanceToPlayer = (transform.position - target.transform.position).magnitude;
                if (distanceToPlayer <= 15 )
                {
                    if (callInFightOnce == 1)
                    {
                        inFight = true;
                       // target.GetComponent<moveExcavator>().inFight = true;
                       // target.GetComponent<moveExcavator>().inFightChange();
                        callInFightOnce = 0;
                    }


                }
                else
                {
                    if (callInFightOnce == 0)
                    {
                        inFight = false;
                       // target.GetComponent<moveExcavator>().inFight = false;
                       // target.GetComponent<moveExcavator>().outFight();
                        callInFightOnce = 1;
                    }
                }
                    if (!inFight)
                {
                    
                        if (pathing)
                    {
                        //StartCoroutine(followPath(this.gameObject));
                        StartCoroutine("moveAttackEnum2");
                    }

                } else
                {
                    OnTriggerStay2();
                    if (spawning)
                    {
                        StartCoroutine("spawnCrawler", this.gameObject);
                    }
                }
            }

            //Orange Boss
            if(this.gameObject.tag == "finalBoss")
            {
             

                float distanceToPlayer = (transform.position - target.transform.position).magnitude;

                if (distanceToPlayer <= 25 && orangeRange == true)
                {
                    if (callInFightOnce == 1)
                    {
                        // target.GetComponent<moveExcavator>().inFight = true;
                        // target.GetComponent<moveExcavator>().inFightChange();
                        // callInFightOnce = 0;
                    }
                    if (spawning) { 
                    StartCoroutine("spawnCrawler", this.gameObject);
                    }
                    OnTriggerStay3();
                    if (orangeArena == true && resting == false)
                    {
                        //transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 0.01f);
                        StartCoroutine("moveAttackEnum", 3);
                    }
                }
                else
                {
                    if (callInFightOnce == 0)
                    {
                       // target.GetComponent<moveExcavator>().inFight = false;
                       // target.GetComponent<moveExcavator>().outFight();
                       // callInFightOnce = 1;
                    }
                }
            }
        }
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
        
    }
    

    public void takeDamage(int dmg)
    {
        health -= dmg;
        healthbar.setSize((float)health / (float)maxHealth);
        playerScript.addDamageCount();
        
    }

    IEnumerator moveAttackEnum(float secs)
    {
        
        Vector2 tar = target.transform.position;
        //Vector2 current = transform.position;
        resting = true;
        //while (transform.position.x != tar.x && transform.position.y != tar.y)
        //{
        if (target.GetComponent<moveExcavator>().paused != true)
        {
            AudioSource.PlayClipAtPoint(moveAttackSound, target.transform.position);
            StartCoroutine(MoveTo(transform, tar, secs));
        }
       // }
        yield return new WaitForSeconds(secs);

        resting = false;

       
        
    }
    IEnumerator moveAttackEnum2()
    {
        Vector2 tar;
        if (currentPoint == 7)
        {
            tar = pathList[0];
            currentPoint = 0;

        }
        else
        {
            tar = pathList[currentPoint + 1];
            currentPoint++;
        }
       
        //Vector2 current = transform.position;
        pathing = false;
        //while (transform.position.x != tar.x && transform.position.y != tar.y)
        //{
        if (target.GetComponent<moveExcavator>().paused != true)
        {
           // AudioSource.PlayClipAtPoint(moveAttackSound, target.transform.position);
            StartCoroutine(MoveTo(transform, tar, 6));
        }
        // }
        yield return new WaitForSeconds(5);

        pathing = true;



    }

    IEnumerator MoveTo(Transform mover, Vector2 destination, float speed)
    {
        // This looks unsafe, but Unity uses
        // en epsilon when comparing vectors.
        while ((Vector2)mover.position != destination )
        {
            mover.position = Vector2.MoveTowards(
                mover.position,
                destination,
                speed * Time.deltaTime);
            // Wait a frame and move again.
            yield return null;
        }
    }

    //SpawnEnemies around
    IEnumerator spawnCrawler(GameObject boss)
    {
        
        spawning = false;
        if(boss.tag == "rightBoss")
        {
            Instantiate(eggFull, transform.position + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), Quaternion.identity);
            yield return new WaitForSeconds(5);
        }
        else if(boss.tag == "finalBoss")
        {
            Instantiate(eggFull, transform.position + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0), Quaternion.identity);
            yield return new WaitForSeconds(3);
        }
       
        spawning = true;
    }

    void OnTriggerStay()
    {

        if ((Time.time - SavedTime) > DelayTime)
        {
            SavedTime = Time.time;

            //Anything in here will be called every two seconds        
            GameObject go = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
            go.GetComponent<projectileMove>().getProjectieInfo(target.transform.position, 50, 0.07f, this.gameObject);
           
        }

    }
    void OnTriggerStay2()
    {

        if ((Time.time - SavedTime) > DelayTime2)
        {
            SavedTime = Time.time;

            //Anything in here will be called every two seconds        
            GameObject go = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
            go.GetComponent<projectileMove>().getProjectieInfo(target.transform.position, 50, 0.08f, this.gameObject);

        }

    }
    void OnTriggerStay3()
    {

        if ((Time.time - SavedTime) > DelayTime3)
        {
            SavedTime = Time.time;

            //Anything in here will be called every two seconds        
            GameObject go = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
            go.GetComponent<projectileMove>().getProjectieInfo(target.transform.position, 50, 0.09f, this.gameObject);

        }

    }


    public float getHealth()
    {
        return health;
    }

    public void setGreenArena(bool a)
    {
       
        greenArena = a;
    }

    public void setGreenRange(bool a)
    {
        
        greenRange = a;
    }
    public void setOrangeArena(bool a)
    {
        orangeArena = a;
    }
    public void setOrangeRange(bool a)
    {
        orangeRange = a;
    }
    private void OnDestroy()
    {
       
        if(this.gameObject.tag == "LeftBoss")
        {
            if (this.getHealth() > 0)
            {
            }
            else
            {
                target.GetComponent<moveExcavator>().setGreenKilled(true);
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
                Instantiate(explo, transform.position, Quaternion.identity);
                GameObject shieldDrop = Instantiate(obsidianshield, transform.position, Quaternion.identity) as GameObject;
            }
        }
        if(this.gameObject.tag == "rightBoss")
        {
            if (this.getHealth() > 0)
            {
            }
            else
            {
                target.GetComponent<moveExcavator>().setPurpleKilled(true);
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
                Instantiate(explo, this.gameObject.transform);
               
            }
        }
        if (this.gameObject.tag == "finalBoss")
        {
            if (this.getHealth() > 0)
            {
            }
            else
            {
                target.GetComponent<moveExcavator>().setOrangeKilled(true);
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
                Instantiate(explo, this.gameObject.transform);

            }
        }
        if (this.gameObject.tag == "greenCrawler")
        {
            Instantiate(greenGoo, transform.position, Quaternion.identity);
        }
        if (this.gameObject.tag == "purpleCrawler")
        {
            Instantiate(purpleGoo, transform.position, Quaternion.identity);
        }
        if (this.gameObject.tag == "orangeCrawler")
        {
            Instantiate(orangeGoo, transform.position, Quaternion.identity);
        }
    }


    
    
}
