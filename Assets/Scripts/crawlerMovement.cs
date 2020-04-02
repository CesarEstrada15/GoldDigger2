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

    private int health;
    private float speed;
    float SavedTime = 0;
    float DelayTime = 1.3f;

    //For green Boss movement

    private float speedAttack = 0.01f;
    
    private bool resting = false;
    public bool greenRange = false;
    public bool greenArena = false;

    //FOR purple boss

    private bool inFight = false;
    private float movemenetSpeed;
    private Vector2[] pathList = new Vector2[8];
    private Vector2 currentVect;
    private int currentPoint;
    private bool pathing = true;


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
        playerScript = target.GetComponent<moveExcavator>();
        if (this.gameObject.tag == "greenCrawler")
        {
            health = 50;
            speed = 0.015f;
        } else if (this.gameObject.tag == "purpleCrawler")
        {
            health = 30;
            speed = .02f;
        } else if (this.gameObject.tag == "orangeCrawler")
        {
            health = 80;
            speed = .03f;
        } else if (this.gameObject.tag == "LeftBoss")
        {
            
            health = 500;
        } else if(this.gameObject.tag == "rightBoss")
        {
            health = 300;
            currentVect = pathList[0];
            currentPoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(greenArena);
        //Debug.Log(greenRange);
        if (target != null && target.GetComponent<moveExcavator>().paused != true) 
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
           // transform.LookAt(target.transform.position);
            if(this.gameObject.tag == "LeftBoss")
            {
                
                float distanceToPlayer = (transform.position - target.transform.position).magnitude;
                if(distanceToPlayer <= 15 && greenArena == true)
                {
                    
                    OnTriggerStay();
                    if (greenRange == true && resting == false)
                    {
                        //transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 0.01f);
                        StartCoroutine("moveAttackEnum");
                    }
                }
                
            }
            if(this.gameObject.tag == "rightBoss")
            {
                if (!inFight)
                {
                    transform.position = Vector2.MoveTowards(transform.position, pathList[currentPoint], 1000);
                    if (pathing)
                    {
                        StartCoroutine(followPath(this.gameObject));
                    }

                } else
                {

                }
            }
        }
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "greenBounce")
        {
          
        }
    }

    public void takeDamage(int dmg)
    {
        health -= dmg;
        playerScript.addDamageCount();
        
    }

    IEnumerator moveAttackEnum()
    {
        
        Vector2 tar = target.transform.position;
        Vector2 current = transform.position;
        resting = true;
        //while (transform.position.x != tar.x && transform.position.y != tar.y)
        //{
        if (target.GetComponent<moveExcavator>().paused != true)
        {
            AudioSource.PlayClipAtPoint(moveAttackSound, target.transform.position);
            StartCoroutine(MoveTo(transform, tar, 5));
        }
       // }
        yield return new WaitForSeconds(5);

        resting = false;

       
        
    }

    IEnumerator MoveTo(Transform mover, Vector2 destination, float speed)
    {
        // This looks unsafe, but Unity uses
        // en epsilon when comparing vectors.
        while ((Vector2)mover.position != destination && inFight != true)
        {
            mover.position = Vector2.MoveTowards(
                mover.position,
                destination,
                speed * Time.deltaTime);
            // Wait a frame and move again.
            yield return null;
        }
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

    public float getHealth()
    {
        return health;
    }

    public void setGreenArena(bool a)
    {
        Debug.Log("calledsetArena");
        greenArena = a;
    }

    public void setGreenRange(bool a)
    {
        Debug.Log("calledsetrange");
        greenRange = a;
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
                GameObject shieldDrop = Instantiate(obsidianshield, transform.position, Quaternion.identity) as GameObject;
            }
        }
        if(this.gameObject.tag == "greenCrawler")
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


    IEnumerator followPath(GameObject monster)
    {
        pathing = false;
        if(currentPoint == 7)
        {
            MoveTo(monster.transform, pathList[0], 5);
            currentPoint = 0;
            
        } else
        {
            MoveTo(monster.transform, pathList[currentPoint+1], 5);
            currentPoint++;
        }
        

        yield return new WaitForSeconds(5);
        pathing = true;
    }
    
}
