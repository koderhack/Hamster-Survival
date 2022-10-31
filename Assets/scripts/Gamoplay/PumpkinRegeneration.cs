using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PumpkinRegeneration : MonoBehaviour
{

    
    public GameObject player;
    float timer;
    public float interval = 2;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vector = new Vector3(transform.position.x + 5, transform.position.y + 5, 0);
        if(player.transform.position.x <= vector.x && player.transform.position.y <= vector.y)
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                PlayerSettings.life++;


                timer -= interval;
            }
        }
    }
}
