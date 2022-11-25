using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonstersSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool active;
    public GameObject oryginal;
    public GameObject iceoryginal;
    public GameObject player;
    public float interval = 25;
    float timer;
    public Tilemap tilemain;
    public void Awake()
    {

        Vector3 position = new Vector3(263, 8, 0);
        Instantiate(iceoryginal, position, Quaternion.identity);
        Vector3 position1 = new Vector3(263, 10, 0);
        Instantiate(iceoryginal, position, Quaternion.identity);
        Vector3 position2 = new Vector3(263, 20, 0);
        Instantiate(iceoryginal, position, Quaternion.identity);
    }
   
    // Update is called once per frame
    void Update()
    {
        if(WorldSettings.creative == false)
        {
            var clones = GameObject.FindGameObjectsWithTag("enemy");
           
            if (clones.Length == 30)
            {
                for (int i = 0; i < clones.Length - 15; i++)
                {
                    Destroy(clones[i]);
                }
            }
            if(active == true)
            {
                

               
                timer += Time.deltaTime;
                if (timer >= interval)
                {
                    Vector3 position = new Vector3(Random.RandomRange(0, 20), Random.RandomRange(0, 20), 20);
                    Vector3Int positiontilemap = new Vector3Int((int)position.x,(int)position.y,0);
                    if (tilemain.GetTile(positiontilemap) == null)
                    {
                        
                           
                          Instantiate(oryginal, position, Quaternion.identity);
                        
                       
                    }

                   


                        


                    
                    timer -= interval;
                }
            }
            else
            {
                
                foreach (var clone in clones)
                {
                    Destroy(clone);
                }
            }

        }
    }

     
}
