using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_inventory : MonoBehaviour
{
   
    public GameObject[] images;
    public Color color;
    public Item currentitem;
   
       
    public void RefreshInventoryItems()
    {
        for (int i = 0; i < images.Length; i++)
        {
            Image image = images[i].GetComponent<Image>();
            image.sprite = null;
            image.color = color;
        }
        for (int i = 0; i < images.Length; i++)
        {

            Text text = images[i].transform.parent.GetComponentInChildren<Text>();
            text.text = "";
        }
            for (int i = 0; i < images.Length; i++)
        {
            
            Image image = images[i].GetComponent<Image>();
           
            if(i < inventory.GetItemList().ToArray().Length)
            {
                image.sprite = inventory.GetItem(i).sprite;
                image.color = Color.white;
            }
        }
        for (int i = 0; i < images.Length; i++)
        {
           
            Text text = images[i].transform.parent.GetComponentInChildren<Text>();
            if (i < inventory.GetItemList().ToArray().Length)
            {
               if(inventory.GetItem(i).amount > 1)
                {
                    text.text = inventory.GetItem(i).amount.ToString();
                }
                else
                {
                    text.text = "";
                }
                
            }
        }




    }
    public void GraphicDeleteInventoryItems()
    {
        for (int i = 0; i < images.Length; i++)
        {
            Image image = images[i].GetComponent<Image>();
            image.sprite = null;
            image.color = color;
        }
        for (int i = 0; i < images.Length; i++)
        {

            Text text = images[i].transform.parent.GetComponentInChildren<Text>();
            text.text = "";
        }
        
       

    }


}
   

