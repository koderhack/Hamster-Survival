using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class ObjectiveManager : MonoBehaviour
{

    Item szko;
    Item lantern;
    Item food;
    Item radiationsuit;
    Item destroyedwall;
    public AudioSource music;
    public Tilemap mapa;
    public Tilemap mapa2;
    public GameObject endpanel;
    public TextMeshProUGUI textanimend;
    public AudioSource effects;
    public AudioClip speak;
    public GameObject panelanimtext;
    public TextMeshProUGUI leveltext;
    public static bool[] done;
   
    // Start is called before the first frame update
    void Start()
    {
        endpanel.SetActive(false);
        
        panelanimtext.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        leveltext.text = PlayerSettings.level.ToString();
        foreach (var item in inventory.itemList)
        {
            if (item.itemtile.name == "szko" )
            {
                szko = item;
               
            }
            else if (item.itemtile.name == "jackolantern" )
            {
                lantern = item;
               

            }
            else if (item.itemtile.name == "apple" || item.itemtile.name == "gruszka" )
            {
                food = item;
            
            }
            else if (item.itemtile.name == "antiradiationsuit")
            {
                radiationsuit = item;
               
            }
            else if (item.itemtile.name == "destroyedwall" )
            {
                destroyedwall = item;
               
            }

          
        }

        if (szko != null && szko.amount >= 2 && PlayerSettings.done[0] == false)
        {
            PlayerSettings.level += 1;
             PlayerSettings.done[0] = true;

        }
        else if ( WorldOptions.killedEnemies == 1 && PlayerSettings.done[1] == false)
        {
            PlayerSettings.level += 1;
            PlayerSettings.done[1] = true;

        }
        //food
        else if (Input.GetKeyDown(KeyCode.H) && PlayerSettings.done[2] == false)
        {
            PlayerSettings.level += 1;

            PlayerSettings.done[2] = true;
        }
        else if ( food != null && PlayerSettings.done[3] == false)
        {
            PlayerSettings.level += 1;
         PlayerSettings.done[3] = true;

        }
        else if ( lantern != null && PlayerSettings.done[4] == false)
        {
            PlayerSettings.level += 1;
 PlayerSettings.done[4] = true;

        }
        else if (radiationsuit != null && PlayerSettings.done[5] == false)
        {
            PlayerSettings.level += 1;
            PlayerSettings.done[5] = true;
        }
        else if  (destroyedwall != null)
        {
            PlayerSettings.level += 1;
           
            endpanel.SetActive(true);
            StartCoroutine(TextAnim());


        }


    }
    public IEnumerator TextAnim()
    {
        effects.clip = speak;
        effects.Play();
        panelanimtext.SetActive(true);
        textanimend.text = "Congratulations Harry! You have accomplished the impossible! ";
        yield return new WaitForSeconds(5);
        textanimend.text = "You managed to survive and get out for the first time ever! Bravo!";

        yield return new WaitForSeconds(5);
        textanimend.text = " The End" +
            " Programmer: Koder " +
            " Graphics designers: Koder,Mc_go��b ";
        yield return new WaitForSeconds(10);
      panelanimtext.SetActive(false);
        endpanel.SetActive(false);
    }
    
}
