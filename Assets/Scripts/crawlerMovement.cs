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

    private AudioClip moveAttack;

    private int health;
    private float speed;
    float SavedTime = 0;
    float DelayTime = 1.3f;

    //For green Boss movement

    private float speedAttack = 0.01f;
    
    private bool resting = false;

    // Start is called before the first frame update
    void Start()
    {
        //  moveAttack = Resources.Load<AudioClip>("Resources/")
        target = GameObject.Find("player");
        if(this.gameObject.tag == "greenCrawler")
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (target != null && target.GetComponent<moveExcavator>().paused != true) 
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
           // transform.LookAt(target.transform.position);
            if(this.gameObject.tag == "LeftBoss")
            {
                
                float distanceToPlayer = (transform.position - target.transform.position).magnitude;
                if(distanceToPlayer <= 15)
                {
                    
                    OnTriggerStay();
                    if (target.GetComponent<moveExcavator>().getGreenRange() && resting == false)
                    {
                        //transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 0.01f);
                        StartCoroutine("moveAttack");
                    }
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
    }

    IEnumerator moveAttack()
    {
        Vector2 tar = target.transform.position;
        Vector2 current = transform.position;
        resting = true;
        //while (transform.position.x != tar.x && transform.position.y != tar.y)
        //{
        StartCoroutine(MoveTo(transform, tar, 5));
       // }
        yield return new WaitForSeconds(5);

        resting = false;

       
        
    }

    IEnumerator MoveTo(Transform mover, Vector2 destination, float speed)
    {
        // This looks unsafe, but Unity uses
        // en epsilon when comparing vectors.
        while ((Vector2)mover.position != destination)
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

            go.GetComponent<projectileMove>().getProjectieInfo(target.transform.position, 50, 0.07f, this.gameObject);
            print(DelayTime + " seconds have passed");
        }

    }

    public float getHealth()
    {
        return health;
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
}
