using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;
    private bool inFightArena = false;
    private bool ArenaGreen = false;
    private bool shaking = false;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        if (shaking == false)
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
    public IEnumerator Shake(float duration, float magnitude)
    {
        shaking = true;
        Vector3 ogPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            transform.localPosition = player.transform.position + offset;
            
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(x, y, ogPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = ogPos;
        shaking = false;
    }
}
