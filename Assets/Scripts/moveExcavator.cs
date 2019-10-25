using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveExcavator : MonoBehaviour
{

    //game oBjects
    private Rigidbody2D rb2d;
    [SerializeField] public healthbarController healthbar;
    [SerializeField] public healthbarController gasResource;

    [SerializeField] public GameObject greenCrawler;
    [SerializeField] public GameObject purpleCrawler;
    [SerializeField] public GameObject orangeCrawler;

    //Excavator capabilities
    int speed = 10;
    private int health = 100;

    //Resources
    int bronzeScore = 0;
    int silverScore = 0;
    int goldScore = 0;
    int gas = 100;

    //UI
    Canvas can;
    Text bronzeTxt;
    Text silverTxt;
    Text goldTxt;

    //Useful variable
    private float depletionRate = 0.0001f;
    
    

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        can = gameObject.GetComponent<Canvas>();

        bronzeTxt = GameObject.Find("Bronze").GetComponent<Text>();
        silverTxt = GameObject.Find("Silver").GetComponent<Text>();
        goldTxt = GameObject.Find("Gold").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 2.5)
        {
            transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0f);
        }
        else
        {
          
        }
        if (gasResource.currentSize > 0)
        {
            gasResource.setSize(gasResource.currentSize - depletionRate);
            
        } else
        {
            //End Game cause you ran out of fuel
            Application.Quit();
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            float temp = Random.value;
            
            if (collision.gameObject.transform.position.y > -35)
            {
                
                if(temp > 0.95)
                {
                    if(temp < 0.975)
                    {
                        bronzeScore++;
                    } else if(temp > 0.975 && temp < 0.99)
                    {
                        silverScore++;
                    }
                   else if (temp > 0.99){
                        goldScore++;
                        if(temp > 0.995)
                        {
                            Instantiate(greenCrawler, collision.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
                        }

                    } else
                    {

                    }
                } 
               
            } else if(collision.gameObject.transform.position.y < -35 && collision.gameObject.transform.position.y > -60)
            {
                if (temp > 0.90)
                {
                    if (temp < 0.95)
                    {
                        bronzeScore++;
                    }
                    else if (temp > 0.95 && temp < 0.98)
                    {
                        silverScore++;
                    }
                    else if (temp > 0.98)
                    {
                        goldScore++;
                        if (temp > 0.99)
                        {
                            Instantiate(greenCrawler, collision.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
                        }

                    }
                    else
                    {

                    }
                }
            }
            else if(collision.gameObject.transform.position.y < -60 && collision.gameObject.transform.position.y > -75)
            {
                if (temp > 0.85)
                {
                    if (temp < 0.93)
                    {
                        bronzeScore++;
                    }
                    else if (temp > 0.93 && temp < 0.97)
                    {
                        silverScore++;
                    }
                    else if (temp > 0.97)
                    {
                        goldScore++;
                        if (temp > 0.975)
                        {
                            Instantiate(greenCrawler, collision.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
                        }

                    }
                    else
                    {

                    }
                }
            }
            else
            {
                if (temp > 0.75)
                {
                    if (temp < 0.85)
                    {
                        bronzeScore++;
                    }
                    else if (temp > 0.85 && temp < 0.95)
                    {
                        silverScore++;
                    }
                    else if (temp > 0.95)
                    {
                        goldScore++;
                        if (temp > 0.965)
                        {
                            Instantiate(greenCrawler, collision.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
                        }

                    }
                    else
                    {

                    }
                }
            }
            updateText();
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "GAS")
        {
            gasResource.currentSize = 1;
            gasResource.setSize(1);
            Destroy(collision.gameObject);
            depletionRate = depletionRate * 2;
        }

        if (collision.gameObject.tag == "greenCrawler") 
        {
            
            if (health > 10)
            {
                Debug.Log(health);
                health = health - 10;
                healthbar.setSize(healthbar.currentSize - .1f);
                healthbar.currentSize -= 0.05f;
                
            }
            else
            {
                Application.Quit();
            }
            
            Destroy(collision.gameObject);
        }
    }

    private void updateText()
    {
        bronzeTxt.text = bronzeScore.ToString();
        silverTxt.text = silverScore.ToString();
        goldTxt.text = goldScore.ToString();
    }
}
