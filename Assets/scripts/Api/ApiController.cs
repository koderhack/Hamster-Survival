using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;

using System.IO;

public class ApiController : MonoBehaviour
{
   public Text cytat;
   
    

    private readonly string ApiUrl = "https://api.quotable.io/random?tags=motivational";
    private readonly string ApiUrl2 = "https://itch.io/api/1/x/wharf/latest?game_id=1574373&channel_name=win-pre-alpha-0.0.2";
    // Start is called before the first frame update
    void Start()
    {
       
      
        cytat.text = "Loading...";
        
        StartCoroutine(SendRequest(ApiUrl));
        
    }

    IEnumerator SendRequest(string url)
    {
        var list = new List<string>();
        string[] lines;
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            string path = Path.Combine(Application.persistentDataPath, "quotes.txt");
            using (StreamReader sr = new StreamReader(path))
            {
                string linia;
                while ((linia = sr.ReadLine()) != null)
                {
                    list.Add(linia);
                }
                   
                
            }
            lines = list.ToArray();
            int id = Random.Range(0, lines.Length);
            cytat.text = lines[id];
        }

        else
        {
            // Response can be accessed through: request.downloadHandler.text
            
            JSONNode info = JSON.Parse(request.downloadHandler.text);
            string cytatpobrany = info["content"];
            string autor = info["author"];
            cytat.text = $"{cytatpobrany} - {autor}";
            


        }
      
    }
 
    // Update is called once per frame
    void Update()
    {
        
    }
 
}

