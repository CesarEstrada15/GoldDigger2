using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointAndShoot : MonoBehaviour
{

    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void getTarget()
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
    }
}
