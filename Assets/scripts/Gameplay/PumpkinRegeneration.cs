using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;

public class PumpkinRegeneration : MonoBehaviour
{

    
    public GameObject player;
    float timer;
    public float interval = 0.5f;
    int pumpkinlife = 20;
    Tilemap mapa;
    Tilemap mapa2;
    AudioSource source;
    public AudioClip pumpkinhealing;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mapa = GameObject.FindGameObjectWithTag("tilemap").GetComponent<Tilemap>();
        mapa2 = GameObject.FindGameObjectWithTag("tilemap2").GetComponent<Tilemap>();
        source = GameObject.FindGameObjectWithTag("source").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (WorldSettings.creative == false)
        {


            Vector3 vector = new Vector3(transform.position.x + 5, transform.position.y + 5, 0);
            Vector3Int tilemapvector = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);
            if (player.transform.position.x <= vector.x && player.transform.position.y <= vector.y)
            {
                timer += Time.deltaTime;
                if (timer >= interval)
                {
                    PlayerSettings.life++;
                    pumpkinlife--;
                    source.clip = pumpkinhealing;
                    source.Play();
                    timer -= interval;
                }
            }
            if (pumpkinlife <= 0)
            {
                mapa.SetTile(tilemapvector, null);
                mapa2.SetTile(tilemapvector, null);
                Destroy(this.gameObject);
            }
        }
    }
}
