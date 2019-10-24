using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveExcavator : MonoBehaviour
{
    int speed = 10;

    int bronzeScore = 0;
    int silverScore = 0;
    int goldScore = 0;
    Canvas can;
    Text bronzeTxt;
    Text silverTxt;
    Text goldTxt;
    private Rigidbody2D rb2d;

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
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            float temp = Random.value;
            if (collision.gameObject.transform.position.y > -50)
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
                    else
                    {
                        goldScore++;
                    }
                }
            } else if(collision.gameObject.transform.position.y < -50 && collision.gameObject.transform.position.y > -100)
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
                    else
                    {
                        goldScore++;
                    }
                }
            }
            else if(collision.gameObject.transform.position.y < -100 && collision.gameObject.transform.position.y > -150)
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
                    else
                    {
                        goldScore++;
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
                    else
                    {
                        goldScore++;
                    }
                }
            }
            updateText();
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
