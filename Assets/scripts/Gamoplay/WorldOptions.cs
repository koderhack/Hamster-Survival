using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

public class WorldOptions : MonoBehaviour
{


    public GameObject kolowrotek;
    public Tilemap mapa;
    public Tilemap mapa2;
    public GameObject lightoryginal;
    public TileBase tilelight;
    GameObject[] clones;
    public GameObject player;
    public GameObject escpanel;
    private bool gamePaused = false;
    public GameObject settingspanel;
    public Slider fovslider;
    public Slider volumeslider;
    public AudioSource music;
    public AudioSource effects;
    public float fovslidervalue;
    public float volumeslidervalue;
    public UI_inventory inventoryui;
    public GameObject diepanel;
    public GameObject survipanel;
    public GameObject craftpanel;
    public GameObject objectivespanel;
    public GameObject pumpkinlightoryginal;
    public TileBase pumpkinlight;

    public Text debug;
    bool craftingopened;
    bool debugopened;
    public Grid grid;

    public void Start()
    {
       
        survipanel.SetActive(false);
        objectivespanel.SetActive(false);
        if (WorldSettings.creative == false)
        {
            survipanel.SetActive(true);
            objectivespanel.SetActive(true);
        }

        fovslider.value = PlayerPrefs.GetFloat("FOV", fovslidervalue);
        Camera.main.orthographicSize = fovslider.value;
        diepanel.SetActive(false);
        volumeslider.value = PlayerPrefs.GetFloat("Volume", volumeslidervalue);
        AudioListener.volume = volumeslider.value;
        settingspanel.SetActive(false);
        mapa2 = GameObject.FindGameObjectWithTag("tilemap2").GetComponent<Tilemap>();
        escpanel.SetActive(false);
        volumeslider.minValue = 0;
        volumeslider.maxValue = 1;
        craftpanel.SetActive(false);
      


        if (PlayerPrefs.HasKey("Key"))
        {
            PlayerPrefs.SetInt("Key", 0);
            PlayerPrefs.Save();
            BaseFunc.Instance.CreateWorld(WorldSettings.worldname, WorldSettings.creative, WorldSettings.settings);
        }
        else if (PlayerPrefs.HasKey("KeyLoad"))
        {
            PlayerPrefs.SetInt("KeyLoad", 0);
            PlayerPrefs.Save();

            BaseFunc.Instance.LoadWorld(mapa, mapa2, tilelight,pumpkinlight, lightoryginal,pumpkinlightoryginal, WorldSettings.worldname, inventoryui);



        }
        Time.timeScale = 1;






    }
    public void ChangeFOVSlider(float value)
    {
        fovslidervalue = value;
        PlayerPrefs.SetFloat("FOV", fovslidervalue);
        PlayerPrefs.Save();
        Camera.main.orthographicSize = fovslidervalue;
    }
    public void CraftItem(CraftableItem itemcraftable)
    {
        List<Item> inventory1 = inventory.GetItemList();
        foreach (var item in inventory1.ToList())
        {
          if(item.sprite.name == itemcraftable.name)
            {
                
              for (int i = 0; i < itemcraftable.count; i++)
              {
                inventory.DeleteItem(item);
                inventoryui.RefreshInventoryItems();
                       

              }
             Item itemtoadd = new Item();
             itemtoadd.sprite = itemcraftable.newsprite;
             itemtoadd.itemtile = itemcraftable.newtile;
             itemtoadd.itemtype = Item.ItemType.Block;
             itemtoadd.amount = 1;

             inventory.AddItem(itemtoadd);
             inventoryui.RefreshInventoryItems();
             Debug.Log("crafted item");

               


            }
            else if(item.sprite.name == itemcraftable.name2)
            {
                for (int i = 0; i < itemcraftable.count2; i++)
                {
                    inventory.DeleteItem(item);
                    inventoryui.RefreshInventoryItems();


                }
                Item itemtoadd = new Item();
                itemtoadd.sprite = itemcraftable.newsprite;
                itemtoadd.itemtile = itemcraftable.newtile;
                itemtoadd.itemtype = Item.ItemType.Block;
                itemtoadd.amount = 1;

                inventory.AddItem(itemtoadd);
                inventoryui.RefreshInventoryItems();
                Debug.Log("crafted item");
            }
            else
           {
            Debug.Log("Cannot craft item");
           }
          
        }
         
           
               
            
         
          

        

    
    

       







    }



    public void ChangeVolumeSlider(float value)
    {
        volumeslidervalue = value;
        PlayerPrefs.SetFloat("Volume", volumeslidervalue);
        PlayerPrefs.Save();
        AudioListener.volume = volumeslidervalue;
    }
    public void SaveButton()
    {
        BaseFunc.Instance.SaveWorld(mapa, mapa2, WorldSettings.worldname);
        PlayerPrefs.Save();
    }


    public void SettingsBtn()
    {

        settingspanel.SetActive(true);
    }
    public void SettingsCloseBtn()
    {
        settingspanel.SetActive(false);

    }

    public void RespawnBtn()
    {
        diepanel.SetActive(false);
       
        PlayerSettings.life = 100;
        PlayerSettings.hunger = 100;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6))
        {
            if (debugopened == true)
            {
               
                debugopened = false;
            }
            else
            {
                

                debugopened = true;
            }
        }
        if (debugopened)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int mouseTileCoords = grid.WorldToCell(mouseWorldPos);
            int numbermonsters = GameObject.FindGameObjectsWithTag("enemy").Length;
            debug.text = $"Player Position: {player.transform.position}\n" +
                $"MousePosition:{mouseWorldPos}\n" +
                $"Mouse Tile Position: {mouseTileCoords}\n" +
                $"Is Day: {cykldniainocy.isday}\n" +
                $"Creative: {WorldSettings.creative}\n" +
                $"Monsters:{numbermonsters}\n";
        }
        else
        {
            debug.text = "";
        }
        if(WorldSettings.creative == false)
        {
            
            if (Input.GetKeyDown(KeyCode.C))
            {
               
                if(craftingopened == true)
                {
                    craftpanel.SetActive(false);
                    craftingopened = false;
                }
                else
                {
                    craftpanel.SetActive(true);
                    
                    craftingopened = true;
                }
       

            }
        }
       

        if (WorldSettings.creative == false)
        {
            if (PlayerSettings.life <= 0)
            {
                PlayerSettings.life = 0;
                diepanel.SetActive(true);
               
            }
            if (PlayerSettings.life > 100)
            {
                PlayerSettings.life = 100;
            }
        }



        if (WorldSettings.creative == true)
        {
            survipanel.SetActive(false);
        }
        else
        {
            survipanel.SetActive(true);
        }
       

      
        /* if (PlayerPrefs.HasKey("Volume"))
         {

             glosnosc = PlayerPrefs.GetFloat("Volume");

             music.volume = glosnosc;
             effects.volume = glosnosc;


         }*/









        clones = GameObject.FindGameObjectsWithTag("LampLight");
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (gamePaused)
            {
                ruchgracza.ExitMenu();
                Time.timeScale = 1;
                gamePaused = false;
                escpanel.SetActive(false);
               
                settingspanel.SetActive(false);
                music.Play();


            }
            else
            {
                ruchgracza.EnterMenu();
                Time.timeScale = 0;
                gamePaused = true;
                escpanel.SetActive(true);
                
                craftpanel.SetActive(false);
                music.Pause();

            }
        }

    }


    public void GoToMenu()
    {
        BaseFunc.Instance.SaveWorld(mapa, mapa2, WorldSettings.worldname);

        SceneManager.LoadScene("Menu");
        PlayerPrefs.Save();
    }
    public void CraftingCloseBtn()
    {
        craftpanel.SetActive(false);

    }





    public void OnApplicationQuit()
    {
        BaseFunc.Instance.SaveWorld(mapa, mapa2, WorldSettings.worldname);
        PlayerPrefs.Save();
    }







}