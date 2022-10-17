using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class LevelObjectives : MonoBehaviour
{
    public GameObject panel;
    public Text objectivetext;
    public Text loretopic;
    public TileBase tileszklo;
    public Item szko;
    // Start is called before the first frame update

    public void Start()
    {
        StartCoroutine(StartObjectives());
        szko = inventory.GetItem(tileszklo);
    }
    // Update is called once per frame
    IEnumerator StartObjectives()
    {

        yield return new WaitForSeconds(10);
        switch (PlayerSettings.level)
        {

            case 0:
                objectivetext.text = "Objective: Get out of the cage";
                loretopic.text = "Get Out to survive!";
                break;
            case 1:
                objectivetext.text = "Objective: Kill 10 enemies";
                loretopic.text = "They can't stop you!";
                break;
            case 2:
                objectivetext.text = "Objective: Find and craft food";
                loretopic.text = "You must eat to live!";
                break;
            case 3:
                objectivetext.text = "Objective: Sleep through the first day";
                loretopic.text = "Zzzzz..., Zzzzz...";
                break;
            case 4:
                objectivetext.text = "Objective: Run in wheel";
                loretopic.text = "Movement is health!";
                break;
            case 5:
                objectivetext.text = "Objective: Kill infected cat boss";
                loretopic.text = "Time to face the biggest predator!";
                break;
            case 6:
                objectivetext.text = "Objective: Craft an anti-radiation suit";
                loretopic.text = "The outside world is dangerous!";
                break;
            case 7:
                objectivetext.text = "Objective: Get out of the home";
                loretopic.text = "The bookcase is key!";
                break;
            case 8:
                objectivetext.text = "Objective: Get out of the home";
                loretopic.text = "The bookcase is key!";
                break;
        }
    }
    public void Update()
    {
      
    }
}
