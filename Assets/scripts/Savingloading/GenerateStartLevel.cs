using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GenerateStartLevel : MonoBehaviour
{
    public Tilemap mapa;
    public Tilemap mapa2;
    // Start is called before the first frame update
    void Start()
    {

        BaseFunc.Instance.ResetStartLevel(mapa,mapa2);
        SceneManager.LoadScene("Menu");
 
    }

  
}
