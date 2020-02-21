using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;
    private bool inFightArena = false;
    private bool ArenaGreen = false;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        if (inFightArena == false)
        {
            transform.position = player.transform.position + offset;
        } else
        {
            if(ArenaGreen == true)
            {
                
            }
        }
    }

    public void setArena(int a)
    {
        if(a == 0)
        {
            ArenaGreen = !ArenaGreen;
            inFightArena = !inFightArena;
        }
    }
}
