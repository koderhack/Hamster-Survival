using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
   public  AudioSource effects;

    public AudioClip speak;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TextAnim());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator TextAnim()
    {
        effects.clip = speak;

        text.text = "Congratulations Harry! You have accomplished the impossible! ";
        yield return new WaitForSeconds(5);
        text.text = "You managed to survive and get out for the first time ever! Bravo!";

        yield return new WaitForSeconds(5);
        text.text = " The End" +
            " Programmer: Koder " +
            " Graphics designers: Koder,Mc_go³¹b ";
        yield return new WaitForSeconds(10);
        PlayerPrefs.SetInt("KeyLoad", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("game");
        PlayerSettings.level = 8;
    }
}
