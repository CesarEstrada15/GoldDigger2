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

    Light spotLight;

    //Excavator capabilities
    int speed = 2;
    private int maxHealth = 100;
    private int health = 100;

    //Resources
    int bronzeScore = 0;
    int silverScore = 0;
    int goldScore = 0;
    int gas = 100;

    //UI
    GameObject panel;
    GameObject mainUI;
    Text bronzeTxt;
    Text silverTxt;
    Text goldTxt;
    Text bronzeU;
    Text silverU;
    Text goldU;
    Text speedlvltxt;
    Text LRlvltxt;
    Text hplvltxt;
    Text weaponlvltxt;
    Text bronzeCost;
    Text silverCost;
    Text goldCost;

    bool panelActive = false;

    //Useful variable
    private float depletionRate = 0.0001f;
    public bool paused = false;

    //Leveling
    private int speedLvl = 1;
    private int LRlvl = 1;
    private int healthlvl = 1;
    private int weaponlvl = 0;

    public struct levelCost{
        public int bronze;
        public int silver;
        public int gold;
    }

    levelCost spc;
    levelCost lrc;
    levelCost hpc;
    levelCost wpc;
   

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        mainUI = GameObject.Find("Canvas");
        panel = GameObject.Find("Panel");
        spotLight = GameObject.Find("Spot Light").GetComponent<Light>();
        spotLight.type = LightType.Spot;
        Debug.Log(spotLight.spotAngle);

        bronzeTxt = GameObject.Find("Bronze").GetComponent<Text>();
        silverTxt = GameObject.Find("Silver").GetComponent<Text>();
        goldTxt = GameObject.Find("Gold").GetComponent<Text>();
        bronzeU = GameObject.Find("Bronze2").GetComponent<Text>();
        silverU = GameObject.Find("Silver2").GetComponent<Text>();
        goldU = GameObject.Find("Gold2").GetComponent<Text>();
        speedlvltxt = GameObject.Find("SpeedLVL").GetComponent<Text>();
        LRlvltxt = GameObject.Find("LightRangeLVL").GetComponent<Text>();
        hplvltxt = GameObject.Find("HealthLVL").GetComponent<Text>();
        weaponlvltxt = GameObject.Find("WeaponLVL").GetComponent<Text>();
        bronzeCost = GameObject.Find("BronzeCostNum").GetComponent<Text>();
        silverCost = GameObject.Find("SilverCostNum").GetComponent<Text>();
        goldCost = GameObject.Find("GoldCostNum").GetComponent<Text>();
        panel.SetActive(panelActive);


        //Setting initial leveling costs
        spc.bronze = 8;
        spc.silver =5;
        spc.gold = 3;

        lrc.bronze = 10;
        lrc.silver = 7;
        lrc.gold = 6;

        hpc.bronze =13;
        hpc.silver = 10;
        hpc.gold = 8;

        wpc.bronze = 17;
        wpc.silver = 13;
        wpc.gold = 11;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
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

            }
            else
            {
                //End Game cause you ran out of fuel
                Application.Quit();
            }

           
        }
        if (Input.GetKeyDown("backspace"))
        {
            Debug.Log("worked");
            panel.SetActive(!panelActive);
            mainUI.SetActive(panelActive);
            panelActive = !panelActive;
            
            paused = !paused;

            if (paused)
            {
                updateTextUpgrades();
            }
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
            updateTextMainUI();
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
                healthbar.setSize((float)health/maxHealth);
                Debug.Log((float)health / maxHealth);
                
            }
            else
            {
                Application.Quit();
            }
            
            Destroy(collision.gameObject);
        }
    }

    private void updateTextMainUI()
    {
        bronzeTxt.text = bronzeScore.ToString();
        silverTxt.text = silverScore.ToString();
        goldTxt.text = goldScore.ToString();
    }
    private void updateTextUpgrades()
    {
        bronzeU.text = bronzeTxt.text;
        silverU.text = silverTxt.text;
        goldU.text = goldTxt.text;
    }

    public void leveler(int a)
    {
        //speedLeveler
        if( a == 0)
        {
            switch(speedLvl){
                case 1:
                    speed += 1;
                    speedLvl++;
                    spc.bronze += 4;
                    spc.silver += 3;
                    spc.gold += 2;

                    break;
                case 2:
                    speed += 2;
                    speedLvl++;
                    spc.bronze += 5;
                    spc.silver += 3;
                    spc.gold += 2;
                    break;
                case 3:
                    speed += 3;
                    speedLvl++;
                    spc.bronze = 0;
                    spc.silver = 0;
                    spc.gold = 0;
                    speedlvltxt.text = "MAX";
                    break;
            }
            updateCostText(a);
            speedlvltxt.text = speedLvl.ToString();
        } else if ( a == 1) //LR leveler
        {
            switch (LRlvl)
            {
                case 1:
                    spotLight.spotAngle += 3;
                    LRlvl++;
                    break;
                case 2:
                    spotLight.spotAngle += 4;
                    LRlvl++;
                    break;
                case 3:
                    spotLight.spotAngle += 5;
                    LRlvl++;
                    break;
            }
            LRlvltxt.text = LRlvl.ToString();
        }
        else if (a == 2) //Health leveler
        {
            switch (healthlvl)
            {
                case 1:
                    maxHealth = 120;
                    health = maxHealth;
                    healthbar.setSize(1f);
                    healthlvl++;
                    break;
                case 2:
                    maxHealth = 150;
                    health = maxHealth;
                    healthbar.setSize(1f);
                    healthlvl++;
                    break;
                case 3:
                    maxHealth = 200;
                    health = maxHealth;
                    healthbar.setSize(1f);
                    healthlvl++;
                    break;
            }
            hplvltxt.text = healthlvl.ToString();
        } else
        {
            switch (weaponlvl)
            {
                case 0:
                    health = 120;
                    healthbar.setSize(1f);
                    healthlvl++;
                    break;
                case 1:
                    health = 150;
                    healthbar.setSize(1f);
                    healthlvl++;
                    break;
                case 2:
                    health = 200;
                    healthbar.setSize(1f);
                    healthlvl++;
                    break;
            }
            weaponlvltxt.text = weaponlvl.ToString();
        }
    }

    public void updateCostText(int a)
    {
        switch (a)
        {
            case 0 :
                bronzeCost.text = spc.bronze.ToString();
                silverCost.text = spc.silver.ToString();
                goldCost.text = spc.gold.ToString();
                break;
            case 1:
                bronzeCost.text = lrc.bronze.ToString();
                silverCost.text = lrc.silver.ToString();
                goldCost.text = lrc.gold.ToString();
                break;
            case 2:
                bronzeCost.text = hpc.bronze.ToString();
                silverCost.text = hpc.silver.ToString();
                goldCost.text = hpc.gold.ToString();
                break;
            case 3:
                bronzeCost.text = wpc.bronze.ToString();
                silverCost.text = wpc.silver.ToString();
                goldCost.text = wpc.gold.ToString();
                break;
        }
    }


}
