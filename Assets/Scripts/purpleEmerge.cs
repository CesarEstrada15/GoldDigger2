using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purpleEmerge : MonoBehaviour
{

    [SerializeField] public GameObject greenCrawler;
    [SerializeField] public GameObject purpleCrawler;
    [SerializeField] public GameObject orangeCrawler;
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("breakEgg", this.gameObject);
    }

    IEnumerator breakEgg(GameObject egg)
    {



        yield return new WaitForSeconds(5);
        if( egg.tag == "purpleEgg")
        {
            Instantiate(purpleCrawler, egg.transform.position, Quaternion.identity);
        }
        else if (egg.tag == "orangeEgg")
        {
            Instantiate(orangeCrawler, egg.transform.position, Quaternion.identity);
        }
        else if (egg.tag == "greenEgg")
        {
            Instantiate(greenCrawler, egg.transform.position, Quaternion.identity);
        }
        else{

        }
        Destroy(egg);
    }
}
