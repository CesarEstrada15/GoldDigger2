using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crawlerMovement : MonoBehaviour
{
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && target.GetComponent<moveExcavator>().paused != true) 
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.01f);
           // transform.LookAt(target.transform);
        }
        
    }
}
