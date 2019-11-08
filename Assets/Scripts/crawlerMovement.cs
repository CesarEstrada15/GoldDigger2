using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crawlerMovement : MonoBehaviour
{
    private GameObject target;
    public GameObject projectile;
    private int health;
    private float speed;
    float SavedTime = 0;
    float DelayTime = 1.3f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("player");
        if(this.gameObject.tag == "greenCrawler")
        {
            health = 50;
            speed = 0.01f;
        } else if (this.gameObject.tag == "prupleCrawler")
        {
            health = 30;
            speed = .05f;
        } else if (this.gameObject.tag == "orangeCrawler")
        {
            health = 80;
            speed = .03f;
        } else if (this.gameObject.tag == "LeftBoss")
        {
            health = 1000;
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
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        
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
}
