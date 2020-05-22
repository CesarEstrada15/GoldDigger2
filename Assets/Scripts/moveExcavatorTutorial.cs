using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class moveExcavatorTutorial : MonoBehaviour
{
    Text bronzeTxt;
    Text silverTxt;
    Text goldTxt;
    private bool shooting = false;
    private bool fireSpeed = false;
    public AudioClip shootSound;
    private int damage = 100;
    public Camera cam;
    public GameObject projectile;
    public GameObject crosshair;
    public GameObject drill;
    private int speed = 2;
    private bool gunActivated = false;
    private Vector2 mousePos;


    private Rigidbody2D rb2d;
    [SerializeField] public Material obbyMat;
  
    [SerializeField] public healthbarController healthbar;
    [SerializeField] public healthbarController gasResource;
  
    [SerializeField] public AudioClip all;
    [SerializeField] public AudioClip boss;
    [SerializeField] public AudioClip endSound;

    [SerializeField] public GameObject overheat;
    [SerializeField] public Joystick joystick;
    [SerializeField] public Joystick joystickshoot;


    [SerializeField] public GameObject greenCrawler;
    [SerializeField] public GameObject purpleCrawler;
    [SerializeField] public GameObject orangeCrawler;
    [SerializeField] public GameObject highscoreTable;
    [SerializeField] public GameObject highscoreTableReal;
    [SerializeField] public GameObject enterName;
    [SerializeField] public GameObject endPanel;

    public GameObject greenC;
    public GameObject purpleC;
    public GameObject orangeC;
 

    //Particles
    public ParticleSystem greenPart;
    public ParticleSystem purplePart;
    public ParticleSystem orangePart;
    private ParticleSystem myPart;

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
 
    public AudioClip buffUp;
    public AudioClip debuff;
    public AudioClip boostSound;
    public AudioClip magicalAmbiance;

    //Excavator capabilities

    private int maxHealth = 100;
    private int health = 100;
   

    //Resources
    int bronzeScore = 20;
    int silverScore = 10;
    int goldScore = 10;
    int gas = 100;
    int greenGoo = 0;
    int purpleGoo = 0;
    int orangeGoo = 0;

    //animations
    [SerializeField] private Animator myAnim;

    //UI
    GameObject panel;
    GameObject mainUI;
    GameObject moveJoy;
    GameObject shootJoy;
   
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
   
    private bool inPowerUpSong = false;




    //GUN VARIABLES
    //TEST TEST TEST TEST
   

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


    //States

    public GameObject state1;
    public GameObject state2;
    public GameObject state3;
    public GameObject state4;
    public GameObject state5;
    public GameObject state6;
    public GameObject state7;
    public GameObject state8;
    public GameObject state9;
    public GameObject state10;
    public GameObject state11;

    private IEnumerator cor;
    private bool moved;
    private bool blockMined;
    // Start is called before the first frame update
    void Start()
    {
        bronzeTxt = GameObject.Find("Bronze").GetComponent<Text>();
        silverTxt = GameObject.Find("Silver").GetComponent<Text>();
        goldTxt = GameObject.Find("Gold").GetComponent<Text>();
        greenG = GameObject.Find("Green").GetComponent<Text>();
        purpleG = GameObject.Find("Purple").GetComponent<Text>();
        orangeG = GameObject.Find("Orange").GetComponent<Text>();

        state1.SetActive(true);
        state2.SetActive(false);
        state3.SetActive(false);
        state4.SetActive(false);
        state5.SetActive(false);
        state6.SetActive(false);
        state7.SetActive(false);
        state8.SetActive(false);
        state9.SetActive(false);
        state10.SetActive(false);
        state11.SetActive(false);
        crosshair.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0f);
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            drill.transform.rotation = Quaternion.LookRotation(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), Vector3.forward);
        }
        if (gunActivated == true)
        {

            crosshair.SetActive(true);

            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            crosshair.transform.position = (new Vector3(mousePos.x, mousePos.y, transform.position.z));
            if (Input.GetMouseButtonDown(0) && shooting == false)
            {
                StartCoroutine("shootMouse");
            }
            // if (joystickshoot.Horizontal != 0 && joystickshoot.Vertical != 0 && shooting == false )
            // {
            //    StartCoroutine("shoot");
            //}

        }
       
       
        
        //state 1
        if (state1.activeSelf)
        {
            cor = waitTostartState(state1, state2);
            StartCoroutine(cor);
        }
        if (state2.activeSelf)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                cor = waitTostartState(state2, state3);
                StartCoroutine(cor);
            }
        }
        if (state3.activeSelf)
        {
            cor = waitTostartState(state3, state4);
            StartCoroutine(cor);

        }
        if(state4.activeSelf)
        {
            cor = waitTostartState(state4, state5, 1f);
            if (blockMined)
            {
                StartCoroutine(cor);
            }
        }
        if (state5.activeSelf)
        {
            
            cor = waitTostartState(state5, state6);
            StartCoroutine(cor);
        }
        if (state6.activeSelf)
        {
            crosshair.SetActive(true);
            gunActivated = true;
           // greenC.GetComponent<crawlerMoveTutorial>().findHealth();
           // purpleC.GetComponent<crawlerMoveTutorial>().findHealth();
           // orangeC.GetComponent<crawlerMoveTutorial>().findHealth();
           if(greenC == null && purpleC == null & orangeC == null)
            {
                cor = waitTostartState(state6, state7, 2f);
                StartCoroutine(cor);
            }
        }
        if (state7.activeSelf)
        {

            if (GameObject.Find("GreenGoo(Clone)") == null && GameObject.Find("PurpleGoo(Clone)") == null && GameObject.Find("OrangeGoo(Clone)") == null){
                cor = waitTostartState(state7, state8);
                StartCoroutine(cor);
                greenGoo = 5;
                purpleGoo =2;
                orangeGoo = 5;
            }
        }
        if (state8.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                if (greenGoo > 0 && deplete == false)
                {
                    AudioSource.PlayClipAtPoint(buffUp, transform.position);
                    StartCoroutine("depleteGoo", 0);


                }
                cor = waitTostartState(state8, state9);
                StartCoroutine(cor);
            }
        }
        if (state9.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                if (purpleGoo > 0 && boost == false)
                {
                    StartCoroutine("boostSpeed");
                }
                cor = waitTostartState(state9, state10);
                StartCoroutine(cor);
            }
            
        }
        if (state10.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (orangeGoo > 0 && deplete == false)
                {
                    AudioSource.PlayClipAtPoint(buffUp, transform.position);
                    StartCoroutine("depleteGoo", 2);
                }

                cor = waitTostartState(state10, state11);
                StartCoroutine(cor);
            }
            
        }
        if (state11.activeSelf)
        {
            
            SceneManager.LoadScene(0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            blockMined = true;
            Destroy(collision.gameObject);
            

        }
        if (collision.gameObject.tag == "greenGoo")
        {
            
            Destroy(collision.gameObject);
            
        }
        if (collision.gameObject.tag == "purpleGoo")
        {
            
            Destroy(collision.gameObject);
            
        }
        if (collision.gameObject.tag == "orangeGoo")
        {
            
            Destroy(collision.gameObject);
            
        }
    }
    IEnumerator shootMouse()
    {
        shooting = true;
        GameObject go = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
        go.GetComponent<projectileMoveTutorial>().getProjectieInfo(
            new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y), damage, 0.35f, this.gameObject);
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
    private void updateTextMainUI()

    {
        bronzeTxt.text = bronzeScore.ToString();
        silverTxt.text = silverScore.ToString();
        goldTxt.text = goldScore.ToString();

    }
    private void updateGooText()
    {
        greenG.text = greenGoo.ToString();
        purpleG.text = purpleGoo.ToString();
        orangeG.text = orangeGoo.ToString();
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
        if (goo == 0)
        {
            if (greenGoo > 0)
            {
                //  if(inPowerUpSong == false)
                //  {
                //     inPowerUpSong = true;
                //   AudioSource.PlayClipAtPoint(magicalAmbiance, transform.position);
                // }
                myPart = Instantiate(greenPart, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);

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
            else
            {

                myPart = null;
                deplete = false;
                inPowerUpSong = false;
                AudioSource.PlayClipAtPoint(debuff, transform.position);
            }
        }
        else if (goo == 1)
        {

            if (purpleGoo > 0)
            {
                myPart = Instantiate(purplePart, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
                purpleGoo--;
                if (boost == false)
                {
                    StartCoroutine("boostSpeed");
                }
            }
            else
            {

                myPart = null;
            }

        }
        else if (goo == 2)
        {
            if (orangeGoo > 0)
            {



                //Coroutine?
                fireSpeed = true;
                orangeGoo--;
                // if (inPowerUpSong == false)
                // {
                //    inPowerUpSong = true;
                //    AudioSource.PlayClipAtPoint(magicalAmbiance, transform.position);
                // }
                if (orangeGoo == 0)
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
                fireSpeed = false;
                myPart = null;
                deplete = false;
                inPowerUpSong = false;
                AudioSource.PlayClipAtPoint(debuff, transform.position);
            }

        }
        else
        {

        }





    }

    IEnumerator boostSpeed()
    {
        myPart = Instantiate(purplePart, new Vector3(this.transform.position.x, this.transform.position.y, -1), Quaternion.identity);
        AudioSource.PlayClipAtPoint(boostSound, transform.position);
        boost = true;
        speed += 5;
        purpleGoo -= 2;
        updateGooText();
        yield return new WaitForSeconds(1f);
        speed -= 5;
        boost = false;
        myPart = null;
    }

    IEnumerator waitTostartState(GameObject a, GameObject b)
    {
        yield return new WaitForSeconds(3f);
        a.SetActive(false);
        b.SetActive(true);
    }
    IEnumerator waitTostartState(GameObject a, GameObject b, float time)
    {
        yield return new WaitForSeconds(time);
        a.SetActive(false);
        b.SetActive(true);
    }
}
