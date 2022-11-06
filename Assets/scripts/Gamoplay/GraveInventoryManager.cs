using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveInventoryManager : MonoBehaviour
{

    private UI_inventory inventoryui;

    public void Start()
    {
        inventoryui = GameObject.FindGameObjectWithTag("inventory").GetComponent<UI_inventory>();
    }
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(inventory.itemList != null)
            {
                 inventoryui.RefreshInventoryItems();  
                Destroy(this.gameObject);
            }
         
          
        }
    }
}