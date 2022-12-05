using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class ObjectiveManager : MonoBehaviour
{

    Item szko;
    Item lantern;
    Item food;
    Item radiationsuit;
    Item destroyedwall;
    // Start is called before the first frame update
    void Start()
    {
        
           
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in inventory.itemList)
        {
            if(item.itemtile.name == "szko")
            {
                szko = item;
            }
            else if (item.itemtile.name == "jackolantern")
            {
                lantern = item;
            }
            else if(item.itemtile.name == "apple" || item.itemtile.name == "gruszka")
            {
                food = item;

            }
            else if (item.itemtile.name == "antiradiationsuit")
            {
                radiationsuit = item;
            }
            else if(item.itemtile.name == "destroyedwall")
            {
                destroyedwall = item;
            }


        }
        if(szko != null)
        {
            if(PlayerSettings.level == 0 && szko.amount >= 2)
            {
            PlayerSettings.level = 1;
               
               
            }
            else if (PlayerSettings.level == 1 && WorldOptions.killedEnemies == 10)
            {
                PlayerSettings.level = 2;


            }
            else if (PlayerSettings.level == 1 && Input.GetKeyDown(KeyCode.H))
            {
                PlayerSettings.level = 3;
            }
            
            //food
            else if (PlayerSettings.level == 2 && Input.GetKeyDown(KeyCode.H))
            {
                PlayerSettings.level = 3;


            }
            else if (PlayerSettings.level == 3 && food != null)
            {
                PlayerSettings.level = 4;


            }
            else if (PlayerSettings.level == 4 && lantern != null)
            {
                PlayerSettings.level = 5;


            }
            else if (PlayerSettings.level == 5 && radiationsuit != null)
            {
                PlayerSettings.level = 6;
            }
            else if (PlayerSettings.level == 6 && destroyedwall != null)
            {
                PlayerSettings.level = 7;
            }

        }

        /*
        else if (PlayerSettings.level == 1 && )
        {

        }*/
    }
}
