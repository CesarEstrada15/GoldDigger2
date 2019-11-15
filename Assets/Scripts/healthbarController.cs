using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthbarController : MonoBehaviour
{

    private Transform bar;
    public float currentSize;
    // Start is called before the first frame update
    private void Start()
    {
        bar = transform.Find("Bar");
        currentSize = 1f;
       
    }

    public void setSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
        currentSize = sizeNormalized;
        if(currentSize < 0)
        {
            bar.localScale = new Vector3(0, 1f);
            currentSize = 0;
        }
    }
   
}
