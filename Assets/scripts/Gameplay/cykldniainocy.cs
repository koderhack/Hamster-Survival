using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class cykldniainocy : MonoBehaviour
{

 


    public float dayLength = 180.0f;
    public float nightLength = 180.0f;
    public static float timeRemaining;
  

    public  static bool daytime { get;  private set; }

    public Text wskaznik;
   
    // Start is called before the first frame update
    void Start()
    {

        daytime = AdditionalSettings.daycontroller;





    }

    void Update()
    {

        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            daytime = !daytime;
            if (daytime)
            {
              MonstersSpawn.active = true;
                wskaznik.text = "Day";
                timeRemaining = dayLength;
                AdditionalSettings.daycontroller = daytime;
            }
            else
            {
                MonstersSpawn.active = false; 
                wskaznik.text = "Night";
                timeRemaining = nightLength;
                AdditionalSettings.daycontroller = daytime;

            }
           
        }


    }

    
} 

