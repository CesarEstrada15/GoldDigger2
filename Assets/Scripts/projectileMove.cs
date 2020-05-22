using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMove : MonoBehaviour
{
    private Vector2 target;
    private Vector2 self;
    private int damage;
    private GameObject player;
    private GameObject Emitter;
    private float proSpeed;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        self = transform.position;
        
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && player.GetComponent<moveExcavator>().paused != true)
        {
            Vector2 directionTarget;
            if (Emitter != player)
            {
                directionTarget = target - self;
            }
            else
            {
                directionTarget = target;
            }
            rb.AddForce(directionTarget * proSpeed, ForceMode2D.Impulse);

            StartCoroutine("destroy");
        }
    }
    public void getProjectieInfo(Vector2 t, int dmg, float pS, GameObject sender)
    {
        target.x = t.x;
        target.y = t.y;
        //target.z = t.z;

        damage = dmg;

        proSpeed = pS;
        Emitter = sender;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
       
       
        if (Emitter != collision.gameObject)
        {
            //NEED TO MAKE ARRAY WITH ALL ENEMY GAMEOBJECTS
            if (collision.gameObject.tag == "greenCrawler" || collision.gameObject.tag == "orangeCrawler" || collision.gameObject.tag == "purpleCrawler" || collision.gameObject.tag == "LeftBoss" || collision.gameObject.tag == "rightBoss" || collision.gameObject.tag == "finalBoss")
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


    IEnumerator destroy()
    {

        
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);

    }

}
