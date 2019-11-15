using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMove : MonoBehaviour
{
    private Vector3 target;
    private Vector3 self;
    private int damage;
    private GameObject player;
    private GameObject Emitter;
    private float proSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        self = transform.position;
        Debug.Log("SELF : " + self);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && player.GetComponent<moveExcavator>().paused != true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, proSpeed);
            if(transform.position == target)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void getProjectieInfo(Vector3 t, int dmg, float pS, GameObject sender)
    {
        target.x = t.x;
        target.y = t.y;
        target.z = t.z;

        damage = dmg;

        proSpeed = pS;
        Emitter = sender;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
       
       
        if (Emitter != collision.gameObject)
        {
            //NEED TO MAKE ARRAY WITH ALL ENEMY GAMEOBJECTS
            if (collision.gameObject.tag == "greenCrawler" || collision.gameObject.tag == "orangeCrawler" || collision.gameObject.tag == "purpleCrawler" || collision.gameObject.tag == "LeftBoss")
            {
                Debug.Log(collision.gameObject.GetComponent<crawlerMovement>().getHealth());
                collision.gameObject.GetComponent<crawlerMovement>().takeDamage(damage);
                
                Destroy(this.gameObject);
            }
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<moveExcavator>().takeDamage(damage);
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator ExecuteAfterTime(float time, GameObject go, Vector3 v)
    {
        yield return new WaitForSeconds(time);
        go.transform.localScale = v;
        // Code to execute after the delay
    }

    
}
