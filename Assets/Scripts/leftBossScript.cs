﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftBossScript : MonoBehaviour
{
    private GameObject player;
    public GameObject shieldDrop;
    private int health = 500;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            transform.LookAt(player.transform.position);
        }
        
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void takeDamage(int dmg)
    {
        health -= dmg;
    }

    private void OnDestroy()
    {
        
    }

}
