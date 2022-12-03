using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightZombieNotSpawn : MonoBehaviour
{
    GameObject[] zombies;
    // Update is called once per frame

    void Update()
    {
        zombies = GameObject.FindGameObjectsWithTag("enemy");

        Vector3 vector = new Vector3(transform.position.x + 5, transform.position.y + 5, 0);
        Vector3Int tilemapvector = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);
        foreach (var zombie in zombies)
        {
            if (zombie.transform.position.x <= vector.x && zombie.transform.position.y <= vector.y)
            {
                Destroy(zombie);
            }
        }

    }
}

