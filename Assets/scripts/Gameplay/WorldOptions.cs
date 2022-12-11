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
using TMPro;

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
    public Slider effectsvolumeslider;
    public Slider musicvolumeslider;
    public AudioSource music;
    public AudioSource effects;
    public float fovslidervalue;
    public float effectsvolumeslidervalue;
    public float musicvolumeslidervalue;
    public UI_inventory inventoryui;
    public GameObject diepanel;
    public GameObject survipanel;
    public GameObject craftpanel;
    public GameObject objectivespanel;
    public GameObject pumpkinlightoryginal;
    public TileBase pumpkinlight;
    public GameObject grave;
    public Text debug;
    bool craftingopened;
    bool debugopened;
    public Grid grid;
    public Button respawnbtn;
    public static bool gravemode;
    public static int killedEnemies;
    public GameObject panelanim;
   public TextMeshProUGUI textanimstart;
    public GameObject panelanimtext;
    public AudioClip effect;
    public AudioClip crafteffect;
    public Tilemap tilemap1;
    public Tilemap tilemap2;
    public AudioSource source;
    public AudioClip death;
    public AudioSource musicmain;
    public TextMeshProUGUI textcraftinfo;
    public GameObject generaltab;
    public GameObject audiotab;
    public GameObject videotab;
    public Dropdown quality;
    public int fullscreenvalue;
    public Toggle fullscreentooggle;
    public void Start()
    {
       
        generaltab.SetActive(true);
        audiotab .SetActive(false);
        videotab.SetActive(false);
        panelanim.SetActive(false);
        panelanimtext.SetActive(false);
        killedEnemies = AdditionalSettings.killedmobs;
        gravemode = false;
        survipanel.SetActive(false);
        objectivespanel.SetActive(false);
        respawnbtn.interactable = true;
        if (WorldSettings.creative == false)
        {
            survipanel.SetActive(true);
            objectivespanel.SetActive(true);
            if (WorldSettings.hardcore == true)
            {
                respawnbtn.interactable = false;
            }
            else
            {
                respawnbtn.interactable = true;
            }
        }

        if (PlayerPrefs.GetInt("full") == 1)
        {
           fullscreentooggle.isOn = true;
            
        }
        else
        {
           fullscreentooggle.isOn = false;
        }

        fovslider.value = PlayerPrefs.GetFloat("FOV", fovslidervalue);
        if (PlayerPrefs.HasKey("qualityindex"))
        {
            quality.value =  PlayerPrefs.GetInt("qualityindex", 0);
        }
        
        Camera.main.orthographicSize = fovslider.value;
        diepanel.SetActive(false);
        effectsvolumeslider.value = PlayerPrefs.GetFloat("VolumeEffects", effectsvolumeslidervalue);
        musicvolumeslider.value = PlayerPrefs.GetFloat("VolumeMusic", musicvolumeslidervalue);
        settingspanel.SetActive(false);
        mapa2 = GameObject.FindGameObjectWithTag("tilemap2").GetComponent<Tilemap>();
        escpanel.SetActive(false);
        effectsvolumeslider.minValue = 0;
        effectsvolumeslider.maxValue = 1;
        musicvolumeslider.minValue = 0;
        musicvolumeslider.maxValue = 1;
        craftpanel.SetActive(false);



        if (PlayerPrefs.HasKey("Key"))
        {
            PlayerPrefs.SetInt("Key", 0);
            PlayerPrefs.Save();
            BaseFunc.Instance.CreateWorld(WorldSettings.worldname, WorldSettings.creative, WorldSettings.hardcore);
            if (WorldSettings.creative == false)
            {
                MonstersSpawn.active = false;
                StartCoroutine(animacja());
                StartCoroutine(textanim());
                MonstersSpawn.active = true;
            }
        }
        else if (PlayerPrefs.HasKey("KeyLoad"))
        {
            PlayerPrefs.SetInt("KeyLoad", 0);
            PlayerPrefs.Save();

            BaseFunc.Instance.LoadWorld(mapa, mapa2, tilelight, pumpkinlight, lightoryginal, pumpkinlightoryginal, WorldSettings.worldname, inventoryui);
         


        }
        Time.timeScale = 1;






    }
    /*
     *   effectsvolumeslidervalue = effectsvolumeslider.value;
        PlayerPrefs.SetFloat("VolumeEffects", effectsvolumeslidervalue);
        PlayerPrefs.Save();
        source.volume = effectsvolumeslidervalue;
     * 
     */
    public void SetQuality(int index)
    {   
     
        PlayerPrefs.SetInt("qualityindex", quality.value);
        PlayerPrefs.Save();
        QualitySettings.SetQualityLevel(index);
     
        
    }
    public void SetFullScreen(bool fullscreen)
    {
        if(fullscreen == true)
        {
        PlayerPrefs.SetInt("full", 1);
        PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("full", 0);
            PlayerPrefs.Save();
        }
      
        Screen.fullScreen = fullscreen;
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


        
        int craftsystem = 1;
        if (itemcraftable.name2 == "")
        {
            craftsystem = 1;
        }
        else
        {
            craftsystem = 2;
        }

        //iteration trought the items in inventory
        foreach (Item item in inventory1.ToList())
        {
            int itemcount = 0;


            if (craftsystem == 1)
            {
                if (item.sprite.name == itemcraftable.name)
                {
                    if (item != null && item.amount >= itemcraftable.count)
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
                        if(itemcraftable.newsprite.name == "antiradiationsuit")
                        {
                            itemtoadd.itemusetype = Item.ItemUseType.RadiationSuit;
                        }
                        itemtoadd.amount = 1;

                        inventory.AddItem(itemtoadd);
                        inventoryui.RefreshInventoryItems();
                        Debug.Log("crafted item");
                        source.clip = crafteffect;
                        source.Play();
                    }
                    else
                    {
                        Debug.Log("cannot craft");
                        textcraftinfo.text = $"Cannot craft {itemcraftable.newsprite.name}, because you don't have enought resources";

                    }

                }
            }







        }
        if (craftsystem == 2)
        {
            if(inventory.GetItem(itemcraftable.name) != null && inventory.GetItem(itemcraftable.name2) != null)
            {
             

                for (int i = 0; i < itemcraftable.count; i++)
                {
                    inventory.DeleteItem(itemcraftable.name);
                    inventoryui.RefreshInventoryItems();
                }
                for (int i = 0; i < itemcraftable.count2; i++)
                {
                    inventory.DeleteItem(itemcraftable.name2);
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
                source.clip = crafteffect;
                source.Play();
            }
            else
            {
                textcraftinfo.text = $"Cannot craft {itemcraftable.newsprite.name}, because you don't have enought resources";
            }
           
           
       }


    }
























    public void ChangeEffectsVolumeSlider()
    {
        effectsvolumeslidervalue = effectsvolumeslider.value;
        PlayerPrefs.SetFloat("VolumeEffects", effectsvolumeslidervalue);
        PlayerPrefs.Save();
        source.volume = effectsvolumeslidervalue;
    }
    public void ChangeMusicVolumeSlider()
    {
        musicvolumeslidervalue = musicvolumeslider.value;
        PlayerPrefs.SetFloat("VolumeMusic", musicvolumeslidervalue);
        PlayerPrefs.Save();
        musicmain.volume = musicvolumeslidervalue;
    }
    public void SaveButton()
    {
        BaseFunc.Instance.SaveWorld(mapa, mapa2, WorldSettings.worldname);
        PlayerPrefs.Save();
    }
    
    public IEnumerator animacja()
    {
        panelanim.SetActive(true);
        yield return new WaitForSeconds(20);
        panelanim.SetActive(false);
    }
    public IEnumerator textanim()
    {
        gravemode = true;
      musicmain.Stop();
        source.clip = effect;
        source.Play();
        panelanimtext.SetActive(true);
        textanimstart.text = "It was a beautiful day. Harry was riding in his wheel";
        yield return new WaitForSeconds(5);
        textanimstart.text = "But suddenly everything changed.";
        yield return new WaitForSeconds(5);
        textanimstart.text = "After a while the asteroid hits earth";
        yield return new WaitForSeconds(5);
        textanimstart.text = "And there was nothing for hamster who survived";
        yield return new WaitForSeconds(5);
        textanimstart.text = "Now the hamster must survive in an evil world. Will he succeed in doing so?";
        yield return new WaitForSeconds(5);
        textanimstart.text = "";
        panelanimtext.SetActive(false);
        musicmain.Play();
        gravemode = false;
       
    }
    public void GetInformationAbutCraft(CraftableItem item)
    {
        if(item.name2 == "")
        {
            textcraftinfo.text = $"To craft this item you need {item.count} x {item.name}. ";
        }
        else {

            textcraftinfo.text = $"To craft this item you need {item.count} x {item.name} and {item.count2} x {item.name2}. ";
        }
       
    }
    public void SettingsBtn()
    {

        settingspanel.SetActive(true);
    }
    public void SettingsCloseBtn()
    {
        settingspanel.SetActive(false);

    }
    public void GeneralTabBtn()
    {
        generaltab.SetActive(true);
        audiotab.SetActive(false);
        videotab.SetActive(false);
    }
    public void AudioTabBtn()
    {
      
        audiotab.SetActive(true);
        videotab.SetActive(false); 
        generaltab.SetActive(false);
    }
    public void VideoTabBtn()
    {
        videotab.SetActive(true);
        audiotab.SetActive(false);
        generaltab.SetActive(false);
    }
    public void RespawnBtn()
    {
        diepanel.SetActive(false);
        Time.timeScale = 1;
        PlayerSettings.life = 100;
        PlayerSettings.hunger = 100;
        Instantiate(grave, player.transform.position, Quaternion.identity);
        player.transform.position = new Vector3(-10.3733997f, 1.1336f, 0);
        gravemode = true;
    }
    public void Update()
    {
        int numbermonsters = GameObject.FindGameObjectsWithTag("enemy").Length;
    
          if(diepanel.active == true)
        {
            
           
           
        }
                
            
        
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
           
                debug.text = $"Player Position: {player.transform.position}\n" +
                $"MousePosition:{mouseWorldPos}\n" +
                $"Mouse Tile Position: {mouseTileCoords}\n" +
                $"IsDay: {cykldniainocy.isday}\n" +
                $"Creative: {WorldSettings.creative}\n" +
                $"Monsters:{numbermonsters}\n" +
                $"If the grave in not accesible,press G when the debug is enabled\n";
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
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (debugopened)
                {
                    inventoryui.RefreshInventoryItems();
                    gravemode = false;
                    
                }

                

            }


        }
       

        if (WorldSettings.creative == false)
        {
            if (PlayerSettings.life <= 0)
            {
               
                PlayerSettings.life = 0;
                diepanel.SetActive(true);
                Time.timeScale = 0;
                inventoryui.GraphicDeleteInventoryItems();
                


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