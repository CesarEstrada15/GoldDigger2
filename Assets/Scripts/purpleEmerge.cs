using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purpleEmerge : MonoBehaviour
{

    [SerializeField] public GameObject greenCrawler;
    [SerializeField] public GameObject purpleCrawler;
    [SerializeField] public GameObject orangeCrawler;
    public GameObject eggBroken;
    private IEnumerator cor;
    private void Start()
    {
        if (this.gameObject.tag != "purpleBroken" || this.gameObject.tag != "orangeBroken")
        {
            cor = breakEgg(this.gameObject, eggBroken);
        }
        else
        {
            cor = breakEgg(this.gameObject);
        }
        StartCoroutine(cor);
    }

    // Update is called once per frame
    void Update()
    {
      
        
    }

    IEnumerator breakEgg(GameObject egg, GameObject broken)
    {
        eggBroken = broken;


        yield return new WaitForSeconds(5);
        if( egg.tag == "purpleEgg")
        {
            Instantiate(purpleCrawler, egg.transform.position, Quaternion.identity);
            Instantiate(eggBroken, egg.transform.position, Quaternion.identity);
        }
        else if (egg.tag == "orangeEgg")
        {
            Instantiate(orangeCrawler, egg.transform.position, Quaternion.identity);
            Instantiate(eggBroken, egg.transform.position, Quaternion.identity);
        }
        else if (egg.tag == "greenEgg")
        {
            Instantiate(greenCrawler, egg.transform.position, Quaternion.identity);
        }
        else if (egg.tag == "purpleBroken"){

        }
        else if(egg.tag == "orangeBroken")
        {

        }
        else
        {

        }
        Destroy(egg);
    }
    IEnumerator breakEgg(GameObject egg)
    {
        


        yield return new WaitForSeconds(5);
        if (egg.tag == "purpleEgg")
        {
            Instantiate(purpleCrawler, egg.transform.position, Quaternion.identity);
            Instantiate(eggBroken, egg.transform.position, Quaternion.identity);
        }
        else if (egg.tag == "orangeEgg")
        {
            Instantiate(orangeCrawler, egg.transform.position, Quaternion.identity);
            Instantiate(eggBroken, egg.transform.position, Quaternion.identity);
        }
        else if (egg.tag == "greenEgg")
        {
            Instantiate(greenCrawler, egg.transform.position, Quaternion.identity);
        }
        else if (egg.tag == "purpleBroken")
        {

        }
        else if (egg.tag == "orangeBroken")
        {

        }
        else
        {

        }
        Destroy(egg);
    }
}
