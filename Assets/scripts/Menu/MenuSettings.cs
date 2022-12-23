using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Tilemaps;

public class MenuSettings : MonoBehaviour
{

    public GameObject panelworlds;

    public GameObject panelcreate;
    public GameObject[] namegameobject;
    public GameObject[] modegameobject;
    public GameObject[] deletegameobject;
    public GameObject[] hardcoregameobject;
    public GameObject paneldelete;
    public InputField nameinput;
    public GameObject btncreate;
    public Button btncreate2;
    public Text worldname;
    public int worldindex;
    string Namedir;
    int startvalue;
    public GameObject panelcredits;
    public GameObject panelinfo;
    private GameObject buttonclicked;
    private GameObject deletebuttonclicked;
    public GameObject slash;
    bool creative;
    bool hardcore;
    public GameObject modebtn;
    public Button hcbtn;
    public GameObject settingspanel;
    public void Start()
    {
        settingspanel.SetActive(false);
        panelworlds.SetActive(false);
        paneldelete.SetActive(false);
        panelcreate.SetActive(false);
        startvalue = 0;
        PlayerPrefs.DeleteKey("Key");
        PlayerPrefs.DeleteKey("KeyLoad");
        PokazSwiaty(startvalue);
        panelinfo.SetActive(true);
        btncreate.SetActive(true);
        panelcredits.SetActive(false);
        creative = true;
        hardcore = false;
        hcbtn.interactable = false;
        slash.SetActive(true);

    }

    public void Awake()
    {
        name = SceneManager.GetActiveScene().name;

    }
    public void DeleteBtnClicked()
    {

        deletebuttonclicked = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        var directories = Directory.GetDirectories(Application.persistentDataPath);

        foreach (var directory in directories)
        {
            string directoryname = Path.GetFileNameWithoutExtension(directory);
            //for each object in the array..

            if (directoryname == deletebuttonclicked.transform.parent.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>().text)
            {
                paneldelete.SetActive(true);
                btncreate.SetActive(false);

                Namedir = directoryname;

            }
        }
    }
    public void DeleteBtnClicked2()
    {
        BaseFunc.Instance.DeleteWorld(Namedir);
        paneldelete.SetActive(false);
        btncreate.SetActive(true);
        PokazSwiaty(0);
    }
    public void UpButtonClicked()
    {



        var directories = Directory.GetDirectories(Application.persistentDataPath);
        startvalue -= 3;
        if (startvalue == directories.Length || startvalue < 0)
        {
            startvalue = 0;
        }
        PokazSwiaty(startvalue);
    }

    public void DownButtonClicked()
    {

        startvalue += 3;
        var directories = Directory.GetDirectories(Application.persistentDataPath);
        if (startvalue == directories.Length || startvalue < 0)
        {
            startvalue = 0;
        }
        PokazSwiaty(startvalue);
    }

    public void Play()
    {
        panelworlds.SetActive(true);
        panelcredits.SetActive(false);
        panelinfo.SetActive(false);
        settingspanel.SetActive(false);
    }
    public void Create1()
    {
        panelcreate.SetActive(true);
        panelworlds.SetActive(false);
    }
    public void Credits()
    {
        panelcredits.SetActive(true);
        panelcreate.SetActive(false);
        panelworlds.SetActive(false);
        panelinfo.SetActive(false);
        settingspanel.SetActive(false);
    }
    public void Back()
    {
        panelcreate.SetActive(false);
        panelworlds.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Twitter()
    {
        Application.OpenURL("https://twitter.com/hsurvivalgame");
    }
    public void Itch()
    {
        Application.OpenURL("https://koder123456.itch.io/hamstersurvival");
    }
    public void Discord()
    {
        Application.OpenURL("https://discord.gg/ckynAPMgzM");
    }
    public void Facebook()
    {
        Application.OpenURL("https://www.facebook.com/profile.php?id=100085246413083");
    }
    public void Instagram()
    {
        Application.OpenURL("https://www.instagram.com/hsurvivalgame/");
    }
    public void WorldBtnClick()
    {
        buttonclicked = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        var directories = Directory.GetDirectories(Application.persistentDataPath);

        foreach (var directory in directories)
        {
            string directoryname = Path.GetFileNameWithoutExtension(directory);
            //for each object in the array..

            if (directoryname == buttonclicked.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>().text)
            {
                WorldSettings.worldname = directoryname;
                SceneManager.LoadScene("game", LoadSceneMode.Single);

                PlayerPrefs.SetInt("KeyLoad", 1);
                PlayerPrefs.Save();

            }
        }

    }
    public void HCBtnClicked()
    {
        if(hardcore == true)
        {
            hardcore = false;
            slash.SetActive(true);
        }
        else
        {
            hardcore = true;
            slash.SetActive(false);
        }
    }
    public void CreativeBtnClicked()
    {
        if(creative == true)
        {
            creative = false;
            modebtn.GetComponentInChildren<Text>().text = "Survival";
            hcbtn.interactable = true;

        }
        else
        {
            creative = true;
            modebtn.GetComponentInChildren<Text>().text = "Creative";
            hcbtn.interactable = false;
        }
    }
    public void Create2()
    {




        SceneManager.LoadScene("game", LoadSceneMode.Single);




        if(nameinput.text != null)
        {
            WorldSettings.worldname = nameinput.text;
            WorldSettings.creative = creative;
            
            WorldSettings.hardcore = hardcore;
            PlayerPrefs.SetInt("Key", 1);
            PlayerPrefs.Save();

        }



    }
    
    public void PokazSwiaty(int startvalue)
    {
        foreach (var text in namegameobject)
        {
            text.GetComponent<Text>().text = "";
            text.transform.parent.transform.parent.gameObject.GetComponent<Button>().interactable = false;


        }
        //  podzielone na 3
        var directories = Directory.GetDirectories(Application.persistentDataPath);

        int directorynumber = 0;


        /* 
         string directoryname = Path.GetFileNameWithoutExtension();

         */

        for (int i = startvalue; i < startvalue + 3; i++)
        {
            if (i < directories.Length)
            {
                string directoryname = Path.GetFileNameWithoutExtension(directories[i]);
                BaseFunc.Instance.LoadSettingsToInfo(directoryname);
                //Debug.Log(WorldInfo.worldname); // dobrze

                int index1 = directorynumber;





                for (int j = 0; j < namegameobject.Length; j++)
                {

                    Button button = namegameobject[j].transform.parent.transform.parent.gameObject.GetComponent<Button>();
                    button.interactable = true;
                    Text text = namegameobject[j].GetComponent<Text>();
                    if (j == directorynumber)
                    {

                        text.transform.parent.transform.parent.gameObject.GetComponent<Button>().interactable = true;
                        text.text = WorldInfo.worldname;

                    }


                }
                for (int k = 0; k < modegameobject.Length; k++)
                {
                    Text mode = modegameobject[k].GetComponent<Text>();
                    if (k == directorynumber)
                    {
                        if(WorldInfo.creative == true)
                        {
                            mode.text = "Creative";
                        }
                        else
                        {
                            mode.text = "Survival";
                        }
                    }

                }
                for (int m = 0; m < hardcoregameobject.Length; m++)
                {
                    Text hc = hardcoregameobject[m].GetComponentInChildren<Text>();
                    if (m == directorynumber)
                    {
                        if (WorldInfo.hardcore == true)
                        {
                            hc.text = "/";
                        }
                        else
                        {
                            hc.text = "HC";
                        }
                    }

                }
                for (int l = 0; l < deletegameobject.Length; l++)
                {
                    Button button = deletegameobject[l].GetComponent<Button>();
                    if (l == directorynumber)
                    {
                        button.interactable = true;

                    }
                }
                directorynumber++;
            }
            else
            {
                for (int j = 0; j < namegameobject.Length; j++)
                {


                    Text text = namegameobject[j].GetComponent<Text>();
                    if (j == directorynumber)
                    {

                        text.transform.parent.transform.parent.gameObject.GetComponent<Button>().interactable = false;
                        text.text = "";
                        
                    }


                }
                for (int k = 0; k < modegameobject.Length; k++)
                {
                    Text mode = modegameobject[k].GetComponent<Text>();
                    if (k == directorynumber)
                    {
                        mode.text = "";
                    }

                }
                for (int m = 0; m < hardcoregameobject.Length; m++)
                {
                    Text hc = hardcoregameobject[m].GetComponentInChildren<Text>();
                    if (m == directorynumber)
                    {
                        hc.text = "";
                    }

                }
                for (int l = 0; l < deletegameobject.Length; l++)
                {
                    Button button = deletegameobject[l].GetComponent<Button>();
                    if(l == directorynumber)
                    {
                        button.interactable = false;
                       
                    }
                }
                directorynumber++;
            }




            }


        }





























    



        public void Update()
        {
            worldname.text = nameinput.text;
            var directories = Directory.GetDirectories(Application.persistentDataPath);
        if (nameinput.text == "")
        {
            btncreate2.interactable = false;
        }
        else
        {
            btncreate2.interactable = true;
        }

        /* if (startvalue + 3 > directories.Length || startvalue + 3 < 0)
         {
             downArrow.GetComponent<Button>().interactable = false;

         }
         else if (startvalue - 3 > directories.Length || startvalue - 3 < 0)
         {
             upArrow.GetComponent<Button>().interactable = false;
         }
         else
         {
             upArrow.GetComponent<Button>().interactable = true;
             downArrow.GetComponent<Button>().interactable = true;
         }

     */

        if (Input.mouseScrollDelta == Vector2.up)
        {
            
            startvalue -= 3;
            if (startvalue == directories.Length || startvalue < 0)
            {
                startvalue = 0;
            }
            else if (startvalue >= directories.Length || startvalue < 0)
            {
                startvalue = 0;

            }
            PokazSwiaty(startvalue);
        }
        if (Input.mouseScrollDelta == Vector2.down)
        {
            startvalue += 3;
           
            if (startvalue == directories.Length || startvalue < 0)
            {
                startvalue = 0;
            }
            else if (startvalue >= directories.Length || startvalue < 0)
            {
                startvalue = 0;

            }
            PokazSwiaty(startvalue);
        }
    }


    
}