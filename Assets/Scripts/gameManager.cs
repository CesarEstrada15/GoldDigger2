using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public GameObject player;
    private GameObject terrain;


    int bronzeScore = 0;
    int silverScore = 0;
    int goldScore = 0;
    Canvas can;
    Text bronzeTxt;
    Text silverTxt;
    Text goldTxt;


    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.ambientLight = Color.black;
        can = player.GetComponent<Canvas>();
  
        bronzeTxt = GameObject.Find("Bronze").GetComponent<Text>();
        silverTxt = GameObject.Find("Silver").GetComponent<Text>();
        goldTxt = GameObject.Find("Gold").GetComponent<Text>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}
