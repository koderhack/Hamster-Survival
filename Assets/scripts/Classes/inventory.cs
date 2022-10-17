using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class inventory 
{
    public static List<Item> itemList;
    
    

    public static float GetCount()
    {
        return itemList.Count;
    }
    public static void AddItem(Item item)
    {
       
        if (item.Stackable() == true)
        {
           
            bool IsItemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemtile.name == item.itemtile.name)
                {
                    
                    if (inventoryItem.amount < 64)
                    {
                        IsItemAlreadyInInventory = true;
                        inventoryItem.amount++;
                        
                    }
                    else
                    {
                        IsItemAlreadyInInventory = false;
                    }
                   
                  
                  
                }

            }
            if (!IsItemAlreadyInInventory)
            {
                
                itemList.Add(item);
               
            }
        }
        else
        {
            
            itemList.Add(item);
       

        }
        
        
      
    }
    public static Item GetItem(int id)
    {
        return itemList[id];
    }
    public static Item GetItem(TileBase tile)
    {
        return itemList.Find(cc => cc.itemtile == tile); 
    }
 
    public static void DeleteItem(int id)
    {
        Item item = GetItem(id);



        if (item.Stackable() == true)
        {
            item.amount--;
            if (item.amount <= 0 && item != null)
            {
                itemList.Remove(item);

            }
        }
        else
        {
            itemList.Remove(item);

        }
    }
    
    public static void DeleteItem(TileBase tile)
    {
        Item item = GetItem(tile);
          
       
       
        if(item.Stackable() == true)
        {    
            if(item.amount > 0)
            {
                item.amount--;
            }
          
            if (item.amount <= 0 && item != null)
            {
                itemList.Remove(item);
                
            }
           
        
        }
        else
        {
            itemList.Remove(item);
          
        }
          

        
    }
    public static void DeleteItem(Item item)
    {
   



        if (item.Stackable() == true)
        {
            item.amount--;
            if (item.amount <= 0 && item != null)
            {
                itemList.Remove(item);

            }
        }
        else
        {
            itemList.Remove(item);

        }



    }
    public static List<Item> GetItemList()
    {
        return itemList;
    }
}
