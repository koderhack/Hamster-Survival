using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]

public class Item
{
    public int id;

    public enum ItemType
    {
        Block,
        Weapon,
        

    }
    public ItemType itemtype;
    public int amount;
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
   

