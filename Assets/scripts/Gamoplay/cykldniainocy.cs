using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class cykldniainocy : MonoBehaviour
{

    private int dayLength;   
    private int dayStart;
    private int nightStart;    
    private float currentTime;
    
    
    private float DayValue;
    private float NightValue;
    public static float timer = 0;
    public static bool isday;
    public static bool daycontroller;

   
    public Text wskaznik;
   
    // Start is called before the first frame update
    void Start()
    {
        dayLength = 1200;
        dayStart = 0;
        nightStart = 600;
       
       
       


    }

    void Update()
    {


    

        if (WorldSettings.creative == false)
        {
            daycontroller = AdditionalSettings.daycontroller;
           
                 if (daycontroller == true)
                {
                    timer = 0;
                }
                else
                {
                    timer = 600;
                }
            
      

          

           

            if (timer >= dayStart)
            {
                //dzieñ tak
                isday = true;
                MonstersSpawn.active = true;
                wskaznik.text = "Day";
                AdditionalSettings.daycontroller = isday;

            }
            if (timer >= nightStart)
            {
                MonstersSpawn.active = false;
                //dzieñ nie (noc)
                isday = false;
                AdditionalSettings.daycontroller = isday;
                wskaznik.text = "Night";
            }
            if (timer >= dayLength)
            {
                timer = 0;
              //reset timera
            }
        }
        

    }

    
} 

