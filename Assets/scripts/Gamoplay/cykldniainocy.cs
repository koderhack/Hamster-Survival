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
   
   
    public Text wskaznik;
   
    // Start is called before the first frame update
    void Start()
    {
        dayLength = 1200;
        dayStart = 0;
        nightStart = 600;

       // timer = AdditionalSettings.timer;
       


    }

    void Update()
    {


        Debug.Log(timer);

        if (WorldSettings.creative == false)
        {
           


            timer = Time.deltaTime;

           

            if (timer >= dayStart)
            {
                //dzie� tak
                isday = true;
                MonstersSpawn.active = true;
                wskaznik.text = "Day";
               
               
            }
            if (timer >= nightStart)
            {
                MonstersSpawn.active = false;
                //dzie� nie (noc)
                isday = false;
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

