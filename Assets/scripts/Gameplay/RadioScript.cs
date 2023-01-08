using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RadioScript : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Item item in inventory.GetItemList())
        {
           if( item.sprite.name == "radio")
            {
                StartCoroutine(PanelActive());
            }
        }
    }
    public IEnumerator PanelActive()
    {
        bool gotowe = false;
        bool gotowe1 = false;
        bool gotowe2 = false;

        if (AdditionalSettings.days == 1 & gotowe != false)
        {
            panel.SetActive(true);
            title.text = "Diary date: 23.15.2040";
            description.text = "I feel tired but I must survive." +
                " After this meteorite impact everything changes. " +
             "I feel I will die and when you read this I am probably dead." +
             " In the fridge on the right of the map you can find fresh vegetables and other things to survive." +
             " Be carefull! ";
            yield return new WaitForSeconds(15);
            panel.SetActive(false);
            gotowe = true;

        }
        else if(AdditionalSettings.days == 3 & gotowe1 != false) 
        {
            panel.SetActive(true);
            title.text = "Diary date: 50.15.2016";
            description.text = "In the past I was a magician.After the apocalipse I must left my cage. " +
                "I give you a recipes under the c key to create magical lantern. Bye ";
            yield return new WaitForSeconds(15);
            panel.SetActive(false);
            gotowe1 = true;
        }
           
                

        
       
    }
}
