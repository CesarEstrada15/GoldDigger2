using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveExcavator : MonoBehaviour
{

    //game oBjects
    private Rigidbody2D rb2d;
    [SerializeField] public Camera cam;
    [SerializeField] public healthbarController healthbar;
    [SerializeField] public healthbarController gasResource;
    [SerializeField] public GameObject projectile;
    [SerializeField] public AudioClip all;
    [SerializeField] public AudioClip boss;

    
    [SerializeField] public GameObject crosshair;
    [SerializeField] public GameObject greenCrawler;
    [SerializeField] public GameObject purpleCrawler;
    [SerializeField] public GameObject orangeCrawler;

    Light spotLight;
    public AudioSource soundSource;

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


    //GUN VARIABLES
    //TEST TEST TEST TEST
    public bool gunActivated = false;
    private Vector2 mousePos;

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
        soundSource = GameObject.Find("Sound").GetComponent<AudioSource>();
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

        Cursor.visible = false;
        crosshair.SetActive(false);
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
            soundSource.mute = !soundSource.mute;
          
            panel.SetActive(!panelActive);
            mainUI.SetActive(panelActive);
            panelActive = !panelActive;
            Cursor.visible = !Cursor.visible;
            paused = !paused;

            if (paused)
            {
                updateTextUpgrades();
            }
        }

        //If guns are active move crosshair to mouse at all times
        if (gunActivated == true)
        {
            
            crosshair.SetActive(true);
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            crosshair.transform.position = (new Vector3(mousePos.x, mousePos.y, transform.position.z));

            if (Input.GetButtonDown("Fire1"))
            {
                GameObject go = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;

            go.GetComponent<projectileMove>().getProjectieInfo(crosshair.transform.position, 10, 0.1f,this.gameObject);
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
                            if (this.gameObject.transform.position.x < 0)
                            {
                                Instantiate(greenCrawler, collision.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
                            }
                            else
                            {
                                Instantiate(purpleCrawler, collision.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
                            }

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
                        if (temp > 0.985)
                        {
                            if (this.gameObject.transform.position.x < 0)
                            {
                                Instantiate(greenCrawler, collision.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
                            }
                            else
                            {
                                Instantiate(purpleCrawler, collision.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
                            }
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
                            if (this.gameObject.transform.position.x < 0)
                            {

                                Instantiate(greenCrawler, collision.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
                                Instantiate(purpleCrawler, collision.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
                            }
                            else
                            {

                                Instantiate(greenCrawler, collision.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
                                Instantiate(purpleCrawler, collision.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
                            }
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
                            Instantiate(orangeCrawler, collision.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
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
            //depletionRate = depletionRate * 2;
        }

        if (collision.gameObject.tag == "greenCrawler") 
        {
            
            if (health > 15)
            {
                Debug.Log(health);
                this.takeDamage(15);
                Debug.Log((float)health / maxHealth);
                
            }
            else
            {
                Application.Quit();
            }
            
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "purpleCrawler")
        {

            if (health > 20)
            {
                
                this.takeDamage(20);
                

            }
            else
            {
                Application.Quit();
            }

            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "orangeCrawler")
        {

            if (health > 30)
            {

                this.takeDamage(30);
                

            }
            else
            {
                Application.Quit();
            }

            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "LeftBoss")
        {
            if (health > 75)
            {

                this.takeDamage(75);


            }
            else
            {
                Application.Quit();
            }
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
                    if (bronzeScore >= spc.bronze && silverScore >= spc.silver && goldScore >= spc.gold)
                    {
                        speed += 1;
                        speedLvl++;
                        discountCost(a);
                        spc.bronze += 4;
                        spc.silver += 3;
                        spc.gold += 2;

                    }
                    break;
                case 2:
                    if (bronzeScore >= spc.bronze && silverScore >= spc.silver && goldScore >= spc.gold)
                    {
                        speed += 2;
                        speedLvl++;
                        discountCost(a);
                        spc.bronze += 5;
                        spc.silver += 3;
                        spc.gold += 2;
                    }
                    break;
                case 3:
                    if (bronzeScore >= spc.bronze && silverScore >= spc.silver && goldScore >= spc.gold)
                    {
                        speed += 3;
                        speedLvl++;
                        discountCost(a);
                        spc.bronze = 0;
                        spc.silver = 0;
                        spc.gold = 0;
                        speedlvltxt.text = "MAX";
                    }
                    break;
            }
            
            speedlvltxt.text = speedLvl.ToString();
        } else if ( a == 1) //LR leveler
        {
            switch (LRlvl)
            {
                case 1:
                    
                    if (bronzeScore >= lrc.bronze && silverScore >= lrc.silver && goldScore >= lrc.gold)
                    {
                        spotLight.spotAngle += 3;
                        LRlvl++;
                        discountCost(a);
                        lrc.bronze += 4;
                        lrc.silver += 3;
                        lrc.gold += 2;

                    }
                    break;
                case 2:
                   
                    if (bronzeScore >= lrc.bronze && silverScore >= lrc.silver && goldScore >= lrc.gold)
                    {
                        spotLight.spotAngle += 4;
                        LRlvl++;
                        discountCost(a);
                        lrc.bronze += 4;
                        lrc.silver += 3;
                        lrc.gold += 2;

                    }
                    break;
                case 3:
                    if (bronzeScore >= lrc.bronze && silverScore >= lrc.silver && goldScore >= lrc.gold)
                    {
                        spotLight.spotAngle += 5;
                        LRlvl++;
                        discountCost(a);
                        lrc.bronze = 0;
                        lrc.silver = 0;
                        lrc.gold = 0;
                        LRlvltxt.text = "MAX";

                    }
                    break;
            }
            LRlvltxt.text = LRlvl.ToString();
        }
        else if (a == 2) //Health leveler
        {
            switch (healthlvl)
            {
                case 1:
                    if (bronzeScore >= hpc.bronze && silverScore >= hpc.silver && goldScore >= hpc.gold)
                    {
                        maxHealth = 120;
                        health = maxHealth;
                        healthbar.setSize(1f);
                        healthlvl++;
                        discountCost(a);
                        hpc.bronze += 4;
                        hpc.silver += 3;
                        hpc.gold += 2;
                    }
                    break;
                case 2:
                    if (bronzeScore >= hpc.bronze && silverScore >= hpc.silver && goldScore >= hpc.gold)
                    {
                        maxHealth = 150;
                        health = maxHealth;
                        healthbar.setSize(1f);
                        healthlvl++;
                        discountCost(a);
                        hpc.bronze += 4;
                        hpc.silver += 3;
                        hpc.gold += 2;
                    }
                    break;
                case 3:
                    if (bronzeScore >= hpc.bronze && silverScore >= hpc.silver && goldScore >= hpc.gold)
                    {
                        maxHealth = 200;
                        health = maxHealth;
                        healthbar.setSize(1f);
                        healthlvl++;
                        discountCost(a);
                        hpc.bronze = 0;
                        hpc.silver = 0;
                        hpc.gold = 0;
                        hplvltxt.text = "MAX";
                    }
                    break;
            }
            hplvltxt.text = healthlvl.ToString();
        } else if (a == 3)
        {
            switch (weaponlvl)
            {
                case 0:
                    if (bronzeScore >= hpc.bronze && silverScore >= hpc.silver && goldScore >= hpc.gold)
                    {
                        gunActivated = true;
                        weaponlvl++;
                        discountCost(a);
                        wpc.bronze += 999;
                        wpc.silver += 999;
                        wpc.gold += 999;
                        
                    }
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
        updateCostText(a);
        updateTextMainUI();
        updateTextUpgrades();
          
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

    public void takeDamage(int dmg)
    {
        health -= dmg;
        healthbar.setSize((float)health / maxHealth);

    }

    public void discountCost(int a)
    {
        if(a == 0)
        {
            bronzeScore -= spc.bronze;
            silverScore -= spc.silver;
            goldScore -= spc.gold;
        } else if ( a == 1)
        {
            bronzeScore -= lrc.bronze;
            silverScore -= lrc.silver;
            goldScore -= lrc.gold;
        } else if( a == 2)
        {
            bronzeScore -= hpc.bronze;
            silverScore -= hpc.silver;
            goldScore -= hpc.gold;
        } else if (a == 3)
        {
            bronzeScore -= wpc.bronze;
            silverScore -= wpc.silver;
            goldScore -= wpc.gold;
        }
    }
}
