using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]

public class Item
{
    

    public enum ItemType
    {
        Block,
        Weapon,
        

    }
    public enum ItemUseType
    {
       Block,
       Food,
       RadiationSuit,



    }
    public int HungerPoints()
    {
        if(itemusetype == ItemUseType.Food)
        {
            if(sprite.name == "apple")
            {
                return 20;
            }
            else if(sprite.name == "gruszka")
            {
                return 50;
            }
            else if (sprite.name == "dynia")
            {
                return 30;
            }
            else
            {
                return 30;
            }
        }
        else
        {
            return 0;
        }
    }
    public ItemType itemtype;
    public ItemUseType itemusetype;
    public int amount = 1;
    [SerializeField] public Sprite sprite; 
    
    public float damagesec()
    {
        if(sprite.name == "ziemia")
        {
            return 0.2f;
        }
        else if(sprite.name == "deski")
        {
            return 1;
        }
        else
        {
            return 1.5f;
        }
    }

   [SerializeField] public TileBase itemtile;
    public bool Stackable()
    {
      
        if (itemtype == ItemType.Block)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
   



}
   

