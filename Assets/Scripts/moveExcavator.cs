using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moveExcavator : MonoBehaviour
{

    //game oBjects
    private Rigidbody2D rb2d;
    [SerializeField] public Material obbyMat;
    [SerializeField] public Camera cam;
    [SerializeField] public healthbarController healthbar;
    [SerializeField] public healthbarController gasResource;
    [SerializeField] public GameObject projectile;
    [SerializeField] public AudioClip all;
    [SerializeField] public AudioClip boss;
    [SerializeField] public AudioClip endSound;
    [SerializeField] public GameObject overheat;
    [SerializeField] public Joystick joystick;
    [SerializeField] public Joystick joystickshoot;


    [SerializeField] public GameObject crosshair;
    [SerializeField] public GameObject greenCrawler;
    [SerializeField] public GameObject purpleCrawler;
    [SerializeField] public GameObject orangeCrawler;
    [SerializeField] public GameObject highscoreTable;
    [SerializeField] public GameObject highscoreTableReal;
    [SerializeField] public GameObject enterName;


    public GameObject leftBoss;
    public GameObject rightBoss;
    public GameObject finalBoss;

    //Texts +1
    public Text bronzeCue;
    public Text silverCue;
    public Text goldCue;




    Light spotLight;
    //Sound
    public AudioSource soundSource;
    public AudioClip BGSound;
    public AudioClip ArenaSound;
    public AudioClip oreCollected;
    public AudioClip crashSound;
    public AudioClip shootSound;

    //Excavator capabilities
    int speed = 2;
    private int maxHealth = 100;
    private int health = 100;
    private int damage = 10;

    //Resources
    int bronzeScore = 100;
    int silverScore = 100;
    int goldScore =100;
    int gas = 100;
    int greenGoo = 10;
    int purpleGoo = 10;
    int orangeGoo = 10;

    //animations
    [SerializeField] private Animator myAnim;

    //UI
    GameObject panel;
    GameObject mainUI;
    GameObject moveJoy;
    GameObject shootJoy;
    Text bronzeTxt;
    Text silverTxt;
    Text goldTxt;
    Text bronzeU;
    Text silverU;
    Text goldU;
    Text greenG;
    Text purpleG;
    Text orangeG;
    Text greenGU;
    Text purpleGU;
    Text orangeGU;
    Text speedlvltxt;
    Text LRlvltxt;
    Text hplvltxt;
    Text weaponlvltxt;
    Text bronzeCost;
    Text silverCost;
    Text goldCost;
    Text greenCost;
    Text purpleCost;
    Text orangeCost;

    bool panelActive = false;

    //Useful variable
    private float depletionRate = 0.0001f;
    public bool paused = false;
    private bool obsidianShield = false;
    private bool excavate = true;
    private bool overheating = false;
    public bool inFight = false;
    private bool inGreenRange = false;
    private float normalZoom = 5;
    private int disablehst = 0;
    private bool end = false;
    private bool endAndHS = false;
    private bool healing = false;
    private bool healingHelper = false;
    private bool deplete = false;
    private bool boost = false;
    private bool fireSpeed = false;
    



    //GUN VARIABLES
    //TEST TEST TEST TEST
    public bool gunActivated = false;
    private bool shooting = false;
    private Vector2 mousePos;

    //Highscore variables
    private int blocksDigged = 0;
    private int damageDealt = 0;
    private string nameField = "";
    public InputField inputField;

    //Leveling
    private int speedLvl = 1;
    private int LRlvl = 1;
    private int healthlvl = 1;
    private int weaponlvl = 0;

    public struct levelCost
    {
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
        moveJoy = GameObject.Find("JoyStickmove");
        shootJoy = GameObject.Find("Joystickshoot");
        soundSource = GameObject.Find("Sound").GetComponent<AudioSource>();
        spotLight = GameObject.Find("Spot Light").GetComponent<Light>();
        spotLight.type = LightType.Spot;
        leftBoss = GameObject.Find("LeftBoss");


        bronzeTxt = GameObject.Find("Bronze").GetComponent<Text>();
        silverTxt = GameObject.Find("Silver").GetComponent<Text>();
        goldTxt = GameObject.Find("Gold").GetComponent<Text>();
        bronzeU = GameObject.Find("Bronze2").GetComponent<Text>();
        silverU = GameObject.Find("Silver2").GetComponent<Text>();
        goldU = GameObject.Find("Gold2").GetComponent<Text>();
        greenG = GameObject.Find("Green").GetComponent<Text>();
        greenGU = GameObject.Find("Green2").GetComponent<Text>();
        purpleG = GameObject.Find("Purple").GetComponent<Text>();
        purpleGU = GameObject.Find("Purple2").GetComponent<Text>();
        orangeG = GameObject.Find("Orange").GetComponent<Text>();
        orangeGU = GameObject.Find("Orange2").GetComponent<Text>();
        speedlvltxt = GameObject.Find("SpeedLVL").GetComponent<Text>();
        LRlvltxt = GameObject.Find("LightRangeLVL").GetComponent<Text>();
        hplvltxt = GameObject.Find("HealthLVL").GetComponent<Text>();
        weaponlvltxt = GameObject.Find("WeaponLVL").GetComponent<Text>();
        bronzeCost = GameObject.Find("BronzeCostNum").GetComponent<Text>();
        silverCost = GameObject.Find("SilverCostNum").GetComponent<Text>();
        goldCost = GameObject.Find("GoldCostNum").GetComponent<Text>();
        greenCost = GameObject.Find("GreenCost").GetComponent<Text>();
        purpleCost = GameObject.Find("PurpleCost").GetComponent<Text>();
        orangeCost = GameObject.Find("OrangeCost").GetComponent<Text>();
        panel.SetActive(panelActive);




        //Setting initial leveling costs
        spc.bronze = 3;
        spc.silver = 2;
        spc.gold = 1;

        lrc.bronze = 3;
        lrc.silver = 2;
        lrc.gold = 1;

        hpc.bronze = 4;
        hpc.silver = 3;
        hpc.gold = 2;

        wpc.bronze = 5;
        wpc.silver = 3;
        wpc.gold = 3;

        Cursor.visible = false;
        crosshair.SetActive(false);
        overheat.SetActive(false);
        shootJoy.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (disablehst != 1) {
            highscoreTable.SetActive(false);
            enterName.SetActive(false);
            panel.SetActive(panelActive);
            disablehst = 1;
        }
        if (end && !endAndHS)
        {

            AudioSource.PlayClipAtPoint(endSound, transform.position);
            paused = true;

            enterName.SetActive(!endAndHS);
            inputField = GameObject.Find("InputField").GetComponent<InputField>();
        }


        if (!paused)
        {


            //Arrow movement
            if (transform.position.y < 2.5)
            {
                // transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0f);
                transform.Translate(joystick.Horizontal * speed * Time.deltaTime, joystick.Vertical * speed * Time.deltaTime, 0f);
            }
            //Touch
            /* if (Input.touchCount > 0) {

                 Touch touch = Input.GetTouch(0);
                 //Movement touch
                 if(touch.position.x < 0)
                 {

                 }

             }
             */
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
                //Application.Quit();

                end = true;


            }


        }
        if (transform.position.y <= -65 && obsidianShield == false)
        {
            excavate = false;
            overheat.SetActive(true);
            if (overheating == false)
            {
                overheating = true;
                StartCoroutine("Overheat");
            }

        }
        else
        {
            excavate = true;
            overheat.SetActive(false);
        }

        if (Input.GetKeyDown("backspace") && end == false)
        {
            soundSource.mute = !soundSource.mute;

            panel.SetActive(!panelActive);
            mainUI.SetActive(panelActive);
            panelActive = !panelActive;
            moveJoy.SetActive(!moveJoy.active);
            if (gunActivated)
            {
                shootJoy.SetActive(!shootJoy.active);
            }
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

            //crosshair.SetActive(true);

            //mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            //crosshair.transform.position = (new Vector3(mousePos.x, mousePos.y, transform.position.z));

            if (joystickshoot.Horizontal != 0 && joystickshoot.Vertical != 0 && shooting == false )
            {
                StartCoroutine("shoot");
            }

        }
        if (Input.GetKeyDown(KeyCode.H))
        {
           if (greenGoo > 0 && deplete == false)
            {
                StartCoroutine("depleteGoo", 0);

                
            }

        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            if(purpleGoo > 0 && boost == false)
            {
                StartCoroutine("boostSpeed");
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            if(orangeGoo > 0 && deplete == false)
            {
                StartCoroutine("depleteGoo", 2);
            }
        }
       

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Target" && excavate)
        {
            blocksDigged++;
            
            
            string tempString = collision.gameObject.GetComponent<oreSelect>().getResult();
            Debug.Log(tempString);
            if (tempString.Equals("n")) {
               
            } else if (tempString.Equals("bronze")) {
                AudioSource.PlayClipAtPoint(oreCollected, collision.gameObject.transform.position);
                Text tempTextBox = Instantiate(bronzeCue, new Vector2(transform.position.x - 5, transform.position.y + 5), Quaternion.identity) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(mainUI.transform, false);
                //Set the text box's text element font size and style:
                tempTextBox.fontSize = 20;
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "+1";
                bronzeScore++;

            } else if (tempString.Equals("silver"))
            {
                AudioSource.PlayClipAtPoint(oreCollected, collision.gameObject.transform.position);
                Text tempTextBox = Instantiate(silverCue, new Vector2(transform.position.x - 5, transform.position.y + 5), Quaternion.identity) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(mainUI.transform, false);
                //Set the text box's text element font size and style:
                tempTextBox.fontSize = 20;
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "+1";
                silverScore++;
            } else if (tempString.Equals("gold"))
            {
                AudioSource.PlayClipAtPoint(oreCollected, collision.gameObject.transform.position);
                Text tempTextBox = Instantiate(goldCue, new Vector2(transform.position.x - 5, transform.position.y + 5), Quaternion.identity) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(mainUI.transform, false);
                //Set the text box's text element font size and style:
                tempTextBox.fontSize = 20;
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "+1";
                goldScore++;
            } else if (tempString.Equals("goldgreenCrawler"))
            {
                AudioSource.PlayClipAtPoint(oreCollected, collision.gameObject.transform.position);
                Text tempTextBox = Instantiate(goldCue, new Vector2(transform.position.x - 5, transform.position.y + 5), Quaternion.identity) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(mainUI.transform, false);
                //Set the text box's text element font size and style:
                tempTextBox.fontSize = 20;
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "+1";
                goldScore++;
                Instantiate(greenCrawler, collision.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
            }
            else if (tempString.Equals("goldpurpleCrawler"))
            {
                AudioSource.PlayClipAtPoint(oreCollected, collision.gameObject.transform.position);
                Text tempTextBox = Instantiate(goldCue, new Vector2(transform.position.x - 5, transform.position.y + 5), Quaternion.identity) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(mainUI.transform, false);
                //Set the text box's text element font size and style:
                tempTextBox.fontSize = 20;
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "+1";
                goldScore++;
                Instantiate(purpleCrawler, collision.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
            }
            else if (tempString.Equals("goldorangeCrawler"))
            {
                AudioSource.PlayClipAtPoint(oreCollected, collision.gameObject.transform.position);
                Text tempTextBox = Instantiate(goldCue, new Vector2(transform.position.x - 5, transform.position.y + 5), Quaternion.identity) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(mainUI.transform, false);
                //Set the text box's text element font size and style:
                tempTextBox.fontSize = 20;
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "+1";
                goldScore++;
                Instantiate(orangeCrawler, collision.transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
            } else
            {

            }
           

            updateTextMainUI();
            
            Destroy(collision.gameObject);


        }

        if (collision.gameObject.tag == "GAS")
        {
            gasResource.currentSize = 1;
            gasResource.setSize(1);
            Destroy(collision.gameObject);
            //depletionRate = depletionRate * 2;
        }

        if (collision.gameObject.tag == "greenCrawler")
        {

           
                
                this.takeDamage(15);
               

            
           

            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "purpleCrawler")
        {

            

                this.takeDamage(20);


           

            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "orangeCrawler")
        {

            

                this.takeDamage(30);


            
          

            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "leftBoss")
        {
            this.takeDamage(60);
        }
        if (collision.gameObject.tag == "rightBoss")
        {
            this.takeDamage(70);
        }
        if (collision.gameObject.tag == "leftfinalBoss")
        {
            this.takeDamage(80);
        }

        if (collision.gameObject.tag == "greenGoo")
        {
            greenGoo++;
            Destroy(collision.gameObject);
            updateGooText();
        }
        if (collision.gameObject.tag == "purpleGoo")
        {
            purpleGoo++;
            Destroy(collision.gameObject);
            updateGooText();
        }
        if (collision.gameObject.tag == "orangeGoo")
        {
            orangeGoo++;
            Destroy(collision.gameObject);
            updateGooText();
        }
        if (collision.gameObject.tag == "LeftBoss")
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

        if (collision.gameObject.tag == "ObbyShield")
        {
            obsidianShield = true;
            this.gameObject.GetComponent<MeshRenderer>().material = obbyMat;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "greenArena")
        {
           // soundSource.clip = ArenaSound;
           // soundSource.PlayDelayed(2);
           // arenaZoom();
            // leftBoss.GetComponent<crawlerMovement>().setGreenArena(true);
            leftBoss.GetComponentInChildren<crawlerMovement>().setGreenArena(true);

        }

        if (collision.gameObject.tag == "greenBounce")
        {
            inGreenRange = true;
            //leftBoss.GetComponent<crawlerMovement>().setGreenRange(true);
            leftBoss.GetComponentInChildren<crawlerMovement>().setGreenRange(true);
        }
        if (collision.gameObject.tag == "orangeArena")
        {
            Debug.Log("entered");
            finalBoss.GetComponentInChildren<crawlerMovement>().setOrangeArena(true);

        }
        if (collision.gameObject.tag == "orangeRange")
        {
            Debug.Log("entered");
            finalBoss.GetComponentInChildren<crawlerMovement>().setOrangeRange(true);

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "greenArena")
        {
            //soundSource.clip = BGSound;
            //soundSource.PlayDelayed(2);
            //cam.orthographicSize = normalZoom;
            // leftBoss.GetComponent<crawlerMovement>().setGreenArena(false);
            leftBoss.GetComponentInChildren<crawlerMovement>().setGreenArena(false);
        }
        if (collision.gameObject.tag == "greenBounce")
        {
            inGreenRange = false;
            // leftBoss.GetComponent<crawlerMovement>().setGreenRange(false);
            leftBoss.GetComponentInChildren<crawlerMovement>().setGreenRange(false);
        }
        if (collision.gameObject.tag == "orangeArena")
        {

            finalBoss.GetComponentInChildren<crawlerMovement>().setOrangeArena(false);

        }
        if (collision.gameObject.tag == "orangeRange")
        {

            finalBoss.GetComponentInChildren<crawlerMovement>().setOrangeRange(false);

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
        greenGU.text = greenG.text;
        purpleGU.text = purpleG.text;
        orangeGU.text = orangeG.text;
    }

    private void updateGooText()
    {
        greenG.text = greenGoo.ToString();
        purpleG.text = purpleGoo.ToString();
        orangeG.text = orangeGoo.ToString();
    }

    public void leveler(int a)
    {
        //speedLeveler
        if (a == 0)
        {
            switch (speedLvl)
            {
                case 1:
                    if (bronzeScore >= spc.bronze && silverScore >= spc.silver && goldScore >= spc.gold)
                    {
                        speed += 1;
                        speedLvl++;
                        discountCost(a);
                        spc.bronze += 3;
                        spc.silver += 2;
                        spc.gold += 1;

                    }
                    break;
                case 2:
                    if (bronzeScore >= spc.bronze && silverScore >= spc.silver && goldScore >= spc.gold)
                    {
                        speed += 1;
                        speedLvl++;
                        discountCost(a);
                        spc.bronze += 5;
                        spc.silver += 3;
                        spc.gold += 3;
                    }
                    break;
                case 3:
                    if (bronzeScore >= spc.bronze && silverScore >= spc.silver && goldScore >= spc.gold)
                    {
                        speed += 1;
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
        }
        else if (a == 1) //LR leveler
        {
            switch (LRlvl)
            {
                case 1:

                    if (bronzeScore >= lrc.bronze && silverScore >= lrc.silver && goldScore >= lrc.gold)
                    {
                        spotLight.spotAngle += 3;
                        cam.orthographicSize = 6;
                        LRlvl++;
                        discountCost(a);
                        lrc.bronze += 3;
                        lrc.silver += 2;
                        lrc.gold += 1;

                    }
                    break;
                case 2:

                    if (bronzeScore >= lrc.bronze && silverScore >= lrc.silver && goldScore >= lrc.gold)
                    {
                        spotLight.spotAngle += 4;
                        cam.orthographicSize = 7;
                        LRlvl++;
                        discountCost(a);
                        lrc.bronze += 5;
                        lrc.silver += 3;
                        lrc.gold += 3;

                    }
                    break;
                case 3:
                    if (bronzeScore >= lrc.bronze && silverScore >= lrc.silver && goldScore >= lrc.gold)
                    {
                        spotLight.spotAngle += 5;
                        cam.orthographicSize = 8;
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
                        hpc.bronze += 5;
                        hpc.silver += 4;
                        hpc.gold += 3;
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
        }
        else if (a == 3)
        {
            switch (weaponlvl)
            {
                case 0:
                    if (bronzeScore >= hpc.bronze && silverScore >= hpc.silver && goldScore >= hpc.gold)
                    {
                        gunActivated = true;


                        weaponlvl++;
                        discountCost(a);
                        wpc.bronze += 4;
                        wpc.silver += 3;
                        wpc.gold += 2;

                    }
                    break;
                case 1:
                    if (bronzeScore >= hpc.bronze && silverScore >= hpc.silver && goldScore >= hpc.gold)
                    {
                        damage += 10;
                        weaponlvl++;
                        discountCost(a);
                        wpc.bronze += 4;
                        wpc.silver += 3;
                        wpc.gold += 2;

                    }
                    break;
                case 2:
                    if (bronzeScore >= hpc.bronze && silverScore >= hpc.silver && goldScore >= hpc.gold)
                    {
                        damage += 10;
                        weaponlvl++;
                        discountCost(a);
                        wpc.bronze += 0;
                        wpc.silver += 0;
                        wpc.gold += 0;

                    }
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
            case 0:
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
        myAnim.SetTrigger("tookDMG");
        health -= dmg;
        healthbar.setSize((float)health / maxHealth);
        AudioSource.PlayClipAtPoint(crashSound, transform.position);
        if (health <= 0)
        {
            end = true;
        }
    }
    public void healHealth(int healing)
    {
        int temp = health + healing;
        if (health == maxHealth)
        {

        }
        else if (temp >= maxHealth)
        {
            health = maxHealth;
            healthbar.setSize((float)health / maxHealth);
        }
        else
        {
            health += healing;
            healthbar.setSize((float)health / maxHealth);

        }

    }


    public void discountCost(int a)
    {
        if (a == 0)
        {
            bronzeScore -= spc.bronze;
            silverScore -= spc.silver;
            goldScore -= spc.gold;
        }
        else if (a == 1)
        {
            bronzeScore -= lrc.bronze;
            silverScore -= lrc.silver;
            goldScore -= lrc.gold;
        }
        else if (a == 2)
        {
            bronzeScore -= hpc.bronze;
            silverScore -= hpc.silver;
            goldScore -= hpc.gold;
        }
        else if (a == 3)
        {
            bronzeScore -= wpc.bronze;
            silverScore -= wpc.silver;
            goldScore -= wpc.gold;
        }
    }

    public void openMenu()
    {
        soundSource.mute = !soundSource.mute;

        panel.SetActive(!panelActive);
        mainUI.SetActive(panelActive);
        panelActive = !panelActive;
        moveJoy.SetActive(!moveJoy.active);
        if (gunActivated)
        {
            shootJoy.SetActive(!shootJoy.active);
        }
        Cursor.visible = !Cursor.visible;
        paused = !paused;

        if (paused)
        {
            updateTextUpgrades();
        }
    }

    private void arenaZoom()
    {
        cam.orthographicSize = 15;
    }

    public bool getGreenRange()
    {
        return inGreenRange;
    }
    public void addDamageCount()
    {
        damageDealt += damage;
    }
    public void setName()
    {
        nameField = inputField.text;
    }
    public void onEndEditName()
    {
        endAndHS = true;
        enterName.SetActive(false);
        highscoreTable.SetActive(true);
        highscoreTableReal.GetComponent<highScoreTable>().addHSE(blocksDigged, damageDealt, Time.deltaTime, nameField);
        //highscoreTableReal.GetComponent<highScoreTable>().addHSE(blocksDigged, damageDealt, Time.deltaTime, nameField);


    }
    public void inFightChange()
    {
        soundSource.clip = ArenaSound;
        soundSource.PlayDelayed(2);
        arenaZoom();
    }
    public void outFight()
    {
        soundSource.clip = BGSound;
        soundSource.PlayDelayed(2);
        cam.orthographicSize = normalZoom;
    }
    IEnumerator Overheat()
    {

        takeDamage(1);
        yield return new WaitForSeconds(1f);
        overheating = false;

    }

    IEnumerator shoot()
    {
        shooting = true;
        GameObject go = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
        go.GetComponent<projectileMove>().getProjectieInfo(
            new Vector2(joystickshoot.Horizontal, joystickshoot.Vertical), damage, 0.35f, this.gameObject);
        if (fireSpeed == false)
        {


            yield return new WaitForSeconds(1f);
        }
        else
        {
            yield return new WaitForSeconds(0.4f);
        }
        
        shooting = false;
    }
   


    IEnumerator heal()
    {
        healingHelper = true;
        Debug.Log("Called2");
        
        healHealth(5);
        yield return new WaitForSeconds(2f);
        healingHelper = false;

    }

    IEnumerator depleteGoo(int goo)
    {
        
        
            Debug.Log("Called");
            deplete = true;
            if (goo == 0 && greenGoo > 0)
            {
                greenGoo--;
                if (healingHelper == false)
                {
                    StartCoroutine("heal");
                }
            updateGooText();
            yield return new WaitForSeconds(1f);
            deplete = false;
            StartCoroutine("depleteGoo", goo);
        }
            else if (goo == 1 && purpleGoo > 0)
            {
                purpleGoo--;
                if(boost == false)
                {
                StartCoroutine("boostSpeed");
                } 
                
            }
            else if (goo == 2 && orangeGoo >0)
            {
                //Coroutine?
                fireSpeed = true;
                orangeGoo--;
                if(orangeGoo == 0)
                {
                    fireSpeed = false;
                }
            updateGooText();
            yield return new WaitForSeconds(1f);
            deplete = false;
            StartCoroutine("depleteGoo", goo);

        }
        else
        {

        }
           
       
        


    }

    IEnumerator boostSpeed()
    {
        boost = true;
        speed += 5;
        yield return new WaitForSeconds(1f);
        speed -= 5;
        boost = false;
    }

   

}
