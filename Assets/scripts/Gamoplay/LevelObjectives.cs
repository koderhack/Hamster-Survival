using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelObjectives : MonoBehaviour
{
    public GameObject panel;
    public Text objectivetext;
    public Text loretopic;
  
    // Start is called before the first frame update

    // Update is called once per frame
    public void Update()
    {
        switch (PlayerSettings.level)
        {

            case 0:
                objectivetext.text = "Objective: Get out of the cage";
                loretopic.text = "Get Out to survive!";
                break;
            case 1:
                objectivetext.text = $"Objective: Kill 10 enemies {WorldOptions.killedEnemies}/10";
                loretopic.text = $"They can't stop you!";
                break;
            case 2:
                objectivetext.text = "Objective:"
                + "Sleep through the first day";
                loretopic.text = "Zzzzz..., Zzzzz...";
                break;
            case 3:
                objectivetext.text = "Objective: Find and craft food";
                loretopic.text = "You must eat to live!";
                break;
            case 4:
                objectivetext.text = $"Objective: Craft a Jack-o'-lantern";
                loretopic.text = $"You will find out in the future";
                break;
            /*case 5:
                objectivetext.text = "Objective:" 
                   + "through the first day";
                loretopic.text = "Zzzzz..., Zzzzz...";
                break;
            case 6:
                objectivetext.text = "Objective: Run in wheel";
                loretopic.text = "Movement is health!";
                break;
            case 7:
                objectivetext.text = "Objective: Kill infected cat boss";
                loretopic.text = "Time to face the biggest predator!";
                break;
            case 8:
                objectivetext.text = "Objective: Craft an anti-radiation suit";
                loretopic.text = "The outside world is dangerous!";
                break;
            case 9:
                objectivetext.text = "Objective: Get out of the home";
                loretopic.text = "The bookcase is key!";
                break;
            case 10:
                objectivetext.text = "Objective: Get out of the home";
                loretopic.text = "The bookcase is key!";
                break;*/
        }
    }
      
       
    }


