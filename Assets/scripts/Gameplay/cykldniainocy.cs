using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
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
        dayLength = 12;//1200
        dayStart = 0;
        nightStart = 6;//600
       
       
       


    }

    void Update()
    {

        timer = Time.fixedTime;
    

        if (WorldSettings.creative == false)
        {
            daycontroller = AdditionalSettings.daycontroller;
           
              
           
            
      

          

           

            if (timer >= dayStart )
            {
                //dzieñ tak
                isday = true;
                MonstersSpawn.active = true;
                wskaznik.text = "Day";
               

            }
            if (timer >= nightStart || daycontroller == false)
            {
                MonstersSpawn.active = false;
                //dzieñ nie (noc)
                isday = false;
                AdditionalSettings.daycontroller = isday;
                wskaznik.text = "Night";
            }
            if (timer >= dayLength)
            {
                timer = 6;
              //reset timera
            }
        }
        

    }

    
} 

