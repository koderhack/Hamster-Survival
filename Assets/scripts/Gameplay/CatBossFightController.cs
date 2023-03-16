using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CatBossFightController : MonoBehaviour
{
    public float life = 1000;
    public int warPhase;
    public GameObject zombieoryginal;
    public GameObject ghostoryginal;

    // Start is called before the first frame update
    void Start()
    {
        warPhase = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (life == 1000)
        {
            warPhase = 1;
        }
        else if(life == 500)
        {
            warPhase = 2;
        }
        else if(life == 250)
        {
            warPhase = 3;
        }

        if(warPhase == 1)
        {
            //cat stand and shoot
        }
        else if (warPhase == 2)
        {
            
            //cat spawns other mobs like zombies,ghosts
            Instantiate(zombieoryginal);
        }
        else if (warPhase == 3)
        {
            //cat attacks player with the horde of zombies,ghosts and other mobs
        }
    }
}
