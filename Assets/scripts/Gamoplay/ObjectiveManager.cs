using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectiveManager : MonoBehaviour
{

    private Item szko;

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
            
        }
        if(szko != null)
        {
            if(PlayerSettings.level == 0 && szko.amount >= 2)
            {
            PlayerSettings.level = 1;
               
               
            }
            if (PlayerSettings.level == 1 && Enemy.killedEnemies == 10)
            {
                PlayerSettings.level = 2;


            }
        }
        
        /*
        else if (PlayerSettings.level == 1 && )
        {

        }*/
    }
}
