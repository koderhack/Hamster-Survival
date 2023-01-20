using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelObjectives : MonoBehaviour
{
    public GameObject panel;
    public Text objectivetext;
    public Text loretopic;
    public bool generateHints;

    // Start is called before the first frame update
    public void Start()
    {
        if (WorldSettings.creative == true)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
        }
        StartCoroutine(Hints());
    }
  
    // Update is called once per frame
    public IEnumerator Hints()
    {
        generateHints = false;
        objectivetext.text = "You can craft things by pressing E";
        loretopic.text = "Hint:";
        yield return new WaitForSeconds(10);
        objectivetext.text = "Sleep via H";
        loretopic.text = "Hint:";
        yield return new WaitForSeconds(10);
        objectivetext.text = "Search for food in the fridge";
        loretopic.text = "Hint:";
        yield return new WaitForSeconds(10);
        objectivetext.text = "Find a lantern and heal with it";
        loretopic.text = "Hint:";
        yield return new WaitForSeconds(10);
        generateHints = true;
    }
   // public IEnumerator  
}


